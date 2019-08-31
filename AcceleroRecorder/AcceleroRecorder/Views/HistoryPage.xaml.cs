using AcceleroRecorder.Models;
using AcceleroRecorder.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
            // Get the list view item index
            var id = e.ItemIndex;

            // Get the filename of the selected record
            string filename = ((HistoryViewModel)BindingContext).Records[id].Filename;

            // Switch page
            Navigation.PushAsync(new DetailRecordPage(filename));
        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            // Get the list view item index
            var id = (int)((Button)sender).CommandParameter;

            // Delete the record
            DeleteRecord(id);

        }

        private void DeleteRecord(int id)
        {
            // Get the filename of the selected record
            string filename = ((HistoryViewModel)BindingContext).Records[id].Filename;

            // Remove the record
            File.Delete(filename);

            // Reload the Binding Context
            BindingContext = new HistoryViewModel();
        }
    }
}