using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_College_CourierFirm.Commands
{ 
    // Реализация интерфейса ICommand для создания кастомных команд в MVVM
    public class RelayCommand : ICommand
    {
        private readonly Action execute; // Принимает метод

        public RelayCommand(Action execute) // Реализация интерфейса
        {
            this.execute = execute;
        }

        // Событие для проверки, можно ли выполнить полученный метод 
        public event EventHandler CanExecuteChanged; 

        public bool CanExecute(object parameter) => true; // Всегда да

        public void Execute(object parameter) // Выполняет метод
        {
            execute();
        }
    }
}
