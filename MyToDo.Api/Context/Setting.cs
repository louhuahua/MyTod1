namespace MyToDo.Api.Context
{
    public class Setting: BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
    }
}
