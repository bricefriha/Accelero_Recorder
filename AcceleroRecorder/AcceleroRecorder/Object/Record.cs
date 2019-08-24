using AcceleroRecorder.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private Collection<Frame> frames;
        private RecordViewModel vm;
        private bool isRecording;

        // Static var
        private static Task process;
        private static Stopwatch watch;

        // Encapse
        public Collection<Frame> Frames { get => frames; }
        public RecordViewModel Vm { get => vm; set => vm = value; }

        /// <summary>
        /// Constructor of the Record object
        /// </summary>
        public Record(RecordViewModel vm)
        {
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
            this.frames = new Collection<Frame>();
            this.isRecording = false;
            this.vm = null;
        }

        /// <summary>
        /// Method which start the recording
        /// </summary>
        /// <returns></returns>
        public void Start()
        {
            

            // Change the state attribut
            this.isRecording = true;

            watch.Start();
            // Launch the timer
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    //this.Milliseconds = this.watch.Elapsed.Milliseconds.ToString("000");
                    //this.Minutes = this.watch.Elapsed.Minutes.ToString("00");
                    //this.Seconds = this.watch.Elapsed.Seconds.ToString("00");

                    //Add a frame to the frames
                    this.frames.Add(new Frame(watch.Elapsed.Minutes.ToString("00") + ":" + watch.Elapsed.Seconds.ToString("00") + ":" + watch.Elapsed.Milliseconds.ToString("000"), this.vm.Xdata, this.vm.Ydata, this.vm.Zdata));

                });
                return this.isRecording; // True = Repeat again, False = Stop the timer
            });
        }

        /// <summary>
        /// Method to stop the recording
        /// </summary>
        public void Stop ()
        {
            // Change the state attribut
            this.isRecording = false;

            watch.Stop();
        }
        public void Clear()
        {
            // Delete every single frames
            this.frames.Clear();
        }
    }
}
