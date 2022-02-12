using Binus.WS.Pattern.Output;
using System;
using System.Collections.Generic;

namespace DuitKu.API.Output
{
    public class _TransactionOutput : OutputBase
    {
        public _TransactionOutput()
        {
            this.Success = 0;
        }
        public int Success { get; set; }
    }
    public class TransOutput : OutputBase
    {
        public List<Transaction> Data { get; set; }

        public TransOutput()
        {
            this.Data = new List<Transaction>();
        }
    }
    public class Transaction
    {
        public int Balance { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public string TransactionType { get; set; }


    }
}