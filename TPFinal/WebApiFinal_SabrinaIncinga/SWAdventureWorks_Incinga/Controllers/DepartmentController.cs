using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWAdventureWorks_Incinga.Data;
using SWAdventureWorks_Incinga.Models;
using System.Collections.Generic;
using System.Linq;

namespace SWAdventureWorks_Incinga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AdventureWorks2019Context context;

        public DepartmentController(AdventureWorks2019Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>> Get()
        {
            return context.Department.Include(x => x.EmployeeDepartmentHistory).ToList();
        }

        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            context.Department.Add(department);
            context.SaveChanges();

            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<Department> Get(short id)
        {
            return context.Department.Include(x => x.EmployeeDepartmentHistory).FirstOrDefault(x=>x.DepartmentId==id);
        }
        [HttpGet("search")]
        public ActionResult<Department> Get(string name)
        {
            return context.Department.Include(x => x.EmployeeDepartmentHistory).FirstOrDefault(x => x.Name == name);
        }
    }
}
