using static System.Math;

namespace GCommon.Math
{
	public static class GMathOps
	{
		#region Clamps
		public static int Clamp(this int input, int min = int.MinValue + 1, int max = int.MaxValue - 1) => input > max ? max : input < min ? min : input;

		public static double Clamp(this double input, double min = double.MinValue + 1d, double max = double.MaxValue - 1d, int digits = 8)
		{
			double output = input > max ? max : input < min ? min : input;
			return digits >= 0 ? output.RoundTo(digits) : output;
		}

		public static float Clamp(this float input, float min = float.MinValue + 1f, float max = float.MaxValue - 1f, int digits = 16)
		{
			float output = input > max ? max : input < min ? min : input;
			return digits >= 0 ? output.RoundTo(digits) : output;
		}

		public static int ClampMin(this int input, int min) => input.Clamp(min);
		public static double ClampMin(this double input, double min, int digits = 8) => input < min ? min.RoundTo(digits) : input.RoundTo(digits);
		public static float ClampMin(this float input, float min, int digits = 16) => input < min ? min.RoundTo(digits) : input.RoundTo(digits);

		public static int ClampMax(this int input, int max) => input > max ? max : input;
		public static double ClampMax(this double input, double max, int digits = 8) => input > max ? max.RoundTo(digits) : input.RoundTo(digits);
		public static float ClampMax(this float input, float max, int digits = 16) => input > max ? max.RoundTo(digits) : input.RoundTo(digits);
		#endregion

		#region Round Expansion
		public static float RoundTo(this float input, int digits) => (float)Round(input, digits);
		public static double RoundTo(this double input, int digits) => Round(input, digits);
		#endregion

		#region Clamps Special
		public static int ClampSpec(this int input, int min = 0, int max = int.MaxValue - 1) => input > 0 ? input.Clamp(min, max) : input < 0 ? input.Clamp(-max, -min) : input;
		public static double ClampSpec(this double input, double min = 0d, double max = double.MaxValue - 1d, int digits = 8) => input > 0d ? input.Clamp(min, max, digits) : input < 0d ? input.Clamp(-max, -min, digits) : input.Clamp(min, max, digits);
		public static float ClampSpec(this float input, float min = 0f, float max = float.MaxValue - 1f, int digits = 16) => input > 0 ? input.Clamp(min, max, digits) : input < 0 ? input.Clamp(-max, -min, digits) : input.Clamp(min, max, digits);
		#endregion

		#region Detection
		public static bool HasDecimals(this double input, out int digits)
		{
			string inputStr = input.ToString();
			digits = 0;

			if (inputStr.Contains("."))
			{
				string[] temps = inputStr.Split('.');

				if (temps.Length > 1 && temps[1] != null)
					digits = temps[1].Length;
			}

			return inputStr.Contains(".");
		}

		public static bool HasDecimals(this float input, out int digits)
		{
			string inputStr = input.ToString();
			digits = 0;

			if (inputStr.Contains("."))
			{
				string[] temps = inputStr.Split('.');

				if (temps.Length > 1 && temps[1] != null)
					digits = temps[1].Length;
			}

			return inputStr.Contains(".");
		}

		public static int NumDigits(this double input) => HasDecimals(input, out int digits) ? digits : 0;
		#endregion

		#region Count Equality
		public static bool AreCountsEqual(this int c1, int c2) => c1 == c2;
		public static bool AreCountsEqual(this int c1, int c2, int c3) => c1.AreCountsEqual(c2) && c2.AreCountsEqual(c3);
		public static bool AreCountsEqual(this int c1, int c2, int c3, int c4) => c1.AreCountsEqual(c2, c3) && c3.AreCountsEqual(c4);
		public static bool AreCountsEqual(this int c1, int c2, int c3, int c4, int c5) => c1.AreCountsEqual(c2, c3, c4) && c4.AreCountsEqual(c5);
		public static bool AreCountsEqual(this int c1, int c2, int c3, int c4, int c5, int c6) => c1.AreCountsEqual(c2, c3, c4, c5) && c5.AreCountsEqual(c6);
		public static bool AreCountsEqual(this int c1, int c2, int c3, int c4, int c5, int c6, int c7) => c1.AreCountsEqual(c2, c3, c4, c5, c6) && c6.AreCountsEqual(c7);
		public static bool AreCountsEqual(this int c1, int c2, int c3, int c4, int c5, int c6, int c7, int c8) => c1.AreCountsEqual(c2, c3, c4, c5, c6, c7) && c7.AreCountsEqual(c8);
		#endregion

		#region Misc
		/// <summary>Is the integer even and not zero?</summary>
		public static bool IsOdd(this int input) => input == 0 ? false : (input % 2) != 0;

		/// <summary>Is the integer odd and not zero?</summary>
		public static bool IsEven(this int input) => input == 0 ? false : (input % 2) == 0;
		#endregion
	}
}