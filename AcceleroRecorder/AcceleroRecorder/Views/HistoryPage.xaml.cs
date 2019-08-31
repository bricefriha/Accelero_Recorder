using AcceleroRecorder.ViewModels;
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
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();

            // Data Binding
            BindingContext = new HistoryViewModel();
        }

        /// <summary>
        ///  On tapped event List view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvRecord_ItemTapped(object sender, ItemTappedEventArgs e)
        {
        }
    }
}