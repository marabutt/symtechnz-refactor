using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_this.Entities
{
    public class Transaction
    {
        public float Amount { get; set; }

        public DateTime Date { get; set; }

        public Guid Id { get; set; }
        public Guid AccountId { get; set; }


    }
}