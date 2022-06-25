using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_PBL.Models.DatabaseModels
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string imageName { get; set; }
        [JsonIgnore]
        public Game Game { get; set; }
        public int gameId { get; set; }
    }
}
