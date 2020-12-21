using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace fundamental_c_sharp
{
    public static class TaskExceptions
    {
        static async Task BadOp()
        {
            throw new AggregateException(new Exception("Inner message"));
        }

        public static async Task Demo()
        {
            Console.WriteLine(" Task Exceptions =================================");
            // sanity check
            try
            {
                await BadOp();
            }
            catch (AggregateException ae)
            {
                Debug.Assert(ae.InnerException!.Message == "Inner message");
            }

            // auto unwrap
            try
            {
                await BadOp().ContinueWith(task =>
                {
                    if (task.Exception is AggregateException ae)
                        return Task.FromException(ae.InnerException!);
                    return task;
                });
            }
            catch (Exception ex)
            {
                Debug.Assert(ex.Message == "Inner message");
            }
        }
    }
}
