using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_PBL.Models.DatabaseModels;
using API_PBL.Models.DtoModels;
using Microsoft.AspNetCore.Authorization;

namespace API_PBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class LibraryController : ControllerBase
    {
        private readonly DataContext _context;
        public LibraryController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("getLibrary")]
        public async Task<ActionResult<List<Game>>> getAllGameIsBought(string userName)
        {
            User user = _context.Users.Where(w => w.userName == userName).FirstOrDefault();
            List<Game> result = new List<Game>();
            List<Library> libraries = _context.Library.Where(w => w.userId == user.userId).ToList();
            foreach (var item in libraries)
            {
                Game game = _context.Games.Where(w => w.Name == item.gameName).FirstOrDefault();
                result.Add(game);
            }
            return Ok(result.OrderBy(o => o.ReleaseDate));
        }
    }
}
