namespace fundamental_c_sharp
{
    public class Polymorphism
    {
        class Parent
        {
            public string Hello() => "world";
        }

        class Child : Parent
        {
            public new string Hello() => "yellow";
        }

        public static void Demo()
        {
            var parent = new Parent();
            Util.WriteLn($"Expecting 'world': {parent.Hello()}");
            var child = new Child();
            Util.WriteLn($"Expecting 'yellow': {child.Hello()}");
        }
    }
}