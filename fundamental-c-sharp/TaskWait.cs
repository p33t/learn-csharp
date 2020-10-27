
using System;
using System.Diagnostics;
using System.IO;

namespace fundamental_c_sharp
{
    public static class TaskWait
    {
        public static void Demo()
        {
            Console.WriteLine(" Task Await =================================");
            var task = File.ReadAllTextAsync("fundamental-c-sharp.deps.json");
            task.Wait();
            Console.Write(task.Result);
        }
    }
}