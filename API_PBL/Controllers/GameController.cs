using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using API_PBL.Models.DatabaseModels;
using API_PBL.Models.DtoModels;
using System.IO;
using API_PBL.Services;

namespace API_PBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IBlobService _blobService;
        private readonly DataContext _context;
        public GameController(DataContext context, IBlobService blobService)
        {
            _context = context;
            _blobService = blobService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Game>>> Get()
        {
            return Ok(await _context.Games.Include(g => g.Tags).ToListAsync());
        }
        //[HttpGet("getHomeGame")]
        //public async Task<ActionResult<List<GameHomePageDto>>> GetAllGame(string request)
        //{
        //    List<GameHomePageDto> resultList = new List<GameHomePageDto>();
        //    if(request == "All")
        //    {
        //        var games = _context.Games.Include(g => g.Tags).ToList();
        //        foreach (var item in games)
        //        {
        //            var tagGame = item.Tags.First();
        //            var temp = new GameHomePageDto
        //            {
        //                gameName = item.Name,
        //                gameTag = tagGame.tagName,

        //            };
        //        }
        //    }
        //}
        [HttpGet("{GameId}")]
        public async Task<ActionResult<GameDto>> GetById(int GameId)
        {
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
            var images = _context.Images.Where(w => w.gameId == game.Id).ToList();
            List<string> url = new List<string>();
            foreach (var item in images)
            {
                var res = _blobService.GetBlob(item.imageName, "images");
                url.Add(res);
            }
            dto.Path = url;
            return dto;
        }
        [HttpPost("Create Game")]
        public async Task<ActionResult> Create(CreateGameDto request)
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
            _context.SaveChanges();
            var gameTemp = await _context.Games.Where(w => w.Name == request.Name).Include(c => c.Tags).FirstOrDefaultAsync();
            if (gameTemp == null) { return NotFound(); }
            List<string> tagGame = request.Tag.ToList();
            foreach(var item in tagGame)
            {
                var tagTemp = _context.Tags.Where(w => w.tagName == item).FirstOrDefault();
                if(tagTemp == null) { return NotFound(); }
                gameTemp.Tags.Add(tagTemp);
            }
            await _context.SaveChangesAsync();
            return NoContent();
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
        [HttpPut("Image")]
        public async Task<IActionResult> updateImageForGame(string gameName, IFormFile file)
        {
            var game = _context.Games.Where(w => w.Name == gameName).FirstOrDefault();
            bool res = await AddImage(game.Id, file);
            if (!res)
            {
                return BadRequest("Upload image failed");
            }
            return Ok("Successfully");
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
        private async Task<bool> AddImage(int gameId,IFormFile file)
        {           
            if(file == null)
            {
                return false;
            }
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var res = await _blobService.UploadBlobAsync(fileName, file, "images");
            if (res)
            {
                var image = new Image
                {
                    gameId = gameId,
                    imageName = fileName
                };
                _context.Images.Add(image);
                await _context.SaveChangesAsync();
                return true;
            }return false;
        }
    }
}
