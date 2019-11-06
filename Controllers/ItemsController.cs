using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CoreMigrationsWebApi.Context;
using CoreMigrationsWebApi.ViewModels;

namespace CoreMigrationsWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/items
        [HttpGet]
        public IActionResult Get()
        {
            var dbItems = _context.Items.ToList();
            var viewModel = new ItemsViewModel
            {
                Items = dbItems.Select(x => new ItemViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList(),
                Count = dbItems.Count,
            };

            return Ok(viewModel);
        }

        // GET api/items/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var dbItem = _context.Items.FirstOrDefault(x => x.Id == id);

            if (dbItem == null)
            {
                return NotFound("item not found!");
            }

            return Ok(new ItemViewModel { Id = dbItem.Id, Name=dbItem.Name });
        }

        // POST api/items
        [HttpPost]
        public IActionResult Post([FromBody]ItemViewModel item)
        {
            var newDbItem = new Item { Name = item.Name };

            try
            {
                _context.Items.Add(newDbItem);
                _context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Get(newDbItem.Id);
        }

        // PUT api/items/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ItemViewModel item)
        {
            var dbItem = _context.Items.FirstOrDefault(x => x.Id == id);
            if (dbItem == null)
            {
                return NotFound("item not found!");
            }

            try
            {
                dbItem.Name = item.Name;
                _context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Get(id);
        }

        // DELETE api/items/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dbItem = _context.Items.FirstOrDefault(x => x.Id == id);
            if (dbItem == null)
            {
                return NotFound("item not found!");
            }

            try
            {
               _context.Items.Remove(dbItem);
               _context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok("Delete successful");
        }
    }
}
