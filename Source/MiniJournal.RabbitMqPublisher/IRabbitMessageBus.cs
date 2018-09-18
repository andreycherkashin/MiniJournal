using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infotecs.MiniJournal.RabbitMqPublisher
{
    /// <summary>
    /// Реализует работу с брокером сообщений RabbitMq.
    /// </summary>
    public interface IRabbitMessageBus : IDisposable

    {

    /// <summary>
    /// Опубликовать сообщение.
    /// </summary>
    /// <typeparam name="TMessage">Тип сообщения.</typeparam>
    /// <param name="message">Сообщение.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    Task PublishAsync<TMessage>(TMessage message);

    /// <summary>
    /// Подписаться на сообщения.
    /// </summary>
    /// <typeparam name="TMessage">Тип сообщения.</typeparam>
    /// <param name="handler">Обработчик сообщения.</param>
    /// <param name="persistant">Нужно ли хранить сообщения, когда подписчик не работает.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    Task SubscribeAsync<TMessage>(Action<TMessage> handler, bool persistant);
    }
}
