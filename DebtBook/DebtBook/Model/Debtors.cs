using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace DebtBook.Model
{
    public class Debtors : BindableBase
    {
        private ObservableCollection<Debtor> _debtorsList;
        public ObservableCollection<Debtor> DebtorsList
        {
            get
            {
                return _debtorsList;
            }
            set
            {
                if (_debtorsList != value)
                {
                    SetProperty(ref _debtorsList, value);
                }
            }
        }

        public Debtors()
        {
            DebtorsList = new ObservableCollection<Debtor>();
        }
    }
}