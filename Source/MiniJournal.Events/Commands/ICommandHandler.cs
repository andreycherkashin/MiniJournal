using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infotecs.MiniJournal.Events.Commands
{
    /// <summary>
    /// Обработчик команды.
    /// </summary>
    /// <typeparam name="TCommand">Тип команды.</typeparam>
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task HandleAsync(TCommand command);
    }
}
