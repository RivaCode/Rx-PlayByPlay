using System.Windows.Controls;

namespace RxExamples.Pages
{
    /// <summary>
    /// Interaction logic for GoogleSearchPage.xaml
    /// </summary>
    public partial class GoogleSearchPage : UserControl
    {
        private string _lastSearchToken;

        public GoogleSearchPage()
        {
            InitializeComponent();

            SearchBox.TextChanged += OnSearchTextChanged;
        }

        private async void OnSearchTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var candidateSearchTerm = SearchBox.Text;
            if (candidateSearchTerm.Length < 3)
            {
                return;
            }

            if (_lastSearchToken == candidateSearchTerm)
            {
                return;
            }

            try
            {
                _lastSearchToken = candidateSearchTerm;
                var (countries, stamp) = await SearchService.SearchAsync(_lastSearchToken);
                CountriesList.ItemsSource = countries;
                TimeBlock.Text = stamp.ToString();
            }
            catch
            {
                // ignore
            }
        }
    }
}
