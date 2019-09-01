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

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            // Get the list view item index
            var id = (int)((Button)sender).CommandParameter;

            // Get the record
            RecordItem rec = ((HistoryViewModel)BindingContext).Records[id];

            // Display a confirm pop up
            if (await DisplayAlert("Delete " + rec.Title, "Are you sure to delete this record? \n(No move back possible)", "Yes, I am sure", "Cancel"))
            {
                // Delete the record
                DeleteRecord(rec);
            }
        }

        /// <summary>
        ///  Method which delete a record and reload the page
        /// </summary>
        /// <param name="recordItem"> record we need to delete </param>
        private void DeleteRecord(RecordItem recordItem)
        {
            // Get the filename of the selected record
            string filename = recordItem.Filename;

            // Remove the record
            File.Delete(filename);

            // Reload the Binding Context
            BindingContext = new HistoryViewModel();
        }

        private async void BtnDeleteAll_Clicked(object sender, EventArgs e)
        {
            // Display a confirm pop up
            if (await DisplayAlert("Delete all", "Are you sure to delete all the records? \n(No move back possible)", "Yes, I am sure", "Cancel"))
            {
                // Remove every single record
                foreach (RecordItem recordItem in ((HistoryViewModel)BindingContext).Records)
                {
                    DeleteRecord(recordItem);
                }
            };
            
        }
    }
}