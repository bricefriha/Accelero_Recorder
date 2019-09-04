using AcceleroRecorder.Object;
using AcceleroRecorder.ViewModels;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AcceleroRecorder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordPage : ContentPage
    {
        Stopwatch watch = new Stopwatch();
        RecordViewModel vm ;
        Record record;

        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;
        public RecordPage()
        {
            InitializeComponent();

            // Bind the viewmodel with the view
            BindingContext = new RecordViewModel();

            // Get the viewmodel
            vm = (RecordViewModel)BindingContext;

            // Instanciate the record var
            record = new Record(vm);

            // Register for reading changes, be sure to unsubscribe when finished
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        }
        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            // Get the data reading
            var data = e.Reading;

            // Refresh the chart with the new value
            vm.RefreshValuesChart(data.Acceleration.X, data.Acceleration.Y, data.Acceleration.Z);

        }

        private void BtnRecord_Clicked(object sender, EventArgs e)
        {
            // Change the style of the button
            SwitchOnOrOff((Button)sender);

            //// Start or Stop the accelerometer
            //ToggleAccelerometer();


        }

        /// <summary>
        /// Switch a button On or Off
        /// </summary>
        /// <param name="sender">the button</param>
        private async void SwitchOnOrOff(Button btn)
        {
            try
            {
                // Verified the state of the button
                switch (btn.CornerRadius)
                {
                    // If it is Off
                    case 100:

                        // Start to recording
                        record.Start();

                        break;

                    // If it is On
                    case 10:

                        // Start to recording
                        record.Stop();

                        // Clear data
                        record.Clear();

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