using MyToDo.Common.Models;
using MyToDo.Shared.Dtos;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class IndexViewModel:BindableBase
    {
        public IndexViewModel()
        {
            TaskBars = new ObservableCollection<TaskBar>();
            CreateTaskbar();
            CreateTestData();
        }

        private ObservableCollection<TaskBar> taskBars;

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<ToDoDto> toDoDtos;

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<MemoDto> memoDtos;

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }

        public void CreateTaskbar()
        {
            TaskBars.Add(new TaskBar() { Icon = "Home", Title = "汇总", Content="9",Color= "#FFBB77", Target="" });
            TaskBars.Add(new TaskBar() { Icon = "Note", Title = "已完成", Content = "9", Color = "#FFBB77", Target = "" });
            TaskBars.Add(new TaskBar() { Icon = "NoteAlert", Title = "完成比例", Content = "100%", Color = "#FFBB77", Target = "" });
            TaskBars.Add(new TaskBar() { Icon = "CogBox", Title = "备忘录", Content = "999", Color = "#FFBB77", Target = "" });
        }

        void CreateTestData() 
        {
            ToDoDtos=new ObservableCollection<ToDoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();

            for (int i = 0; i < 10; i++)
            {
                ToDoDtos.Add(new ToDoDto() {Id=i,Title="待办"+i,Content="Content"+i });
                MemoDtos.Add(new MemoDto() {Id=i,Title="备忘"+i,Content="Content"+i});
            }
        }
    }
}
