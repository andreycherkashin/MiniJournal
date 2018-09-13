using System;

namespace Infotecs.MiniJournal.Events.Events
{    
    /// <summary>
    /// Событие.
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Дата создания события.
        /// </summary>
        DateTime DateOfCreate { get; }
    }
}
