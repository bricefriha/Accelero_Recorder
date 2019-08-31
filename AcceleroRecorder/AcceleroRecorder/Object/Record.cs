using AcceleroRecorder.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AcceleroRecorder.Object
{
    /// <summary>
    /// Class of the object which contain all the recording attributs
    /// </summary>
    public class Record
    {
        // Attributs
        private string title;
        private Collection<Frame> frames;
        private RecordViewModel vm;
        private bool isRecording;

        // Static var
        private static Task process;
        private static Stopwatch watch;

        // Encapse
        public Collection<Frame> Frames { get => frames; }
        public RecordViewModel Vm { get => vm; set => vm = value; }
        public string Title { get => title; set => title = value; }

        /// <summary>
        /// Constructor of the Record object
        /// </summary>
        public Record(RecordViewModel vm)
        {
            DateTime dateNow = DateTime.Now;
            this.title = dateNow.ToString("dd-MM-yyyy") + "_" + dateNow.ToString("HH:mm:ss");//HH:mm:ss
            this.frames = new Collection<Frame>();
            watch = new Stopwatch();
            this.isRecording = false;
            this.vm = vm;
        }

        /// <summary>
        /// Constructor of the Record object without view model
        /// </summary>
        public Record()
        {
            this.title = null;
            this.frames = null;
            this.vm = null;
        }
        /// <summary>
        /// Constructor of the Record object setting it from a Json
        /// </summary>
        public Record(string recordJsonPath)
        {
            try
            {
                // Get the Json text
                string recordJson = File.ReadAllText(recordJsonPath);

                // Deserialize Json to object
                Record myRecord = JsonConvert.DeserializeObject<Record>(recordJson);

                // Set the attributs from  
                this.title = myRecord.Title;
                this.frames = myRecord.Frames;
                this.isRecording = myRecord.isRecording;
                this.vm = myRecord.Vm;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method which start the recording
        /// </summary>
        /// <returns></returns>
        public void Start()
        {
            

            // Change the state attribut
            this.isRecording = true;

            //Start the watch
            watch.Start();

            // Launch the timer
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {

                    //Add a frame to the frames
                    this.frames.Add(new Frame(watch.Elapsed.Minutes.ToString("00") + ":" + watch.Elapsed.Seconds.ToString("00") + ":" + watch.Elapsed.Milliseconds.ToString("000"), this.vm.Xdata, this.vm.Ydata, this.vm.Zdata));

                });
                return this.isRecording; 
            });
        }

        /// <summary>
        /// Method to stop the recording
        /// </summary>
        public void Stop ()
        {
            // Change the state attribut
            this.isRecording = false;

            // Stop the watch
            watch.Stop();

            // Reset the watch
            watch.Reset();
        }
        /// <summary>
        /// Method to clear the Record data
        /// </summary>
        public void Clear()
        {
            // Delete every single frames
            this.frames.Clear();
        }

        /// <summary>
        /// Method to save the record object in Json
        /// </summary>
        public void Save()
        {
            // Ask the user to set the title

            //
            // ToDo later
            //

            // Convert the object to JSON
            string recordJson = JsonConvert.SerializeObject(this);

            // Set the file name
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), this.title + ".json");

            // Verify if the file dont allready exist
            if (!File.Exists(fileName))
            {
                // Write the local file with the JSON with the filename specified
                File.WriteAllText(fileName, recordJson);

            }
            else
            {
                // Declare a new file name variable
                string newFileName = fileName;

                // While the newfile exist
                for (int i = 0; File.Exists(newFileName); i++)
                {
                    // Increment a number at the end
                    newFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), this.title + "_" + i.ToString() + ".json");
                }

                // Write the local file with the JSON with the new filename
                File.WriteAllText(newFileName, recordJson);
            }

        }
    }
}
