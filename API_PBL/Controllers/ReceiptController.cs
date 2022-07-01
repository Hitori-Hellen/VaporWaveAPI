using API_PBL.Models.DatabaseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_PBL.Models.DtoModels;
using API_PBL.Services;
using Microsoft.AspNetCore.Authorization;

namespace API_PBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IBlobService _blobService;
        public ReceiptController(DataContext context, IBlobService blobService)
        {
            _context = context;
            _blobService = blobService;
        }
        [Microsoft.AspNetCore.Mvc.HttpGet("all"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Receipt>>> GetAllReceipt()
        {
            return Ok(await _context.Receipts.ToListAsync());
        }
        [HttpGet("topselling"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<GameSellingDto>>> getTopSelling()
        {
            var receipts = _context.Receipts.ToList();
            var games = _context.Games.ToList();
            List<string> gameNameList = new List<string>();
            var resultList = new List<GameSellingDto>();
            foreach (var item in receipts)
            {
                var game = _context.Games.Where(w => w.Id == item.gameId).FirstOrDefault();
                gameNameList.Add(game.Name);
            }
            //var singleName = gameNameList.GroupBy(n => n).Where(g => g.Count() == 1).Select(g => g.Key).ToList();
            var q = from x in gameNameList
                    group x by x into g
                    let count = g.Count()
                    orderby count descending
                    select new { Value = g.Key, Count = count };
            foreach (var item in q)
            {
                var game = _context.Games.Where(w => w.Name == item.Value).FirstOrDefault();
                var images = _context.Images.Where(w => w.gameId == game.Id).First();
                var gameResponse = new GameSellingDto
                {
                    gameId = game.Id,
                    gameName = game.Name,
                    gameRevenue = game.Price * item.Count,
                    imageUrl = _blobService.GetBlob(images.imageName, "images")
                };
                resultList.Add(gameResponse);
            }
            return resultList;
        }
        [HttpGet("Revenue"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<double>> GetAllRevenue()
        {
            var receipts = _context.Receipts.ToList();
            double sum = 0;
            foreach(var item in receipts)
            {
                sum += item.gamePrice;
            }
            return sum;
        }
        [HttpGet("Month"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<double>> GetRevenueByMonth(string month)
        {
            var receipts = _context.Receipts.ToList();
            var tempList = new List<Receipt>();
            foreach(var item in receipts)
            {
                if((item.purchaseDate).ToString("MM") == month)
                {
                    tempList.Add(item);
                }
            }
            double sum = 0;
            foreach (var item in receipts)
            {
                sum += item.gamePrice;
            }
            return sum;
        }
        [HttpGet("goal"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<double>> MonthsGoal(string month)
        {
            double goalEachmonth = 100;
            double testValue = GetRevenueByMonth(month).Result.Value;
            return (testValue / goalEachmonth) * 100;
        }
        [HttpPost("{userName}/{gameName}"), Authorize(Roles = "User")]
        public async Task<IActionResult> PayUp(string gameName, string userName)
        {
            var user = _context.Users.Where(w => w.userName == userName).FirstOrDefault();
            var game = _context.Games.Where(w => w.Name == gameName).FirstOrDefault();
            if (user.userWallet >= game.Price)
            {
                user.userWallet -= game.Price;
                Library library = new Library
                {
                    userId = user.userId,
                    gameName = game.Name,
                    userName = user.userName
                };
                _context.Library.Add(library);
                await _context.SaveChangesAsync();
                Receipt receipt = new Receipt
                {
                    gamePrice = game.Price,
                    purchaseDate = DateTime.Now,
                    userId = user.userId,
                    gameId = game.Id
                };
                _context.Receipts.Add(receipt);
                await _context.SaveChangesAsync();
                return Ok("Successfully");
            }
            return BadRequest("Not enough money");
        }
    }
}
