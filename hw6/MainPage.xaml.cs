using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace hw6
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        public string WordInput { get; set; }// Our Entry value for the word

        public MainPage()
        {
            BindingContext = this;
            InitializeComponent();

            

        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            HttpClient Client = new HttpClient();//Our Client for utilizing http

            //Basically, our link, We need to inject the Word that the user wants to search in the dictionary into the URI
            Uri uri = new Uri("https://owlbot.info/api/v2/dictionary/" + WordInput + "?format=json");

            //Our message object that will be used with our uri to get a response
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = uri; //assign uri to the HTTP request message


            HttpResponseMessage response = null;// set response equal to null for now.

            
            
            try
            {
                response = await Client.SendAsync(request);//send our request with the client and we will get the response here
            }
            catch (Exception ex)
            {
                //otherwise if this fails, then the user may not be connected to the interent, therefore return
                await DisplayAlert("Oops!", "You are not connected to the internet", "OK");
                return;
            }

            //needs to be array in the case that Multiple objects are returned. However in our case we will only need to access Data[0] for only 1 definition
            DictionaryItem[] Data = null;//this wil be the data returned from our Http request as a DictionaryItem object (array of them)

            //If our response indicated a success status code, e.g in the rage of (200-299), or (not an error code)
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                try
                {
                    Data = DictionaryItem.FromJson(json); //Transfers all of that unreadable Json craziness into an nice object that we can access
                }
                catch (Exception ex)
                {
                    //otherwise Json could not be transferred
                    await DisplayAlert("Oops!", "There was an issue with your search. Please try again.", "OK");
                    return;
                }

                
                
                   
                //string holder variables 
                string t = ""; //type
                string d = ""; //definition
                string eg = ""; //example



                //Check to make sure data was returned
                if (Data[0].Type != null)
                {
                    t = Data[0].Type;
                    this.type.Text = t;
                }
                else
                    this.type.Text = "N/A";//Make NA in the case that there is no data for the field

                //Check to make sure data was returned 
                if (Data[0].Definition != null)
                {
                    d = Data[0].Definition;
                    this.definition.Text = d;
                }
                else
                    this.definition.Text = "N/A";//Make NA in the case that there is no data for the field

                //Check to make sure data was returned for e.g.
                if (Data[0].Example != null)
                {
                    eg = Data[0].Example;
                    this.example.Text = eg;
                }
                else
                    this.example.Text = "N/A";//Make NA in the case that there is no data for the field


                
            }
        }

        //Function will use Xamarin Essentials to easily check network access state
        //It will display the appropriate message depending on state
        // Sourced/Learned this from https://docs.microsoft.com/en-us/xamarin/essentials/connectivity?tabs=android
        void Check_Connection(object sender, EventArgs e)
        {
            var ConnectionState = Connectivity.NetworkAccess;

            if (ConnectionState == NetworkAccess.Internet)
            {
                DisplayAlert("Alert", "You are connected to the Internet", "Ok");
            }
            else
            {
                DisplayAlert("Alert", "You are NOT connected to the Internet", "Ok");
            }
        }
    }
}
