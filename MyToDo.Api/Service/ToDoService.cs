using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Service
{
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ToDoService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> AddAsync(ToDoDto dto)
        {
            try
            {
                var model=mapper.Map<ToDo>(dto);
                model.CreateTime=DateTime.Now;
                model.UpdateTime=DateTime.Now;
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

        public async Task<ApiResponse> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDo>();
                var toDos = await repository.GetPagedListAsync(predicate:
                    x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Title.Contains(parameter.Search),
                    pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreateTime));
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

        public async Task<ApiResponse> UpdateAsync(ToDoDto dto)
        {
            try
            {
                var model = mapper.Map<ToDo>(dto);

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
