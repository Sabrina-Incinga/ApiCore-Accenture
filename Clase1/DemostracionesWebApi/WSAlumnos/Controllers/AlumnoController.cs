using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WSAlumnos.Models;

namespace WSAlumnos.Controllers
{
    [Route("api/alumno")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private List<Alumno> ListAlumnos()
        {
            List<Alumno> alumnoList = new List<Alumno>()
            {
                new Alumno(){Apellido="Perez", Id= 1, Nombre="Juan"},
                new Alumno(){Apellido="García", Id= 2, Nombre="Franco"},
                new Alumno(){Apellido="Lopez", Id= 3, Nombre="María"}
            };

            return alumnoList;
        }

        //GET api/alumno
        [HttpGet]
        public IEnumerable<Alumno> GetAlumnos()
        {
            return ListAlumnos();
        }
        //GET api/alumno/3
        [HttpGet("{id}")]
        public ActionResult<Alumno> getById(int id)
        {
            Alumno alumno = (from a in ListAlumnos()
                         where a.Id == id
                         select a).FirstOrDefault();

            return alumno;
        }
    }
}
