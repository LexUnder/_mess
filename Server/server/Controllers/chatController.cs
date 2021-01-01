using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class chatController : ControllerBase
    {
        public static MessagesClass ms = new MessagesClass();


        // GET api/<chatController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            string json = " not found";
            if ((id < ms.messages.Count) && (id >= 0))
            {
                json = JsonSerializer.Serialize(ms.messages.ElementAt(id));
            }
            return json;
        }

        // POST api/<chatController>
        [HttpPost]
        public void Post([FromBody] message msg)
        {
            ms.Add(msg.username, msg.text);
        }


    }
}
