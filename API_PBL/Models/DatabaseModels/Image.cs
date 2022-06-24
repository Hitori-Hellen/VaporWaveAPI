using System.ComponentModel.DataAnnotations;

namespace API_PBL.Models.DatabaseModels
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string imageName { get; set; }
        public Game Game { get; set; }
        public int gameId { get; set; }
    }
}
