using ExampleSPA.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleSPA.Client.DataAccess
{
    public class ConsultantDataAccess
    {
        ConsultantContext db = new ConsultantContext();

        public IEnumerable<Consultant> GetAllConsultants()
        {
            try
            {
                return db.Consultants.ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AddConsultant(Consultant Consultant)
        {
            try
            {
                db.Consultants.Add(Consultant);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateConsultant(Consultant Consultant)
        {
            try
            {
                db.Entry(Consultant).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Consultant GetConsultantData(int id)
        {
            try
            {
                Consultant Consultants = db.Consultants.Find(id);
                return Consultants;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteConsultant(int id)
        {
            try
            {
                Consultant con = db.Consultants.Find(id);
                db.Consultants.Remove(con);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
