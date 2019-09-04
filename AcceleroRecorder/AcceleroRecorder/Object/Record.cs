using AcceleroRecorder.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AcceleroRecorder.Object
{
    /// <summary>
    /// Class of the object which contain all the recording attributs
    /// </summary>
    public class Record
    {
        // Set speed delay for monitoring changes.
        private static SensorSpeed speed = SensorSpeed.UI;
        // Attributs
        private string title;
        private Collection<Frame> frames;
        private RecordViewModel vm;
        private bool isRecording;

        // Static var
        private static Task process;
        private static Stopwatch watch;

        // Encapse
        public Collection<Frame> Frames { get => frames; set => frames = value; }
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
                //this.frames = new Collection<Frame>();//myRecord.Frames;
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
            // Start the accelerometer
            ToggleAccelerometer();

            // Make the bouton Squary
            this.vm.BtnCorner = 10;
            
            // Change the state attribut
            this.isRecording = true;

            //Start the watch
            watch.Start();

            // Launch the timer
            Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    // Update the timer
                    this.vm.Milliseconds = watch.Elapsed.Milliseconds.ToString("000");
                    //this.vm.Minutes = watch.Elapsed.Minutes.ToString("00");
                    this.vm.Seconds = watch.Elapsed.Seconds.ToString("00");

                    //Add a frame to the frames
                    this.frames.Add(new Frame(/*watch.Elapsed.Minutes.ToString("00") + ":" +*/ watch.Elapsed.Seconds.ToString("00") + ":" + watch.Elapsed.Milliseconds.ToString("000"), this.vm.Xdata, this.vm.Ydata, this.vm.Zdata));

                    // Limited the timer at 30 seconds
                    if (watch.Elapsed.Seconds >= 30)
                        this.Stop();

                });
                return this.isRecording; 
            });
        }

        /// <summary>
        /// Method to stop the recording
        /// </summary>
        public void Stop ()
        {
            // Stop the accelerometer
            ToggleAccelerometer();

            // Change the state attribut
            this.isRecording = false;

            // Reset the watch
            watch.Reset();

            // Make the bouton rounded again
            this.vm.BtnCorner = 100;

            // Save the data
            this.Save();
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
        private void Save()
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
        /// <summary>
        /// Method allowing to Start or Stop the accelerometer
        /// </summary>
        private void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                {
                    // Stop the Accelerometer
                    Accelerometer.Stop();

                    // Clear data on the screen
                    this.vm.ClearValuesChart();
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
    }
}
