using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using API_PBL.Models.DatabaseModels;
using API_PBL.Models.DtoModels;
using System.IO;
using API_PBL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Office.Interop.Excel;


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
        [HttpGet("{tagName}")]
        public async Task<ActionResult<List<GameHomePageDto>>> GetAllGame(string tagName)
        {
            List<GameHomePageDto> resultList = new List<GameHomePageDto>();
            if (tagName == "All")
            {
                var games = _context.Games.Include(g => g.Tags).ToList();
                foreach (var item in games)
                {
                    var listTag = item.Tags.First();
                    var images = _context.Images.Where(w => w.gameId == item.Id).First();
                    
                    GameHomePageDto game = new GameHomePageDto
                    {
                        gameId = item.Id,
                        gameName = item.Name,
                        gamePrice = item.Price,
                        gameTag = listTag.tagName,
                        gameImageUrl = _blobService.GetBlob(images.imageName,"images")
                    };
                    resultList.Add(game);
                }

                return resultList;
            }
            else
            { 
                var games = _context.Games.Include(g => g.Tags).ToList();
                foreach (var item in games)
                {
                    var tagRequest = item.Tags.Where(t => t.tagName == tagName).FirstOrDefault();
                    if (tagRequest != null)
                    {
                        var images = _context.Images.Where(w => w.gameId == item.Id).First();
                    
                        GameHomePageDto game = new GameHomePageDto
                        {
                            gameId=item.Id,
                            gameName = item.Name,
                            gamePrice = item.Price,
                            gameTag = tagName,
                            gameImageUrl = _blobService.GetBlob(images.imageName,"images")
                        };
                        resultList.Add(game);
                    }
                }
                if(resultList == null)
                {
                    return BadRequest("Error!");
                }
                else
                {
                    return resultList;
                }
            }
        }
        [HttpGet("gameId")]
        public async Task<ActionResult<GameDto>> GetByGameId(int gameId)
        {
            var game = _context.Games.Where(w => w.Id == gameId).FirstOrDefault();
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
            var games = _context.Games.Include(g => g.Tags).Where(w => w.Id == game.Id).ToList();
            List<string> gametags = new List<string>();
            foreach (var item in games)
            {
                var listTag = item.Tags.ToList();
                foreach (var tag in listTag)
                {
                    gametags.Add(tag.tagName);
                }
            }

            dto.Tag = gametags;
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
        [HttpGet("search")]
        public async Task<ActionResult<List<GameHomePageDto>>> Search(string gameName)
        {
            var games = _context.Games.Include(g => g.Tags).ToList();
            List<GameHomePageDto> resultList = new List<GameHomePageDto>();
            foreach (var item in games)
            {
                string itemName = item.Name.ToLower();
                if ((itemName).Contains(gameName.ToLower()) == true)
                {
                    var listTag = item.Tags.First();
                    var images = _context.Images.Where(w => w.gameId == item.Id).First();

                    GameHomePageDto game = new GameHomePageDto
                    {
                        gameId = item.Id,
                        gameName = item.Name,
                        gamePrice = item.Price,
                        gameTag = listTag.tagName,
                        gameImageUrl = _blobService.GetBlob(images.imageName, "images")
                    };
                    resultList.Add(game);
                }
            }
            if (resultList == null)
            {
                return BadRequest(gameName + "not found");
            }
            return resultList;
        }
        [HttpPost("Create Game"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateGameDto request)
        {
            var games = _context.Games.Where(w => w.Name == request.Name).FirstOrDefault();
            if(games != null)
            {
                return BadRequest("Game is exist");
            }
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
            await _context.SaveChangesAsync();
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
            return Ok("Create successful");
        }
        [HttpPut("UpdateGameInformation"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGame(GameDto request)
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
            return Ok("Update successfully");
        }
        [HttpPut("Image"), Authorize(Roles = "Admin")]
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
        [HttpDelete("{Id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGame(int Id)
        {
            var gameTemp = await _context.Games.FindAsync(Id);
            if (gameTemp == null)
            {
                return BadRequest("Game not found");
            }
            _context.Games.Remove(gameTemp);
            await _context.SaveChangesAsync();
            return Ok("Delete Successfully");
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
