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
            //Owner ow = new Owner();
            //return Json<Owner>(ow);
            return dc.GetOwners;
        }

        // GET api/values/5
        public Owner Get(int id)
        {
            return null;
        }

        // POST api/values
        public void Post([FromBody]int id)
        {
            
        }

        //PUT api/values/5
        public IEnumerable<Owner> Put(string ownerName)
        {
            if (ModelState.IsValid)
            {
                dc.CreateNewOwner(ownerName);
            } 
            return dc.GetOwners;
        }

        /*public IHttpActionResult CreateNewOwner(string ownerName)
        {
            if (ModelState.IsValid)
            {
                if (!dc.CreateNewOwner(ownerName))
                {
                    ModelState.AddModelError("ownerName", "New owner creation failed");
                }
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }*/

        // DELETE api/values/5
        public IEnumerable<Owner> Delete(string ownerName)
        {
            if (ModelState.IsValid)
            {
                dc.DeleteOwner(ownerName);
            }
            return dc.GetOwners;
        }
    }
}
