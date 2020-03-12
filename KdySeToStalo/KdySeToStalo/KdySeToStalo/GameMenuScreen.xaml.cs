using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KdySeToStalo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameMenuScreen : ContentPage
    {
        public GameMenuScreen()
        {
            InitializeComponent();
        }
        private async void CompareGameStartBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoSeStaloDrivGameScreen());
        }
        private async void KdySeToStaloStartBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KdySeToStaloGameScreen());
        }
    }
}