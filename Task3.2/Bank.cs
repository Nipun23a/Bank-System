using System;
using System.Collections.Generic;

namespace Task6._2
{
    using Task3._2;
    using Task5._2;
    using Task7._1;

    internal class Bank
    {
        private List<Account> _accounts;
        private List<Transaction> _transactions;



        public List<Transaction> Transactions
        {
            get { return _transactions; }
        }

        public Bank()
        {
            _accounts = new List<Account>();
            _transactions = new List<Transaction>();
        }

        // Method to add an account to the bank
        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        // Method to get an account by name
        public Account GetAccount(string name)
        {
            foreach (var acc in _accounts)
            {
                if (acc.Name == name)
                {
                    return acc;
                }
            }
            return null;
        }

        // Method to execute a transaction
        public void ExecuteTransaction(TransferTransaction transaction)
        {
            _transactions.Add(transaction);
            transaction.Execute();
        }
        public void RollbackTransaction(Transaction transaction)
        {
            if (!_transactions.Contains(transaction))
            {
                throw new InvalidOperationException("The transaction is not part of this bank's transactions.");
            }

            transaction.Rollback();
        }

        public void PrintTransactionHistory()
        {
            if (_transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }

            for (int i = 0; i < _transactions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Transaction:");
                _transactions[i].Print();
                Console.WriteLine($"Last Operation: {_transactions[i].DateStamp}");
                Console.WriteLine();
            }
        }
    }
}


