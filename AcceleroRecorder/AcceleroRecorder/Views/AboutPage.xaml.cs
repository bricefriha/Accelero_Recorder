using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AcceleroRecorder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            //
            // ToDo: Make a pull request in the Xamarin Repo to add a OnClick event on the frames (https://github.com/xamarin/XamarinComponents/issues)
            //
            // On click event Frames
            var tapTwitter = new TapGestureRecognizer();
            tapTwitter.Tapped += async (sender, e) =>
            {
                // Change a frame's states when we click on it
                await FrameBehaviour(sender);

                // Open the twitter app to the Brice's profile
                Device.OpenUri(new Uri("twitter://user?user_id=1067124325822668800"));
            };

            // Add the gesture to the frame
            frameTwitter.GestureRecognizers.Add(tapTwitter);

            // On click event Frames
            var tapGithub = new TapGestureRecognizer();
            tapGithub.Tapped += async (sender, e) =>
            {
                // Change a frame's states when we click on it
                await FrameBehaviour(sender);

                // Open the github repository
                Device.OpenUri(new Uri("https://github.com/bricefriha/Accelero_Recorder"));


            };

            // Add the gesture to the frame
            frameGithub.GestureRecognizers.Add(tapGithub);

        }
        /// <summary>
        /// Method alowing to change a frame's states when we click on it
        /// </summary>
        /// <param name="sender"> the frame as sender </param>
        /// <returns></returns>
        private async Task FrameBehaviour(object sender)
        {
            try
            {
                // Fetch the Frame object
                var frame = (Frame)sender;

                // Reduice the opacity of the frame
                await frame.FadeTo(0.5, 2000);

                // Wait...
                await Task.Delay(100);

                // Reset the opacity of the frame
                await frame.FadeTo(1, 2000);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}