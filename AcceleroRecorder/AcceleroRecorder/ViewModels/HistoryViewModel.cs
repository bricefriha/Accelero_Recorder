using AcceleroRecorder.Models;
using AcceleroRecorder.Object;
using GsbMobileXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace AcceleroRecorder.ViewModels
{
    /// <summary>
    /// The History page ViewModel
    /// </summary>
    public class HistoryViewModel : BaseViewModel
    {
        private bool isFilled;
        public bool IsFilled
        {
            get { return isFilled; }
            set
            {
                isFilled = value;
                OnPropertyChanged();
            }
        }

        public bool IsNotFilled
        {
            get { return !isFilled; }
        }

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
            // Set the title
            this.Title = "History";

            // Fill the record collection
            FillRecords();
            
        }

        private void FillRecords()
        {
            // Instanciate the collection of records as a new one
            this.records = new ObservableCollection<RecordItem>();

            // Getting all .json files
            string[] filenames = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "*.json");

            // Define an index
            int i = 0;

            // If some record are getted
            if (filenames.Length > 0)
            {
                // Claimed it
                this.isFilled = true;
            }
            else
            {
                // unclaimed it
                this.isFilled = false;
            }
            // Scaning all filnames
            foreach (string file in filenames)
            {
                // Get the record object
                Record record = new Record(file);

                // Add the file name to the Collection
                this.records.Add(
                    new RecordItem
                    {
                        Id = i++,
                        Title = record.Title,
                        Filename = file,
                    }) ;
            }
        }

    }
}
