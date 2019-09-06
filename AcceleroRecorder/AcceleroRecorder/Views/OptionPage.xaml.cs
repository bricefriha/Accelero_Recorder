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
    public partial class OptionPage : ContentPage
    {
        OptionViewModel vm;
        Page previousPage;
        /// <summary>
        /// Constructor of the Option page
        /// </summary>
        /// <param name="filename">file of the record</param>
        /// <param name="pPreviousPage"></param>
        public OptionPage(string filename, Page pPreviousPage)
        {
            InitializeComponent();

            // Fetch the View Model
            vm = new OptionViewModel(filename);

            // Bind with the view model
            BindingContext = vm;

            // Fetch the previous pqge
            previousPage = pPreviousPage;

        }
        /// <summary>
        /// Save button behaviour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            // Fetch the record object
            Object.Record rec = vm.Rec;

            // Set the new name
            rec.Title = txtName.Text;

            // Save the changes
            rec.SaveData();

            // Navigate back to the Detail page
            Navigation.RemovePage(this);

            // Remove the previous page
            Navigation.RemovePage(previousPage);

            // Navigate to a new Detail Record Page
            Navigation.PushAsync(new DetailRecordPage(rec.Filename));
        }
    }
}