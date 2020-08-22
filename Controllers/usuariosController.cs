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
    public class usuariosController : ControllerBase
    {
        // GET: api/<usuariosController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                string cmd = string.Format("select * from usr_usuarios ");

                DataSet Ds = SqlServerDbConn.Ejecutar(cmd);
                       
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(Ds.Tables[0]);

                string dataArray = "{\"Data\":" + JSONresult + "}";
                return Ok(dataArray);
            }
            catch (Exception err)
            {
                string message = "[{\"status\":\"" + err.Message + "\"}]";
                return StatusCode(200, message);
            }
        }

        // GET api/<usuariosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<usuariosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<usuariosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<usuariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
