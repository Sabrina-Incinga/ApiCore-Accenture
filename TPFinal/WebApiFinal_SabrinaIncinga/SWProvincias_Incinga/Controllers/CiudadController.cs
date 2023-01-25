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
    public class CiudadController : ControllerBase
    {
        private readonly DBPaisFinalContext context;

        public CiudadController(DBPaisFinalContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Ciudad>> Get()
        {
            return context.Ciudades.Include(x => x.Provincia).ToList();
        }

        [HttpPost]
        public ActionResult Create(Ciudad ciudad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            context.Ciudades.Add(ciudad);
            context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Ciudad ciudad)
        {
            if (id != ciudad.IdCiudad)
            {
                return BadRequest();
            }
            context.Entry(ciudad).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Ciudad> Delete(int id)
        {
            var ciudad = context.Ciudades.Find(id);
            if (ciudad == null)
            {
                return NotFound();
            }

            context.Ciudades.Remove(ciudad);
            context.SaveChanges();
            context.SaveChanges();

            return ciudad;
        }
    }
}
