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
    public partial class RecordPage : ContentPage
    {
        public RecordPage()
        {
            InitializeComponent();

            BindingContext = new RecordViewModel();
        }

        private void BtnRecord_Clicked(object sender, EventArgs e)
        {
            // Change the style of the button
            SwitchOnOrOff((Button)sender);
        }

        /// <summary>
        /// Switch a button On or Off
        /// </summary>
        /// <param name="sender">the button</param>
        private void SwitchOnOrOff(Button btn)
        {
            try
            {
                // Verified the state of the button
                switch (btn.CornerRadius)
                {
                    // If it is Off
                    case 100:
                        //Set it On
                        btn.CornerRadius = 10;
                        break;

                    // If it is On
                    case 10:
                        //Set it Off
                        btn.CornerRadius = 100;
                        break;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}