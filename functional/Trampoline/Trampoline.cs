using System.Collections.Immutable;

namespace functional.Trampoline;

public class Trampoline
{
    /// <summary>
    /// Trampoline is a technique to achieve recursion without tail-call optimization (which C# doesn't have)
    /// </summary>
    public static void Demo()
    {
        Console.WriteLine("Trampoline ====================================================");
        // we want to enumerate all the paths from a root node to all the leaves (directed acyclic graph)
        var root = new Node
        {
            Children = new[]
            {
                new Node(),
                new Node
                {
                    Children = new[]
                    {
                        new Node()
                    }
                }
            }
        };


        // Recursive solution
        IEnumerable<ImmutableStack<Node>> FindPaths(Node node, ImmutableStack<Node> pathSoFar)
        {
            var nextPathSoFar = pathSoFar.Push(node);
            return node.Children.Any() ?
                // vertex node
                node.Children.SelectMany(child => FindPaths(child, nextPathSoFar)) :
                // leaf node
                new[] { nextPathSoFar };
        }

        Console.WriteLine("Recursive solution:");
        foreach (var path in FindPaths(root, ImmutableStack<Node>.Empty))
        {
            Console.WriteLine(String.Join('-', path.Reverse().Select(n => n.Id.ToString())));
        }
    }

    private static int _nextNodeId = 1;

    public class Node
    {
        public readonly int Id = _nextNodeId++;
        public IEnumerable<Node> Children = ImmutableArray<Node>.Empty;
    }
}