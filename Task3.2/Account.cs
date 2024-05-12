using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3._2
{
    internal class Account
    {
        private decimal _balance;
        private string _name;

        // Constructor
        public Account(string name, decimal balance)
        {
            _name = name;
            _balance = balance;
        }

        // Deposit money into the account
        public bool Deposit(decimal amount)
        {
            if (amount > 0)
            {
                _balance += amount;
                return true;
            }
            else
            {
                Console.WriteLine("Invalid deposit.");
                return false;
            }
        }

        // Withdraw money from the account
        public bool Withdraw(decimal amount)
        {
            if (amount <= _balance)
            {
                _balance -= amount;
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
                return false;
            }
        }

        // Print the details 
        public void Print()
        {
            Console.WriteLine($"Account Name: {_name}");
            Console.WriteLine($"Balance: {_balance:C}");
        }

        // To get name
        public string Name
        {
            get { return _name; }
        }
    }
}
    
