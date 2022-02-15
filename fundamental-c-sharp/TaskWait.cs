
using System;
using System.IO;

namespace fundamental_c_sharp
{
    public static class TaskWait
    {
        public static void Demo()
        {
            Console.WriteLine(" Task Await =================================");
            var filePath = "fundamental-c-sharp.deps.json";
            var task = File.ReadAllTextAsync(filePath);
            task.Wait();
            Console.WriteLine($"Loaded {task.Result.Length} chars from file {filePath}");
        }
    }
}