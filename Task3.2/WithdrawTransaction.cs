using System;
using Task3._2;
using Task7._1;

namespace Task5._2
{
    internal class WithdrawTransaction : Transaction
    {
        private Account _account;
        private bool _success;
        private bool _reversed;

        public WithdrawTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
            _success = false;
            _reversed = false;
        }

        public override void Print()
        {
            Console.WriteLine("Withdraw Transaction Details:");
            Console.WriteLine("Amount: " + _amount);
            Console.WriteLine("Executed: " + _executed);
            Console.WriteLine("Success: " + _success);
            Console.WriteLine("Reversed: " + _reversed);
            Console.WriteLine("DateStamp: " + _datestamp);
        }

        public override bool Success => _success;

        public override void Execute()
        {
            base.Execute();

            try
            {
                _account.Withdraw(_amount);
                _success = true;
            }
            catch (InvalidOperationException ex)
            {
                _success = false;
                throw new InvalidOperationException("Withdraw transaction failed: " + ex.Message);
            }
        }

        public decimal Amount
        {
            get { return _amount; }
        }
        public bool Executed
        {
            get { return _executed; }
        }

        public override void Rollback()
        {
            base.Rollback();

            if (_success)
            {
                _account.Deposit(_amount);
                _reversed = true;
            }
            else
            {
                throw new InvalidOperationException("Withdraw transaction was not successful.");
            }
        }

        public bool Reversed => _reversed;
        public Account Account => _account;
    }
}