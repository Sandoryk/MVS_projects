using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OwnersVSPets.Controllers
{
    public class PetsController : ApiController
    {
        // GET api/pets
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/pets/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/pets
        public void Post([FromBody]string value)
        {
        }

        // PUT api/pets/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/pets/5
        public void Delete(int id)
        {
        }
    }
}
