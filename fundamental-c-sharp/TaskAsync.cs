using System.Threading.Tasks;

namespace fundamental_c_sharp
{
    public class TaskAsync
    {
        static Task<string> sansAsync()
        {
            return Task.Delay(1000)
                .ContinueWith(_ => "sansAsync");
        }

        static async Task<string> withAsync()
        {
            await Task.Delay(1000);
            return "withAsync";
        }

        static Task<string> unstarted()
        {
            return new Task<string>(() =>
            {
                Util.WriteLn("'unstarted' has started");
                // Might be causing problems.  Try later
                // Task.Delay(1000).Wait();
                return "unstarted";
            });
        }
        
        public static async Task Demo()
        {
            Util.WriteLn("Creating sansAsync()");
            var sans = sansAsync().ContinueWith(t => Util.WriteLn($"1) Finished {t.Result}"));
            Util.WriteLn("Delay 1");
            await Task.Delay(1100);
            
            Util.WriteLn("Creating withAsync()");
            var with = withAsync().ContinueWith(t => Util.WriteLn($"2) Finished {t.Result}"));
            Util.WriteLn("Delay 2");
            await Task.Delay(1100);
            
            Util.WriteLn("Creating unstarted()");
            var un = unstarted().ContinueWith(t => Util.WriteLn($"3) Finished {t.Result}"));
            Util.WriteLn("Delay 3");
            await Task.Delay(1100);
            
            Util.WriteLn("Manually starting 'un' NOOOOOT... don't know how to do this.");
            
            // Exception: Start may not be called on a continuation task.
            //un.Start();
            
            // Never Returns
            // await un;

            // Doesn't work
            // Task.Run(() => un);

            Util.WriteLn("Delay 4");
            await Task.Delay(1100);
            
            Util.WriteLn("TaskAsync finished");
        }
    }
}