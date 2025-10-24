using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace fundamental_c_sharp;

public class Streams
{
    public static async Task DemoAsync()
    {
        Console.WriteLine("Streams ==========================="); 
        var from = new MemoryStream(Encoding.UTF8.GetBytes("Hello World"));
        var result = await new StreamReader(from, Encoding.UTF8).ReadToEndAsync();
        Debug.Assert(result == "Hello World");

        // var to = new MemoryStream(12);
        // to.WriteAsync("Hello")
    }
}