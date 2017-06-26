using OwnersVSPets.DAL;
using OwnersVSPets.Models;
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
        string connectionString = "OwnersPetsConnection";
        DataSourceHandler dh;
        int ownersOnPage = 3;
        int maxPagesAmount = 5;

        private PetsWithMeta RetrievePets(int ownerID, int pageNum)
        {
            List<Pet> displayPets = new List<Pet>();
            PetsWithMeta ownersWithMeta = null;

            if (ownerID > -1 && pageNum>0)
            {
                using (dh = new DataSourceHandler(connectionString))
                {
                    List<DBPet> dbPets = dh.GetPetsPartly(ownerID, pageNum, ownersOnPage);
                    if (dbPets != null)
                    {
                        foreach (var pet in dbPets)
                        {
                            displayPets.Add(new Pet
                            {
                                ID = (int)pet.ID,
                                Name = pet.Name,
                                OwnerID = (int)pet.OwnerID
                            });
                        }
                    }
                    ownersWithMeta = new PetsWithMeta
                    {
                        Meta = new Meta { ItemsOnPage = ownersOnPage, MaxPageAmount = maxPagesAmount, ItemsTotally = dh.CountOwnerPets(ownerID) },
                        PetList = displayPets
                    };
                }    
            }
 
            return ownersWithMeta;
        }
        // GET api/pets
        public PetsWithMeta Get()
        {
            return null;
        }

        // GET api/pets/5
        public PetsWithMeta Get(string ownerID)
        {
            int number,ID = -1;
            if (!String.IsNullOrEmpty(ownerID))
            {
                if (Int32.TryParse(ownerID, out number))
                {
                    ID = number;
                }
            }
            return RetrievePets(ID, 1);
        }
        // GET api/pets/5/5
        public PetsWithMeta Get(string ownerID, int page)
        {
            int number, ID = -1;
            if (!String.IsNullOrEmpty(ownerID))
            {
                if (Int32.TryParse(ownerID, out number))
                {
                    ID = number;
                }
            }
            return RetrievePets(ID, page);
        }

        // POST api/pets
        public void Post([FromBody]string value)
        {
        }

        // PUT api/pets/5/5
        public bool Put(string ownerID, string petName)
        {
            bool result = false;
            int number, ID = -1;

            if (!String.IsNullOrEmpty(ownerID))
            {
                if (Int32.TryParse(ownerID, out number))
                {
                    ID = number;
                }
            }
            if (ModelState.IsValid)
            {
                using (dh = new DataSourceHandler(connectionString))
                {
                    result = dh.CreateNewPet(ID, petName);
                }
            }
            return result;
        }

        // DELETE api/pets/5
        public bool Delete(string petID)
        {
            bool result = false;

            if (ModelState.IsValid)
            {
                using (dh = new DataSourceHandler(connectionString))
                {
                    result = dh.DeletePet(petID);
                }
            }
            return result;
        }
    }
}
