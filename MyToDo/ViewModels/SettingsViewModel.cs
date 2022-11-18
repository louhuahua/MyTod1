using MyToDo.Common.Models;
using MyToDo.Extensions;
using Prism.Commands;
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
    public class SettingsViewModel:BindableBase
    {
        private readonly IRegionManager regionManager;
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }

        public SettingsViewModel(IRegionManager _regionManager)
        {
            this.regionManager = _regionManager;
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);

            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenubar();

        }

        private void Navigate(MenuBar obj)
        {
            regionManager.Regions[PrismManager.SettingViewRegionName].RequestNavigate(obj.NameSpace);
        }
        private ObservableCollection<MenuBar> menuBars;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        public void CreateMenubar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "个性化", NameSpace = "SkinView" });
            MenuBars.Add(new MenuBar() { Icon = "Note", Title = "系统设置", NameSpace = "" });
            MenuBars.Add(new MenuBar() { Icon = "NoteAlert", Title = "关于更多", NameSpace = "AboutView" });
        }
    }
}
