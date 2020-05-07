using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CovidApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            Detail = new NavigationPage(new AboutCovid());

            IsPresented = false;
        }

        private void Btn_NewsFeed_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new News_Feed());
            IsPresented = false;
        }

        private void Btn_Statistics_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Statistics());
            IsPresented = false;
        }

        private void Btn_StatusChecker_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new StatusChecker());
            IsPresented = false;
        }

        private void Btn_Hotlines_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Hotlines());
            IsPresented = false;
        }

        private void Btn_AboutDevelopers_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new AboutDevelopers());
            IsPresented = false;
        }

        private void Btn_AboutCovid_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new AboutCovid());
            IsPresented = false;
        }
    }
}
