using System;
using System.Collections.Generic;
using GCommon.Collections;
using GCommon.Enums;
using GCommon.Math;

namespace GCommon.ArgHandlers
{
	public class GArgParamSet
	{
		public string ParamName { get; set; }
		public GList<GArgParamType> TypeOfData { get; set; } = new GList<GArgParamType>();
		public DataType GetDataType(int itemNum) => TypeOfData[itemNum.Clamp(1, TypeOfData.Capacity) - 1].ParamType;
	}

	/// <summary>A set of parameters with one possible value.</summary>
	public class GArgParamSet<T1> : GArgParamSet, IEquatable<GArgParamSet<T1>>
	{
		public T1 Item1 { get; set; }

		public GArgParamSet(string paramName) => ParamName = paramName;

		public GArgParamSet(string paramName, T1 item1) : this(paramName)
		{
			Item1 = item1;

			TypeOfData.Capacity = 1;
			TypeOfData[0].DetermineDataType(Item1);
		}

		public void SetAll(T1 item1) => Item1 = item1;

		public override bool Equals(object obj) => Equals(obj as GArgParamSet<T1>);
		public bool Equals(GArgParamSet<T1> other) => other != null && EqualityComparer<T1>.Default.Equals(Item1, other.Item1);
		public override int GetHashCode() => 592959197 + EqualityComparer<T1>.Default.GetHashCode(Item1);

		public static bool operator ==(GArgParamSet<T1> set1, GArgParamSet<T1> set2) => EqualityComparer<GArgParamSet<T1>>.Default.Equals(set1, set2);
		public static bool operator !=(GArgParamSet<T1> set1, GArgParamSet<T1> set2) => !(set1 == set2);
	}

	/// <summary>A set of parameters with two possible values.</summary>
	public class GArgParamSet<T1, T2> : GArgParamSet, IEquatable<GArgParamSet<T1, T2>>
	{
		public T1 Item1 { get; set; }
		public T2 Item2 { get; set; }

		public GArgParamSet(string paramName) => ParamName = paramName;

		public GArgParamSet(string paramName, T1 item1, T2 item2) : this(paramName)
		{
			Item1 = item1;
			Item2 = item2;

			TypeOfData.Capacity = 2;
			TypeOfData[0].DetermineDataType(Item1);
			TypeOfData[1].DetermineDataType(Item2);
		}

		public void SetAll(T1 item1, T2 item2)
		{
			Item1 = item1;
			Item2 = item2;
		}

		public override bool Equals(object obj) => Equals(obj as GArgParamSet<T1, T2>);
		public bool Equals(GArgParamSet<T1, T2> other) => other != null && EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2);

		public override int GetHashCode()
		{
			int hashCode = -1030903623;
			hashCode = (hashCode * -1521134295) + EqualityComparer<T1>.Default.GetHashCode(Item1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T2>.Default.GetHashCode(Item2);
			return hashCode;
		}

		public static bool operator ==(GArgParamSet<T1, T2> set1, GArgParamSet<T1, T2> set2) => EqualityComparer<GArgParamSet<T1, T2>>.Default.Equals(set1, set2);
		public static bool operator !=(GArgParamSet<T1, T2> set1, GArgParamSet<T1, T2> set2) => !(set1 == set2);
	}

	/// <summary>A set of parameters with three possible values.</summary>
	public class GArgParamSet<T1, T2, T3> : GArgParamSet, IEquatable<GArgParamSet<T1, T2, T3>>
	{
		public T1 Item1 { get; set; }
		public T2 Item2 { get; set; }
		public T3 Item3 { get; set; }

		public GArgParamSet(string paramName) => ParamName = paramName;

		public GArgParamSet(string paramName, T1 item1, T2 item2, T3 item3) : this(paramName)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;

			TypeOfData.Capacity = 3;
			TypeOfData[0].DetermineDataType(Item1);
			TypeOfData[1].DetermineDataType(Item2);
			TypeOfData[2].DetermineDataType(Item3);
		}

