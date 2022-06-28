using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_PBL.Models.DatabaseModels;
using API_PBL.Models.DtoModels;
using API_PBL.Services;
using Microsoft.AspNetCore.Authorization;

namespace API_PBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IBlobService _blobService;
        public LibraryController(DataContext context, IBlobService blobService)
        {
            _context = context;
            _blobService = blobService;
        }
        [HttpGet("getLibrary"),Authorize(Roles = "User")]
        public async Task<ActionResult<List<Game>>> getAllGameIsBought(string userName)
        {
            var user = _context.Users.Where(w => w.userName == userName).FirstOrDefault();
            List<Receipt> receipt = _context.Receipts.Where(w => w.userId == user.userId).ToList();
            List<Game> result = new List<Game>();
            foreach (var item in receipt)
            {
                Game game = _context.Games.Find(item.gameId);
                game.isPayed = true;
                result.Add(game);
            }

            return result;
        }
        [HttpGet("getInfomation")]
        public async Task<ActionResult<GameLibraryDto>> getGameInfomation(string gameName)
        {
            var game = _context.Games.Where(w => w.Name == gameName).FirstOrDefault();
            GameLibraryDto dto = new GameLibraryDto();
            List<string> pathString = new List<string>();
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
            dto.isPayed = true;
            var games = _context.Games.Include(g => g.Tags).Where(w => w.Name == gameName).ToList();
            List<string> gametags = new List<string>();
            foreach(var item in games)
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
    }
}
