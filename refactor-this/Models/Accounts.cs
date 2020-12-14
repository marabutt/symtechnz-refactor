using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace refactor_this.Models
{
    public class Account
    {

        /// <summary>
        /// add some parameterisation
        /// </summary>
        /// <param name="account"></param>
        public void Save(Entities.Account account)
        {
            using (var connection = Helpers.NewConnection())
            {
                SqlCommand command;
                if (account.IsNew)
                {
                    command = new SqlCommand($"insert into Accounts (Id, Name, Number, Amount) values ('{Guid.NewGuid()}', @name, @number, @amount)", connection);

                    command.Parameters.AddWithValue("@number", account.Number);
                    command.Parameters.AddWithValue("@amount", account.Amount);
           
                }
                else
                {
                    command = new SqlCommand($"update Accounts set Name = @name, where Id = @id", connection);
                    command.Parameters.AddWithValue("@id", account.Id);
                }
                command.Parameters.AddWithValue("@name", account.Name);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateBalance(Guid id, float amount)
        {
            using (var connection = Helpers.NewConnection())
            {
                SqlCommand command = new SqlCommand();
                command = new SqlCommand($"update Accounts set amount = amount + {amount} where Id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// need to protect from sql injectiopn
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            using (var connection = Helpers.NewConnection())
            {
                SqlCommand command = new SqlCommand($"delete from Accounts where Id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// note may want to limit the result count in production
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Entities.Account> Get()
        {
            var accounts = new List<Entities.Account>();
            using (var connection = Helpers.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"select Id,Name,Number,Amount from Accounts", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                    accounts.Add(RowToObject(reader));

                return accounts;
            }
        }

        public Entities.Account GetById(Guid id)
        {
            var account = new Account();
            using (var connection = Helpers.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"select Id,Name,Number,Amount from Accounts where id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();

                while (reader.Read())
                    return RowToObject(reader);
                                
                return null;
            }
        }
        public Entities.Account RowToObject(SqlDataReader sqlDataReader)
        {
            return new Entities.Account()
            {
                Id = Guid.Parse(sqlDataReader["Id"].ToString()),
                Name = sqlDataReader["Name"].ToString(),
                Number = sqlDataReader["Number"].ToString(),
                Amount = float.Parse(sqlDataReader["Amount"].ToString())
            };

        }

    }
}
