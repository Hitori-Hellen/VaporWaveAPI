namespace API_PBL.Models.DtoModels
{
    public class CreateGameDto
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int AgeRating { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
    }
}
