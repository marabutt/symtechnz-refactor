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
    public class AccountController : ApiController
    {
        [HttpGet, Route("api/Accounts/{id}")]
        public IHttpActionResult GetById(Guid id)
        {

            Account AccountsModel = new Account();

            Entities.Account account = AccountsModel.GetById(id);
            if (account == null)
                return NotFound();

            return Ok(account);

        }

        [HttpGet, Route("api/Accounts")]
        public IHttpActionResult Get()
        {
            Account AccountsModel = new Account();
            return Ok(AccountsModel.Get());
        }



        [HttpPost, Route("api/Accounts")]
        public IHttpActionResult Add(Entities.Account account)
        {
            Account accountModel = new Account();
            account.IsNew = true;
            accountModel.Save(account);

            return Ok();//should be created
        }

        [HttpPut, Route("api/Accounts/{id}")]
        public IHttpActionResult Update(Guid id, Entities.Account account)
        {

            Account accountModel = new Account();

            Entities.Account existingAccount = accountModel.GetById(id);
            if (existingAccount == null)
                return NotFound();

            //assuming we can only update the name

            existingAccount.Name = account.Name;

            accountModel.Save(existingAccount);


            return Ok();



        }

        [HttpDelete, Route("api/Accounts/{id}")]
        public IHttpActionResult Delete(Guid id)
        {

            Account accountModel = new Account();

            Entities.Account existingAccount = accountModel.GetById(id);
            if (existingAccount == null)
            {
                NotFound();
            }
            accountModel.Delete(id);

            return Ok();
        }
    }
}