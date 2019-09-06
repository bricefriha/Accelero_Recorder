using AcceleroRecorder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AcceleroRecorder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;

        public MenuPage()
        {
            InitializeComponent();


            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Record, Title="Record" },
                new HomeMenuItem {Id = MenuItemType.History, Title="History" },
                new HomeMenuItem {Id = MenuItemType.About, Title="About" }
            };

            ListViewMenu.ItemsSource = menuItems;

            
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                // Deselect the item.
                if (sender is ListView lv)
                    lv.SelectedItem = null;

                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
                
                

            };
        }

        private void ListViewMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}