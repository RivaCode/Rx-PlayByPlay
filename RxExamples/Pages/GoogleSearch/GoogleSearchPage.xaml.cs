using System;
using System.Reactive.Linq;
using System.Windows.Controls;

namespace RxExamples.Pages
{
    /// <summary>
    /// Interaction logic for GoogleSearchPage.xaml
    /// </summary>
    public partial class GoogleSearchPage : UserControl
    {
        public GoogleSearchPage()
        {
            InitializeComponent();
            Observable.FromEventPattern(SearchBox, nameof(SearchBox.TextChanged))
                .Select(_ => SearchBox.Text)
                .Where(newSearchToken => newSearchToken.Length > 2)
                .Throttle(TimeSpan.FromSeconds(0.5))
                .DistinctUntilChanged()
                .Select(searchToken => SearchService.SearchAsync(searchToken))
                .Switch()
                .ObserveOnDispatcher()
                .Subscribe(result =>
                {
                    var (countries, stamp) = result;
                    CountriesList.ItemsSource = countries;
                    TimeBlock.Text = stamp.ToString();
                });
        }
    }
}
