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

        [HttpPut("{id}")]
        public ActionResult Update(short id, [FromBody] Department department)
        {
            if(department.DepartmentId != id)
            {
                return BadRequest();
            }
            context.Entry(department).State = EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Department> Delete(short id)
        {
            Department department = Get(id).Value;
            if (department == null)
            {
                return NotFound();
            }
            if(department.EmployeeDepartmentHistory.Count > 0)
            {
                foreach(var departmentHistory in department.EmployeeDepartmentHistory)
                {
                    context.EmployeeDepartmentHistory.Remove(departmentHistory);
                }
            }
            context.Department.Remove(department);
            context.SaveChanges();

            return department;
        }
    }
}
