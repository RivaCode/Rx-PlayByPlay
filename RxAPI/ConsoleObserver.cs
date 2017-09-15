using System;

namespace RxAPI
{
    public class ConsoleObserver : IObserver<int>
    {
        public void OnNext(int data) => Console.WriteLine(data);

        public void OnError(Exception err)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(err);
            Console.ResetColor();
        }

        public void OnCompleted()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("done");
            Console.ResetColor();
        }
    }
}
