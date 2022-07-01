using API_PBL.Models.DtoModels;

namespace API_PBL.Services
{
    public interface IEmailService
    {
        Task SendEmail(EmailDto email);
    }
}
