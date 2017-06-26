using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OwnersVSPets.Models;
using OwnersVSPets.DAL;

namespace OwnersVSPets.Controllers
{
    public class OwnersController : ApiController
    {
        string connectionString = "OwnersPetsConnection";
        DataSourceHandler dh;
        int ownersOnPage = 3;
        int maxPagesAmount = 5;

        private OwnersWithMeta RetrieveOwners(int pageNum)
        {
            List<Owner> displayOwners = new List<Owner>();
            OwnersWithMeta ownersWithMeta = null;

            if (pageNum>0)
            {
                using (dh = new DataSourceHandler(connectionString))
                {
                    List<DBOwner> dbOwners = dh.GetOwnersPartly(pageNum, ownersOnPage);
                    if (dbOwners!=null)
                    {
                        foreach (var owner in dbOwners)
                        {
                            displayOwners.Add(new Owner
                            {
                                ID = (int)owner.ID,
                                Name = owner.Name,
                                PetsAmount = dh.CountOwnerPets((int)owner.ID)
                            });
                        } 
                    }
                    ownersWithMeta = new OwnersWithMeta
                    {
                        Meta = new Meta { ItemsOnPage = ownersOnPage, MaxPageAmount = maxPagesAmount, ItemsTotally = dh.CountOwners() },
                        OwnerList = displayOwners
                    };
                }   
            }

            return ownersWithMeta;
        }

        // GET api/values
        public OwnersWithMeta Get()
        {
            return RetrieveOwners(1);
        }

        // GET api/values/5
        public OwnersWithMeta Get(int page)
        {
            return RetrieveOwners(page);
        }

        // POST api/values
        public void Post([FromBody]int id)
        {
            
        }

        //PUT api/values/5
        public bool Put(string ownerName)
        {
            bool result = false;

            if (ModelState.IsValid)
            {
                using (dh = new DataSourceHandler(connectionString))
                {
                    result = dh.CreateNewOwner(ownerName);
                }
            }
            return result;
        }

        /*public IHttpActionResult CreateNewOwner(string ownerName)
        {
            if (ModelState.IsValid)
            {
                if (!dh.CreateNewOwner(ownerName))
                {
                    ModelState.AddModelError("ownerName", "New owner creation failed");
                }
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }*/

        // DELETE api/values/5
        public bool Delete(string ownerID)
        {
            bool result = false;

            if (ModelState.IsValid)
            {
                using (dh = new DataSourceHandler(connectionString))
                {
                    result = dh.DeleteOwner(ownerID);
                }
            }
            return result;
        }
    }
}
