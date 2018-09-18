using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infotecs.MiniJournal.RabbitMqPublisher
{
    /// <inheritdoc cref="IRabbitMessageBus" />
    public class RabbitMessageBus : IRabbitMessageBus, IDisposable
    {
        private static readonly string uniqueClientId = Guid.NewGuid().ToString();
        private readonly Dictionary<string, IModel> exchangeChannels = new Dictionary<string, IModel>();
        private readonly Dictionary<string, IModel> queueChannels = new Dictionary<string, IModel>();
        private readonly Dictionary<string, EventingBasicConsumer> queueConsumers = new Dictionary<string, EventingBasicConsumer>();
        private readonly Dictionary<string, IModel> consumerTags = new Dictionary<string, IModel>();
        private readonly string connectionString;

        private IConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMessageBus"/> class.
        /// </summary>
        /// <param name="connectionString">Строка подключения к RabbitMq.</param>
        public RabbitMessageBus(string connectionString)
        {
            this.connectionString = connectionString;

            this.CreateConnection();
        }

        /// <inheritdoc cref="IRabbitMessageBus" />
        public Task PublishAsync<TMessage>(TMessage message)
        {
            string exchangeName = this.GetExchangeName<TMessage>();
            IModel exchangeChannel = this.GetExchangeChannel(exchangeName);
            string routingKey = this.GetRoutingKey<TMessage>();

            IBasicProperties messageProperties = exchangeChannel.CreateBasicProperties();
            messageProperties.Persistent = true;

            lock (exchangeChannel)
            {
                exchangeChannel.ConfirmSelect();

                string json = JsonConvert.SerializeObject(message);
                byte[] body = System.Text.Encoding.UTF8.GetBytes(json);

                exchangeChannel.BasicPublish(exchangeName, routingKey, true, messageProperties, body);
                exchangeChannel.WaitForConfirmsOrDie();
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc cref="IRabbitMessageBus" />
        public Task SubscribeAsync<TMessage>(Action<TMessage> handler, bool persistant)
        {
            EventingBasicConsumer queueConsumer = this.GetQueueConsumer<TMessage>(persistant);
            string queueName = this.GetQueueName<TMessage>(persistant);
            IModel queueChannel = this.GetQueueChannel(queueName);

            queueConsumer.Received += (ch, ea) =>
            {
                Task.Run(() =>
                {
                    byte[] body = ea.Body;
                    string json = System.Text.Encoding.UTF8.GetString(body);
                    var message = JsonConvert.DeserializeObject<TMessage>(json);
                    handler(message);
                })
                .ContinueWith(task =>
                {
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        queueChannel.BasicNack(ea.DeliveryTag, false, true);
                    }
                    else
                    {
                        queueChannel.BasicAck(ea.DeliveryTag, false);
                    }
                });
            };

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            foreach (KeyValuePair<string, IModel> consumerTag in this.consumerTags)
                consumerTag.Value.BasicCancel(consumerTag.Key);

            this.consumerTags.Clear();

            foreach (KeyValuePair<string, IModel> queueChannel in this.queueChannels)
                queueChannel.Value.Close();

            this.queueChannels.Clear();

            foreach (KeyValuePair<string, IModel> exchangeChannel in this.exchangeChannels)
                exchangeChannel.Value.Close();

            this.exchangeChannels.Clear();

            this.connection?.Close();
            this.connection = null;
        }

        private EventingBasicConsumer GetQueueConsumer<TMessage>(bool persistant)
        {
            string queueName = this.GetQueueName<TMessage>(persistant);
            return this.GetFromDictionaryThreadSafe(this.queueConsumers, queueName, () => this.CreateConsumer<TMessage>(persistant));
        }

        private IModel GetExchangeChannel(string exchangeName)
        {
            return this.GetFromDictionaryThreadSafe(this.exchangeChannels, exchangeName, () => this.connection.CreateModel());
        }

        private IModel GetQueueChannel(string queueName)
        {
            return this.GetFromDictionaryThreadSafe(this.queueChannels, queueName, () => this.connection.CreateModel());
        }

        private EventingBasicConsumer CreateConsumer<TMessage>(bool persistant)
        {
            string queueName = this.GetQueueName<TMessage>(persistant);
            string exchangeName = this.GetExchangeName<TMessage>();
            string routingKey = this.GetRoutingKey<TMessage>();

            IModel channel = this.GetQueueChannel(queueName);

            lock (channel)
            {
                channel.ExchangeDeclare(exchangeName, ExchangeType.Topic, true, false);
                channel.QueueDeclare(queueName, true, false, !persistant);
                channel.QueueBind(queueName, exchangeName, routingKey);

                var consumer = new EventingBasicConsumer(channel);
                string consumerTag = channel.BasicConsume(queueName, false, consumer);

                this.consumerTags[consumerTag] = channel;

                return consumer;
            }
        }

        private string GetRoutingKey<TMessage>()
        {
            return $"{typeof(TMessage).FullName}.#";
        }

        private string GetExchangeName<TMessage>()
        {
            return typeof(TMessage).Namespace;
        }

        private string GetQueueName<TMessage>(bool persistant)
        {
            string applicationName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            string queueName = $"{typeof(TMessage).FullName}.{applicationName}";

            if (persistant)
            {
                return queueName;
            }

            return $"{queueName}.{uniqueClientId}";
        }

        private T GetFromDictionaryThreadSafe<T>(IDictionary<string, T> dictionary, string key, Func<T> valueFactory)
        {
            if (dictionary.TryGetValue(key, out T value))
            {
                return value;
            }

            lock (dictionary)
            {
                if (dictionary.TryGetValue(key, out value))
                {
                    return value;
                }

                value = valueFactory();

                dictionary[key] = value;

                return value;
            }
        }

        private void CreateConnection()
        {
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(this.connectionString)
            };

            this.connection = connectionFactory.CreateConnection();
        }
    }
}
