using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Service
{
    public class MemoService : IMemoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MemoService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> AddAsync(MemoDto dto)
        {
            try
            {
                var model=mapper.Map<Memo>(dto);
                model.CreateTime=DateTime.Now;
                model.UpdateTime=DateTime.Now;
                await unitOfWork.GetRepository<Memo>().InsertAsync(model);
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
                var repository =  unitOfWork.GetRepository<Memo>();
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
                var repository = unitOfWork.GetRepository<Memo>();
                var memoS = await repository.GetPagedListAsync(predicate:
                    x=> string.IsNullOrWhiteSpace(parameter.Search)?true:x.Title.Contains(parameter.Search),
                    pageIndex:parameter.PageIndex,
                    pageSize:parameter.PageSize,
                    orderBy:source=> source.OrderByDescending(t => t.CreateTime));
                return new ApiResponse(true, memoS);

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
                var repository = unitOfWork.GetRepository<Memo>();
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

        public async Task<ApiResponse> UpdateAsync(MemoDto dto)
        {
            try
            {
                var model = mapper.Map<Memo>(dto);

                var repository = unitOfWork.GetRepository<Memo>();
                var toDo=await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));
                toDo.Title = model.Title;
                toDo.Content = model.Content;
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
