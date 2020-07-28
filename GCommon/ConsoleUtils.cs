using static System.Console;

namespace GCommon
{
	public static class ConsoleUtils
	{
		/// <summary>Displays the common end of program text and waits for input.</summary>
		/// <param name="showText">If false, will not show any text and will return immediately.</param>
		public static void FinalizeProgram(bool showText = true)
		{
			if (showText)
			{
				EmptyLine();
				ToLine("Process Complete!");
				EmptyLine();
				ToLine("Press any key to end program . . . ", false);
				ReadKey(true);
			}
		}

		/// <summary>Writes the input string to the console.</summary>
		/// <param name="endLine">(Default) If true, ends the line at the end of the input.</param>
		public static void ToLine(this string input, bool endLine = true)
		{
			if (endLine)
				WriteLine(input);
			else
				Write(input);
		}

		/// <summary>Writes a empty line to the console.</summary>
		/// <param name="endLine">(Default) If true, ends the line after the empty string.</param>
		public static void EmptyLine(bool endLine = true)
		{
			if (endLine)
				WriteLine();
			else
				Write(string.Empty);
		}
	}
}