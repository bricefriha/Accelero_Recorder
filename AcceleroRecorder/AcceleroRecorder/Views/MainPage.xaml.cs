using AcceleroRecorder.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AcceleroRecorder.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;


            // Add the original page
            MenuPages.Add((int)MenuItemType.Record, (NavigationPage)Detail);
        }
        /// <summary>
        /// Method which manage the navigation from the menu
        /// </summary>
        /// <param name="id"> The ID of the menu item </param>
        /// <returns></returns>
        public async Task NavigateFromMenu(int id)
        {
            NavigationPage newPage = null;

            // If the page is not already fill
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    // Navigate to the record page
                    case (int)MenuItemType.Record:
                        MenuPages.Add(id, new NavigationPage(new RecordPage()));
                        newPage = new NavigationPage(new RecordPage());
                        break;

                    // Navigate to the history page
                    case (int)MenuItemType.History:
                        //NavigationPage navHistPage = new NavigationPage(new HistoryPage());
                        //MenuPages.Add(id, navHistPage);
                        newPage = new NavigationPage(new HistoryPage());

                        break;

                    // Navigate to the about page
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        newPage = new NavigationPage(new AboutPage());
                        break;
                }
            }
            else
            {
                // The next page will be the one of the list
                newPage = MenuPages[id];
            }

            // Go to the specified page
            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

    }
}
