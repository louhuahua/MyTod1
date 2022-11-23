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
    public class MainViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal regionNavigationJournal;
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }

        private ObservableCollection<MenuBar> menuBars;
        public MainViewModel(IRegionManager _regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenubar();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = _regionManager;
            GoBackCommand = new DelegateCommand(() =>
            {
                if (regionNavigationJournal!=null && regionNavigationJournal.CanGoBack)
                {
                    regionNavigationJournal.GoBack();
                }
            });

            GoForwardCommand = new DelegateCommand(() =>
            {
                if (regionNavigationJournal != null && regionNavigationJournal.CanGoForward)
                {
                    regionNavigationJournal.GoForward();
                }
            });
        }

        private void Navigate(MenuBar obj)
        {
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, back=>
            {
                regionNavigationJournal = back.Context.NavigationService.Journal;
            }
            );

            
        }

        

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        public void CreateMenubar() 
        {
            MenuBars.Add(new MenuBar() {Icon="Home",Title="首页",NameSpace="IndexView" });
            MenuBars.Add(new MenuBar() {Icon="Note",Title="待办事项",NameSpace="ToDoView" });
            MenuBars.Add(new MenuBar() {Icon="NoteAlert",Title="备忘录",NameSpace="MemoView" });
            MenuBars.Add(new MenuBar() {Icon="CogBox",Title="设置",NameSpace="SettingsView" });
            MenuBars.Add(new MenuBar() {Icon="Home",Title="主窗口",NameSpace="MainView" });
        }
    }
}
