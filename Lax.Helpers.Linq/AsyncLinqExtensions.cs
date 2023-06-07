using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lax.Helpers.Linq {

    public static class AsyncLinqExtensions {

        public static async Task<IEnumerable<T>> WhereAsync<T>(
            this IEnumerable<T> items,
            Func<T, Task<bool>> predicate) {
            var itemTaskList = items.Select(item => new {Item = item, PredTask = predicate.Invoke(item)}).ToList();
            await Task.WhenAll(itemTaskList.Select(x => x.PredTask));
            return itemTaskList.Where(x => x.PredTask.Result).Select(x => x.Item);
        }

        public static async Task<bool> AnyAsync<T>(
            this IEnumerable<T> items,
            Func<T, Task<bool>> predicate) {
            var itemTaskList = items.Select(item => new {Item = item, PredTask = predicate.Invoke(item)}).ToList();
            await Task.WhenAll(itemTaskList.Select(x => x.PredTask));
            return itemTaskList.Any(x => x.PredTask.Result);
        }

    }

}