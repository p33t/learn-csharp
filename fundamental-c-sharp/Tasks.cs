using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace fundamental_c_sharp
{
    public class Tasks
    {
        public static async Task<string> ThrowAfter(TimeSpan span, string message)
        {
            await Task.Delay(span);
            throw new Exception(message);
        }
        
        public static async Task DemoAsync()
        {
            Console.WriteLine(" Tasks =================================");

            string Finisher(Task<string> t)
            {
                // ContinueWith function needs to cater to failed task
                if (t.IsCompletedSuccessfully) return $"Finished {t.Result}";
                return "BAD";
            }

            var success1 = Task.FromResult("1");
            var continue1 = await success1.ContinueWith(Finisher);
            Debug.Assert(continue1 == "Finished 1");

            var failed2 = Task.FromException<string>(new ApplicationException("Manual exception"));
            var continue2 = await failed2.ContinueWith(Finisher);
            Debug.Assert(continue2 == "BAD");

            // Incomplete offers Exception that is null
            var twoSecs = TimeSpan.FromSeconds(2);
            var delayed3 = Task.Delay(twoSecs).ContinueWith(_ => "3").ContinueWith(Finisher);
            var before3 = DateTime.Now;
            Debug.Assert(delayed3.Exception is null);
            var continue3 = await delayed3;
            var after3 = DateTime.Now;
            Debug.Assert(continue3 == "Finished 3");
            Debug.Assert(TimeSpan.FromMilliseconds(1900) < after3 - before3);

            // Wait() for await within non-async function (?)
            // Exception is still null in the meantime
            // Will ultimately be AggregateException that is a wrapper and could be 'Cancelled'
            var delayed4 = ThrowAfter(twoSecs, "4");
            Debug.Assert(delayed4.Exception is null);
            // delayed4.GetAwaiter() DON'T USE DIRECTLY
            try
            {
                delayed4.Wait();
                throw new Exception("Should not reach here");
            }
            catch (AggregateException e)
            {
                Debug.Assert(e.InnerException?.Message == "4");
            }
            Debug.Assert(delayed4.Exception?.InnerException?.Message == "4");


            // Can still await a completed, failed task
            try
            {
                await failed2;
                throw new Exception("Should not reach here");
            }
            catch (ApplicationException e)
            {
                // expected
            }
            
            // Cancellation
            var cancelTokenSource = new CancellationTokenSource();
            var cancelToken = cancelTokenSource.Token;
            var delayed5 = Task.Delay(twoSecs, cancelToken).ContinueWith(_ => "6", cancelToken);
            Debug.Assert(!delayed5.IsCompleted);
            cancelTokenSource.Cancel();
            Debug.Assert(delayed5.IsCompleted);
            // Exception is still null when cancelled
            Debug.Assert(delayed5.Exception is null);
            
            // Use this to conveniently check and throw a cancellation token (?)
            // cancelToken.ThrowIfCancellationRequested();

            // awaiting a cancelled task results in exception
            try
            {
                await delayed5;
                throw new Exception("Should not reach here");
            }
            catch (TaskCanceledException e)
            {
                // expected
            }

            // Auto cancel after period of time
            var autoCancelSource = new CancellationTokenSource(TimeSpan.FromSeconds(1));
            var delayed6 = Task.Delay(twoSecs, autoCancelSource.Token);
            try
            {
                await delayed6;
                throw new Exception("Should not reach here");
            }
            catch (TaskCanceledException e)
            {
                // expected
            }
            
            Console.WriteLine($"Finished Tasks.Demo()");
        }        
    }
}