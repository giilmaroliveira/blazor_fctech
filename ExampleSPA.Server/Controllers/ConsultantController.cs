using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleSPA.Client.DataAccess;
using ExampleSPA.Shared.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExampleSPA.Server.Controllers
{
    public class ConsultantController : Controller
    {
        ConsultantDataAccess objConsultant = new ConsultantDataAccess();

        [HttpGet]
        [Route("api/Consultant/Index")]
        public IEnumerable<Consultant> Index()
        {
            return objConsultant.GetAllConsultants();
        }

        [HttpPost]
        [Route("api/Consultant/Create")]
        public void Create([FromBody] Consultant Consultant)
        {
            if (ModelState.IsValid)
                objConsultant.AddConsultant(Consultant);
        }

        [HttpGet]
        [Route("api/Consultant/Details/{id}")]
        public Consultant Details(int id)
        {

            return objConsultant.GetConsultantData(id);
        }

        [HttpPut]
        [Route("api/Consultant/Edit")]
        public void Edit([FromBody]Consultant Consultant)
        {
            if (ModelState.IsValid)
                objConsultant.UpdateConsultant(Consultant);
        }

        [HttpDelete]
        [Route("api/Consultant/Delete/{id}")]
        public void Delete(int id)
        {
            objConsultant.DeleteConsultant(id);
        }
    }
}
