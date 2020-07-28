namespace GCommon
{
	public static class CharUtils
	{
		#region Case Conversions
		public static char[] ToLower(this char[] input) => input.ToString().ToLower().ToCharArray();
		public static char[] ToUpper(this char[] input) => input.ToString().ToUpper().ToCharArray();
		#endregion
	}
}