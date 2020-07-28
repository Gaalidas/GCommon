using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using GCommon.Collections;
using GCommon.Enums;

namespace GCommon.ArgHandlers
{
	public class GArgTag : IEquatable<GArgTag>
	{
		public string PrimaryTag { get; private set; }
		public GList<string> AlternateTags { get; set; } = default;
		public GArgData Data { get; set; }
		public DataType TypeOfData { get; private set; }

		public GArgTag(string primaryTag) => PrimaryTag = primaryTag;
		public GArgTag(string primaryTag, DataType typeOfData) : this(primaryTag) => TypeOfData = typeOfData;

		public GArgTag(string primaryTag, object data) : this(primaryTag)
		{
			if (data.DetermineDataType(out DataType outType))
				TypeOfData = outType;

			switch (TypeOfData)
			{
				case DataType.Int:
					Data = new GArgData<int>(this, (int)data);
					break;
				case DataType.Double:
					Data = new GArgData<double>(this, (double)data);
					break;
				case DataType.Float:
					Data = new GArgData<float>(this, (float)data);
					break;
				case DataType.String:
					Data = new GArgData<string>(this, (string)data);
					break;
				case DataType.Char:
					Data = new GArgData<char>(this, (char)data);
					break;
				case DataType.Array:
					Data = new GArgData<Array>(this, (Array)data);
					break;
				case DataType.IList:
					Data = new GArgData<IList>(this, (IList)data);
					break;
				case DataType.FileInfo:
					Data = new GArgData<FileInfo>(this, (FileInfo)data);
					break;
				case DataType.DirectoryInfo:
					Data = new GArgData<DirectoryInfo>(this, (DirectoryInfo)data);
					break;
				case DataType.GDict:
					Data = new GArgData<GDict>(this, (GDict)data);
					break;
				case DataType.ArrayInt:
					Data = new GArgData<int[]>(this, (int[])data);
					break;
				case DataType.ArrayDouble:
					Data = new GArgData<double[]>(this, (double[])data);
					break;
				case DataType.ArrayFloat:
					Data = new GArgData<float[]>(this, (float[])data);
					break;
				case DataType.ArrayString:
					Data = new GArgData<string[]>(this, (string[])data);
					break;
				case DataType.ArrayChar:
					Data = new GArgData<char[]>(this, (char[])data);
					break;
				case DataType.ArrayFileInfo:
					Data = new GArgData<FileInfo[]>(this, (FileInfo[])data);
					break;
				case DataType.ArrayDirectoryInfo:
					Data = new GArgData<DirectoryInfo[]>(this, (DirectoryInfo[])data);
					break;
				default:
					break;
			}
		}

		public void AddTag(string tagName)
		{
			if (AlternateTags.Contains(tagName))
				AlternateTags.Remove(tagName);

			AlternateTags.Add(tagName);
		}

		public override bool Equals(object obj) => Equals(obj as GArgTag);
		public bool Equals(GArgTag other) => other != null && PrimaryTag == other.PrimaryTag && EqualityComparer<GList<string>>.Default.Equals(AlternateTags, other.AlternateTags) && EqualityComparer<GArgData>.Default.Equals(Data, other.Data) && TypeOfData == other.TypeOfData;

		public override int GetHashCode()
		{
			int hashCode = -578797260;
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(PrimaryTag);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GList<string>>.Default.GetHashCode(AlternateTags);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GArgData>.Default.GetHashCode(Data);
			hashCode = (hashCode * -1521134295) + TypeOfData.GetHashCode();
			return hashCode;
		}

		public static bool operator ==(GArgTag tag1, GArgTag tag2) => EqualityComparer<GArgTag>.Default.Equals(tag1, tag2);
		public static bool operator !=(GArgTag tag1, GArgTag tag2) => !(tag1 == tag2);
	}
}