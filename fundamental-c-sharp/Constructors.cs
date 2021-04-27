using System;

namespace fundamental_c_sharp
{
    class Parent
    {
        public Parent()
        {
            Title = this.GetType().Name;
        }

        public string Title { get; set; }
    }

    class Child : Parent
    {
    }

    public class Constructors
    {
        public static void Demo()
        {
            Console.WriteLine("Expecting 'Parent': " + new Parent().Title);
            Console.WriteLine("Expecting 'Child': " + new Child().Title);
        }
    }
}