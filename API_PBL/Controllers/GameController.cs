using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using API_PBL.Models.DatabaseModels;
using API_PBL.Models.DtoModels;

namespace API_PBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly DataContext _context;
        public GameController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Game>>> Get()
        {
            return Ok(await _context.Games.Include(g => g.Tags).ToListAsync());
        }
        [HttpGet("{GameId}")]
        public async Task<ActionResult<List<Game>>> GetById(int GameId)
        {
            var game = await _context.Games
                .Where(g => g.Id == GameId)
                .Include(g => g.Tags)
                .ToListAsync();
            return game;
        }
        [HttpPost]
        public async Task<ActionResult<List<Game>>> Create(CreateGameDto request)
        {
            var newGame = new Game
            {
                Name = request.Name,
                ReleaseDate = request.ReleaseDate,
                AgeRating = request.AgeRating,
                Price = request.Price,
                Description = request.Description,
                Developer = request.Developer
            };

            _context.Games.Add(newGame);
            _context.SaveChangesAsync();

            return Ok(newGame);
        }
        [HttpPost("GameTag")]
        public async Task<ActionResult<List<Game>>> AddGameTag(AddGameTag request)
        {
            var game_temp = await _context.Games.Where(c => c.Id == request.IdGame).Include(c => c.Tags).FirstOrDefaultAsync();
            if(game_temp == null) { return NotFound(); }
            var tag_temp = await _context.Tags.FindAsync(request.IdTag);
            if (tag_temp== null) { return NotFound(); }

            _context.Tags.Add(tag_temp);
            await _context.SaveChangesAsync();

            return Ok(game_temp);
        }
    }
}
