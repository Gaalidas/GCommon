using System;
using System.Collections;
using System.Collections.Generic;
using GCommon.Enums;
using GCommon.Math;

namespace GCommon.Collections.Params
{
	public class GParamSet
	{
		public GList<GParamType> TypeOfData { get; set; } = new GList<GParamType>();
		public DataType GetDataType(int itemNum) => TypeOfData[itemNum.Clamp(1, TypeOfData.Capacity) - 1].ParamType;

		public static GParamSet<T1> Empty<T1>() => new GParamSet<T1>();
		public static GParamSet<T1, T2> Empty<T1, T2>() => new GParamSet<T1, T2>();
		public static GParamSet<T1, T2, T3> Empty<T1, T2, T3>() => new GParamSet<T1, T2, T3>();
		public static GParamSet<T1, T2, T3, T4> Empty<T1, T2, T3, T4>() => new GParamSet<T1, T2, T3, T4>();
		public static GParamSet<T1, T2, T3, T4, T5> Empty<T1, T2, T3, T4, T5>() => new GParamSet<T1, T2, T3, T4, T5>();
		public static GParamSet<T1, T2, T3, T4, T5, T6> Empty<T1, T2, T3, T4, T5, T6>() => new GParamSet<T1, T2, T3, T4, T5, T6>();
		public static GParamSet<T1, T2, T3, T4, T5, T6, T7> Empty<T1, T2, T3, T4, T5, T6, T7>() => new GParamSet<T1, T2, T3, T4, T5, T6, T7>();
		public static GParamSet<T1, T2, T3, T4, T5, T6, T7, T8> Empty<T1, T2, T3, T4, T5, T6, T7, T8>() => new GParamSet<T1, T2, T3, T4, T5, T6, T7, T8>();
	}

	/// <summary>A set of parameters with one possible value.</summary>
	public class GParamSet<T1> : GParamSet, IEquatable<GParamSet<T1>>
	{
		public T1 Item1 { get; set; } = default;

		public GParamSet() { }

		public GParamSet(T1 item1)
		{
			Item1 = item1;

			TypeOfData.Capacity = 1;
			TypeOfData.Add(new GParamType(Item1));
		}

		public void SetAll(T1 item1) => Item1 = item1;

		public override bool Equals(object obj) => Equals(obj as GParamSet<T1>);
		public bool Equals(GParamSet<T1> other) => other != null && EqualityComparer<T1>.Default.Equals(Item1, other.Item1);
		public override int GetHashCode() => 592959197 + EqualityComparer<T1>.Default.GetHashCode(Item1);

		public static bool operator ==(GParamSet<T1> set1, GParamSet<T1> set2) => EqualityComparer<GParamSet<T1>>.Default.Equals(set1, set2);
		public static bool operator !=(GParamSet<T1> set1, GParamSet<T1> set2) => !(set1 == set2);
	}

	/// <summary>A set of parameters with two possible values.</summary>
	public class GParamSet<T1, T2> : GParamSet, IEquatable<GParamSet<T1, T2>>
	{
		public T1 Item1 { get; set; } = default;
		public T2 Item2 { get; set; } = default;

		public GParamSet() { }

		public GParamSet(T1 item1, T2 item2)
		{
			Item1 = item1;
			Item2 = item2;

			TypeOfData.Capacity = 2;
			TypeOfData.Add(new GParamType(Item1));
			TypeOfData.Add(new GParamType(Item2));
		}

		public void SetAll(T1 item1, T2 item2)
		{
			Item1 = item1;
			Item2 = item2;
		}

		public override bool Equals(object obj) => Equals(obj as GParamSet<T1, T2>);
		public bool Equals(GParamSet<T1, T2> other) => other != null && EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2);

		public override int GetHashCode()
		{
			int hashCode = -1214724318;
			hashCode = (hashCode * -1521134295) + EqualityComparer<T1>.Default.GetHashCode(Item1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T2>.Default.GetHashCode(Item2);
			return hashCode;
		}

		public static bool operator ==(GParamSet<T1, T2> set1, GParamSet<T1, T2> set2) => EqualityComparer<GParamSet<T1, T2>>.Default.Equals(set1, set2);
		public static bool operator !=(GParamSet<T1, T2> set1, GParamSet<T1, T2> set2) => !(set1 == set2);
	}

	/// <summary>A set of parameters with three possible values.</summary>
	public class GParamSet<T1, T2, T3> : GParamSet, IEquatable<GParamSet<T1, T2, T3>>
	{
		public T1 Item1 { get; set; } = default;
		public T2 Item2 { get; set; } = default;
		public T3 Item3 { get; set; } = default;

