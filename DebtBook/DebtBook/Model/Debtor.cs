using System;
using System.Collections.Generic;

namespace DebtBook.Model
{
    public class Debtor
    {
        public double _totalDebt { get; set; }

        public string _name { get; set; }

        public List<string> _transactionList { get; private set; }

        public Debtor(string name = "default", double debt = 0)
        {
            _totalDebt = debt;
            _name = name;
            _transactionList = new List<string>();
        }

        public void AddDebt(double debt)
        {
            _totalDebt += debt;
        }

        private void AddNewTransaction(double debt)
        {
            string dateTime = (DateTime.Now.ToString()) + " : " + debt + " total debt : " + _totalDebt;
            _transactionList.Add(dateTime);
        }

        
    }
}