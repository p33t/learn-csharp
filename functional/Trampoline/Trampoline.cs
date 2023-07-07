using System.Collections;
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

        // increase depth. Only Trampoline solution can handle this
        // root = new Node();
        // for (int i = 0; i < 1_000_000; i++)
        // {
        //     root = new Node
        //     {
        //         Children = new[] { root }
        //     };
        // }


        // Recursive solution
        IEnumerable<ImmutableStack<Node>> FindPaths(Node node, ImmutableStack<Node> pathSoFar)
        {
            var nextPathSoFar = pathSoFar.Push(node);
            return node.Children.Any()
                // vertex node
                ? node.Children.SelectMany(child => FindPaths(child, nextPathSoFar))
                // leaf node
                : new[] { nextPathSoFar };
        }

        Console.WriteLine("Recursive solution:");
        OutputPaths(FindPaths(root, ImmutableStack<Node>.Empty));

        // Trampoline (sort of)
        // Will have another dimension; trampoline will be used to find the 'next' element in an iterator.
        // State must be maintained between elements.

        (ImmutableStack<Args>, ImmutableStack<Node>?) CyclePath(ImmutableStack<Args> walkState)
        {
            if (walkState.IsEmpty)
            {
                throw new ArgumentException("WalkState should not be empty", nameof(walkState));
            }

            var head = walkState.Peek();
            if (head.IsLeaf)
            {
                using var disposeHead = head;
                return (walkState.Pop(), head.Path);
            }

            if (head.RemainingPeers.MoveNext())
            {
                // more children left
                var nextNode = head.RemainingPeers.Current;
                var nextArg = new Args(nextNode, head.Path); // will get disposed later
                var nextWalkState = walkState.Push(nextArg);

                // response indicates no path on this round but need to bounce again
                return (nextWalkState, null);
            }

            {
                // no more children
                using var disposeHead = head;
                return (walkState.Pop(), null);
            }
        }

        IEnumerable<ImmutableStack<Node>> FindPaths2()
        {
            using var rootArgs = new Args(root, ImmutableStack<Node>.Empty);
            var walkState = ImmutableStack<Args>.Empty.Push(rootArgs);
            while (walkState.Any())
            {
                // still walking the tree
                var (nextWalkState, path) = CyclePath(walkState);
                walkState = nextWalkState;
                if (path != null)
                {
                    // found a leaf node
                    yield return path;
                }
            }
        }

        Console.WriteLine("Trampoline solution:");
        OutputPaths(FindPaths2());
    }

    public class Args : IDisposable
    {
        public readonly ImmutableStack<Node> Path;
        public readonly IEnumerator<Node> RemainingPeers;
        public readonly bool IsLeaf;

        public Args(Node node, ImmutableStack<Node> parentPath)
        {
            Path = parentPath.Push(node);
            RemainingPeers = node.Children.GetEnumerator();
            IsLeaf = !node.Children.Any();
        }

        public void Dispose()
        {
            RemainingPeers.Dispose();
        }
    }

    private static void OutputPaths(IEnumerable<ImmutableStack<Node>> recursivePaths)
    {
        foreach (var path in recursivePaths)
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