using MyToDo.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class ToDoViewModel:BindableBase
    {
        public ToDoViewModel()
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            CreateTestData();
            AddCommand = new DelegateCommand(Add);
            OpenRightDrawer = new DelegateCommand(Open);
        }

        private void Open()
        {
            IsRightDrawerOpen = true;
        }

        private void Add()
        {
            IsRightDrawerOpen = false;
        }

        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand OpenRightDrawer { get; private set; }
        private bool isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<ToDoDto> toDoDtos;

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }

        void CreateTestData()
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();

            for (int i = 0; i < 10; i++)
            {
                ToDoDtos.Add(new ToDoDto() { Id = i, Title = "待办" + i, Content = "Content" + i });
            }
        }
    }
}
