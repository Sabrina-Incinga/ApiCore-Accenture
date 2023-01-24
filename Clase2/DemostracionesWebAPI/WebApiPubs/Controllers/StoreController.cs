using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiPubs.Data;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly pubsContext context;

        public StoreController(pubsContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Store>> Get()
        {
            return context.Stores.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Store> Get(string id)
        {
            return context.Stores.Find(id);
        }

        [HttpGet("name/{name}")]
        public ActionResult<IEnumerable<Store>> GetByName(string name)
        {
            List<Store> store = (from s in context.Stores
                           where s.StorName == name
                           select s).ToList();
            return store;
        }

        [HttpGet("zip/{zip}")]
        public ActionResult<IEnumerable<Store>> GetByZip(string zip)
        {
            List<Store> store = (from s in context.Stores
                           where s.Zip == zip
                           select s).ToList();
            return store;
        }

        [HttpGet("city-state")]
        public ActionResult<IEnumerable<Store>> GetByCityState(string city, string state)
        {
            List<Store> store = (from s in context.Stores
                           where s.City == city && s.State == state
                           select s).ToList();
            return store;
        }

        [HttpPost]
        public ActionResult Create(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            context.Stores.Add(store);
            context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Update(string id, [FromBody] Store store)
        {
            if (id != store.StorId)
            {
                return BadRequest();
            }
            context.Entry(store).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Store> Delete(string id)
        {
            var store = context.Stores.Find(id);
            if (store == null)
            {
                return NotFound();
            }
            List<Sale> sales = (from s in context.Sales
                                where s.StorId == id
                                select s).ToList();

            if(sales.Count != 0)
            {
                foreach(Sale s in sales)
                {
                    context.Sales.Remove(s);
                    context.SaveChanges();
                }
            }
            context.Stores.Remove(store);
            context.SaveChanges();

            return store;
        }
    }
}
