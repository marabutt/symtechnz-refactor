using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace refactor_this.Models
{
    public class Transaction
    {
        

        public IEnumerable<Entities.Transaction> Get( Guid accountId) {


            using (var connection = Helpers.NewConnection())
            {
                List<Entities.Transaction> transactions = new List<Entities.Transaction>();

                SqlCommand command = new SqlCommand($"select Id, AccountId, Amount, Date from Transactions where accountId = @accountId", connection);
                command.Parameters.AddWithValue("@accountId", accountId);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    transactions.Add(RowToObject(reader));
                }
                return transactions;
            }
        }

        public bool Add( Entities.Transaction transaction)
        {     
            using (var connection = Helpers.NewConnection())
            {
                SqlCommand command = new SqlCommand($"INSERT INTO Transactions (Id, Amount, Date, AccountId) VALUES ('{Guid.NewGuid()}',@amount, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', @accountId)", connection);
                command.Parameters.AddWithValue("@amount", transaction.Amount);
                command.Parameters.AddWithValue("@accountId", transaction.AccountId);
                connection.Open();

                if (command.ExecuteNonQuery() != 1)
                    return false;

                return true;
            }
            
        }



        public Entities.Transaction RowToObject(SqlDataReader sqlDataReader)
        {
            return new Entities.Transaction()
            {
                Id = Guid.Parse(sqlDataReader["Id"].ToString()),
                Date = DateTime.Parse(sqlDataReader["Date"].ToString()),
                AccountId = Guid.Parse(sqlDataReader["AccountId"].ToString()),
                Amount = float.Parse(sqlDataReader["Amount"].ToString())
            };

        }
    }
}