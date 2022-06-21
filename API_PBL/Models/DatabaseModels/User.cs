using System.ComponentModel.DataAnnotations;

namespace API_PBL.Models.DatabaseModels
{
    public class User
    {
        [Key]
        public string userId { get; set; }
        public string userName { get; set; }
        public string dateOfBirth { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public double userWallet { get; set; }
    }
}
