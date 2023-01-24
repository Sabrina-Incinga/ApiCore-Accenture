using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiPubs.Data;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/publisher")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly pubsContext context;

        public PublisherController(pubsContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Publisher>> Get()
        {
            return context.Publishers.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Publisher> Get(string id)
        {
            return context.Publishers.Find(id);
        }

        [HttpPost]
        public ActionResult Create(Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            context.Publishers.Add(publisher);
            context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Update(string id, [FromBody] Publisher publisher)
        {
            if (id != publisher.PubId)
            {
                return BadRequest();
            }
            context.Entry(publisher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Publisher> Delete(string id)
        {
            var publisher = context.Publishers.Find(id);
            if (publisher == null)
            {
                return NotFound();
            }
            context.Publishers.Remove(publisher);
            context.SaveChanges();

            return publisher;
        }

    }
}
