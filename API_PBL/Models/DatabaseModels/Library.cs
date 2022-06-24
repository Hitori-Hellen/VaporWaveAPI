using System.ComponentModel.DataAnnotations;

namespace API_PBL.Models.DatabaseModels
{
    public class Library
    {
        [Key]
        public int id { get; set; }
        public string userId { get; set; }
        public string gameName { get; set; }
    }
}
