using Arch.EntityFrameworkCore.UnitOfWork;
using MyToDo.Api.Context;

namespace MyToDo.Api.Service
{
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork unitOfWork;

        public ToDoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> AddAsync(ToDo model)
        {
            try
            {
                await unitOfWork.GetRepository<ToDo>().InsertAsync(model);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, model);
                }
                else
                {
                    return new ApiResponse("添加数据失败");
                }
            }
            catch(Exception ex) 
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var repository =  unitOfWork.GetRepository<ToDo>();
                var toDo = repository.GetFirstOrDefaultAsync(predicate:x=>x.Id.Equals(id));
                repository.Delete(id);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, "");
                }
                else
                {
                    return new ApiResponse("删除数据失败！");
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDo>();
                var toDos = await repository.GetAllAsync();
                return new ApiResponse(true, toDos);

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDo>();
                var toDo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return new ApiResponse(true, toDo);
                //if (await unitOfWork.SaveChangesAsync() > 0)
                //{
                //    return new ApiResponse(true, toDo);
                //}
                //else
                //{
                //    return new ApiResponse("获取数据失败！");
                //}
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(ToDo model)
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDo>();
                var toDo=await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));
                toDo.Title = model.Title;
                toDo.Content = model.Content;
                toDo.Status = model.Status;
                toDo.UpdateTime = DateTime.Now;

                repository.Update(model);

                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, toDo);
                }
                else
                {
                    return new ApiResponse("更新数据失败！");
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}
