using GCommon.FTypes.Parsers.Enums;

namespace GCommon.FTypes
{
	public class GenericFile { }

	public class GenericFile<T> : GenericFile
	{
		public T GFile { get; set; }
		public DataFormat FileFormat { get; private set; }

		public GenericFile(T gFile) => GFile = gFile;
	}
}