using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OwnersVSPets.Models;

namespace OwnersVSPets.Controllers
{
    public class ValuesController : ApiController
    {
        DataContext dc = new DataContext();
        // GET api/values
        public IEnumerable<Owner> Get()
        {
            return dc.GetOwners;
        }

        // GET api/values/5
        public Owner GetOwner(int id)
        {
            return null;
        }

        // POST api/values
        [HttpPost]
        public void CreateNewOwner([FromBody]string name)
        {
        }

        // PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
