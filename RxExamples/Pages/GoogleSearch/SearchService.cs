using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RxExamples.Pages
{
    public static class SearchService
    {
        private static int _attemtNumber = 0;

        public static async Task<List<string>> SearchAsync(
            string searchToken,
            CancellationToken token = default(CancellationToken))
        {
            var results = GetSearchResults();
            await Task.Delay(1000, token);
            return results;
        }

        private static List<string> GetSearchResults()
        {
            _attemtNumber++;
            return Enumerable.Range(1, 10)
                .Select(number => $"Search result_{number} - [attempt {_attemtNumber}]")
                .ToList();
        }
    }
}
