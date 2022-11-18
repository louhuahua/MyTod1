using Arch.EntityFrameworkCore.UnitOfWork;

namespace MyToDo.Api.Context.Repository
{

    public class SettingRepository : Repository<Setting>, IRepository<Setting>
    {
        public SettingRepository(MyToDoContext dbContext) : base(dbContext)
        {
        }
    }
}

