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
        public async Task<ActionResult<List<GameHomePageDto>>> getAllGameIsBought(string userName)
        {
            var user = _context.Users.Where(w => w.userName == userName).FirstOrDefault();
            List<Receipt> receipt = _context.Receipts.Where(w => w.userId == user.userId).ToList();
            List<Game> gameList = new List<Game>();
            foreach (var item in receipt)
            {
                Game game = _context.Games.Where(w => w.Id == item.gameId).Include(g => g.Tags).FirstOrDefault();
                gameList.Add(game);
            }
            List<GameHomePageDto> resultList = new List<GameHomePageDto>();
            foreach(var item in gameList)
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
            return resultList;
        }
        [HttpGet("getInfomation")]
        public async Task<ActionResult<GameLibraryDto>> getGameInfomation(int gameId)
        {
            var game = _context.Games.Where(w => w.Id == gameId).FirstOrDefault();
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
            var games = _context.Games.Include(g => g.Tags).Where(w => w.Name == game.Name).ToList();
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
