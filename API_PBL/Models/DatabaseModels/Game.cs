using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_PBL.Models.DatabaseModels
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int AgeRating { get; set; }
        public double GameRating { get; set; }
        public double Price { get; set;}
        public string Description { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Website { get; set; }
        public string Spec { get; set; }
        public bool isLiked { get; set; } = false;
        public List<Tag> Tags { get; set; }

    }
}
