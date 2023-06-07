using System;
using System.Collections.Generic;
using System.Linq;

namespace Lax.Helpers.Common {

    public static class CollectionExtensions {

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> objs) {
            foreach (var obj in objs) {
                collection.Add(obj);
            }
        }

        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> objs) {
            foreach (var obj in objs) {
                collection.Remove(obj);
            }
        }

        public static void MergeChanges<TSource, TDestination>(
            this ICollection<TDestination> collection,
            IEnumerable<TSource> source,
            Func<TDestination, TSource> destinationToSourceFunc,
            Func<TSource, TDestination> sourceToDestinationFunc) {
            collection.RemoveRange(collection.Where(_ => !source.Contains(destinationToSourceFunc(_))).ToList());
            collection.AddRange(source.Where(_ => !collection.Any(x => destinationToSourceFunc(x).Equals(_)))
                .Select(sourceToDestinationFunc).ToList());
        }

    }

}