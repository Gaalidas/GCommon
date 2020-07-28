using System.Collections.Generic;
using System.IO;
using System.Xml;
using GCommon.Enums;
using GCommon.IO;
using GCommon.Math;

namespace GCommon.Xml
{
	public static class StdXmlUtils
	{
		#region Get XML Files
		public static bool GetXmlFiles(this DirectoryInfo root, string fileNamePattern, out List<FileInfo> output, bool tryRecursive = false)
		{
			fileNamePattern = !fileNamePattern.IsNullOrEmpty() ? $"{fileNamePattern.Replace("*", string.Empty).Replace(".xml", string.Empty)}*.xml" : "*.xml";
			return root.GetFilesByPattern(fileNamePattern, out output);
		}

		public static FileInfo GetFirstXmlFile(this DirectoryInfo root, string fileNamePattern, bool tryRecursive = false) => root.GetXmlFiles(fileNamePattern, out List<FileInfo> outputList, tryRecursive) && outputList.Count > 0 ? outputList[0] : null;
		#endregion

		#region Node Gathering System.Xml Files
		public static bool GetNodesByName(this XmlDocument docTarget, string nodeName, out XmlNodeList output)
		{
			output = docTarget.GetElementsByTagName(nodeName);
			return output != null && output.Count > 0;
		}

		public static bool GetFirstNodeByName(this XmlDocument docTarget, string nodeName, out XmlNode output)
		{
			output = null;

			if (docTarget.GetNodesByName(nodeName, out XmlNodeList nodeList) && nodeList.Count > 0)
				output = nodeList[0];

			return output != null;
		}
		#endregion

		#region Generic XmlDocument Operations
		public static bool GetNodeValue(this XmlNode nodeTarget, out string output) => (output = nodeTarget.InnerText) != string.Empty;
		public static bool GetNodeValue(this XmlNode nodeTarget, out int output) => int.TryParse(nodeTarget.InnerText, out output);
		public static bool GetNodeValue(this XmlNode nodeTarget, out double output) => double.TryParse(nodeTarget.InnerText, out output);
		public static bool GetNodeValue(this XmlNode nodeTarget, out float output) => float.TryParse(nodeTarget.InnerText, out output);
		#endregion

		#region Node Mathematics
		public static bool ManipulateNodeValue(this XmlNode nodeTarget, MathMethod method, int val = 2, int min = int.MinValue + 1, int max = int.MaxValue - 1)
		{
			int output = 0;

			if (int.TryParse(nodeTarget.InnerText, out int input))
			{
				output = input.DoAction(method, val);
				output = method == MathMethod.Special ? output.ClampSpec(min, max) : output.Clamp(min, max);
				nodeTarget.InnerText = output.ToString();
			}

			return nodeTarget.InnerText == output.ToString();
		}

		public static bool ManipulateNodeValue(this XmlNode nodeTarget, MathMethod method, double val = 2d, double min = double.MinValue + 1d, double max = double.MaxValue - 1d, int digits = 8)
		{
			double output = 0d;

			if (double.TryParse(nodeTarget.InnerText, out double input))
			{
				if (digits == -1)
					digits = input.HasDecimals(out int newDigits) ? newDigits : 0;

				output = input.DoAction(method, val);
				output = method == MathMethod.Special ? output.ClampSpec(min, max, digits) : output.Clamp(min, max, digits);
				nodeTarget.InnerText = output.ToString();
			}

			return nodeTarget.InnerText == output.ToString();
		}

		public static bool ManipulateNodeValue(this XmlNode nodeTarget, MathMethod method, float val = 2f, float min = float.MinValue + 1f, float max = float.MaxValue - 1f, int digits = 16)
		{
			float output = 0f;

			if (float.TryParse(nodeTarget.InnerText, out float input))
			{
				if (digits == -1)
					digits = input.HasDecimals(out int newDigits) ? newDigits : 0;

				output = input.DoAction(method, val);
				output = method == MathMethod.Special ? output.ClampSpec(min, max, digits) : output.Clamp(min, max, digits);
				nodeTarget.InnerText = output.ToString();
			}

			return nodeTarget.InnerText == output.ToString();
		}
		#endregion

		#region Actions
		public static int DoAction(this int input, MathMethod method, int val = 2)
		{
			switch (method)
			{
				case MathMethod.Add when val.NotZero():
					return input + val;
				case MathMethod.Subtract when val.NotZero():
					return input - val;
				case MathMethod.Multiply when input.NotZeroOrOne() && val.NotZeroOrOne():
					return input * val;
				case MathMethod.Divide when input.NotZero() && val.NotZeroOrOne():
					return input / val;
				case MathMethod.Special when input.NotZero() && val.NotZeroOrOne():
					return input > 0 ? input * val : input / val;
				case MathMethod.None:
				default:
					return input;
			}
		}

		public static double DoAction(this double input, MathMethod method, double val = 2d)
		{
			switch (method)
			{
				case MathMethod.Add when val.NotZero():
					return input + val;
				case MathMethod.Subtract when val.NotZero():
					return input - val;
				case MathMethod.Multiply when input.NotZeroOrOne() && val.NotZeroOrOne():
					return input * val;
				case MathMethod.Divide when input.NotZeroOrOne() && val.NotZeroOrOne():
					return input / val;
				case MathMethod.Special when input.NotZero() && val.NotZeroOrOne():
					return input > 0 ? input * val : input / val;
				case MathMethod.None:
				default:
					return input;
			}
		}

		public static float DoAction(this float input, MathMethod method, float val = 2f)
		{
			switch (method)
			{
				case MathMethod.Add when val.NotZero():
					return input + val;
				case MathMethod.Subtract when val.NotZero():
					return input - val;
				case MathMethod.Multiply when input.NotZeroOrOne() && val.NotZeroOrOne():
					return input * val;
				case MathMethod.Divide when input.NotZeroOrOne() && val.NotZeroOrOne():
					return input / val;
				case MathMethod.Special when input.NotZero() && val.NotZeroOrOne():
					return input > 0 ? input * val : input / val;
				case MathMethod.None:
				default:
					return input;
			}
		}
		#endregion

		#region Not Various
		public static bool NotZero(this int input) => input != 0;
		public static bool NotZero(this double input) => input != 0d;
		public static bool NotZero(this float input) => input != 0f;

		public static bool NotNaN(this double input) => input != double.NaN;
		public static bool NotNan(this float input) => input != float.NaN;

		public static bool NotZeroOrOne(this int input) => input != 0 && input != 1 && input != -1;
		public static bool NotZeroOrOne(this double input) => input != 0d && input != 1d && input != -1d;
		public static bool NotZeroOrOne(this float input) => input != 0f && input != 1f && input != -1f;
		#endregion

		#region Saves
		public static bool SaveXmlFile(this XmlDocument doc, FileInfo file)
		{
			doc.Save(file.FullName);
			file.Refresh();
			return file.Exists;
		}
		#endregion
	}
}