using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using MyToDo.Api.Context;
using MyToDo.Api.Context.Repository;
using MyToDo.Api.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var ConnString = "ToDoConnection";
//×¢Èë
builder.Services.AddDbContext<MyToDoContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("ToDoConnection"))).AddUnitOfWork<MyToDoContext>()
    .AddCustomRepository<ToDo, ToDoRepository>()
    .AddCustomRepository<Memo, MemoRepository>()
    .AddCustomRepository<User, UserRepository>()
    .AddCustomRepository<Setting, SettingRepository>(); 

builder.Services.AddTransient<IToDoService,ToDoService>();
//builder.Services.AddUnitOfWork<MyToDoContext>()
//    .AddCustomRepository<ToDo,ToDoRepository>()
//    .AddCustomRepository<Memo, MemoRepository>()
//    .AddCustomRepository<User, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
