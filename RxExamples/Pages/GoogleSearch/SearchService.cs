using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RxExamples.Pages
{
    public static class SearchService
    {
        private static int _attemtNumber = 0;
        private static Random _rnd = new Random();

        public static Task<(IEnumerable<Country>, SearchStamp)> SearchAsync(
            string searchToken,
            CancellationToken token = default(CancellationToken))
        {
            _attemtNumber++;
            var lowercaseToken = searchToken.ToLower();
            return Task.Delay(_rnd.Next(100, 200), token)
                .ContinueWith(_ =>
                    (
                    CountryProvider.All.Where(c =>
                        c.Name.ToLower().Contains(lowercaseToken) ||
                        c.Capital.ToLower().Contains(lowercaseToken)
                    ),
                    new SearchStamp(_attemtNumber, searchToken)
                    ), token);
        }
    }

    public class SearchStamp
    {
        private readonly int _attempt;
        private readonly string _term;
        private readonly DateTime _when;

        public SearchStamp(int attempt, string term)
        {
            _attempt = attempt;
            _term = term;
            _when = DateTime.Now;
        }

        public override string ToString() => $"Attempt #{_attempt} was executed at {_when:T} for search token:[{_term}]";
    }
}
