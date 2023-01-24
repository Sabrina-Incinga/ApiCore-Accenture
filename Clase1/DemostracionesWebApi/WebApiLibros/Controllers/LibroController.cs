using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebApiLibros.Data;
using WebApiLibros.Models;

namespace WebApiLibros.Controllers
{
    [Route("api/libro")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly DBLibrosContext context;
        public LibroController(DBLibrosContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            //return context.Libros.ToList();
            var resultado = context.Libros.Include(x => x.Autor).ToList();

            return resultado;
        }

        [HttpGet("{id}")]
        public ActionResult<Libro> Get(int id)
        {
            //Libro libro = context.Libros.Find(id);
            Libro libro = context.Libros.Include(x => x.Autor).FirstOrDefault(x => x.Id == id);
            /*if(libro != null)
            {
                Autor autor = context.Autores.Find(libro.AutorId);

                libro.Autor = autor;
            }*/

            return libro;
        }

        [HttpGet("autor/{autorId}")]
        public ActionResult<IEnumerable<Libro>> GetByAutorId(int autorId)
        {
            List<Libro> libros = (from l in context.Libros
                                  where l.AutorId == autorId
                                  select l).ToList();

            return libros;
        }

        [HttpPost]
        public ActionResult Create(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            context.Libros.Add(libro);
            context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Libro libro)
        {
            if(id != libro.Id)
            {
                return BadRequest();
            }
            context.Entry(libro).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            var libro = context.Libros.Find(id);

            if(libro == null)
            {
                return NotFound();
            }

            context.Libros.Remove(libro);
            context.SaveChanges();

            return libro;
        }

    }
}