		public GParamSet() { }

		public GParamSet(T1 item1, T2 item2, T3 item3)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;

			TypeOfData.Capacity = 3;
			TypeOfData.Add(new GParamType(Item1));
			TypeOfData.Add(new GParamType(Item2));
			TypeOfData.Add(new GParamType(Item3));
		}

		public void SetAll(T1 item1, T2 item2, T3 item3)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
		}

		public override bool Equals(object obj) => Equals(obj as GParamSet<T1, T2, T3>);
		public bool Equals(GParamSet<T1, T2, T3> other) => other != null && EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3);

		public override int GetHashCode()
		{
			int hashCode = -1214724318;
			hashCode = (hashCode * -1521134295) + EqualityComparer<T1>.Default.GetHashCode(Item1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T2>.Default.GetHashCode(Item2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T3>.Default.GetHashCode(Item3);
			return hashCode;
		}

		public static bool operator ==(GParamSet<T1, T2, T3> set1, GParamSet<T1, T2, T3> set2) => EqualityComparer<GParamSet<T1, T2, T3>>.Default.Equals(set1, set2);
		public static bool operator !=(GParamSet<T1, T2, T3> set1, GParamSet<T1, T2, T3> set2) => !(set1 == set2);
	}

	/// <summary>A set of parameters with four possible values.</summary>
	public class GParamSet<T1, T2, T3, T4> : GParamSet, IEquatable<GParamSet<T1, T2, T3, T4>>
	{
		public T1 Item1 { get; set; } = default;
		public T2 Item2 { get; set; } = default;
		public T3 Item3 { get; set; } = default;
		public T4 Item4 { get; set; } = default;

		public GParamSet() { }

		public GParamSet(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;

			TypeOfData.Capacity = 4;
			TypeOfData.Add(new GParamType(Item1));
			TypeOfData.Add(new GParamType(Item2));
			TypeOfData.Add(new GParamType(Item3));
			TypeOfData.Add(new GParamType(Item4));
		}

		public void SetAll(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
		}

		public override bool Equals(object obj) => Equals(obj as GParamSet<T1, T2, T3, T4>);
		public bool Equals(GParamSet<T1, T2, T3, T4> other) => other != null && EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4);

		public override int GetHashCode()
		{
			int hashCode = -1214724318;
			hashCode = (hashCode * -1521134295) + EqualityComparer<T1>.Default.GetHashCode(Item1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T2>.Default.GetHashCode(Item2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T3>.Default.GetHashCode(Item3);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T4>.Default.GetHashCode(Item4);
			return hashCode;
		}

		public static bool operator ==(GParamSet<T1, T2, T3, T4> set1, GParamSet<T1, T2, T3, T4> set2) => EqualityComparer<GParamSet<T1, T2, T3, T4>>.Default.Equals(set1, set2);
		public static bool operator !=(GParamSet<T1, T2, T3, T4> set1, GParamSet<T1, T2, T3, T4> set2) => !(set1 == set2);
	}

	/// <summary>A set of parameters with five possible values.</summary>
	public class GParamSet<T1, T2, T3, T4, T5> : GParamSet, IEquatable<GParamSet<T1, T2, T3, T4, T5>>
	{
		public T1 Item1 { get; set; } = default;
		public T2 Item2 { get; set; } = default;
		public T3 Item3 { get; set; } = default;
		public T4 Item4 { get; set; } = default;
		public T5 Item5 { get; set; } = default;

		public GParamSet() { }

		public GParamSet(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;

			TypeOfData.Capacity = 5;
			TypeOfData.Add(new GParamType(Item1));
			TypeOfData.Add(new GParamType(Item2));
			TypeOfData.Add(new GParamType(Item3));
			TypeOfData.Add(new GParamType(Item4));
			TypeOfData.Add(new GParamType(Item5));
		}

		public void SetAll(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
		}

		public override bool Equals(object obj) => Equals(obj as GParamSet<T1, T2, T3, T4, T5>);
		public bool Equals(GParamSet<T1, T2, T3, T4, T5> other) => other != null && EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(Item5, other.Item5);

		public override int GetHashCode()
		{
			int hashCode = -1214724318;
			hashCode = (hashCode * -1521134295) + EqualityComparer<T1>.Default.GetHashCode(Item1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T2>.Default.GetHashCode(Item2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T3>.Default.GetHashCode(Item3);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T4>.Default.GetHashCode(Item4);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T5>.Default.GetHashCode(Item5);
			return hashCode;
		}

		public static bool operator ==(GParamSet<T1, T2, T3, T4, T5> set1, GParamSet<T1, T2, T3, T4, T5> set2) => EqualityComparer<GParamSet<T1, T2, T3, T4, T5>>.Default.Equals(set1, set2);
		public static bool operator !=(GParamSet<T1, T2, T3, T4, T5> set1, GParamSet<T1, T2, T3, T4, T5> set2) => !(set1 == set2);
	}

	/// <summary>A set of parameters with sic possible values.</summary>
	public class GParamSet<T1, T2, T3, T4, T5, T6> : GParamSet, IEquatable<GParamSet<T1, T2, T3, T4, T5, T6>>
	{
		public T1 Item1 { get; set; } = default;
		public T2 Item2 { get; set; } = default;
		public T3 Item3 { get; set; } = default;
		public T4 Item4 { get; set; } = default;
		public T5 Item5 { get; set; } = default;
		public T6 Item6 { get; set; } = default;

		public GParamSet() { }

		public GParamSet(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
			Item6 = item6;

			TypeOfData.Capacity = 6;
			TypeOfData.Add(new GParamType(Item1));
			TypeOfData.Add(new GParamType(Item2));
			TypeOfData.Add(new GParamType(Item3));
			TypeOfData.Add(new GParamType(Item4));
			TypeOfData.Add(new GParamType(Item5));
			TypeOfData.Add(new GParamType(Item6));
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

		public override bool Equals(object obj) => Equals(obj as GParamSet<T1, T2, T3, T4, T5, T6>);
		public bool Equals(GParamSet<T1, T2, T3, T4, T5, T6> other) => other != null && EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(Item6, other.Item6);

		public override int GetHashCode()
		{
			int hashCode = -1214724318;
			hashCode = (hashCode * -1521134295) + EqualityComparer<T1>.Default.GetHashCode(Item1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T2>.Default.GetHashCode(Item2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T3>.Default.GetHashCode(Item3);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T4>.Default.GetHashCode(Item4);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T5>.Default.GetHashCode(Item5);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T6>.Default.GetHashCode(Item6);
			return hashCode;
		}

		public static bool operator ==(GParamSet<T1, T2, T3, T4, T5, T6> set1, GParamSet<T1, T2, T3, T4, T5, T6> set2) => EqualityComparer<GParamSet<T1, T2, T3, T4, T5, T6>>.Default.Equals(set1, set2);
		public static bool operator !=(GParamSet<T1, T2, T3, T4, T5, T6> set1, GParamSet<T1, T2, T3, T4, T5, T6> set2) => !(set1 == set2);
	}

	/// <summary>A set of parameters with seven possible values.</summary>
	public class GParamSet<T1, T2, T3, T4, T5, T6, T7> : GParamSet, IEquatable<GParamSet<T1, T2, T3, T4, T5, T6, T7>>
	{
		public T1 Item1 { get; set; } = default;
		public T2 Item2 { get; set; } = default;
		public T3 Item3 { get; set; } = default;
		public T4 Item4 { get; set; } = default;
		public T5 Item5 { get; set; } = default;
		public T6 Item6 { get; set; } = default;
		public T7 Item7 { get; set; } = default;

		public GParamSet() { }

		public GParamSet(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
			Item6 = item6;
			Item7 = item7;

			TypeOfData.Capacity = 7;
			TypeOfData.Add(new GParamType(Item1));
			TypeOfData.Add(new GParamType(Item2));
			TypeOfData.Add(new GParamType(Item3));
			TypeOfData.Add(new GParamType(Item4));
			TypeOfData.Add(new GParamType(Item5));
			TypeOfData.Add(new GParamType(Item6));
			TypeOfData.Add(new GParamType(Item7));
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

		public override bool Equals(object obj) => Equals(obj as GParamSet<T1, T2, T3, T4, T5, T6, T7>);
		public bool Equals(GParamSet<T1, T2, T3, T4, T5, T6, T7> other) => other != null && EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(Item6, other.Item6) && EqualityComparer<T7>.Default.Equals(Item7, other.Item7);

		public override int GetHashCode()
		{
			int hashCode = -1214724318;
			hashCode = (hashCode * -1521134295) + EqualityComparer<T1>.Default.GetHashCode(Item1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T2>.Default.GetHashCode(Item2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T3>.Default.GetHashCode(Item3);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T4>.Default.GetHashCode(Item4);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T5>.Default.GetHashCode(Item5);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T6>.Default.GetHashCode(Item6);
			hashCode = (hashCode * -1521134295) + EqualityComparer<T7>.Default.GetHashCode(Item7);
			return hashCode;
		}

		public static bool operator ==(GParamSet<T1, T2, T3, T4, T5, T6, T7> set1, GParamSet<T1, T2, T3, T4, T5, T6, T7> set2) => EqualityComparer<GParamSet<T1, T2, T3, T4, T5, T6, T7>>.Default.Equals(set1, set2);
		public static bool operator !=(GParamSet<T1, T2, T3, T4, T5, T6, T7> set1, GParamSet<T1, T2, T3, T4, T5, T6, T7> set2) => !(set1 == set2);
	}

	/// <summary>A set of parameters with eight possible values.</summary>
	public class GParamSet<T1, T2, T3, T4, T5, T6, T7, T8> : GParamSet, IEquatable<GParamSet<T1, T2, T3, T4, T5, T6, T7, T8>>
	{
		public T1 Item1 { get; set; } = default;
		public T2 Item2 { get; set; } = default;
		public T3 Item3 { get; set; } = default;
		public T4 Item4 { get; set; } = default;
		public T5 Item5 { get; set; } = default;
		public T6 Item6 { get; set; } = default;
		public T7 Item7 { get; set; } = default;
		public T8 Item8 { get; set; } = default;

		public GParamSet() { }

		public GParamSet(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
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
			TypeOfData.Add(new GParamType(Item1));
			TypeOfData.Add(new GParamType(Item2));
			TypeOfData.Add(new GParamType(Item3));
			TypeOfData.Add(new GParamType(Item4));
			TypeOfData.Add(new GParamType(Item5));
			TypeOfData.Add(new GParamType(Item6));
			TypeOfData.Add(new GParamType(Item7));
			TypeOfData.Add(new GParamType(Item8));
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

		public override bool Equals(object obj) => Equals(obj as GParamSet<T1, T2, T3, T4, T5, T6, T7, T8>);
		public bool Equals(GParamSet<T1, T2, T3, T4, T5, T6, T7, T8> other) => other != null && EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(Item6, other.Item6) && EqualityComparer<T7>.Default.Equals(Item7, other.Item7) && EqualityComparer<T8>.Default.Equals(Item8, other.Item8);

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

		public static bool operator ==(GParamSet<T1, T2, T3, T4, T5, T6, T7, T8> set1, GParamSet<T1, T2, T3, T4, T5, T6, T7, T8> set2) => EqualityComparer<GParamSet<T1, T2, T3, T4, T5, T6, T7, T8>>.Default.Equals(set1, set2);
		public static bool operator !=(GParamSet<T1, T2, T3, T4, T5, T6, T7, T8> set1, GParamSet<T1, T2, T3, T4, T5, T6, T7, T8> set2) => !(set1 == set2);
	}

	/// <summary>A set of parameters with eight possible values.</summary>
	public class GParamSetNew<T1, T2, T3, T4, T5, T6, T7, T8> : GParamSet, IEquatable<GParamSetNew<T1, T2, T3, T4, T5, T6, T7, T8>>
	{
		public GParam<T1> Item1 { get; set; } = new GParam<T1>();
		public GParam<T2> Item2 { get; set; } = new GParam<T2>();
		public GParam<T3> Item3 { get; set; } = new GParam<T3>();
		public GParam<T4> Item4 { get; set; } = new GParam<T4>();
		public GParam<T5> Item5 { get; set; } = new GParam<T5>();
		public GParam<T6> Item6 { get; set; } = new GParam<T6>();
		public GParam<T7> Item7 { get; set; } = new GParam<T7>();
		public GParam<T8> Item8 { get; set; } = new GParam<T8>();

		public GParamSetNew() { }

		public GParamSetNew(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			Item1.Param = item1;
			Item2.Param = item2;
			Item3.Param = item3;
			Item4.Param = item4;
			Item5.Param = item5;
			Item6.Param = item6;
			Item7.Param = item7;
			Item8.Param = item8;

			TypeOfData.Capacity = 8;
			TypeOfData.Add(new GParamType(Item1.Param));
			TypeOfData.Add(new GParamType(Item2.Param));
			TypeOfData.Add(new GParamType(Item3.Param));
			TypeOfData.Add(new GParamType(Item4.Param));
			TypeOfData.Add(new GParamType(Item5.Param));
			TypeOfData.Add(new GParamType(Item6.Param));
			TypeOfData.Add(new GParamType(Item7.Param));
			TypeOfData.Add(new GParamType(Item8.Param));
		}

		public void SetAll(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			Item1.Param = item1;
			Item2.Param = item2;
			Item3.Param = item3;
			Item4.Param = item4;
			Item5.Param = item5;
			Item6.Param = item6;
			Item7.Param = item7;
			Item8.Param = item8;
		}

		public override bool Equals(object obj) => Equals(obj as GParamSetNew<T1, T2, T3, T4, T5, T6, T7, T8>);
		public bool Equals(GParamSetNew<T1, T2, T3, T4, T5, T6, T7, T8> other) => other != null && EqualityComparer<GParam<T1>>.Default.Equals(Item1, other.Item1) && EqualityComparer<GParam<T2>>.Default.Equals(Item2, other.Item2) && EqualityComparer<GParam<T3>>.Default.Equals(Item3, other.Item3) && EqualityComparer<GParam<T4>>.Default.Equals(Item4, other.Item4) && EqualityComparer<GParam<T5>>.Default.Equals(Item5, other.Item5) && EqualityComparer<GParam<T6>>.Default.Equals(Item6, other.Item6) && EqualityComparer<GParam<T7>>.Default.Equals(Item7, other.Item7) && EqualityComparer<GParam<T8>>.Default.Equals(Item8, other.Item8);

		public override int GetHashCode()
		{
			int hashCode = -753528820;
			hashCode = (hashCode * -1521134295) + EqualityComparer<GParam<T1>>.Default.GetHashCode(Item1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GParam<T2>>.Default.GetHashCode(Item2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GParam<T3>>.Default.GetHashCode(Item3);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GParam<T4>>.Default.GetHashCode(Item4);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GParam<T5>>.Default.GetHashCode(Item5);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GParam<T6>>.Default.GetHashCode(Item6);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GParam<T7>>.Default.GetHashCode(Item7);
			hashCode = (hashCode * -1521134295) + EqualityComparer<GParam<T8>>.Default.GetHashCode(Item8);
			return hashCode;
		}

		public static bool operator ==(GParamSetNew<T1, T2, T3, T4, T5, T6, T7, T8> new1, GParamSetNew<T1, T2, T3, T4, T5, T6, T7, T8> new2) => EqualityComparer<GParamSetNew<T1, T2, T3, T4, T5, T6, T7, T8>>.Default.Equals(new1, new2);
		public static bool operator !=(GParamSetNew<T1, T2, T3, T4, T5, T6, T7, T8> new1, GParamSetNew<T1, T2, T3, T4, T5, T6, T7, T8> new2) => !(new1 == new2);
	}

	/// <summary>A set of parameters with eight possible values.</summary>
	public class GParamSetNew2<T1, T2, T3, T4, T5, T6, T7, T8> : GParamSet, IEnumerable<object>, IEquatable<GParamSetNew2<T1, T2, T3, T4, T5, T6, T7, T8>>
	{
		public GList<object> Items { get; set; } = new GList<object>();

		public GParamSetNew2() { }

		public GParamSetNew2(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			Items.Add(item1);
			Items.Add(item2);
			Items.Add(item3);
			Items.Add(item4);
			Items.Add(item5);
			Items.Add(item6);
			Items.Add(item7);
			Items.Add(item8);

			TypeOfData.Capacity = 8;

			for (int i = 0; i < TypeOfData.Capacity; i++)
				TypeOfData.Add(new GParamType(Items[i]));
		}

		public void SetAll(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			Items[0] = item1;
			Items[1] = item2;
			Items[2] = item3;
			Items[3] = item4;
			Items[4] = item5;
			Items[5] = item6;
			Items[6] = item7;
			Items[7] = item8;
		}

		public IEnumerator<object> GetEnumerator() => ((IEnumerable<object>)Items).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<object>)Items).GetEnumerator();

		public override bool Equals(object obj) => Equals(obj as GParamSetNew2<T1, T2, T3, T4, T5, T6, T7, T8>);
		public bool Equals(GParamSetNew2<T1, T2, T3, T4, T5, T6, T7, T8> other) => other != null && EqualityComparer<GList<object>>.Default.Equals(Items, other.Items);
		public override int GetHashCode() => -604923257 + EqualityComparer<GList<object>>.Default.GetHashCode(Items);

		public static bool operator ==(GParamSetNew2<T1, T2, T3, T4, T5, T6, T7, T8> new1, GParamSetNew2<T1, T2, T3, T4, T5, T6, T7, T8> new2) => EqualityComparer<GParamSetNew2<T1, T2, T3, T4, T5, T6, T7, T8>>.Default.Equals(new1, new2);
		public static bool operator !=(GParamSetNew2<T1, T2, T3, T4, T5, T6, T7, T8> new1, GParamSetNew2<T1, T2, T3, T4, T5, T6, T7, T8> new2) => !(new1 == new2);
	}
}