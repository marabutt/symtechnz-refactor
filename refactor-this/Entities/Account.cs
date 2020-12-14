using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_this.Entities
{
    
    public class Account
    {
        private bool isNew;
   
        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public float Amount { get; set; }
    }
}