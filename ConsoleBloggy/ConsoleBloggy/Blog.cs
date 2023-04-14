using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBloggy
{
    internal class Blog
    {
        public int y { get; set; }
        public int x { get; set; }
        public string Title { get; set; } 
        public SqlConnection Connect;
        public void FindBlogTitle()
        {
            string Sql = "SELECT BlogTitle FROM BlogPost WHERE Id = '" + y + "'";
            SqlCommand command = new SqlCommand(Sql, Connect);
            Connect.Open();
            SqlDataReader TitleResult = command.ExecuteReader();
            
            while (TitleResult.Read())
            {
                Title = TitleResult.GetValue(0).ToString();
                Console.WriteLine("      " + x + " . " + Title);
            }
            Connect.Close();
        }
    }
}
