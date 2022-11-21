using MyToDo.Shared.Dtos;

namespace MyToDo.Api.Service
{
    public interface ILoginService
    {
        public Task<ApiResponse> LoginAsync(string account, string password);
        public Task<ApiResponse> Register(UserDto user);
    }
}
