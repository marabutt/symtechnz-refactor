using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace refactor_this.Controllers
{
    public class TransactionController : ApiController
    {
        [HttpGet, Route("api/Accounts/{accountId}/Transactions")]
        public IHttpActionResult GetTransactions(Guid accountId)
        {
            using (var connection = Helpers.NewConnection())
            {
                Transaction transactionModel = new Transaction();

                List<Entities.Transaction> transactions = new List<Entities.Transaction>();

                transactions = (List<Entities.Transaction>)transactionModel.Get(accountId);
                if (transactions.Count == 0)
                    return NotFound();

                return Ok(transactions);
            }
        }

        [HttpPost, Route("api/Accounts/{accountId}/Transactions")]
        public IHttpActionResult Add( Guid accountId, Transaction transaction)
        {
            //check account exists
            Account accountModel = new Account();
            Entities.Account existingAccount = accountModel.GetById(transaction.AccountId);
            if (existingAccount == null)
                return NotFound();

            Transaction transactionModel = new Transaction();          
            transactionModel.Add(transaction);

            return Ok();
        }
    }
}