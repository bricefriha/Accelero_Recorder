using AcceleroRecorder.Models;
using AcceleroRecorder.Object;
using GsbMobileXamarin.ViewModels;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AcceleroRecorder.ViewModels
{
    public class DetailRecordViewModel : BaseViewModel
    {
        /// <summary>
        /// the Average Frame
        /// </summary>
        private FrameItem averageFrame;
        public FrameItem AverageFrame
        {
            get { return averageFrame; }
            set
            {
                averageFrame = value;
                OnPropertyChanged();
            }
        }
        private int chartSize;
        public int ChartSize
        {
            get { return chartSize; }
            set
            {
                chartSize = value;
                OnPropertyChanged();
            }
        }
        private DonutChart chartAverage;
        public DonutChart ChartAverage
        {
            get { return chartAverage; }
            set
            {
                chartAverage = value;
                OnPropertyChanged();
            }
        }
        private LineChart chartZdata;
        public LineChart ChartZdata
        {
            get { return chartZdata; }
            set
            {
                chartZdata = value;
                OnPropertyChanged();
            }
        }
        private LineChart chartXdata;
        public LineChart ChartXdata
        {
            get { return chartXdata; }
            set
            {
                chartXdata = value;
                OnPropertyChanged();
            }
        }
        private LineChart chartYdata;
        public LineChart ChartYdata
        {
            get { return chartYdata; }
            set
            {
                chartYdata = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<FrameItem> frameItems;
        public ObservableCollection<FrameItem> FrameItems
        {
            get
            {
                return frameItems;
            }

            set
            {
                frameItems = value;
            }
        }

        private string duration;
        public string Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }

        public DetailRecordViewModel(string filename)
        {
            this.FrameItems = new ObservableCollection<FrameItem>();

            // Get record data
            Record record = new Record(filename);

            // Set the Title
            this.Title = record.Title;


            float totalX = 0;
            float totalY = 0;
            float totalZ = 0;

            // Define data entries
            Collection<Microcharts.Entry> xEntries = new Collection<Microcharts.Entry>();
            Collection<Microcharts.Entry> yEntries = new Collection<Microcharts.Entry>();
            Collection<Microcharts.Entry> zEntries = new Collection<Microcharts.Entry>();
            // For each Frames
            foreach (Frame frame in record.Frames)
            {
                // Get all the Frames
                this.FrameItems.Add(new FrameItem() {
                    Time = frame.Time,
                    XData = frame.XData,
                    YData = frame.YData,
                    ZData = frame.ZData,
                });

                // Add the XData to its entries
                xEntries.Add(new Microcharts.Entry(frame.XData)
                {
                    Label = frame.Time,
                    ValueLabel = frame.XData.ToString(),
                    Color = SKColor.Parse("#18CC00")
                });
                //
                // Add the YData to its entries
                yEntries.Add(new Microcharts.Entry(frame.YData)
                {
                    Label = frame.Time,
                    ValueLabel = frame.YData.ToString(),
                    Color = SKColor.Parse("#E5001B")
                });
                //
                // Add the ZData to its entries
                zEntries.Add(new Microcharts.Entry(frame.ZData)
                {
                    Label = frame.Time,
                    ValueLabel = frame.ZData.ToString(),
                    Color = SKColor.Parse("#0018CC")
                });

                // Add the xdata
                totalX += frame.XData;
                totalZ += frame.ZData;
                totalY += frame.YData;
            }

            // Get the total of frames
            int countFrames = record.Frames.Count;

            // Set the chart size
            this.chartSize = countFrames * 100;

            // Set Average value
            this.AverageFrame = new FrameItem() {
                XData = totalX / countFrames,
                YData = totalY / countFrames,
                ZData = totalZ / countFrames,
            };

            // Get the last frame index
            int lastIndex = countFrames - 1;

            // Get the duration 
            this.Duration = record.Frames[lastIndex].Time;

            // Define the Average charts
            this.ChartAverage = new DonutChart()
            {
                Entries = new Collection<Microcharts.Entry>
                 {
                     new Microcharts.Entry(this.AverageFrame.XData)
                     {
                         Label = "X data",
                         ValueLabel = this.AverageFrame.XData.ToString(),
                         Color = SKColor.Parse("#18CC00")
                     },
                     new Microcharts.Entry(this.AverageFrame.YData)
                     {
                         Label = "Y data",
                         ValueLabel = this.AverageFrame.YData.ToString(),
                         Color = SKColor.Parse("#E5001B")
                     },
                     new Microcharts.Entry(this.AverageFrame.YData)
                     {
                         Label = "Z data",
                         ValueLabel = this.AverageFrame.YData.ToString(),
                         Color = SKColor.Parse("#0018CC")
                     },
                }
            };
            //
            // Define the XData charts
            this.ChartXdata = new LineChart()
            {
                Entries = xEntries,
            };
            //
            // Define the YData charts
            this.ChartYdata = new LineChart()
            {
                Entries = yEntries,
            };
            //
            // Define the ZData charts
            this.ChartZdata = new LineChart()
            {
                Entries = zEntries,
            };
        }
    }
}
