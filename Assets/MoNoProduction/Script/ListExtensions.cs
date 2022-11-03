using System.Collections.Generic;

namespace MoNo.Utility
{
    public static class ListExtensions
    {
        /// <summary>
        /// 先頭にあるオブジェクトを削除せずに返します
        /// </summary>
        public static T Peek<T>(this IList<T> self)
        {
            return self[0];
        }

        /// <summary>
        /// 先頭にあるオブジェクトを削除し、返します.
        /// 引数のindexを入れた場合、indexで指定したオブジェクトを削除して、返します・
        /// </summary>
        public static T Pop<T>(this IList<T> self, int index = 0)
        {
            var result = self[index];
            self.RemoveAt(index);
            return result;
        }

        /// <summary>
        /// 末尾にオブジェクトを追加します
        /// </summary>
        public static void Push<T>(this IList<T> self, T item)
        {
            self.Insert(0, item);
        }
    }
}
