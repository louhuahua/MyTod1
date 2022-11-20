using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;

namespace MyToDo.Api.Extention
{
    public class AutoMapperProfile:MapperConfigurationExpression
    {
        public AutoMapperProfile()
        {
            CreateMap<ToDo,ToDoDto>().ReverseMap();
            CreateMap<Memo,MemoDto>().ReverseMap();
        }
    }
}
