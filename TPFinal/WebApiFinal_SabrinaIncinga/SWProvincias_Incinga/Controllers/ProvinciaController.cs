using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWProvincias_Incinga.Data;
using SWProvincias_Incinga.Models;
using System.Collections.Generic;
using System.Linq;

namespace SWProvincias_Incinga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly DBPaisFinalContext context;

        public ProvinciaController(DBPaisFinalContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Provincia>> Get()
        {
            return context.Provincias.Include(x => x.Ciudades).ToList();
        }

        [HttpPost]
        public ActionResult Create(Provincia provincia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            context.Provincias.Add(provincia);
            context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Provincia provincia)
        {
            if(id != provincia.IdProvincia)
            {
                return BadRequest();
            }
            context.Entry(provincia).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Provincia> Delete(int id)
        {
            var provincia = context.Provincias.Find(id);
            if(provincia == null)
            {
                return NotFound();
            }

            context.Provincias.Remove(provincia);
            context.SaveChanges();

            return provincia;
        }
    }
}
