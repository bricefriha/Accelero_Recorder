using AcceleroRecorder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AcceleroRecorder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordPage : ContentPage
    {
        
        RecordViewModel vm ;
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;
        public RecordPage()
        {
            InitializeComponent();

            // Bind the viewmodel with the view
            BindingContext = new RecordViewModel();

            // Get the viewmodel
            vm = (RecordViewModel)BindingContext;

            // Register for reading changes, be sure to unsubscribe when finished
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        }
        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;

            // Set X data
            vm.Xdata = Convert.ToDouble(data.Acceleration.X);

            // Set Y data
            vm.Ydata = Convert.ToDouble(data.Acceleration.Y);

            // Set Z data
            vm.Zdata = Convert.ToDouble(data.Acceleration.Z);
        }

        private void BtnRecord_Clicked(object sender, EventArgs e)
        {
            // Change the style of the button
            SwitchOnOrOff((Button)sender);

            // Start or Stop the accelerometer
            ToggleAccelerometer();

        }
        /// <summary>
        /// Method allowing to Start or Stop the accelerometer
        /// </summary>
        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                {
                    // Stop the Accelerometer
                    Accelerometer.Stop();

                    // Clean data on the screen
                    vm.Xdata = 0;
                    vm.Ydata = 0;
                    vm.Zdata = 0;
                }
                else
                    // Start the Accelerometer
                    Accelerometer.Start(speed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
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