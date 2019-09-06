using AcceleroRecorder.Object;
using GsbMobileXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcceleroRecorder.ViewModels
{
    public class OptionViewModel : BaseViewModel
    {
        private Record rec;
        public Record Rec
        {
            get { return rec; }
            set
            {
                rec = value;
            }
        }

        private string recordName;
        public string RecordName
        {
            get { return recordName; }
        }

        public OptionViewModel(string filename)
        {
            this.Title = "Options";
            this.rec = new Record(filename);

            // Set the file name by getting it from the record object
            this.recordName = rec.Title;

        }
    }
}
