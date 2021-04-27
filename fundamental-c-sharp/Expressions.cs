using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace fundamental_c_sharp
{
    public static class Expressions
    {
        // public static Expression<Func<FlagHost, bool>> IsFlaggedExpression => host => host.FlaggedAt != null;
        //
        // public class FlagHost
        // {
        //     public Guid Id { get; set; } = Guid.NewGuid();
        //     public DateTime? FlaggedAt { get; set; }
        //
        //     public bool IsFlagged => IsFlaggedExpression.Compile().Invoke(this);
        // }
        //
        //
        public static void Demo()
        {
            Console.WriteLine("Expressions (Linq) ===================");
            Trace.Assert(true);
            var elems = new List<int>{1,2,3,5,8,13};

            Expression<Func<int, bool>> predicate = e => true;
            
            Trace.Assert(6 == elems.Count(predicate.Compile()));
            
            // Need predicate builder: http://www.albahari.com/nutshell/predicatebuilder.aspx
            //  Is in LINQKit.. will try without

            // var hosts = new[] { new FlagHost{FlaggedAt = }, };

            // predicate = e => e < 10 && predicate.Body;
            // predicate = Expression.Add(predicate, predicate);
        }
    }
}