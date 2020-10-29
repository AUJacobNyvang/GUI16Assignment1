using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace DebtBook.Model
{
    public class Debtor : BindableBase
    {
        private double _totalDebt;

        public double TotalDebt
        {
            get
            {
                return _totalDebt;
            }
            set
            {
                if (_totalDebt != value)
                    SetProperty(ref _totalDebt, value);
            }
        }

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                    SetProperty(ref _name, value);
            }
        }

        public ObservableCollection<string> TransactionList { get; private set; }

        public Debtor()
        {
            TransactionList = new ObservableCollection<string>();
        }

        public Debtor(string name, double debt)
        {
            TotalDebt = debt;
            Name = name;
            TransactionList = new ObservableCollection<string>();
        }

        public void AddDebt(double debt)
        {
            TotalDebt += debt;
            AddNewTransaction(debt);
        }

        private void AddNewTransaction(double debt)
        {
            string dateTime = (DateTime.Now.ToShortDateString())+ " " + (DateTime.Now.ToShortTimeString()) + " : " + debt + "  |  New Total Debt: " + TotalDebt;
            TransactionList.Add(dateTime);
        }

        
    }
}