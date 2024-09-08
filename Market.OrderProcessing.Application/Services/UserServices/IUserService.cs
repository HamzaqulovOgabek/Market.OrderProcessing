
namespace Market.Auth.Application.Services.UserServices;

public interface IUserService
{
    Task<UserBaseDto> GetAsync(int userId);
}
