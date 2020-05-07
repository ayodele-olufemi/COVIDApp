using CovidApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Statistics : ContentPage
    {
        public Statistics()
        {
            InitializeComponent();
        }

        private async void Btn_Refresh_Clicked(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            string url_World = "https://corona.lmao.ninja/v2/all";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url_World);
                response.EnsureSuccessStatusCode();
                string responseString = await response.Content.ReadAsStringAsync();
                dynamic responseJSON = JsonConvert.DeserializeObject(responseString);
                JObject responseArray = JObject.Parse(responseString);                
                TimeSpan time = TimeSpan.FromMilliseconds(double.Parse((string)responseArray["updated"]));
                DateTime updatedDate = new DateTime(1970, 1, 1) + time;
                Updated.Text = updatedDate.ToString();
                AffectedCountries.Text = String.Format("{0:n0}", (double)responseArray["affectedCountries"]);
                Cases.Text = String.Format("{0:n0}", (double)responseArray["cases"]);
                CasesToday.Text = String.Format("{0:n0}", (double)responseArray["todayCases"]);
                CasesRecovered.Text = String.Format("{0:n0}", (double)responseArray["recovered"]);
                Deaths.Text = String.Format("{0:n0}", (double)responseArray["deaths"]);
                DeathsToday.Text = String.Format("{0:n0}", (double)responseArray["todayDeaths"]);
            }
            catch(HttpRequestException)
            {
                await DisplayAlert("Error!", "Site is currently unavailable", "OK");
            }
        }

        private async void Btn_RefreshC_Clicked(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            string url_CountriesData = "https://corona.lmao.ninja/v2/countries?sort=country";

            try
            {
                HttpResponseMessage response1 = await client.GetAsync(url_CountriesData);
                string responseString1 = await response1.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<Countries>>(responseString1);
                List<Countries> countries = new List<Countries>();
                foreach (var item in model)
                {
                    countries.Add(new Countries
                    {
                        Updated = item.Updated,
                        Country = item.Country,
                        CountryInfo = new CountryInfo()
                        {
                            Flag = item.CountryInfo.Flag
                        },
                        Cases = item.Cases,
                        Deaths = item.Deaths,
                        TodayCases = item.TodayCases,
                        Recovered = item.Recovered,
                        Continent = item.Continent,
                        TodayDeaths = item.TodayDeaths
                    });
                }
                countryList.ItemsSource = countries.OrderBy(p => p.Country).Select(a => a.Country).ToArray();
                countryList.IsVisible = true;
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Error!", "Site is currently unavailable", "OK");
            }
        }

        private async void countryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SL1.IsVisible = true;
            SL2.IsVisible = true;

            HttpClient client = new HttpClient();
            string url_CountriesData = "https://corona.lmao.ninja/v2/countries?sort=country";
            try
            {
                HttpResponseMessage response1 = await client.GetAsync(url_CountriesData);
                string responseString1 = await response1.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<Countries>>(responseString1);
                List<Countries> countries = new List<Countries>();
                foreach (var item in model)
                {
                    countries.Add(new Countries
                    {
                        Updated = item.Updated,
                        Country = item.Country,
                        CountryInfo = new CountryInfo()
                        {
                            Flag = item.CountryInfo.Flag
                        },
                        Cases = item.Cases,
                        Deaths = item.Deaths,
                        TodayCases = item.TodayCases,
                        Recovered = item.Recovered,
                        Continent = item.Continent,
                        TodayDeaths = item.TodayDeaths
                    });
                }
                var selectedCountry = countryList.SelectedItem.ToString();
                
                CasesC.Text = String.Format("{0:n0}", (double)countries.Where(p => p.Country == selectedCountry).Select(g => g.Cases).FirstOrDefault());
                countryFlag.Source = new UriImageSource
                {
                    Uri = new Uri((string)countries.Where(p => p.Country == selectedCountry).Select(g => g.CountryInfo.Flag).FirstOrDefault()),
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(5,0,0,0)
                };
                TimeSpan time = TimeSpan.FromMilliseconds(double.Parse(countries.Where(p => p.Country == selectedCountry).Select(g => g.Updated).FirstOrDefault().ToString()));
                DateTime updatedDate = new DateTime(1970, 1, 1) + time;
                UpdatedC.Text = updatedDate.ToString();
                CasesTodayC.Text = String.Format("{0:n0}", (double)countries.Where(p => p.Country == selectedCountry).Select(g => g.TodayCases).FirstOrDefault());
                CasesRecoveredC.Text = String.Format("{0:n0}", (double)countries.Where(p => p.Country == selectedCountry).Select(g => g.Recovered).FirstOrDefault());
                DeathsC.Text = String.Format("{0:n0}", (double)countries.Where(p => p.Country == selectedCountry).Select(g => g.Deaths).FirstOrDefault());
                DeathsTodayC.Text = String.Format("{0:n0}", (double)countries.Where(p => p.Country == selectedCountry).Select(g => g.TodayDeaths).FirstOrDefault());
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Error!", "Site is currently unavailable", "OK");
            }
        }
    }
}