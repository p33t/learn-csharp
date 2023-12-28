using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace fundamental_c_sharp
{
    public class Collections
    {
        public class TestClass
        {
            public IDictionary<string, string> DictProp { get; set; } = new Dictionary<string, string>();

            public IImmutableDictionary<string, string> ImmutDictProp { get; set; } = 
                ImmutableSortedDictionary<string, string>.Empty;
        }
        
        public static void Demo()
        {
            Console.WriteLine("Collections ===========================");

            var colours = new List<string> { "brown", "blue", "grey", "green" };
            var byFirstLetter = colours.GroupBy(s => s[0]).ToDictionary(grp => grp.Key, grp => grp.ToList());
            Trace.Assert(byFirstLetter.ContainsKey('b'));
            Trace.Assert(byFirstLetter.ContainsKey('g'));
            Trace.Assert(!byFirstLetter.ContainsKey('x'));
            Trace.Assert(byFirstLetter['b'].Count == 2);
            Trace.Assert(byFirstLetter['b'].Contains("brown"));
            
            // ImmutableDictionary property initialization caveat =============================================
            var bad = new TestClass
            {
                // This works fine
                DictProp =
                {
                    {"uno", "one"},
                    {"due", "two"}
                },
                
                // This does NOT initialize the field, and compiler is happy
                ImmutDictProp =
                {
                    { "uno", "one" },
                    { "due", "two" }
                }
            };
            Trace.Assert(bad.DictProp.Count == 2);
            Trace.Assert(bad.ImmutDictProp.Count == 0); // <<<< expecting 2 elements
            
            
            var good = new TestClass
            {
                ImmutDictProp = new Dictionary<string, string>
                {
                    { "uno", "one" },
                    { "due", "two" }
                }.ToImmutableSortedDictionary()
            };
            Trace.Assert(good.ImmutDictProp.Count == 2);
            
            // Does not compile
            // good.ImmutDictProp =
            // {
            //     {"uno", "one"}
            // };
            
            
            // don't forget that a sorted dictionary is ordered by key (NOT the insertion order)
            var keyArr = good.ImmutDictProp.Keys.ToArray();
            Trace.Assert(keyArr[0] == "due");
            Trace.Assert(keyArr[1] == "uno");
        }
    }
}