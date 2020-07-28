using System;
using System.Collections.Generic;

namespace GCommon.Collections
{
	/// <summary>A generic class for the <see cref="GValueSet{TVal1, TVal2}"/> objects.</summary>
	public class GValueSet
	{
		public static GValueSet<T1> Empty<T1>() => new GValueSet<T1>();
		public static GValueSet<T1, T2> Empty<T1, T2>() => new GValueSet<T1, T2>();
		public static GValueSet<T1, T2, T3> Empty<T1, T2, T3>() => new GValueSet<T1, T2, T3>();
		public static GValueSet<T1, T2, T3, T4> Empty<T1, T2, T3, T4>() => new GValueSet<T1, T2, T3, T4>();
		public static GValueSet<T1, T2, T3, T4, T5> Empty<T1, T2, T3, T4, T5>() => new GValueSet<T1, T2, T3, T4, T5>();
	}

	/// <summary>A set with only one value in it and no key.  Mostly for compatibility with two-value dictionaries.</summary>
	public class GValueSet<TVal1> : GValueSet, IEquatable<GValueSet<TVal1>>
	{
		#region Fields
		public TVal1 Val1 { get; set; } = default;
		#endregion

		#region Validation
		public bool IsValid => Val1 != default;
		#endregion

		#region Constructors
		public GValueSet() { }
		public GValueSet(TVal1 val1) => Val1 = val1;
		#endregion

		#region Equality
		public override bool Equals(object obj) => Equals(obj as GValueSet<TVal1>);
		public bool Equals(GValueSet<TVal1> other) => other != null && EqualityComparer<TVal1>.Default.Equals(Val1, other.Val1) && IsValid == other.IsValid;

		public override int GetHashCode()
		{
			int hashCode = -613829392;
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal1>.Default.GetHashCode(Val1);
			hashCode = (hashCode * -1521134295) + IsValid.GetHashCode();
			return hashCode;
		}

		public static bool operator ==(GValueSet<TVal1> set1, GValueSet<TVal1> set2) => EqualityComparer<GValueSet<TVal1>>.Default.Equals(set1, set2);
		public static bool operator !=(GValueSet<TVal1> set1, GValueSet<TVal1> set2) => !(set1 == set2);
		#endregion
	}

	/// <summary>A set with two values in it.</summary>
	public class GValueSet<TVal1, TVal2> : GValueSet, IEquatable<GValueSet<TVal1, TVal2>>
	{
		#region Fields
		public TVal1 Val1 { get; set; } = default;
		public TVal2 Val2 { get; set; } = default;
		#endregion

		#region Validation
		public bool IsValid => Val1 != default && Val2 != default;
		public bool Contains(TVal1 val1) => (object)Val1 == (object)val1;
		#endregion

		#region Constructors
		public GValueSet() { }
		public GValueSet(TVal1 val1) => Val1 = val1;
		public GValueSet(TVal1 val1, TVal2 val2) : this(val1) => Val2 = val2;
		#endregion

		#region Equality
		public override string ToString() => base.ToString();
		public override bool Equals(object obj) => Equals(obj as GValueSet<TVal1, TVal2>);
		public bool Equals(GValueSet<TVal1, TVal2> other) => other != null && EqualityComparer<TVal1>.Default.Equals(Val1, other.Val1) && EqualityComparer<TVal2>.Default.Equals(Val2, other.Val2);

		public override int GetHashCode()
		{
			int hashCode = 556588965;
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal1>.Default.GetHashCode(Val1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal2>.Default.GetHashCode(Val2);
			return hashCode;
		}

		public static bool operator ==(GValueSet<TVal1, TVal2> valset1, GValueSet<TVal1, TVal2> valset2) => EqualityComparer<GValueSet<TVal1, TVal2>>.Default.Equals(valset1, valset2);
		public static bool operator !=(GValueSet<TVal1, TVal2> valset1, GValueSet<TVal1, TVal2> valset2) => !(valset1 == valset2);
		#endregion
	}

