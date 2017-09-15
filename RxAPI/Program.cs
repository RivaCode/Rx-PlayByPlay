using System;
using RxAPI.Observables;

namespace RxAPI
{
    class Program
    {
        #region Example 1
        //static void Main(string[] args)
        //{
        //    Button myBtn = new Button();
        //    myBtn.Click += MyBtnOnClick;
        //}

        //private static void MyBtnOnClick(object sender, EventArgs eventArgs)
        //{
        //    Console.WriteLine("Clicked");
        //}

        #endregion

        #region Example 2
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("Before");
        //    var list = new List<int>(new[] { 10, 20, 30, 40, 50, 60 });
        //    list.ForEach(num => Console.WriteLine(num));
        //    Console.WriteLine("After");

        //    Console.Read();
        //} 
        #endregion

        #region Example 3

        //static void Main(string[] args)
        //{
        //    RunItAsync(OnNext, OnError).GetAwaiter().GetResult();
        //    Console.Read();
        //}

        //private static async Task RunItAsync(Action<string> onNextCallback, Action<Exception> onErrorCallback)
        //{
        //    WebClient client = new WebClient();
        //    try
        //    {
        //        onNextCallback(await client.DownloadStringTaskAsync("http://jsonplaceholder.typicode.com/users/2"));
        //    }
        //    catch (Exception e)
        //    {
        //        onErrorCallback(e);
        //    }
        //}

        //private static void OnNext(string value) => Console.WriteLine($"We got the result: {value}");
        //private static void OnError(Exception e) => Console.WriteLine($":( {e}");

        #endregion

        #region Example 4

        //static void Main(string[] args) => RunItAsync(OnNext, OnError, OnDone).GetAwaiter().GetResult();

        //private static async Task RunItAsync(Action<string> onNextCallback, Action<Exception> onErrorCallback, Action onDoneCallback)
        //{
        //    WebClient client = new WebClient();
        //    try
        //    {
        //        foreach (var id in new[] { 1, 2, 3 })
        //        {
        //            onNextCallback(await client.DownloadStringTaskAsync($"http://jsonplaceholder.typicode.com/users/{id}"));
        //        }
        //        onDoneCallback();
        //    }
        //    catch (Exception e)
        //    {
        //        onErrorCallback(e);
        //    }
        //}


        //private static void OnNext(string value) => Console.WriteLine($"We got the result: {value}");
        //private static void OnError(Exception e) => Console.WriteLine($":( {e}");
        //private static void OnDone() => Console.WriteLine("Hooray!");

        #endregion

        #region Example 5

        //static void Main(string[] args)
        //{
        //    Console.Title = "RX Demo";
        //    ConsoleObserver<int> c = new ConsoleObserver<int>();
        //    NeedData(c.Next, c.Error, c.Complete);
        //    Console.Read();
        //    // ...
        //}

        //public static void NeedData(
        //    Action<int> nextCB, 
        //    Action<Exception> errCB, 
        //    Action completeCB)
        //{
        //    foreach (var num in new[] { 10, 20, 30 })
        //    {
        //        nextCB(num);
        //    }
        //    completeCB();
        //}

        #endregion

        #region Example 6

        //static void Main(string[] args)
        //{
        //    var data = NeedData();
        //    //..
        //}

        //public static IEnumerable<int> NeedData() => new[] { 10, 20, 30 };

        #endregion

        #region Example 7

        //static void Main(string[] args)
        //{
        //    Console.Title = "RX Demo";
        //    ConsoleObserver observer = new ConsoleObserver();
        //    NeedData(observer);
        //    Console.Read();
        //}

        //public static void NeedData(IObserver<int> observer)
        //{
        //    foreach (var num in new[] { 10, 20, 30 })
        //    {
        //        observer.OnNext(num);
        //    }
        //    observer.OnCompleted();
        //}

        #endregion

        #region Example 8

        //static void Main(string[] args)
        //{
        //    Console.Title = "RX Demo";
        //    ConsoleObserver observer = new ConsoleObserver();
        //    Observable observable = new Observable(new [] {10, 20, 30});
        //    observable.Subscribe(observer);
        //    Console.Read();
        //}

        #endregion

        #region Example 8

        //static void Main(string[] args)
        //{
        //    Console.Title = "RX Demo";
        //    ConsoleObserver observer = new ConsoleObserver();
        //    IObservable<int> observable = 
        //            new[] { 10, 20, 30 }
        //                .ToObservable();
        //    observable.Subscribe(observer);
        //    Console.Read();
        //}

        #endregion

        #region Example 9

        //static void Main(string[] args)
        //{
        //    Console.Title = "RX Demo";
        //    ConsoleObserver co = new ConsoleObserver();
        //    IObservable<int> obs =
        //        new[] { 10, 20, 30 }
        //            .ToObservable()
        //            .Where(n => n > 10);
        //    obs.Subscribe(co);
        //    Console.Read();
        //}

        #endregion

        #region Example 10

        static void Main(string[] _)
        {
            Console.Title = "RX Demo";
            ConsoleObserver co = new ConsoleObserver();
            IObservable<int> obs =
                new[] { 10, 20, 30 }
                    .ToObservable()
                    .Where(n => n > 10)
                    .Select(n => n * 10);
            obs.Subscribe(co);
            Console.Read();
        }

        #endregion
    }
}
