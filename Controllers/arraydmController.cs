using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using SignalRApi.ConexionesDB;
using SignalRApi.Models;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;



namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class arraydmController : ControllerBase
    {
        // GET: api/<dbConecctionController>
        [HttpGet]
        public IActionResult Get()
        {
            TextReader connString;

            connString = new StreamReader("conf.json");

            JObject o = JObject.Parse(connString.ReadToEnd());

            //string connstr = (string)o["data"][0]["dbconn"];
            string connstr = (string)o["data"][1]["dbname"];

            // byte[] base64EncodedBytes = Convert.FromBase64String(connstr);

            return Ok(connstr);
        }

        // POST api/<arraydmController>
        [HttpPost]
        public IActionResult Post(StringConnection s)
        {
            try
            {
                string dbDataConn = string.Format("Data Source={0} ;Initial Catalog={1} ;User ID={2} ;Password={3} ;MultipleActiveResultSets=True;Application Name=EntityFramework"

                    , s.source.Trim()
                    , s.database.Trim()
                    , s.user.Trim()
                    , s.password.Trim());

                byte[] plainTextBytes = Encoding.UTF8.GetBytes(dbDataConn);

                string jsonString = "{\"data\":[{\"dbconn\":\""+Convert.ToBase64String(plainTextBytes).Trim()+"\"},{\"dbname\":\"" + s.database + "\"}]}";

                TextWriter archivo;

                archivo = new StreamWriter("demo.json");

                archivo.WriteLine(jsonString);

                archivo.Close();

                SqlConnection con = new SqlConnection(dbDataConn);
                con.Open();
                con.Close();

                return Ok("[{\"status\":\"Conectado a la base de datos: " + s.database + "\"}]");
            }
            catch (Exception err)
            {
                string message = "[{\"status\":\"" + err.Message + "\"}]";
                return StatusCode(200, message);
            }

        }

        //// PUT api/<arraydmController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<arraydmController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
