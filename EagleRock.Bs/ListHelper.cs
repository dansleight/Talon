using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EagleRock.Bs
{
	public static class ListHelper
	{
		public static List<int> StringToIntList(string orderedIds)
		{
			IEnumerable<string> stringIds = orderedIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			List<int> intIds = new List<int>();
			foreach (string id in stringIds)
			{
				int toAdd = 0;
				if (Int32.TryParse(id, out toAdd)) intIds.Add(toAdd);
			}
			return intIds;
		}

		public static class ThreadSafeRandom
		{
			[ThreadStatic]
			private static Random Local;

			public static Random ThisThreadsRandom
			{
				get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
			}
		}

		public static IList<T> Shuffle<T>(this IList<T> list)
		{
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
			return list;
		}
	}
}
