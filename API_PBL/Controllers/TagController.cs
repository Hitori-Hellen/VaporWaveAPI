using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_PBL.Models.DatabaseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace API_PBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly DataContext _context;

        public TagController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Tag>>> get()
        {
            return Ok(await _context.Tags.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> create(string request)
        {
            var tag = new Tag
            {
                tagName = request
            };
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("ExcelFile")]
        public async Task<IActionResult> getTagByFile(IFormFile file)
        {
            return NoContent();
        }
    }
}
