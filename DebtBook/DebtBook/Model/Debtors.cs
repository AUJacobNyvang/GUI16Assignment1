using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DebtBook.Model
{
    public class Debtors
    {
        public ObservableCollection<Debtor> DebtorsList { get; private set; }

        public Debtors()
        {
            DebtorsList = new ObservableCollection<Debtor>();
        }

        public void AddDebtorToList(Debtor debtor)
        {
            DebtorsList.Add(debtor);
        }
    }
}