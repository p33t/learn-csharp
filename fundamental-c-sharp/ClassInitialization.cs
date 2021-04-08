using System;
using System.Diagnostics;

namespace fundamental_c_sharp
{
    public static class ClassInitialization
    {
        public enum ElevatorDirection
        {
            Up, Down
        }

        public class Parent
        {
            public string? OptString { get; set; }

            public int? OptInt { get; set; }

            public ElevatorDirection? OptDirection { get; set; }
        }

        public class Child : Parent
        {
            public Child()
            {
                OptString = "default-string";
                OptInt = 99;
                OptDirection = ElevatorDirection.Down;
            }
        }

        public static void Demo()
        {
            Console.WriteLine("Class Initialization =======================================");
            var nullInt = new Child
            {
                OptInt = null
            };
            Debug.Assert(null == nullInt.OptInt);

            var nullString = new Child
            {
                OptString = null
            };
            Debug.Assert(null == nullString.OptString);

            var nullDirection = new Child
            {
                OptDirection = null
            };
            Debug.Assert(null == nullDirection.OptDirection);
        }
    }
}
