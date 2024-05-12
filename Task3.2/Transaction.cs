using System;
using System.Security.Cryptography.X509Certificates;

namespace Task7._1
{
    public abstract class Transaction
    {
        protected decimal _amount;
        protected bool _executed;
        protected DateTime _datestamp;

        public Transaction(decimal amount)
        {
            _amount = amount;
            _executed = false;
        }



        public abstract void Print();
        public abstract bool Success { get; }
        public DateTime DateStamp => _datestamp;

        public virtual void Execute()
        {
            if (_executed)
            _executed = true;
            _datestamp = DateTime.Now;
        }

        public virtual void Rollback()
        {
            if (!_executed)
            _executed = false;
            _datestamp = DateTime.Now;
        }
    }
}


