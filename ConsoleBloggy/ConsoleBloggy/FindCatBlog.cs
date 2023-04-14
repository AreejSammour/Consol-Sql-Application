using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBloggy
{
    internal class FindCatBlog
    {
        public int IdResult { get; set; }
        public int y { get; set; }
        public List<int> IdToGet {get; set;}
        
        public SqlConnection Connect;

        public void FindCBId()
        {
            string Sql = "SELECT BlogId FROM CategoryBlog WHERE CategoryId = '" + IdResult + "'";
            SqlCommand command = new SqlCommand(Sql, Connect);
            Connect.Open();
            SqlDataReader BlogIdResult = command.ExecuteReader();
            
            IdToGet = new List<int>();
            y = 0;
            while (BlogIdResult.Read())
            {
                IdToGet.Add((int)BlogIdResult.GetValue(0));
                y++;
            }
            Connect.Close();
           
        }
    }
}
