using System;
using System.Collections.Generic;

namespace Task3._2
{
    using Task5._2;
    using Task6._2;
    using Task7._1;
    // Enumeration for menu options
    enum MenuOption
    {
        AddAccount = 1,
        Withdraw = 2,
        Deposit = 3,
        Print = 4,
        Transfer = 5,
        PrintTransactionHistory = 6,
        Quit = 7
    }

    internal class BankSystem
    {
        private Bank _bank;

        public BankSystem(Bank bank)
        {
            _bank = bank;
        }

        public void Run()
        {
            try
            {
                while (true)
                {
                    MenuOption option = ReadUserOption();

                    switch (option)
                    {
                        case MenuOption.AddAccount:
                            DoAddAccount();
                            break;
                        case MenuOption.Withdraw:
                            DoWithdraw();
                            break;
                        case MenuOption.Deposit:
                            DoDeposit();
                            break;
                        case MenuOption.Print:
                            DoPrint();
                            break;
                        case MenuOption.Transfer:
                            DoTransfer();
                            break;
                        case MenuOption.PrintTransactionHistory:
                            DoPrintTransaction();
                            break;
                        case MenuOption.Quit:
                            Console.WriteLine("Exiting.");
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please enter a valid menu option.");
                            break;
                    }
                }
            }
            catch (StackOverflowException)
            {
                Console.WriteLine("An unexpected error occured.Exiting");
            }

        }


        // Method to read user's menu option choice
        private MenuOption ReadUserOption()
        {
            MenuOption option = MenuOption.Quit;

            while (true)
            {
                Console.WriteLine("\nBank System Menu:");

                Console.WriteLine("1. Add Account");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Print Account Details");
                Console.WriteLine("5. Transfer");
                Console.WriteLine("6. Print Transaction History");
                Console.WriteLine("7. Quit");
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                if (!Enum.IsDefined(typeof(MenuOption), choice))
                {
                    Console.WriteLine("Invalid option. Please enter a valid menu option.");
                    continue;
                }

                option = (MenuOption)choice;
                break;
            }

            return option;
        }


        // Method to perform deposit operation
        private void DoDeposit()
        {
            Account account = FindAccount(_bank);
            if (account == null)
            {
                return;
            }

            Console.Write("Deposit amount : ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (amount < 0)
                {
                    Console.WriteLine("Negative amount entered. Deposit aborted.");
                    return; // Breaks the program and returns to the main menu
                }
                if (account.Deposit(amount))
                {
                    Console.WriteLine("Deposit successful.");
                }
                else
                {
                    Console.WriteLine("Invalid deposit");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        // Method to perform withdraw operation
        private void DoWithdraw()
        {
            Account account = FindAccount(_bank);
            if (account == null)
            {
                return;
            }

            Console.Write("Withdraw amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (amount < 0)
                {
                    Console.WriteLine("Negative amount entered. Withdrawal aborted.");
                    return; // Breaks the program and returns to the main menu
                }

                if (account.Withdraw(amount))
                {
                    Console.WriteLine("Withdrawal successful");
                }
                else
                {
                    Console.WriteLine("Invalid withdrawal");
                }
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }


        // Method to print account details
        private void DoPrint()
        {
            Account account = FindAccount(_bank);
            if (account == null)
            {
                return;
            }
            account.Print();
        }

        // Method to do transfer
        private void DoTransfer()
        {
            // Find source account
            Account sourceAccount = FindAccount(_bank);
            if (sourceAccount == null)
            {
                Console.WriteLine("Source account not found.");
                return;
            }

            // Find destination account
            Account destAccount = FindAccount(_bank);
            if (destAccount == null)
            {
                Console.WriteLine("Destination account not found.");
                return;
            }

            // Get amount to transfer
            Console.Write("Enter amount to transfer: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            if (amount < 0)
            {
                Console.WriteLine("Negative amount entered. Transaction aborted.");
                return;
            }

            try
            {
                TransferTransaction transfer = new TransferTransaction(sourceAccount, destAccount, amount);
                transfer.Execute();
                transfer.Print();
                _bank.ExecuteTransaction(transfer);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }





        // Method for add account
        private void DoAddAccount()
        {
            Console.Write("Enter Account Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Starting Balance: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal balance))
            {
                Account newAccount = new Account(name, balance);
                _bank.AddAccount(newAccount);
                Console.WriteLine("Account Added Successfully");
            }
            else
            {
                Console.WriteLine("Invalid Balance");
            }
        }

        // Method to find an account
        private static Account FindAccount(Bank bank)
        {
            Console.Write("Enter Account Name: ");
            string accountName = Console.ReadLine();
            Account account = bank.GetAccount(accountName);

            if (account == null)
            {
                Console.WriteLine("Account Not Found");
            }
            return account;
        }

        // Method to print transaction history
        private void DoPrintTransaction()
        {
            _bank.PrintTransactionHistory();
        }

        private void DoRollback()
        {
            Console.Write("Enter the transaction index to rollback (or 0 to cancel): ");
            int index = int.Parse(Console.ReadLine());

            if (index == 0)
                return;

            if (index < 1 || index > _bank.Transactions.Count)
            {
                Console.WriteLine("Invalid transaction index.");
                return;
            }

            Transaction transaction = _bank.Transactions[index - 1];

            try
            {
                _bank.RollbackTransaction(transaction);
                Console.WriteLine("Transaction rolled back successfully.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error rolling back transaction: {ex.Message}");
            }
        }
    }

        class Program
    {
        static void Main(string[] args)
        {
            // Creating a bank
            Bank bank = new Bank();

            // Creating accounts and adding to the bank
            Account myAccount = new Account("shehan", 5000);
            Account otherAccount = new Account("silva", 10000);
            bank.AddAccount(myAccount);
            bank.AddAccount(otherAccount);

            BankSystem bankSystem = new BankSystem(bank);

            bankSystem.Run();
        }
    }
}

