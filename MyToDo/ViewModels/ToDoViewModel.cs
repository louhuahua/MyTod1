using MyToDo.Common.Models;
using MyToDo.Service;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class ToDoViewModel: NavigationViewModel
    {
        public ToDoViewModel(IToDoService service,IContainerProvider provider):base(provider)
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            AddCommand = new DelegateCommand(Add);
            OpenRightDrawer = new DelegateCommand(Open);
            this.service = service;
            GetDataAsync();

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
        private readonly IToDoService service;

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }

        async void GetDataAsync()
        {
            UpdateLoading(true);
            ToDoDtos.Clear();
            var result = await service.GetAllAsync(new QueryParameter() {PageIndex=0,PageSize=100 });
            if (result.Status)
            {
                foreach (var item in result.Result.Items)
                {
                    ToDoDtos.Add(item);
                }
            }
            UpdateLoading(false);

        }

        //public override void OnNavigatedTo(NavigationContext navigationContext)
        //{
        //    base.OnNavigatedTo(navigationContext);
        //    GetDataAsync();
        //}
    }
}