		public void SetAll(T1 item1, T2 item2, T3 item3)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
		}

		public override bool Equals(object obj) => Equals(obj as GArgParamSet<T1, T2, T3>);
		public bool Equals(GArgParamSet<T1, T2, T3> other) => other != null && EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3);

		public override int GetHashCode()
		{
			int hashCode = -753528820;
			hashCode = (hashCode * -1521134295) + EqualityComparer<T1>.Default.GetHashCode(Item1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T2>.Default.GetHashCode(Item2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T3>.Default.GetHashCode(Item3);
			return hashCode;
		}

		public static bool operator ==(GArgParamSet<T1, T2, T3> set1, GArgParamSet<T1, T2, T3> set2) => EqualityComparer<GArgParamSet<T1, T2, T3>>.Default.Equals(set1, set2);
		public static bool operator !=(GArgParamSet<T1, T2, T3> set1, GArgParamSet<T1, T2, T3> set2) => !(set1 == set2);
	}

	/// <summary>A set of parameters with four possible values.</summary>
	public class GArgParamSet<T1, T2, T3, T4> : GArgParamSet, IEquatable<GArgParamSet<T1, T2, T3, T4>>
	{
		public T1 Item1 { get; set; }
		public T2 Item2 { get; set; }
		public T3 Item3 { get; set; }
		public T4 Item4 { get; set; }

		public GArgParamSet(string paramName) => ParamName = paramName;

		public GArgParamSet(string paramName, T1 item1, T2 item2, T3 item3, T4 item4) : this(paramName)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;

			TypeOfData.Capacity = 4;
			TypeOfData[0].DetermineDataType(Item1);
			TypeOfData[1].DetermineDataType(Item2);
			TypeOfData[2].DetermineDataType(Item3);
			TypeOfData[3].DetermineDataType(Item4);
		}

		public void SetAll(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
		}

		public override bool Equals(object obj) => Equals(obj as GArgParamSet<T1, T2, T3, T4>);
		public bool Equals(GArgParamSet<T1, T2, T3, T4> other) => other != null && EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4);

		public override int GetHashCode()
		{
			int hashCode = -753528820;
			hashCode = (hashCode * -1521134295) + EqualityComparer<T1>.Default.GetHashCode(Item1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T2>.Default.GetHashCode(Item2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T3>.Default.GetHashCode(Item3);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T4>.Default.GetHashCode(Item4);
			return hashCode;
		}

		public static bool operator ==(GArgParamSet<T1, T2, T3, T4> set1, GArgParamSet<T1, T2, T3, T4> set2) => EqualityComparer<GArgParamSet<T1, T2, T3, T4>>.Default.Equals(set1, set2);
		public static bool operator !=(GArgParamSet<T1, T2, T3, T4> set1, GArgParamSet<T1, T2, T3, T4> set2) => !(set1 == set2);
	}

	/// <summary>A set of parameters with five possible values.</summary>
	public class GArgParamSet<T1, T2, T3, T4, T5> : GArgParamSet, IEquatable<GArgParamSet<T1, T2, T3, T4, T5>>
	{
		public T1 Item1 { get; set; }
		public T2 Item2 { get; set; }
		public T3 Item3 { get; set; }
		public T4 Item4 { get; set; }
		public T5 Item5 { get; set; }

		public GArgParamSet(string paramName) => ParamName = paramName;

		public GArgParamSet(string paramName, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5) : this(paramName)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;

			TypeOfData.Capacity = 5;
			TypeOfData[0].DetermineDataType(Item1);
			TypeOfData[1].DetermineDataType(Item2);
			TypeOfData[2].DetermineDataType(Item3);
			TypeOfData[3].DetermineDataType(Item4);
			TypeOfData[4].DetermineDataType(Item5);
		}

		public void SetAll(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
		}

		public override bool Equals(object obj) => Equals(obj as GArgParamSet<T1, T2, T3, T4, T5>);
		public bool Equals(GArgParamSet<T1, T2, T3, T4, T5> other) => other != null && EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(Item5, other.Item5);

		public override int GetHashCode()
		{
			int hashCode = -753528820;
			hashCode = (hashCode * -1521134295) + EqualityComparer<T1>.Default.GetHashCode(Item1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T2>.Default.GetHashCode(Item2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T3>.Default.GetHashCode(Item3);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T4>.Default.GetHashCode(Item4);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T5>.Default.GetHashCode(Item5);
			return hashCode;
		}

		public static bool operator ==(GArgParamSet<T1, T2, T3, T4, T5> set1, GArgParamSet<T1, T2, T3, T4, T5> set2) => EqualityComparer<GArgParamSet<T1, T2, T3, T4, T5>>.Default.Equals(set1, set2);
		public static bool operator !=(GArgParamSet<T1, T2, T3, T4, T5> set1, GArgParamSet<T1, T2, T3, T4, T5> set2) => !(set1 == set2);
	}

	/// <summary>A set of parameters with sic possible values.</summary>
	public class GArgParamSet<T1, T2, T3, T4, T5, T6> : GArgParamSet, IEquatable<GArgParamSet<T1, T2, T3, T4, T5, T6>>
	{
		public T1 Item1 { get; set; }
		public T2 Item2 { get; set; }
		public T3 Item3 { get; set; }
		public T4 Item4 { get; set; }
		public T5 Item5 { get; set; }
		public T6 Item6 { get; set; }

		public GArgParamSet(string paramName) => ParamName = paramName;

		public GArgParamSet(string paramName, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6) : this(paramName)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
			Item6 = item6;

			TypeOfData.Capacity = 6;
			TypeOfData[0].DetermineDataType(Item1);
			TypeOfData[1].DetermineDataType(Item2);
			TypeOfData[2].DetermineDataType(Item3);
			TypeOfData[3].DetermineDataType(Item4);
			TypeOfData[4].DetermineDataType(Item5);
			TypeOfData[5].DetermineDataType(Item6);
		}

		public void SetAll(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
			Item6 = item6;
		}

		public override bool Equals(object obj) => Equals(obj as GArgParamSet<T1, T2, T3, T4, T5, T6>);
		public bool Equals(GArgParamSet<T1, T2, T3, T4, T5, T6> other) => other != null && EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(Item6, other.Item6);

		public override int GetHashCode()
		{
			int hashCode = -753528820;
			hashCode = (hashCode * -1521134295) + EqualityComparer<T1>.Default.GetHashCode(Item1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T2>.Default.GetHashCode(Item2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T3>.Default.GetHashCode(Item3);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T4>.Default.GetHashCode(Item4);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T5>.Default.GetHashCode(Item5);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T6>.Default.GetHashCode(Item6);
			return hashCode;
		}

		public static bool operator ==(GArgParamSet<T1, T2, T3, T4, T5, T6> set1, GArgParamSet<T1, T2, T3, T4, T5, T6> set2) => EqualityComparer<GArgParamSet<T1, T2, T3, T4, T5, T6>>.Default.Equals(set1, set2);
		public static bool operator !=(GArgParamSet<T1, T2, T3, T4, T5, T6> set1, GArgParamSet<T1, T2, T3, T4, T5, T6> set2) => !(set1 == set2);
	}

	/// <summary>A set of parameters with seven possible values.</summary>
	public class GArgParamSet<T1, T2, T3, T4, T5, T6, T7> : GArgParamSet, IEquatable<GArgParamSet<T1, T2, T3, T4, T5, T6, T7>>
	{
		public T1 Item1 { get; set; }
		public T2 Item2 { get; set; }
		public T3 Item3 { get; set; }
		public T4 Item4 { get; set; }
		public T5 Item5 { get; set; }
		public T6 Item6 { get; set; }
		public T7 Item7 { get; set; }

		public GArgParamSet(string paramName) => ParamName = paramName;

		public GArgParamSet(string paramName, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7) : this(paramName)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
			Item6 = item6;
			Item7 = item7;

			TypeOfData.Capacity = 7;
			TypeOfData[0].DetermineDataType(Item1);
			TypeOfData[1].DetermineDataType(Item2);
			TypeOfData[2].DetermineDataType(Item3);
			TypeOfData[3].DetermineDataType(Item4);
			TypeOfData[4].DetermineDataType(Item5);
			TypeOfData[5].DetermineDataType(Item6);
			TypeOfData[6].DetermineDataType(Item7);
		}

		public void SetAll(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
			Item6 = item6;
			Item7 = item7;
		}

		public override bool Equals(object obj) => Equals(obj as GArgParamSet<T1, T2, T3, T4, T5, T6, T7>);
		public bool Equals(GArgParamSet<T1, T2, T3, T4, T5, T6, T7> other) => other != null && EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(Item6, other.Item6) && EqualityComparer<T7>.Default.Equals(Item7, other.Item7);

		public override int GetHashCode()
		{
			int hashCode = -753528820;
			hashCode = (hashCode * -1521134295) + EqualityComparer<T1>.Default.GetHashCode(Item1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T2>.Default.GetHashCode(Item2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T3>.Default.GetHashCode(Item3);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T4>.Default.GetHashCode(Item4);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T5>.Default.GetHashCode(Item5);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T6>.Default.GetHashCode(Item6);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T7>.Default.GetHashCode(Item7);
			return hashCode;
		}

		public static bool operator ==(GArgParamSet<T1, T2, T3, T4, T5, T6, T7> set1, GArgParamSet<T1, T2, T3, T4, T5, T6, T7> set2) => EqualityComparer<GArgParamSet<T1, T2, T3, T4, T5, T6, T7>>.Default.Equals(set1, set2);
		public static bool operator !=(GArgParamSet<T1, T2, T3, T4, T5, T6, T7> set1, GArgParamSet<T1, T2, T3, T4, T5, T6, T7> set2) => !(set1 == set2);
	}

	/// <summary>A set of parameters with eight possible values.</summary>
	public class GArgParamSet<T1, T2, T3, T4, T5, T6, T7, T8> : GArgParamSet, IEquatable<GArgParamSet<T1, T2, T3, T4, T5, T6, T7, T8>>
	{
		public T1 Item1 { get; set; }
		public T2 Item2 { get; set; }
		public T3 Item3 { get; set; }
		public T4 Item4 { get; set; }
		public T5 Item5 { get; set; }
		public T6 Item6 { get; set; }
		public T7 Item7 { get; set; }
		public T8 Item8 { get; set; }

		public GArgParamSet(string paramName) => ParamName = paramName;

		public GArgParamSet(string paramName, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8) : this(paramName)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
			Item6 = item6;
			Item7 = item7;
			Item8 = item8;

			TypeOfData.Capacity = 8;
			TypeOfData[0].DetermineDataType(Item1);
			TypeOfData[1].DetermineDataType(Item2);
			TypeOfData[2].DetermineDataType(Item3);
			TypeOfData[3].DetermineDataType(Item4);
			TypeOfData[4].DetermineDataType(Item5);
			TypeOfData[5].DetermineDataType(Item6);
			TypeOfData[6].DetermineDataType(Item7);
			TypeOfData[7].DetermineDataType(Item8);
		}

		public void SetAll(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
			Item6 = item6;
			Item7 = item7;
			Item8 = item8;
		}

		public override bool Equals(object obj) => Equals(obj as GArgParamSet<T1, T2, T3, T4, T5, T6, T7, T8>);
		public bool Equals(GArgParamSet<T1, T2, T3, T4, T5, T6, T7, T8> other) => other != null && EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(Item6, other.Item6) && EqualityComparer<T7>.Default.Equals(Item7, other.Item7) && EqualityComparer<T8>.Default.Equals(Item8, other.Item8);

		public override int GetHashCode()
		{
			int hashCode = -753528820;
			hashCode = (hashCode * -1521134295) + EqualityComparer<T1>.Default.GetHashCode(Item1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T2>.Default.GetHashCode(Item2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T3>.Default.GetHashCode(Item3);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T4>.Default.GetHashCode(Item4);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T5>.Default.GetHashCode(Item5);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T6>.Default.GetHashCode(Item6);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T7>.Default.GetHashCode(Item7);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T8>.Default.GetHashCode(Item8);
			return hashCode;
		}

		public static bool operator ==(GArgParamSet<T1, T2, T3, T4, T5, T6, T7, T8> set1, GArgParamSet<T1, T2, T3, T4, T5, T6, T7, T8> set2) => EqualityComparer<GArgParamSet<T1, T2, T3, T4, T5, T6, T7, T8>>.Default.Equals(set1, set2);
		public static bool operator !=(GArgParamSet<T1, T2, T3, T4, T5, T6, T7, T8> set1, GArgParamSet<T1, T2, T3, T4, T5, T6, T7, T8> set2) => !(set1 == set2);
	}
}