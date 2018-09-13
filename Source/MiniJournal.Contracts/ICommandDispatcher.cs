using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Contracts.Commands;

namespace Infotecs.MiniJournal.Contracts
{
    /// <summary>
    /// Класс для публикации команд.
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Асинхронно передает команду подписчикам.
        /// </summary>
        /// <typeparam name="T">Тип команды.</typeparam>
        /// <param name="command">Команда.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task DispatchAsync<T>(T command)
            where T : ICommand;
    }
}
