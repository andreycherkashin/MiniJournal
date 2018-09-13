using System;

namespace Infotecs.MiniJournal.Contracts.Events
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
