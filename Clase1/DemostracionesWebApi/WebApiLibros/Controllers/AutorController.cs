using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiLibros.Data;
using WebApiLibros.Models;

namespace WebApiLibros.Controllers
{
    [Route("api/autor")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        #region Inyeccion de dependencia
        private readonly DBLibrosContext context;

        public AutorController(DBLibrosContext context)
        {
            this.context = context;
        }
        #endregion

        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            //return context.Autores.ToList();
            var result = context.Autores.Include(x => x.Libros).ToList();

            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<Autor> GetById(int id)
        {
            Autor autor = (from a in context.Autores
                           where a.IdAutor == id
                           select a).FirstOrDefault();

            return autor;
        }

        [HttpPost]
        public ActionResult Create(Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Autores.Add(autor);

            context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Autor autor)
        {
            if (id != autor.IdAutor)
            {
                return BadRequest();
            }

            context.Entry(autor).State = EntityState.Modified;

            context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var autor = (from a in context.Autores
                         where a.IdAutor == id
                         select a).FirstOrDefault();

            if(autor == null)
            {
                return NotFound();
            }

            context.Autores.Remove(autor);
            context.SaveChanges();

            return autor;
        }

        [HttpGet("edad/{edad}")]
        public ActionResult<IEnumerable<Autor>> Get(int edad)
        {
            List<Autor> autores = (from a in context.Autores
                                   where a.Edad == edad
                                   select a).ToList();

            return autores;
        }
    }
}
