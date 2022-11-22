using MyToDo.Common.Models;
using MyToDo.Shared.Dtos;
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
    public class MemoViewModel:BindableBase
    {
        public MemoViewModel()
        {
            MemoDtos = new ObservableCollection<MemoDto>();
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

        private ObservableCollection<MemoDto> memoDtos;

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }

        void CreateTestData()
        {
            MemoDtos = new ObservableCollection<MemoDto>();

            for (int i = 0; i < 20; i++)
            {
                MemoDtos.Add(new MemoDto() { Id = i, Title = "备忘录" + i, Content = "Content" + i });
            }
        }
    }
}
