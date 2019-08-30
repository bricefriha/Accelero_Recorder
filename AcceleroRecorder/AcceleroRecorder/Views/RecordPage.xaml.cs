﻿using AcceleroRecorder.Object;
using AcceleroRecorder.ViewModels;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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

                    // Clear data on the screen
                    vm.ClearValuesChart();
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
        private async void SwitchOnOrOff(Button btn)
        {
            try
            {
                // Verified the state of the button
                switch (btn.CornerRadius)
                {
                    // If it is Off
                    case 100:
                        // Set it On
                        btn.CornerRadius = 10;

                        // Start The timer
                        vm.StartTimer();

                        // Start to recording
                        record.Start();

                        break;

                    // If it is On
                    case 10:

                        // Set it Off
                        btn.CornerRadius = 100;

                        // Stop the timer
                        vm.StopTimer();

                        // Start to recording
                        record.Stop();

                        record.Save();

                        string title = record.Title;
                        // Clear data
                        record.Clear();

                        // Test
                        Record testRecord = new Record(/*File.ReadAllText(fileName)*/Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), title + ".json"));
                        await recPage.DisplayAlert("Test",testRecord.Title /*testRecord.Frames[0].Time,/* frame.Time + " | Z :" + testRecord.Frames[0].XData.ToString() + "Y :" + testRecord.Frames[0].YData.ToString() + "X :" + frame.ZData.ToString() */, "Good for you sir");



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