using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_PBL.Models.DatabaseModels
{
    public class Library
    {
        [Key]
        public int id { get; set; }
        public string gameName { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string userId { get; set; }
        
        
    }
}
