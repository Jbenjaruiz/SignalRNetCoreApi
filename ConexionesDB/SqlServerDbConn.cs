using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace SignalRApi.ConexionesDB
{
    public class SqlServerDbConn
    {
        public static DataSet Ejecutar(string cmd)
        {
            TextReader  connString;

            connString = new StreamReader("conf.json");

            JObject o = JObject.Parse(connString.ReadToEnd());
            
            string connstr = (string)o["data"][0]["dbconn"];

            byte[] base64EncodedBytes = Convert.FromBase64String(connstr);

            SqlConnection con = new SqlConnection(Encoding.UTF8.GetString(base64EncodedBytes));
            con.Open();

            DataSet DS = new DataSet();
            SqlDataAdapter DP = new SqlDataAdapter(cmd, con);

            DP.Fill(DS);

            con.Close();


            return DS;
        }
    }
}
