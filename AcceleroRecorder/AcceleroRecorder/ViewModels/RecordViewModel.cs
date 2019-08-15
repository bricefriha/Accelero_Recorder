using GsbMobileXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcceleroRecorder.ViewModels
{
    /// <summary>
    /// ViewModel of the Record page
    /// </summary>
    public class RecordViewModel : BaseViewModel
    {
        private double xdata;
        public double Xdata
        {
            get { return xdata; }
            set
            {
                xdata = value;
                OnPropertyChanged();
            }
        }
        private double ydata;
        public double Ydata
        {
            get { return ydata; }
            set
            {
                ydata = value;
                OnPropertyChanged();
            }
        }
        private double zdata;
        public double Zdata
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
            this.Title = "Recording";

            // Set accelerometers data up
            this.Xdata = 0;
            this.Ydata = 0;
            this.Zdata = 0;
        }
    }
}
