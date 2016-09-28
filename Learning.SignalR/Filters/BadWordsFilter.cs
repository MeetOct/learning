using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Learning.SignalR.Filters
{
	public class BadWordsFilter
	{
		private Dictionary<string, object> hash = new Dictionary<string, object>();
		private BitArray firstCharCheck = new BitArray(char.MaxValue);
		private BitArray allCharCheck = new BitArray(char.MaxValue);
		private int maxLength = 0;

		public BadWordsFilter(string filePath)
		{
			string badworlds = string.Empty;
			using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.GetEncoding("gb2312")))
			{
				badworlds = sr.ReadToEnd();
			}
			this.Init(badworlds.Split('|'));
		}

		private void Init(string[] badwords)
		{
			foreach (string word in badwords)
			{
				if (!hash.ContainsKey(word))
				{
					hash.Add(word, null);
					maxLength = Math.Max(maxLength, word.Length);
					firstCharCheck[word[0]] = true;

					foreach (char c in word)
					{
						allCharCheck[c] = true;
					}
				}
			}
		}

		public bool HasBadWord(string text)
		{
			int index = 0;
			while (index < text.Length)
			{
				if (!firstCharCheck[text[index]])
				{
					while (index < text.Length - 1 && !firstCharCheck[text[++index]]) ;
				}

				for (int j = 1; j <= Math.Min(maxLength, text.Length - index); j++)
				{
					if (!allCharCheck[text[index + j - 1]])
					{
						break;
					}

					string sub = text.Substring(index, j);

					if (hash.ContainsKey(sub))
					{
						return true;
					}
				}

				index++;
			}

			return false;
		}
	}
}
