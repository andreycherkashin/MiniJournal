using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Events.Events;
using Microsoft.AspNetCore.SignalR;

namespace Infotecs.MiniJournal.WebApp.Hubs
{
    /// <inheritdoc />
    /// <summary>
    /// Хаб для нотификации клиентов об изменениях в модели.
    /// </summary>
    public class NotificationsHub : Hub
    {
        /// <summary>
        /// Рассылает оповещение клиентам.
        /// </summary>
        /// <param name="event">Событие.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task Notify(object @event)
        {
            await this.Clients.All.SendAsync(@event.GetType().Name, @event);
        }        
    }
}
