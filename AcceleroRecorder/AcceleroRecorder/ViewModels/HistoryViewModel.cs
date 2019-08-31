using AcceleroRecorder.Models;
using GsbMobileXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AcceleroRecorder.ViewModels
{
    /// <summary>
    /// The History page ViewModel
    /// </summary>
    public class HistoryViewModel : BaseViewModel
    {
        // Declare an observable collection of records
        private ObservableCollection<RecordItem> records; 
        public ObservableCollection<RecordItem> Records
        {
            get { return records; }
            set
            {
                records = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Constructor of the History page ViewModel 
        /// </summary>
        public HistoryViewModel ()
        {
            // Instanciate the collection of records as a new one
            this.records = new ObservableCollection<RecordItem>();


        }

    }
}
