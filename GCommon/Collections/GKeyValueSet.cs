using System;
using System.Collections.Generic;
using GCommon.DTypes;

namespace GCommon.Collections
{
	public class GKeyValueSet
	{
		public static GKeyValueSet<TKey, TVal1> Empty<TKey, TVal1>() => new GKeyValueSet<TKey, TVal1>();
		public static GKeyValueSet<TKey, TVal1, TVal2> Empty<TKey, TVal1, TVal2>() => new GKeyValueSet<TKey, TVal1, TVal2>();
		public static GKeyValueSet<TKey, TVal1, TVal2, TVal3> Empty<TKey, TVal1, TVal2, TVal3>() => new GKeyValueSet<TKey, TVal1, TVal2, TVal3>();
		public static GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4> Empty<TKey, TVal1, TVal2, TVal3, TVal4>() => new GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4>();

		public bool IsValid<T1, T2>(T1 item1, T2 item2) => item1 != default && item2 != default;
		public bool IsValid<T1, T2, T3>(T1 item1, T2 item2, T3 item3) => IsValid(item1, item2) && item3 != default;
		public bool IsValid<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4) => IsValid(item1, item2, item3) && item4 != default;
		public bool IsValid<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5) => IsValid(item1, item2, item3, item4) && item5 != default;
	}

	public class GKeyValueSet<TKey, TVal1> : GKeyValueSet, IEquatable<GKeyValueSet<TKey, TVal1>>
	{
		public TKey Key { get; set; }
		public TVal1 Val1 { get; set; }

		public GKeyValueSet()
		{
			Key = default;
			Val1 = default;
		}

		public GKeyValueSet(TKey key, TVal1 val1)
		{
			Key = key;
			Val1 = val1;
		}

		public bool IsValid => IsValid(Key, Val1);

		public bool TrueForAll
		{
			get
			{
				if (Key is bool || Val1 is bool)
				{
					GTrinaryBoolean KeyState = new GTrinaryBoolean();
					GTrinaryBoolean Val1State = new GTrinaryBoolean();

					if (Key is bool outKeyBool)
						KeyState[true] = outKeyBool;

					if (Val1 is bool outVal1Bool)
						Val1State[true] = outVal1Bool;

					if (KeyState.IsTrueInt > 0 || Val1State.IsTrueInt > 0)
					{
						bool[] states = new bool[] { KeyState[true], Val1State[true] };
						return states.TrueForAll();
					}
				}

				return IsValid;
			}
		}

		public override bool Equals(object obj) => Equals(obj as GKeyValueSet<TKey, TVal1>);
		public bool Equals(GKeyValueSet<TKey, TVal1> other) => other != null && EqualityComparer<TKey>.Default.Equals(Key, other.Key) && EqualityComparer<TVal1>.Default.Equals(Val1, other.Val1);

		public override int GetHashCode()
		{
			int hashCode = -1032651837;
			hashCode = (hashCode * -1521134295) + EqualityComparer<TKey>.Default.GetHashCode(Key);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal1>.Default.GetHashCode(Val1);
			return hashCode;
		}

		public override string ToString() => new string[] { Key.ToString(), Val1.ToString() }.ToPairString();
		public static bool operator ==(GKeyValueSet<TKey, TVal1> set1, GKeyValueSet<TKey, TVal1> set2) => EqualityComparer<GKeyValueSet<TKey, TVal1>>.Default.Equals(set1, set2);
		public static bool operator !=(GKeyValueSet<TKey, TVal1> set1, GKeyValueSet<TKey, TVal1> set2) => !(set1 == set2);
	}

	public class GKeyValueSet<TKey, TVal1, TVal2> : GKeyValueSet, IEquatable<GKeyValueSet<TKey, TVal1, TVal2>>
	{
		public TKey Key { get; set; }
		public TVal1 Val1 { get; set; }
		public TVal2 Val2 { get; set; }

		public GKeyValueSet()
		{
			Key = default;
			Val1 = default;
			Val2 = default;
		}

		public GKeyValueSet(TKey key, TVal1 val1)
		{
			Key = key;
			Val1 = val1;
			Val2 = default;
		}

		public GKeyValueSet(TKey key, TVal1 val1, TVal2 val2) : this(key, val1) => Val2 = val2;

		public bool IsValid => IsValid(Key, Val1, Val2);

		public bool TrueForAll
		{
			get
			{
				if (Key is bool || Val1 is bool || Val2 is bool)
				{
					GTrinaryBoolean KeyState = new GTrinaryBoolean();
					GTrinaryBoolean Val1State = new GTrinaryBoolean();
					GTrinaryBoolean Val2State = new GTrinaryBoolean();

					if (Key is bool outKeyBool)
						KeyState[true] = outKeyBool;

					if (Val1 is bool outVal1Bool)
						Val1State[true] = outVal1Bool;

					if (Val2 is bool outVal2Bool)
						Val2State[true] = outVal2Bool;

					if (KeyState.IsTrueInt > 0 || Val1State.IsTrueInt > 0 || Val2State.IsTrueInt > 0)
					{
						bool[] states = new bool[] { KeyState[true], Val1State[true], Val2State[true] };
						return states.TrueForAll();
					}
				}

				return IsValid;
			}
		}

		public override bool Equals(object obj) => Equals(obj as GKeyValueSet<TKey, TVal1, TVal2>);
		public bool Equals(GKeyValueSet<TKey, TVal1, TVal2> other) => other != null && EqualityComparer<TKey>.Default.Equals(Key, other.Key) && EqualityComparer<TVal1>.Default.Equals(Val1, other.Val1) && EqualityComparer<TVal2>.Default.Equals(Val2, other.Val2);

		public override int GetHashCode()
		{
			int hashCode = 556588965;
			hashCode = (hashCode * -1521134295) + EqualityComparer<TKey>.Default.GetHashCode(Key);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal1>.Default.GetHashCode(Val1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal2>.Default.GetHashCode(Val2);
			return hashCode;
		}

		public override string ToString() => new string[] { Key.ToString(), Val1.ToString(), Val2.ToString() }.ToPairString();
		public static bool operator ==(GKeyValueSet<TKey, TVal1, TVal2> set1, GKeyValueSet<TKey, TVal1, TVal2> set2) => EqualityComparer<GKeyValueSet<TKey, TVal1, TVal2>>.Default.Equals(set1, set2);
		public static bool operator !=(GKeyValueSet<TKey, TVal1, TVal2> set1, GKeyValueSet<TKey, TVal1, TVal2> set2) => !(set1 == set2);
	}

	public class GKeyValueSet<TKey, TVal1, TVal2, TVal3> : GKeyValueSet, IEquatable<GKeyValueSet<TKey, TVal1, TVal2, TVal3>>
	{
		public TKey Key { get; set; }
		public TVal1 Val1 { get; set; }
		public TVal2 Val2 { get; set; }
		public TVal3 Val3 { get; set; }

		public GKeyValueSet()
		{
			Key = default;
			Val1 = default;
			Val2 = default;
			Val3 = default;
		}

		public GKeyValueSet(TKey key, TVal1 val1)
		{
			Key = key;
			Val1 = val1;
			Val2 = default;
			Val3 = default;
		}

		public GKeyValueSet(TKey key, TVal1 val1, TVal2 val2) : this(key, val1)
		{
			Val2 = val2;
			Val3 = default;
		}

		public GKeyValueSet(TKey key, TVal1 val1, TVal2 val2, TVal3 val3) : this(key, val1, val2) => Val3 = val3;

		public bool IsValid => IsValid(Key, Val1, Val2, Val3);

		public bool TrueForAll
		{
			get
			{
				if (Key is bool || Val1 is bool || Val2 is bool || Val3 is bool)
				{
					GTrinaryBoolean KeyState = new GTrinaryBoolean();
					GTrinaryBoolean Val1State = new GTrinaryBoolean();
					GTrinaryBoolean Val2State = new GTrinaryBoolean();
					GTrinaryBoolean Val3State = new GTrinaryBoolean();

					if (Key is bool outKeyBool)
						KeyState[true] = outKeyBool;

					if (Val1 is bool outVal1Bool)
						Val1State[true] = outVal1Bool;

					if (Val2 is bool outVal2Bool)
						Val2State[true] = outVal2Bool;

					if (Val3 is bool outVal3Bool)
						Val3State[true] = outVal3Bool;

					if (KeyState.IsTrueInt > 0 || Val1State.IsTrueInt > 0 || Val2State.IsTrueInt > 0 || Val3State.IsTrueInt > 0)
					{
						bool[] states = new bool[] { KeyState[true], Val1State[true], Val2State[true], Val3State[true] };
						return states.TrueForAll();
					}
				}

				return IsValid;
			}
		}

		public override bool Equals(object obj) => Equals(obj as GKeyValueSet<TKey, TVal1, TVal2, TVal3>);
		public bool Equals(GKeyValueSet<TKey, TVal1, TVal2, TVal3> other) => other != null && EqualityComparer<TKey>.Default.Equals(Key, other.Key) && EqualityComparer<TVal1>.Default.Equals(Val1, other.Val1) && EqualityComparer<TVal2>.Default.Equals(Val2, other.Val2) && EqualityComparer<TVal3>.Default.Equals(Val3, other.Val3);

		public override int GetHashCode()
		{
			int hashCode = 1520855658;
			hashCode = (hashCode * -1521134295) + EqualityComparer<TKey>.Default.GetHashCode(Key);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal1>.Default.GetHashCode(Val1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal2>.Default.GetHashCode(Val2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal3>.Default.GetHashCode(Val3);
			return hashCode;
		}

		public override string ToString() => new string[] { Key.ToString(), Val1.ToString(), Val2.ToString(), Val3.ToString() }.ToPairString();
		public static bool operator ==(GKeyValueSet<TKey, TVal1, TVal2, TVal3> set1, GKeyValueSet<TKey, TVal1, TVal2, TVal3> set2) => EqualityComparer<GKeyValueSet<TKey, TVal1, TVal2, TVal3>>.Default.Equals(set1, set2);
		public static bool operator !=(GKeyValueSet<TKey, TVal1, TVal2, TVal3> set1, GKeyValueSet<TKey, TVal1, TVal2, TVal3> set2) => !(set1 == set2);
	}

	public class GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4> : GKeyValueSet, IEquatable<GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4>>
	{
		public TKey Key { get; set; }
		public TVal1 Val1 { get; set; }
		public TVal2 Val2 { get; set; }
		public TVal3 Val3 { get; set; }
		public TVal4 Val4 { get; set; }

		public GKeyValueSet()
		{
			Key = default;
			Val1 = default;
			Val2 = default;
			Val3 = default;
			Val4 = default;
		}

		public GKeyValueSet(TKey key, TVal1 val1)
		{
			Key = key;
			Val1 = val1;
			Val2 = default;
			Val3 = default;
			Val4 = default;
		}

		public GKeyValueSet(TKey key, TVal1 val1, TVal2 val2) : this(key, val1)
		{
			Val2 = val2;
			Val3 = default;
			Val4 = default;
		}

		public GKeyValueSet(TKey key, TVal1 val1, TVal2 val2, TVal3 val3) : this(key, val1, val2)
		{
			Val3 = val3;
			Val4 = default;
		}

		public GKeyValueSet(TKey key, TVal1 val1, TVal2 val2, TVal3 val3, TVal4 val4) : this(key, val1, val2, val3) => Val4 = val4;

		public bool IsValid => IsValid(Key, Val1, Val2, Val3, Val4);

		public bool TrueForAll
		{
			get
			{
				if (Key is bool || Val1 is bool || Val2 is bool || Val3 is bool || Val4 is bool)
				{
					GTrinaryBoolean KeyState = new GTrinaryBoolean();
					GTrinaryBoolean Val1State = new GTrinaryBoolean();
					GTrinaryBoolean Val2State = new GTrinaryBoolean();
					GTrinaryBoolean Val3State = new GTrinaryBoolean();
					GTrinaryBoolean Val4State = new GTrinaryBoolean();

					if (Key is bool outKeyBool)
						KeyState[true] = outKeyBool;

					if (Val1 is bool outVal1Bool)
						Val1State[true] = outVal1Bool;

					if (Val2 is bool outVal2Bool)
						Val2State[true] = outVal2Bool;

					if (Val3 is bool outVal3Bool)
						Val3State[true] = outVal3Bool;

					if (Val4 is bool outVal4Bool)
						Val3State[true] = outVal4Bool;

					if (KeyState.IsTrueInt > 0 || Val1State.IsTrueInt > 0 || Val2State.IsTrueInt > 0 || Val3State.IsTrueInt > 0 || Val4State.IsTrueInt > 0)
					{
						bool[] states = new bool[] { KeyState[true], Val1State[true], Val2State[true], Val3State[true], Val4State[true] };
						return states.TrueForAll();
					}
				}

				return IsValid;
			}
		}

		public override bool Equals(object obj) => Equals(obj as GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4>);
		public bool Equals(GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4> other) => other != null && EqualityComparer<TKey>.Default.Equals(Key, other.Key) && EqualityComparer<TVal1>.Default.Equals(Val1, other.Val1) && EqualityComparer<TVal2>.Default.Equals(Val2, other.Val2) && EqualityComparer<TVal3>.Default.Equals(Val3, other.Val3) && EqualityComparer<TVal4>.Default.Equals(Val4, other.Val4);

		public override int GetHashCode()
		{
			int hashCode = -275119630;
			hashCode = (hashCode * -1521134295) + EqualityComparer<TKey>.Default.GetHashCode(Key);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal1>.Default.GetHashCode(Val1);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal2>.Default.GetHashCode(Val2);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal3>.Default.GetHashCode(Val3);
			hashCode = (hashCode * -1521134295) + EqualityComparer<TVal4>.Default.GetHashCode(Val4);
			return hashCode;
		}

		public override string ToString() => new string[] { Key.ToString(), Val1.ToString(), Val2.ToString(), Val3.ToString(), Val4.ToString() }.ToPairString();
		public static bool operator ==(GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4> set1, GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4> set2) => EqualityComparer<GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4>>.Default.Equals(set1, set2);
		public static bool operator !=(GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4> set1, GKeyValueSet<TKey, TVal1, TVal2, TVal3, TVal4> set2) => !(set1 == set2);
	}
}