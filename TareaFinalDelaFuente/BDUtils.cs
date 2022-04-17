using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TareaFinalDelaFuente
{
    public class BDUtils
    {
        public string connectionString = "";
         public MySqlConnection ConexionDB()
        {
            String server = "127.0.0.1";
            String database = "alvaodoo";
            String uid = "dela";
            String password = "abc123.";
            //string connectionString;
            connectionString = "SERVER=" + server + "; PORT = 3306 ;" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            
            MySqlConnection mycon = new MySqlConnection(connectionString);
            return mycon;
        }

        public DataTable RellenarDt(string sentenciaSql, MySqlConnection con)
        {
                using (MySqlCommand cmd = new MySqlCommand(sentenciaSql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
        }

        
    }
}
