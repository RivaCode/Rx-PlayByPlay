using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;


namespace RxExamples.Pages
{
    /// <summary>
    /// Interaction logic for GoogleSearchPage.xaml
    /// </summary>
    public partial class GoogleSearchPage : UserControl
    {
        private CancellationTokenSource _cts;
        private DispatcherTimer _throttleTimer;
        private string _lastSearchToken;
        private string _searchCandidate;

        public GoogleSearchPage()
        {
            InitializeComponent();

            SearchBox.TextChanged += OnSearchTextChanged;

            _throttleTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.5)};
            _throttleTimer.Tick += OnThrottleElapsed;
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var candidateSearchTerm = SearchBox.Text;
            if (candidateSearchTerm.Length < 3)
            {
                _throttleTimer.Stop();
                return;
            }

            _searchCandidate = candidateSearchTerm;
            RestartThrottle();
        }

        private async void OnThrottleElapsed(object sender, EventArgs elapsedEventArgs)
        {
            _throttleTimer.Stop();
            if (_lastSearchToken == _searchCandidate)
            {
                return;
            }

            _cts?.Cancel(true);
            _cts = new CancellationTokenSource();
            try
            {
                _lastSearchToken = _searchCandidate;
                var (countries, stamp) = await SearchService.SearchAsync(_lastSearchToken, _cts.Token);
                CountriesList.ItemsSource = countries;
                TimeBlock.Text = stamp.ToString();
            }
            catch
            {
                // ignore
            }
        }

        private void RestartThrottle()
        {
            _throttleTimer.Stop();
            _throttleTimer.Start();
        }
    }
}
