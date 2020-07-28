namespace GCommon
{
	public static class BooleanUtils
	{
		#region True For All
		/// <summary>Seriously redundant method that helps with "TrueForAll" predicated methods.</summary>
		public static bool IsBoolTrue(this bool input) => input;

		/// <summary>Less redundant method taht actually checks if an array of bools is all true.  Returns false immediately upon finding the dirst false boolean.</summary>
		public static bool TrueForAll(this bool[] inputArray)
		{
			for (int i = 0; i < inputArray.Length; i++)
			{
				if (!inputArray[i])
					return false;
			}

			return true;
		}
		#endregion
	}
}