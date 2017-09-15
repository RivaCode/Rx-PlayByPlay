using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RxExamples.Pages
{
    /// <summary>
    /// Interaction logic for DeferRequestPage.xaml
    /// </summary>
    public partial class DeferRequestPage : UserControl
    {
        public DeferRequestPage() => InitializeComponent();

        private async void GetStatusesOnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                BusyInteraction(Visibility.Visible, false);

                var results = await StatusService.GetAsync();
                ResultList.ItemsSource = results.ToList();
            }
            catch
            {
                //
            }
            finally
            {
                BusyInteraction(Visibility.Hidden, true);
            }

            void BusyInteraction(Visibility state, bool isEnabled)
            {
                StatusBtn.IsEnabled = isEnabled;
                Busy.Visibility = state;
                if (isEnabled)
                {
                    StatusBtn.Focus();
                }
            }
        }
    }
}
