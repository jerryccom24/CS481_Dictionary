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
        //public string type ="";
        //public string desc ="";
        //public string example="";

        public MainPage()
        {
            BindingContext = this;
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

            //---
            
            try
            {
                response = await Client.SendAsync(request);
            }
            catch (Exception ex)
            {
                //if it fails, displays an alert to user and ends the function
                await DisplayAlert("Connection error", "Sorry, something went wrong! Make sure you are connected to the Internet", "OK");
                return;
            }
            //---
            DictionaryItem[] Data = null;


            if(response.IsSuccessStatusCode)
            {
                //await DisplayAlert("Success","It worked...","OK");

                string content = await response.Content.ReadAsStringAsync();

                try
                {
                    Data = DictionaryItem.FromJson(content);
                }
                catch (Exception ex)
                {
                    //if json is bad, displays error message and returns the function
                    await DisplayAlert("Data error", "Something went wrong while retrieving data. Please try again!", "OK");
                    return;
                }
                


                for (int i = 0; i < Data.Length; i++)
                {
                    string type1 = "";
                    string desc1 = "";
                    string example1 = "";
                    //ifs are needed because some fields may be null
                    if (Data[i].Type != null)
                    {
                        type1 = Data[i].Type;
                        this.type.Text = type1;
                        
                    }
                    if (Data[i].Definition != null)
                    {
                        desc1 = Data[i].Definition;
                        this.desc.Text = desc1;
                    }
                    if (Data[i].Example != null)
                    {
                        example1 = Data[i].Example;
                        this.example.Text = example1;
                    }

                }

            }
        }
    }
}
