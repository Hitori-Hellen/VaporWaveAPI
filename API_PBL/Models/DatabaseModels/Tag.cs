using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_PBL.Models.DatabaseModels
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string tagName { get; set; }
        [JsonIgnore]
        public List<Game> Games { get; set; }
    }
}