	/// <summary>A set with three values in it.</summary>
	public class GValueSet<TVal1, TVal2, TVal3> : GValueSet, IEquatable<GValueSet<TVal1, TVal2, TVal3>>
	{
		#region Fields
		public TVal1 Val1 { get; set; } = default;
		public TVal2 Val2 { get; set; } = default;
		public TVal3 Val3 { get; set; } = default;
		#endregion

		#region Enumeration
		public GDict<TVal1, TVal2, TVal3> Values
		{
			get => new GDict<TVal1, TVal2, TVal3>(Val1, Val2, Val3);
			set
			{
				Val1 = value.Keys[0];
				Val2 = value.Vals1[0];
				Val3 = value.Vals2[0];
			}
		}
		#endregion

		#region Validation
		public bool IsValid => Val1 != default && Val2 != default && Val3 != default;
		#endregion

		#region Constructors
		public GValueSet() { }
		public GValueSet(TVal1 val1) => Val1 = val1;
		public GValueSet(TVal1 val1, TVal2 val2) : this(val1) => Val2 = val2;
		public GValueSet(TVal1 val1, TVal2 val2, TVal3 val3) : this(val1, val2) => Val3 = val3;
		#endregion

		#region Equality
		public override string ToString() => base.ToString();
		public override bool Equals(object obj) => Equals(obj as GValueSet<TVal1, TVal2, TVal3>);
		public bool Equals(GValueSet<TVal1, TVal2, TVal3> other) => other != null && EqualityComparer<TVal1>.Default.Equals(Val1, other.Val1) && EqualityComparer<TVal2>.Default.Equals(Val2, other.Val2) && EqualityComparer<TVal3>.Default.Equals(Val3, other.Val3);

