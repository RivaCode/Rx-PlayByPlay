using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace RxExamples.Pages
{
    public static class StatusService
    {
        public static async Task<IEnumerable<string>> GetAsync()
        {
            return await Enumerable.Range(1, 6).ToObservable()
                .Select(id =>
                    Observable.Defer(
                            () => GetStatusAsync(id).ToObservable()
                        )
                        .Retry(1 /*more time*/)
                        .Catch(Observable.Return($"{id} didn't make it"))
                )
                .Merge(4 /*at a time*/)
                .Aggregate(
                    Enumerable.Empty<string>(),
                    (acc, x) => acc.Concat(new[] {x}));
        }

        private static async Task<string> GetStatusAsync(int id)
        {
            string FormatTimeNow()
            {
                return DateTime.Now.TimeOfDay.ToString("hh':'mm':'ss");
            }

            var result = $"ID:{id} - request Time:[{FormatTimeNow()}]";
            await Task.Delay(1000);

            if (id == 3)
            {
                throw new Exception(":(");
            }
            return $"{result} - response Time:[{FormatTimeNow()}]";
        }
    }
}
