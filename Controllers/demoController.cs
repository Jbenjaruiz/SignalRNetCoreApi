using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using SignalRApi.ConexionesDB;
using SignalRApi.Models;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class demoController : ControllerBase
    {
        // GET: api/<demoController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                string cmd = string.Format("execute facturas_selectAll ");


                DataSet Ds = SqlServerDbConn.Ejecutar(cmd);

                object[] data = new object[Ds.Tables[0].Rows.Count];

                for (int i = 1; i <= Ds.Tables[0].Rows.Count; i++)
                {

                    data[i - 1] = Ds.Tables[0].Rows[i - 1];
                }
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(Ds.Tables[0]);

                
                string dataArray= "{\"Data\":" + JSONresult + "}";
                return Ok(dataArray);
            }
            catch (Exception err)
            {
                string message = "[{\"status\":\""+err.Message+"\"}]";
                return StatusCode(200,  message);
            }
        }

        // GET api/<demoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<demoController>
        [HttpPost]
        public IActionResult Post(StringConnection s)
        {
            try
            {
                string cmd = string.Format("execute articulos_insert '{0}','{1}','{2}','{3}'"

                    , s.source.Trim()
                    , s.database.Trim()
                    , s.user.Trim()
                    , s.password.Trim());

                DataSet Ds = SqlServerDbConn.Ejecutar(cmd);

                return Ok(Ds);
            }
            catch (Exception err)
            {
                return StatusCode(400, "Revise la sentencia sql" + err);
            }
        }

        // PUT api/<demoController>/5
        [HttpPut]
        public IActionResult Put(Data d)
        {
            try
            {
                string cmd = string.Format("execute recorrer_arrayJson '{0}'"

                    , d.jsonstring.Trim()
                    );


                DataSet Ds = SqlServerDbConn.Ejecutar(cmd);

                return Ok(Ds);
            }
            catch (Exception err)
            {
                return StatusCode(400, "Revise la sentencia sql" + err);
            }
        }

        // DELETE api/<demoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
