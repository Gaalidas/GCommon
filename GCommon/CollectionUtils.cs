using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GCommon.Collections;

namespace GCommon
{
	public static class CollectionUtils
	{
		#region Collection Extensions
		/// <summary>Returns the last index in the <see cref="IGListNew"/> collection.</summary>
		public static int GetLastIndex(this IList input) => input.Count - 1;

		/// <summary>Finds the last index in the <see cref="IGListNew"/> collection.</summary>
		/// <param name="input">A valid collection.</param>
		/// <param name="output">The index called for.</param>
		/// <returns>True if the input collection is not empty.</returns>
		public static bool GetLastIndex(this IList input, out int output) => (output = input.GetLastIndex()) > 0;

		/// <summary>Removes the last item in the <see cref="IGListNew"/> collection.</summary>
		/// <param name="output">The resulting <see cref="IGListNew"/> collection.</param>
		/// <returns>True if successful.</returns>
		public static bool RemoveLastIndex(this IList input, out IList output)
		{
			output = input;
			output.RemoveAt(input.GetLastIndex());
			return output.Count < input.Count;
		}

		/// <summary>Returns the last valid index position in the array.</summary>
		public static int LastIndex<T>(this T[] inputArray) => inputArray.Length - 1;

		/// <summary>Returns the last valid index position in the list.</summary>
		public static int LastIndex<T>(this List<T> inputList) => inputList.Count - 1;

		/// <summary>Returns whether or not the item exists in the provided array. The first matching entry returns the value.</summary>
		public static bool Contains<T>(this T item, T[] inputArray)
		{
			for (int i = 0; i < inputArray.Length; i++)
			{
				if (Equals(inputArray[i], item))
					return true;
			}

			return false;
		}

		/// <summary>Discovers if the item is in the input array and returns true, outputting the number of times it was found.</summary>
		public static bool Contains<T>(this T item, T[] inputArray, out int numInstances)
		{
			numInstances = 0;

			for (int i = 0; i < inputArray.Length; i++)
			{
				if (Equals(inputArray[i], item))
					numInstances++;
			}

			return numInstances > 0;
		}

		/// <summary>Discovers if the item is in the input array and returns true, outputting the number of times it was found and the indices of the items in a new array.</summary>
		public static bool Contains<T>(this T item, T[] inputArray, out int numInstances, out int[] objectIndexArray)
		{
			numInstances = 0;
			List<int> indices = new List<int>();

			for (int i = 0; i < inputArray.Length; i++)
			{
				if (Equals(inputArray[i], item))
				{
					numInstances++;
					indices.Add(i);
				}
			}

			objectIndexArray = indices.ToArray();
			return numInstances > 0;
		}

		/// <summary>Returns the number of items in the array. Same as <see cref="Array.Length"/>.</summary>
		public static int Count<T>(this T[] inputArray) => inputArray.Length;

		/// <summary>Determines if the provided <see cref="GList{T}"/> objects are equal in size.  Checks two inputs.</summary>
		public static bool ListsEqual<T1, T2>(GList<T1> list1, GList<T2> list2) => list1.Count == list2.Count;

		/// <summary>Determines if the provided <see cref="GList{T}"/> objects are equal in size.  Checks three inputs.</summary>
		public static bool ListsEqual<T1, T2, T3>(GList<T1> list1, GList<T2> list2, GList<T3> list3) => ListsEqual(list1, list2) && ListsEqual(list1, list3);

		/// <summary>Determines if the provided <see cref="GList{T}"/> objects are equal in size.  Checks four inputs.</summary>
		public static bool ListsEqual<T1, T2, T3, T4>(GList<T1> list1, GList<T2> list2, GList<T3> list3, GList<T4> list4) => ListsEqual(list1, list2, list3) && ListsEqual(list1, list4);

		/// <summary>Determines if the provided <see cref="GList{T}"/> objects are equal in size.  Checks five inputs.</summary>
		public static bool ListsEqual<T1, T2, T3, T4, T5>(GList<T1> list1, GList<T2> list2, GList<T3> list3, GList<T4> list4, GList<T5> list5) => ListsEqual(list1, list2, list3, list4) && ListsEqual(list1, list5);

		/// <summary>Determines if the provided <see cref="List{T}"/> objects are equal in size.  Checks two inputs.</summary>
		public static bool ListsEqual<T1, T2>(List<T1> list1, List<T2> list2) => list1.Count == list2.Count;

		/// <summary>Turns an <see cref="Array"/> of type T into a <see cref="GList{T}"/>.</summary>
		public static GList<T> ToGList<T>(this T[] inputArray)
		{
			GList<T> output = GList.Empty<T>();

			for (int i = 0; i < inputArray.Length; i++)
				output.Add(inputArray[i]);

			return output;
		}

		/// <summary>Turns a <see cref="List{T}"/> into a <see cref="GList{T}"/>.</summary>
		public static GList<T> ToGList<T>(this List<T> inputList)
		{
			GList<T> output = default;

			foreach (T item in inputList)
				output.Add(item);

			return output;
		}

		/// <summary>Turns a <see cref="Dictionary{TKey, TValue}"/> into a <see cref="GDict{TKey, TVal1}"/>.</summary>
		public static GDict<T1, T2> ToGDict<T1, T2>(this Dictionary<T1, T2> input)
		{
			GDict<T1, T2> output = new GDict<T1, T2>();

			foreach (KeyValuePair<T1, T2> pair in input)
				output.Add(pair.Key, pair.Value);

			return output;
		}
		#endregion

		#region ToTypeArray
		public static string[] ToStringArray(this char[] charArray) => charArray.Cast<string>().ToArray();
		#endregion
	}
}