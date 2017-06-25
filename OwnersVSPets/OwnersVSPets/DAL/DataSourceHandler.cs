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
        public bool DeleteOwner(string OwnerID)
        {
            bool result = false;

            if (!String.IsNullOrEmpty(OwnerID))
            {
                int ID = Int32.Parse(OwnerID);
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
        public int CountOwnerPets(DBOwner owner)
        {
            int pets = 0;

            if (owner!=null)
            {
                pets = db.Pets.Where(t => t.OwnerID == owner.ID).Count();
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