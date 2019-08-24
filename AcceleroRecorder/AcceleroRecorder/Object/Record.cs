using AcceleroRecorder.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

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

        // Encapse
        public Collection<Frame> Frames { get => frames; }
        public RecordViewModel Vm { get => vm; set => vm = value; }

        /// <summary>
        /// Constructor of the Record object
        /// </summary>
        public Record(RecordViewModel vm)
        {
            this.frames = new Collection<Frame>();
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

            // Start the process
            process = Task.Factory.StartNew(() => {

                try
                {
                    while (this.isRecording)
                    {
                        // Add a frame to the frames
                        this.frames.Add(new Frame(DateTime.Now.ToString("mm:ss:fff"), this.vm.Xdata, this.vm.Ydata, this.vm.Zdata));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            });

            //Task task = Task.Factory.StartNew(() =>
            //{

            //});

            //// Finish the process
            //task.ContinueWith(t =>
            //{
            //    // Change the state attribut again
            //    this.isRecording = false;
            //});
            
            // Return the state of the record in real time
            //return this.isRecording;
        }
        private void Stop ()
        {
            // Change the state attribut
            this.isRecording = false;
        }
    }
}
