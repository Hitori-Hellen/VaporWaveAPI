using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_PBL.Models.DatabaseModels
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string username { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public string role { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string userId { get; set; }
    }
}
