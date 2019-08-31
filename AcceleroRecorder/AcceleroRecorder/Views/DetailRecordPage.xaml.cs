using AcceleroRecorder.ViewModels;
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
    public partial class DetailRecordPage : ContentPage
    {
        public DetailRecordPage(string filename)
        {
            InitializeComponent();

            // Bind the view model
            BindingContext = new DetailRecordViewModel(filename);
        }
    }
}