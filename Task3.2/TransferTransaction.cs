using System;
using Task3._2;
using Task7._1;

namespace Task5._2
{
    internal class TransferTransaction : Transaction
    {
        private WithdrawTransaction _withdraw;
        private DepositTransaction _deposit;

        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount)
            : base(amount)
        {
            _withdraw = new WithdrawTransaction(fromAccount, amount);
            _deposit = new DepositTransaction(toAccount, amount);
        }

        public override void Print()
        {
            Console.WriteLine($"Transferred ${_withdraw.Amount} from {_withdraw.Account.Name}'s account to {_deposit.Account.Name}'s account:");
            _withdraw.Print();
            _deposit.Print();
        }

        public override bool Success => _withdraw.Success && _deposit.Success;

        public override void Execute()
        {
            base.Execute();

            if (_withdraw.Executed || _deposit.Executed)
                throw new InvalidOperationException("Transfer transaction has already been executed.");

            try
            {
                _withdraw.Execute();
                _deposit.Execute();
            }
            catch (InvalidOperationException ex)
            {
                Rollback();
                throw ex;
            }
        }

        public override void Rollback()
        {
            base.Rollback();

            if (!_withdraw.Executed || !_deposit.Executed)
                throw new InvalidOperationException("Cannot rollback the transfer transaction.");

            try
            {
                _deposit.Rollback();
                _withdraw.Rollback();
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public bool Reversed => _withdraw.Reversed && _deposit.Reversed;
    }
}