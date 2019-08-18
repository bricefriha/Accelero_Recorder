﻿using AcceleroRecorder.ViewModels;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            //Timer timer = new Timer(300.0);
            //timer.Start();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            // Launch the timer
            Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                    {
                        lblMilliSec.Text = watch.Elapsed.Milliseconds.ToString();
                        lblMinutes.Text = watch.Elapsed.Minutes.ToString("00");
                        lblSecond.Text = watch.Elapsed.Seconds.ToString("00");
                    });
                return true; // True = Repeat again, False = Stop the timer
            });

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