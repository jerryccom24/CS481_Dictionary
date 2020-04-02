using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;

namespace hw6
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        public string WordInput { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            HttpClient Client = new HttpClient();
            Uri uri = new Uri("https://owlbot.info/api/v2/dictionary/" + WordInput + "?format=json");


            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = uri;


            HttpResponseMessage response = null;
            response = await Client.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                await DisplayAlert("Success","It worked...","OK");
            }
        }
    }
}
