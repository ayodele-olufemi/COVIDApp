using CovidApp.Models;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace CovidApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class News_Feed : ContentPage
    {
        public News_Feed()
        {
            InitializeComponent();
        }

        private async void Btn_GetLatestNews_Clicked(object sender, EventArgs e)
        {
            var newsApiClient = new NewsApiClient("a9aa143457d44c2ba57d76347562c998");
            var articlesResponse = await newsApiClient.GetEverythingAsync(new EverythingRequest
            {
                Q = "Covid",
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = new DateTime(2020, 4, 29)
            });
            if (articlesResponse.Status == Statuses.Ok)
            {
                var articles = articlesResponse.Articles;
                NewsList.ItemsSource = articles;
            }
        }

        private void NewsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Article selectedArticle = e.Item as Article;
            Launcher.TryOpenAsync(selectedArticle.Url);
        }
    }
}