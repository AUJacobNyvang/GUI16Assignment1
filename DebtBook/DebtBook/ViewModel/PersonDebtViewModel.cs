using System.Windows;
using System.Windows.Input;
using DebtBook.Model;
using Prism.Commands;
using Prism.Mvvm;

namespace DebtBook.ViewModel
{
    public class PersonDebtViewModel : BindableBase
    {
        public Debtor CurrentDebtor { get; set; }

        public double Value { get; set; }

        private Window _currentWindow { get; set; }

        public PersonDebtViewModel(Debtor debtor, Window window)
        {
            Value = 0;
            CurrentDebtor = debtor;
            _currentWindow = window;
        }


        private ICommand addTransCommand;

        public ICommand AddTransCommand
        {
            get
            { 
                return addTransCommand ??= new DelegateCommand(AddTransCommandHandler);
                
            }
        }

        private void AddTransCommandHandler()
        {
            CurrentDebtor.AddDebt(Value);
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