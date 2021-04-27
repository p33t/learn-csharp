using System;
using System.Threading.Tasks;

namespace fundamental_c_sharp
{
    public static class Disposable
    {

        private class MyDisposable : IDisposable
        {
            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

        private static async Task MyMethod(MyDisposable disposable)
        {
            await Task.Delay(100);
            Console.WriteLine("Is disposed: " + disposable.IsDisposed);
        }

        public static Task Demo()
        {
            Console.WriteLine("Disposable ========================");

            // checking 'using' combination with async operations
            using var disposable = new MyDisposable();
            return MyMethod(disposable); // This needs 'await' to avoid premature dispose
        }
    }
}
