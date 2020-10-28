using System.Windows;
using System.Windows.Input;
using DebtBook.Model;
using DebtBook.Pages;
using Prism.Commands;
using Prism.Mvvm;

namespace DebtBook.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        public Debtors Debtors { get; set; }

        public MainWindowViewModel()
        {
            Debtors = new Debtors();
            Debtor Asger = new Debtor("Asger", -10000.152);
            Debtor Tue = new Debtor("Tue", 1312512.123);
            Debtors.DebtorsList.Add(Asger);
            Debtors.DebtorsList.Add(Tue);
            CurrentDebtor = Debtors.DebtorsList[0];
        }

        private Debtor currentDebtor = null;

        public Debtor CurrentDebtor
        {
            get { return currentDebtor; }
            set
            {
                if (currentDebtor != value)
                {
                    SetProperty(ref currentDebtor, value);
                }
            }
        }

        private int currentIndex = -1;
        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {
                SetProperty(ref currentIndex, value);
            }
        }

        private ICommand addDebtorCommand;

        public ICommand AddDebtorCommand
        {
            get
            {
                return addDebtorCommand ??= new DelegateCommand(AddDebtorCommandHandler);
            }
        }

        private void AddDebtorCommandHandler()
        {
            AddPerson windowAddPerson = new AddPerson();
            windowAddPerson.DataContext = new AddPersonViewModel(Debtors,windowAddPerson);
            windowAddPerson.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            windowAddPerson.ShowDialog();
        }

        private ICommand deleteDebtorCommand;

        public ICommand DeleteDebtorCommand
        {
            get
            {
                return deleteDebtorCommand ?? (deleteDebtorCommand =
                        new DelegateCommand(DeleteDebtorCommandExecute, DeleteDebtorCommandCanExecute).ObservesProperty(() =>
                            CurrentIndex)
                    );
            }
        }
        private void DeleteDebtorCommandExecute()
        {
            if (CurrentIndex >= 0)
                Debtors.DebtorsList.RemoveAt(CurrentIndex);
        }
        private bool DeleteDebtorCommandCanExecute()
        {
            if (CurrentIndex >= 0 && Debtors.DebtorsList.Count > 0)
                return true;
            else
                return false;
        }


    }
}