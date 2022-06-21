using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using API_PBL.Models.DatabaseModels;
using API_PBL.Models.DtoModels;
using System.IO;

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
        public async Task<ActionResult<GameDto>> GetById(int GameId)
        {
            //var game = await _context.Games
            //    .Where(g => g.Id == GameId)
            //    .Include(g => g.Tags)
            //    .ToListAsync();

            Game game = await _context.Games.FindAsync(GameId);
            GameDto dto = new GameDto();
            List<string> pathString = new List<string>();
            dto.Id = game.Id;
            dto.Name = game.Name;
            dto.Description = game.Description;
            dto.ReleaseDate = game.ReleaseDate;
            dto.AgeRating = game.AgeRating;
            dto.GameRating = game.GameRating;
            dto.Price = game.Price;
            dto.Developer = game.Developer;
            dto.Publisher = game.Publisher;
            dto.Website = game.Website;
            dto.Spec = game.Spec;
            //int fCount = Directory.GetFiles($"C:\\Users\\duong\\Downloads\\API_PBL-20220605T161930Z-001\\API_PBL\\API_PBL\\Image\\{GameId}\\", "+", SearchOption.AllDirectories).Length;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo($"C:\\Users\\duong\\Downloads\\API_PBL-20220605T161930Z-001\\API_PBL\\API_PBL\\Image\\{GameId}\\");
            int count = dir.GetFiles().Length;
            dto.len = count;

            for (int i = 1; i <= count; i++)
            {
                pathString.Add($"C:\\Users\\duong\\Downloads\\API_PBL-20220605T161930Z-001\\API_PBL\\API_PBL\\Image\\{GameId}\\{i}");
            }
            dto.Path = pathString;
            return dto;
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
