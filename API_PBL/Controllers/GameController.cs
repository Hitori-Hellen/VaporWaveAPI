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
        [HttpPost("Game")]
        public async Task<ActionResult<List<Game>>> Create(CreateGameDto request)
        {
            var newGame = new Game
            {
                Name = request.Name,
                ReleaseDate = request.ReleaseDate,
                AgeRating = request.AgeRating,
                GameRating = request.GameRating,
                Price = request.Price,
                Description = request.Description,
                Developer = request.Developer,
                Publisher = request.Publisher,
                Spec = request.Spec,
                Website = request.Website
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

            game_temp.Tags.Add(tag_temp);
            await _context.SaveChangesAsync();

            return Ok(game_temp);
        }
        [HttpPut]
        public async Task<ActionResult<List<Game>>> UpdateGame(GameDto request)
        {
            var game_temp = await _context.Games.FindAsync(request.Id);
            if(game_temp == null)
            {
                return BadRequest("Game not found");
            }

            game_temp.Name = request.Name;
            game_temp.ReleaseDate = request.ReleaseDate;
            game_temp.AgeRating = request.AgeRating;
            game_temp.GameRating = request.GameRating;
            game_temp.Price = request.Price;
            game_temp.Description = request.Description;
            game_temp.Developer = request.Developer;
            game_temp.Publisher = request.Publisher;
            game_temp.Website = request.Website;
            game_temp.Spec = request.Spec;

            await _context.SaveChangesAsync();
            return Ok(game_temp);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<Game>>> DeleteGame(int Id)
        {
            var game_temp = await _context.Games.FindAsync(Id);
            if (game_temp == null)
            {
                return BadRequest("Game not found");
            }
            game_temp.Tags.RemoveAll(tag => tag.Id == Id);
            _context.Games.Remove(game_temp);
            
            // Chua xong
            return Ok(await _context.Games.Include(g => g.Tags).ToListAsync());
        }
    }
}
