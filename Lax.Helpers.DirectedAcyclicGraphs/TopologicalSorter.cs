using System;
using System.Collections.Generic;
using System.Linq;

// Source: https://gist.github.com/Sup3rc4l1fr4g1l1571c3xp14l1d0c10u5/3341dba6a53d7171fe3397d13d00ee3f

namespace Lax.Helpers.DirectedAcyclicGraphs {

    public static class TopologicalSorter {

        /// <summary>
        /// Topological Sorting (Kahn's algorithm) 
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/Topological_sorting</remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodes">All nodes of directed acyclic graph.</param>
        /// <param name="edges">All edges of directed acyclic graph.</param>
        /// <returns>Sorted node in topological order.</returns>
        public static List<T> TopologicalSort<T>(HashSet<T> nodes, HashSet<Tuple<T, T>> edges) where T : IEquatable<T> {
            // Empty list that will contain the sorted elements
            var sortedElements = new List<T>();

            // Set of all nodes with no incoming edges
            var startingNodes = new HashSet<T>(nodes.Where(n => edges.All(e => e.Item2.Equals(n) == false)));

            // while S is non-empty do
            while (startingNodes.Any()) {
                //  remove a node n from S
                var n = startingNodes.First();
                startingNodes.Remove(n);

                // add n to tail of L
                sortedElements.Add(n);

                // for each node m with an edge e from n to m do
                foreach (var e in edges.Where(e => e.Item1.Equals(n)).ToList()) {
                    var m = e.Item2;

                    // remove edge e from the graph
                    edges.Remove(e);

                    // if m has no other incoming edges then
                    if (edges.All(me => me.Item2.Equals(m) == false)) {
                        // insert m into S
                        startingNodes.Add(m);
                    }
                }
            }

            // if graph has edges then
            return edges.Any() ? null : sortedElements;
        }

    }

}