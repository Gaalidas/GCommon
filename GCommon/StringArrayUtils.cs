namespace GCommon
{
	public static class StringArrayUtils
	{
		public static string ToPairString(this string[] itemStrings)
		{
			if (itemStrings.Length == 0)
				return "[Empty]";

			string output = "[";

			if (itemStrings.Length > 0)
			{
				for (int i = 0; i < itemStrings.Length; i++)
				{
					if (i == 0)
					{
						output += $"{itemStrings[i]}";

						if (itemStrings.Length > 1)
							output += ", (";
					}

					if (itemStrings.Length > 1)
						output += i != itemStrings.Length - 1 ? $"{itemStrings[i]}, " : $"{itemStrings[i]})";
				}
			}

			return $"{output}]";
		}

		public static bool TrySplit(this string input, string splitStr, out string[] output) => (output = input.Split(splitStr.ToCharArray())).Length > 1;
	}
}