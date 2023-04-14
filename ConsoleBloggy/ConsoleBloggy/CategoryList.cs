using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBloggy
{
    internal class CategoryList
    {
        public int h { get; set; }
        public List<string> GetCatName { get; set; }
        public List<int> GetCatId { get; set; }
       
        public SqlConnection connect;

        public void FindCats()
        {
            h = 0;
            string Sql1 = "SELECT * FROM Category";
            SqlCommand Command = new SqlCommand(Sql1, connect);

            connect.Open();
            SqlDataReader CategoryIdResult = Command.ExecuteReader();

            GetCatId = new List<int>();
            GetCatName = new List<string>();

            while (CategoryIdResult.Read())
            {
                h++;
                GetCatId.Add((int)CategoryIdResult.GetValue(0));
                GetCatName.Add(CategoryIdResult.GetString(1));
            }
            connect.Close();
        }







    }
}
