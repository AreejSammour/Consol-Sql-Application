using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBloggy
{
    internal class FindCategory
    {
        public int IdResult {get; set;}
        public string NameToFind { get; set;}
        public SqlConnection Connect;

        public void FindCat()
        {
            IdResult = 0;
            string Sql = "SELECT Id FROM Category WHERE CategoryName = '" + NameToFind + "'";
            SqlCommand Command = new SqlCommand(Sql, Connect);
            Connect.Open();

            SqlDataReader CategoryIdResult = Command.ExecuteReader();
            while (CategoryIdResult.Read())
            {
                IdResult = (int)CategoryIdResult.GetValue(0);
            }
            Connect.Close();
        }
        
    }
}
