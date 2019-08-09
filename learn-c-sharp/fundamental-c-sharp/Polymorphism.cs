namespace fundamental_c_sharp
{
    public class Polymorphism
    {
        class Parent
        {
            public string Hello() => "parent1";

            public virtual string HelloV() => "parent2";
        }

        class Child : Parent
        {
            public new string Hello() => "child1";

            public override string HelloV() => "child2";
        }

        public static void Demo()
        {
            var parent = new Parent();
            Util.WriteLn($"Expecting 'parent1': {parent.Hello()}");
            var child = new Child();
            Util.WriteLn($"Expecting 'child1': {child.Hello()}");
            Util.WriteLn($"Expecting 'child2': {child.HelloV()}");
            
            // pretty whacky effect of 'new'
            parent = child;
            Util.WriteLn($"Expecting 'parent1' due to 'hiding' polymorphism: {parent.Hello()}");
            Util.WriteLn($"Expecting 'child2' due to 'override' polymorphism: {parent.HelloV()}");
        }
    }
}