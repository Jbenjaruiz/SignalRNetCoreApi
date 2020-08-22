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
    public class chatsController : ControllerBase
    {
        // GET: api/<chatsController>
        [HttpGet]
        public IActionResult Get(int usrId)
        {
            try
            {
                string cmd = string.Format("exec chats_selectAll '{0}' ", usrId.ToString());

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

        // GET api/<chatsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<chatsController>
        [HttpPost]
        public IActionResult Post(Chat c)
        {
            try
            {
                string cmd = string.Format("exec chats_insert '{0}','{1}','{2}' ", 
                    c.ct_usr_sender.ToString()
                    ,c.ct_usr_receptor.ToString()
                    ,c.ct_mensaje.Trim());

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

        // PUT api/<chatsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<chatsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
