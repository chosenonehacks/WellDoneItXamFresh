using System.Collections.Generic;
using FreshMvvm;
using Xamarin.Forms;

namespace WellDoneItXamFresh.Helpers
{
    public class ThemedMasterDetailNavigationContainer : FreshMasterDetailNavigationContainer
    {
        List<Models.MenuItem> pageIcons = new List<Models.MenuItem>();

        public void AddPageWithIcon<T>(string title, string icon = "", object data = null) where T : FreshBasePageModel
        {
            base.AddPage<T>(title, data);
            pageIcons.Add(new Models.MenuItem
            {
                Title = title,
                IconSource = icon
            });
        }

        protected override void CreateMenuPage(string menuPageTitle, string menuIcon = null)
        {
            //var stackLayout = new StackLayout();
            

            var listview = new ListView();
            var _menuPage = new ContentPage();
            _menuPage.Title = menuPageTitle;
            _menuPage.BackgroundColor = Color.FromHex("#FFFFFF");

            listview.Header = new StackLayout {
                HeightRequest = 100,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(5, 10, 5, 0),
				BackgroundColor = Color.FromHex("#009688"),
				Children ={
     //               new Label { Text = "Header" },
					//new Label { Text = "Subhead", FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) }
                    
                }
            };

            listview.ItemsSource = pageIcons;

            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetValue(TextCell.TextColorProperty, Color.Black);
            //cell.SetValue(ImageCell.);
            cell.SetBinding(ImageCell.TextProperty, "Title");
            cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");
            

            listview.ItemTemplate = cell;
            listview.ItemSelected += (sender, args) =>
            {
                if (Pages.ContainsKey(((Models.MenuItem)args.SelectedItem).Title))
                {
                    Detail = Pages[((Models.MenuItem)args.SelectedItem).Title];
                }
                IsPresented = false;
            };

            _menuPage.Content = listview;

            var navPage = new NavigationPage(_menuPage) { Title = "Menu" };

            if (!string.IsNullOrEmpty(menuIcon))
                navPage.Icon = menuIcon;
            
            Master = navPage;

        }

        protected override Page CreateContainerPage(Page page)
        {
            //var navigation = new NavigationPage(page) {BackgroundColor = Color.FromHex("#512DA8"), BarTextColor = Color.White };
            var navigation = new NavigationPage(page) { BackgroundColor = Color.FromHex("#009688"), BarTextColor = Color.White, BarBackgroundColor = Color.FromHex("#009688"), Icon = "check.png" };

            return navigation;
        }
    }
}