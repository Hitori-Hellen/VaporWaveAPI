using API_PBL.Models.DatabaseModels;
namespace API_PBL.Services
{
    public interface IExcelService
    {
        Task<List<Game>> GetGameListAsync();
    }
}
