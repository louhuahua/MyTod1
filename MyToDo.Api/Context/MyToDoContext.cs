using Microsoft.EntityFrameworkCore;

namespace MyToDo.Api.Context
{
    public class MyToDoContext:DbContext
    {
        //protected readonly IConfiguration Configuration;

        //public MyToDoContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    // connect to sqlite database
        //    options.UseSqlite(Configuration.GetConnectionString("ToDoConnection"));
        //}
        public MyToDoContext(DbContextOptions<MyToDoContext> options):base(options)
        {

        }
        public DbSet<ToDo> ToDo { get; set; }
        public DbSet<Memo> Memo { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Setting> Setting { get; set; }
    }
}
