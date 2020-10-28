using System.Windows;
using System.Windows.Input;
using DebtBook.Model;
using Prism.Commands;
using Prism.Mvvm;

namespace DebtBook.ViewModel
{
    public class AddPersonViewModel : BindableBase
    {
        public Debtors _debtors { get; private set; }

        public Debtor Debtor { get; set; }

        private Window _currentWindow { get; set; }

        public AddPersonViewModel(Debtors debtors, Window window)
        {
            Debtor = new Debtor();
            _debtors = debtors;
            _currentWindow = window;
        }


        private ICommand saveNewPersonCommand;

        public ICommand SaveNewPerCommand
        {
            get
            {
                return saveNewPersonCommand ??= new DelegateCommand(SaveNewPerCommandHandler);
            }
        }

        private void SaveNewPerCommandHandler()
        {
            _debtors.DebtorsList.Add(Debtor);
            _currentWindow.Close();
        }

         private ICommand closeWindowCommand;

        public ICommand CloseWindowCommand
        {
            get
            {
                return closeWindowCommand ??= new DelegateCommand(CloseWindowCommandHandler);
            }
        }

        private void CloseWindowCommandHandler()
        {
            _currentWindow.Close();
        }


    }
}