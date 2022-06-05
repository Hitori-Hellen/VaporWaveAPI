namespace API_PBL.Models.DtoModels
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int AgeRating { get; set; }
        public double GameRating { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Website { get; set; }
        public string Spec { get; set; }
    }
}
