using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BottleShop.Storage
{
	static class StorageUtil
	{
		public static string GetIdFromRowKey(string content, string name)
		{
			var parts = content.Split('_');

			for (int i = 0; i < parts.Length; i++)
			{
				var part = parts[i];

				if (part == name)
				{
					if (i + 1 < parts.Length)
					{
						return parts[i + 1];
					}

					break;
				}
			}

			return null;
		}

		public static string GetRowKeyContent(params Tuple<string, string>[] valuePairs)
		{
			var content = valuePairs
				.Select(p => $"{p.Item1}_{p.Item2}")
				.Aggregate((i, j) => $"{i}_{j}");

			return content;
		}

		public static List<T> GetContentList<T>(string content)
		{
			if (string.IsNullOrEmpty(content)) return new List<T>();

			return JsonConvert.DeserializeObject<List<T>>(content);
		}

		public static string GetContentString<T>(List<T> list)
		{
			if (list == null || list.Count <= 0) return string.Empty;

			return JsonConvert.SerializeObject(list, Formatting.None);
		}
	}
}
