using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_PBL.Models.DatabaseModels
{
    public class Receipt
    {
        [Key]
        public int receiptId { get; set; }
        public double gamePrice { get; set; }
        public DateTime purchaseDate { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string userId { get; set; }
        [JsonIgnore]
        public Game Game { get; set; }
        public int gameId { get; set; }
    }
}
