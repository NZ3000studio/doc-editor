using System.Data.SqlClient;

namespace doc_editor.Models
{
   
  
    public class DBhandler
    {
        public static string myDBconn = "Data Source=(localdb)\\ProjectModels;Initial Catalog=DocEditorDB;Integrated Security=True;Connect Timeout=30;Encrypt=False";

        public static void LogConnection(string username)
        {
            string time = DateTime.Now.ToString("h:mm:ss tt");

            SqlConnection conn = new SqlConnection(myDBconn);
            conn.Open();
           
            string insert = $"INSERT into Logs values ('{username} is connected at {time}','no Update')";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

        }


        public static void LogUpdate(string username, int position, string change, string type)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");

            SqlConnection conn = new SqlConnection(myDBconn);
            conn.Open();

            string insert = $"INSERT into Logs values ('{username}  {type} at {time}','{change} {type} in position {position}')";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

        }




    }
}
