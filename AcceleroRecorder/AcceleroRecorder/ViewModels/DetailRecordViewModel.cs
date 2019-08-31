using AcceleroRecorder.Object;
using GsbMobileXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcceleroRecorder.ViewModels
{
    public class DetailRecordViewModel : BaseViewModel
    {
        public DetailRecordViewModel(string filename)
        {
            // Get record data
            Record record = new Record(filename);

            // Set the Title
            this.Title = record.Title;
        }
    }
}
