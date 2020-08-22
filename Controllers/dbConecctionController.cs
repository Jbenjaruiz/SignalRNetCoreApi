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


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class dbConecctionController : ControllerBase
    {
        // GET: api/<dbConecctionController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<dbConecctionController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<dbConecctionController>
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

                string jsonString = "{\"data\":[{\"dbconn\":\"" + Convert.ToBase64String(plainTextBytes).Trim() + "\"},{\"dbname\":\"" + s.database + "\"}]}";

                TextWriter archivo;

                archivo = new StreamWriter("conf.json");

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

        // PUT api/<dbConecctionController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<dbConecctionController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
