using OwnersVSPets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwnersVSPets.DAL
{
    public class DataSourceHandler : IDisposable
    {
        private OwnersPetsContext db;
        private bool disposed = false;

        public DataSourceHandler(string connectionStr)
        {
            db = new OwnersPetsContext(connectionStr);
        }
        public List<DBOwner> GetAllOwners
        {
            get { return db.Owners.ToList(); }
        }

        public DBOwner GetOwnerByID(int ownerID)
        {
            DBOwner owner = db.Owners.First(t=>t.ID==ownerID);
            return owner;
        }
        public List<DBOwner> GetOwnersPartly(int pageNum, int ownersOnPage)
        {
            List<DBOwner> owners = new List<DBOwner>();
            int skipItems = (pageNum - 1) * ownersOnPage;
            try
            {
                owners = db.Owners.OrderBy(t=>t.ID).Skip(skipItems).Take(ownersOnPage).ToList();
            }
            catch (ArgumentNullException)
            {
            }

            return owners;
        }
        public List<DBPet> GetPetsPartly(int ownerID, int pageNum, int petsOnPage)
        {
            List<DBPet> pets = new List<DBPet>();
            if (ownerID>-1)
            {
                int skipItems = (pageNum - 1) * petsOnPage;
                try
                {
                    pets = db.Pets.Where(t=>t.OwnerID==ownerID).OrderBy(t => t.ID).Skip(skipItems).Take(petsOnPage).ToList();
                }
                catch (ArgumentNullException)
                {
                }    
            }

            return pets;
        }
        public bool CreateNewOwner(string newOwnerName)
        {
            bool result = false; 

            if (!String.IsNullOrEmpty(newOwnerName))
            {
                DBOwner owner = new DBOwner { Name = newOwnerName };
                db.Owners.Add(owner);
                db.SaveChanges();
                result = true;
            }

            return result;
        }
        public bool CreateNewPet(int ownerID,string newPetName)
        {
            bool result = false;

            if (!String.IsNullOrEmpty(newPetName) && ownerID>-1)
            {
                DBPet pet = new DBPet { Name = newPetName, OwnerID = ownerID };
                db.Pets.Add(pet);
                db.SaveChanges();
                result = true;
            }

            return result;
        }
        public bool DeleteOwner(string ownerID)
        {
            bool result = false;

            if (!String.IsNullOrEmpty(ownerID))
            {
                int ID = Int32.Parse(ownerID);
                DBOwner owner = db.Owners.First(t => t.ID == ID);
                if (owner != null)
                {
                    db.Owners.Remove(owner);
                    db.SaveChanges();
                    result = true;
                }
            }

            return result;
        }
        public bool DeletePet(string petID)
        {
            bool result = false;

            if (!String.IsNullOrEmpty(petID))
            {
                int ID = Int32.Parse(petID);
                DBPet pet = db.Pets.First(t => t.ID == ID);
                if (pet != null)
                {
                    db.Pets.Remove(pet);
                    db.SaveChanges();
                    result = true;
                }
            }

            return result;
        }
        public int CountOwnerPets(int ownerID)
        {
            int pets = 0;

            if (ownerID>-1)
            {
                DBOwner owner = db.Owners.First(t => t.ID == ownerID);
                if (owner != null)
                {
                    pets = db.Pets.Where(t => t.OwnerID == owner.ID).Count();
                } 
            }
            
            return pets;
        }

        public int CountOwners()
        {
            int records = 0;

            try
            {
                records = db.Owners.Count();
            }
            catch (Exception)
            {
            }

            return records;
        }

        public int CountPets()
        {
            int records = 0;

            try
            {
                records = db.Pets.Count();
            }
            catch (Exception)
            {
            }

            return records;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}