		public override int GetHashCode()
		{
			int hashCode = 556588965;
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal1>.Default.GetHashCode(Val1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal2>.Default.GetHashCode(Val2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal3>.Default.GetHashCode(Val3);
			return hashCode;
		}

		public static bool operator ==(GValueSet<TVal1, TVal2, TVal3> valset1, GValueSet<TVal1, TVal2, TVal3> valset2) => EqualityComparer<GValueSet<TVal1, TVal2, TVal3>>.Default.Equals(valset1, valset2);
		public static bool operator !=(GValueSet<TVal1, TVal2, TVal3> valset1, GValueSet<TVal1, TVal2, TVal3> valset2) => !(valset1 == valset2);
		#endregion
	}

	/// <summary>A set with four values in it.</summary>
	public class GValueSet<TVal1, TVal2, TVal3, TVal4> : GValueSet, IEquatable<GValueSet<TVal1, TVal2, TVal3, TVal4>>
	{
		#region Fields
		public TVal1 Val1 { get; set; } = default;
		public TVal2 Val2 { get; set; } = default;
		public TVal3 Val3 { get; set; } = default;
		public TVal4 Val4 { get; set; } = default;
		#endregion

		#region Validation
		public bool IsValid => Val1 != default && Val2 != default && Val3 != default && Val4 != default;
		#endregion

		#region Constructors
		public GValueSet() { }
		public GValueSet(TVal1 val1) => Val1 = val1;
		public GValueSet(TVal1 val1, TVal2 val2) : this(val1) => Val2 = val2;
		public GValueSet(TVal1 val1, TVal2 val2, TVal3 val3) : this(val1, val2) => Val3 = val3;
		public GValueSet(TVal1 val1, TVal2 val2, TVal3 val3, TVal4 val4) : this(val1, val2, val3) => Val4 = val4;
		#endregion

		#region Equality
		public override string ToString() => base.ToString();
		public override bool Equals(object obj) => Equals(obj as GValueSet<TVal1, TVal2, TVal3, TVal4>);
		public bool Equals(GValueSet<TVal1, TVal2, TVal3, TVal4> other) => other != null && EqualityComparer<TVal1>.Default.Equals(Val1, other.Val1) && EqualityComparer<TVal2>.Default.Equals(Val2, other.Val2) && EqualityComparer<TVal3>.Default.Equals(Val3, other.Val3) && EqualityComparer<TVal4>.Default.Equals(Val4, other.Val4);

		public override int GetHashCode()
		{
			int hashCode = 1520855658;
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal1>.Default.GetHashCode(Val1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal2>.Default.GetHashCode(Val2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal3>.Default.GetHashCode(Val3);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal4>.Default.GetHashCode(Val4);
			return hashCode;
		}

		public static bool operator ==(GValueSet<TVal1, TVal2, TVal3, TVal4> valset1, GValueSet<TVal1, TVal2, TVal3, TVal4> valset2) => EqualityComparer<GValueSet<TVal1, TVal2, TVal3, TVal4>>.Default.Equals(valset1, valset2);
		public static bool operator !=(GValueSet<TVal1, TVal2, TVal3, TVal4> valset1, GValueSet<TVal1, TVal2, TVal3, TVal4> valset2) => !(valset1 == valset2);
		#endregion
	}

	/// <summary>A set with five values in it.</summary>
	public class GValueSet<TVal1, TVal2, TVal3, TVal4, TVal5> : GValueSet, IEquatable<GValueSet<TVal1, TVal2, TVal3, TVal4, TVal5>>
	{
		#region Fields
		public TVal1 Val1 { get; set; } = default;
		public TVal2 Val2 { get; set; } = default;
		public TVal3 Val3 { get; set; } = default;
		public TVal4 Val4 { get; set; } = default;
		public TVal5 Val5 { get; set; } = default;
		#endregion

		#region Validation
		public bool IsValid => Val1 != default && Val2 != default && Val3 != default && Val4 != default && Val5 != default;
		#endregion

		#region Constructors
		public GValueSet() { }
		public GValueSet(TVal1 val1) => Val1 = val1;
		public GValueSet(TVal1 val1, TVal2 val2) : this(val1) => Val2 = val2;
		public GValueSet(TVal1 val1, TVal2 val2, TVal3 val3) : this(val1, val2) => Val3 = val3;
		public GValueSet(TVal1 val1, TVal2 val2, TVal3 val3, TVal4 val4) : this(val1, val2, val3) => Val4 = val4;
		public GValueSet(TVal1 val1, TVal2 val2, TVal3 val3, TVal4 val4, TVal5 val5) : this(val1, val2, val3, val4) => Val5 = val5;
		#endregion

		#region Equality
		public override string ToString() => base.ToString();
		public override bool Equals(object obj) => Equals(obj as GValueSet<TVal1, TVal2, TVal3, TVal4, TVal5>);
		public bool Equals(GValueSet<TVal1, TVal2, TVal3, TVal4, TVal5> other) => other != null && EqualityComparer<TVal1>.Default.Equals(Val1, other.Val1) && EqualityComparer<TVal2>.Default.Equals(Val2, other.Val2) && EqualityComparer<TVal3>.Default.Equals(Val3, other.Val3) && EqualityComparer<TVal4>.Default.Equals(Val4, other.Val4) && EqualityComparer<TVal5>.Default.Equals(Val5, other.Val5);

		public override int GetHashCode()
		{
			int hashCode = 1520855658;
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal1>.Default.GetHashCode(Val1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal2>.Default.GetHashCode(Val2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal3>.Default.GetHashCode(Val3);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal4>.Default.GetHashCode(Val4);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal5>.Default.GetHashCode(Val5);
			return hashCode;
		}

		public static bool operator ==(GValueSet<TVal1, TVal2, TVal3, TVal4, TVal5> valset1, GValueSet<TVal1, TVal2, TVal3, TVal4, TVal5> valset2) => EqualityComparer<GValueSet<TVal1, TVal2, TVal3, TVal4, TVal5>>.Default.Equals(valset1, valset2);
		public static bool operator !=(GValueSet<TVal1, TVal2, TVal3, TVal4, TVal5> valset1, GValueSet<TVal1, TVal2, TVal3, TVal4, TVal5> valset2) => !(valset1 == valset2);
		#endregion
	}
}