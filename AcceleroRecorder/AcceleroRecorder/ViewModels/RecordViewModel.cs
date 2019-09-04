using GsbMobileXamarin.ViewModels;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AcceleroRecorder.ViewModels
{
    /// <summary>
    /// ViewModel of the Record page
    /// </summary>
    public class RecordViewModel : BaseViewModel
    {
        private double btnCorner ;
        public double BtnCorner
        {
            get { return btnCorner; }
            set
            {
                btnCorner = value;
                OnPropertyChanged();
            }
        }
        private Stopwatch watch;
        public Stopwatch Watch
        {
            get { return watch; }
            set
            {
                watch = value;
                OnPropertyChanged();
            }
        }
        private RadarChart chart;
        public RadarChart Chart
        {
            get { return chart; }
            set
            {
                chart = value;
                OnPropertyChanged();
            }
        }
        private string milliseconds;
        public string Milliseconds
        {
            get { return milliseconds; }
            set
            {
                milliseconds = value;
                OnPropertyChanged();
            }
        }
        private string seconds;
        public string Seconds
        {
            get { return seconds; }
            set
            {
                seconds = value;
                OnPropertyChanged();
            }
        }
        private string minutes;
        public string Minutes
        {
            get { return minutes; }
            set
            {
                minutes = value;
                OnPropertyChanged();
            }
        }
        private Collection<Microcharts.Entry> entries;
        public Collection<Microcharts.Entry> Entries
        {
            get { return entries; }
            set
            {
                entries = value;
                OnPropertyChanged();
            }
        }
        private float xdata;
        public float Xdata
        {
            get { return xdata; }
            set
            {
                xdata = value;
                OnPropertyChanged();
            }
        }
        private float ydata;
        public float Ydata
        {
            get { return ydata; }
            set
            {
                ydata = value;
                OnPropertyChanged();
            }
        }
        private float zdata;
        public float Zdata
        {
            get { return zdata; }
            set
            {
                zdata = value;
                OnPropertyChanged();
            }
        }
        public RecordViewModel()
        {
            // Set the bouton rounded
            this.BtnCorner = 100;

            // Set the title
            this.Title = "Recording";

            // Set accelerometers data up
            this.Xdata = 0;
            this.Ydata = 0;
            this.Zdata = 0;
            this.milliseconds = "000";
            this.seconds = "00";
            this.minutes = "00";

            this.watch = new Stopwatch();
            //this.entries

            //this.Chart = new LineChart()
            //{
            //    Entries = this.entries
            //};
            entries = new Collection<Microcharts.Entry>
             {
                 new Microcharts.Entry(0)
                 {
                     Label = "Xdata",
                     ValueLabel = "0",
                     Color = SKColor.Parse("#2c3e50")
                 },
                 new Microcharts.Entry(0)
                 {
                     Label = "Ydata",
                     ValueLabel = "0",
                     Color = SKColor.Parse("#77d065")
                 },
                 new Microcharts.Entry(0)
                 {
                     Label = "Zdata",
                     ValueLabel = "0",
                     Color = SKColor.Parse("#b455b6")
                 },
            };
            this.Chart = new RadarChart() { Entries = entries };
        }
        /// <summary>
        /// This is the method allowing to refresh the chart values
        /// </summary>
        /// <param name="xData"></param>
        /// <param name="yData"></param>
        /// <param name="zData"></param>
        public void RefreshValuesChart(float xData, float yData, float zData)
        {
            // Set X data
            this.Xdata = xData;

            // Set Y data
            this.Ydata = yData;

            // Set Z data
            this.Zdata = zData;

            // Refresh the chart value
            this.Chart = new RadarChart()
            {
                Entries =  new Collection<Microcharts.Entry>
                {
                    // Set Y data
                    new Microcharts.Entry(yData)
                    {
                        Label = "Ydata",
                        ValueLabel = yData.ToString(),
                        Color = SKColor.Parse("#18CC00")
                    },
                    // Set X data
                    new Microcharts.Entry(xData)
                    {
                        Label = "Xdata",
                        ValueLabel = xData.ToString(),
                        Color = SKColor.Parse("#E5001B")
                    },
                    // Set Z data
                    new Microcharts.Entry(zdata)
                    {
                        Label = "Zdata",
                        ValueLabel = zdata.ToString(),
                        Color = SKColor.Parse("#0018CC")
                    },
                }
            };
        }
        /// <summary>
        /// This is the method allowing to clear the chart values
        /// </summary>
        public void ClearValuesChart()
        {
            const float zero = 0;
            

            // Set X data
            this.Xdata = zero;

            // Set Y data
            this.Ydata = zero;

            // Set Z data
            this.Zdata = zero;

            // Refresh the chart value
            this.Chart = new RadarChart()
            {
                Entries = new Collection<Microcharts.Entry>
                {
                    // Set Y data
                    new Microcharts.Entry(zero)
                    {
                        Label = "Ydata",
                        ValueLabel = zero.ToString(),
                        Color = SKColor.Parse("#18CC00")
                    },
                    // Set X data
                    new Microcharts.Entry(zero)
                    {
                        Label = "Xdata",
                        ValueLabel = zero.ToString(),
                        Color = SKColor.Parse("#E5001B")
                    },
                    // Set Z data
                    new Microcharts.Entry(zero)
                    {
                        Label = "Zdata",
                        ValueLabel = zero.ToString(),
                        Color = SKColor.Parse("#0018CC")
                    },
                }
            };
        }
    }
}
