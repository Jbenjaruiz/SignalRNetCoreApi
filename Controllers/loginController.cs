using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using SignalRApi.ConexionesDB;
using SignalRApi.Models;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class loginController : ControllerBase
    {
        // GET: api/<loginController>
        [HttpGet]
        public IActionResult Get(string user, string pass)
        {
            try
            {
                string cmd = string.Format("execute demoLogin '{0}','{1}'"
                    ,user.Trim()
                    , pass.Trim()
                    );


                DataSet Ds = SqlServerDbConn.Ejecutar(cmd);

                return Ok(Ds.Tables);
            }
            catch (Exception err)
            {
                string message = "[{\"status\":\"" + err.Message + "\"}]";
                return StatusCode(400, message);
            }
        }

        // GET api/<loginController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<loginController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<loginController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<loginController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
