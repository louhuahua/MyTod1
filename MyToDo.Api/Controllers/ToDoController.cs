﻿using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context;
using MyToDo.Api.Service;

namespace MyToDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToDoController : Controller
    {
        private readonly IToDoService service;

        public ToDoController(IToDoService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await service.GetSingleAsync(id);
        [HttpGet]
        public async Task<ApiResponse> GetAll(int id) => await service.GetAllAsync();
        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] ToDo model) => await service.UpdateAsync(model);
        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] ToDo model) => await service.AddAsync(model);
        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await service.DeleteAsync(id);
    }
}
