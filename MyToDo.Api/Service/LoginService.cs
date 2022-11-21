using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;

namespace MyToDo.Api.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public LoginService(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> LoginAsync(string account, string password)
        {
            try
            {
                var user = await work.GetRepository<User>().GetFirstOrDefaultAsync(predicate:x=>x.Account.Equals(account) && x.Password.Equals(password));

                if (user==null)
                {
                    return new ApiResponse("账号或密码错误");
                }
                else
                {
                    return new ApiResponse(true, "");
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse("登陆失败："+ex.Message);
            }
        }

        public async Task<ApiResponse> Register(UserDto dto)
        {
            try
            {
                var repository=work.GetRepository<User>();
                var user = await repository.GetFirstOrDefaultAsync(predicate:x=>x.Account.Equals(dto.Account));

                if (user==null)
                {
                    var model = mapper.Map<User>(dto);

                    model.CreateTime = DateTime.Now;
                    model.UpdateTime = DateTime.Now;
                    await repository.InsertAsync(model);
                    if (await work.SaveChangesAsync() > 0)
                    {
                        return new ApiResponse(true, "");
                    }
                    else
                    {
                        return new ApiResponse("添加数据失败");
                    }
                }

                return new ApiResponse("已存在相同用户名的账号");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}
