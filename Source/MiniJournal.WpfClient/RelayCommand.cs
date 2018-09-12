using System;
using System.Windows.Input;

namespace Infotecs.MiniJournal.WpfClient
{
    /// <inheritdoc />
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> canExecute;
        private readonly Action<object> execute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="canExecute">Предикат, переключающий состояние команды.</param>
        /// <param name="execute">Метод, который будет вызван при выполнении команды.</param>
        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }
        
        /// <inheritdoc />
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <inheritdoc />
        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        /// <inheritdoc />
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
