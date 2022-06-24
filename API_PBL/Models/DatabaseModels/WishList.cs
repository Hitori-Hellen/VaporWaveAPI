using System.ComponentModel.DataAnnotations;

namespace API_PBL.Models.DatabaseModels
{
    public class WishList
    {
        [Key]
        public string userId { get; set; }
        public string gameName { get; set; }
    }
}
