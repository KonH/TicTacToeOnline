using System;
using System.Collections.Generic;

namespace GameLogics {
	static class Guard {
		public static void NotNull(object value, string msg = "") {
			if ( value == null ) {
				throw new ArgumentException(msg);
			}
		}

		public static void NotNullOrEmpty(string str, string msg = "") {
			if ( string.IsNullOrEmpty(str) ) {
				throw new ArgumentException(msg);
			}
		}

		public static void NonNegative(int value, string msg = "") {
			if ( value < 0 ) {
				throw new ArgumentException(msg);
			}
		}

		public static void NonNegative(int[] values, string msg = "") {
			foreach ( var value in values ) {
				NonNegative(value, msg);
			}
		}

		public static void NonNegative(params int[] values) {
			NonNegative(values, "");
		}

		public static void NonLess(int value, int limit, string msg = "") {
			if ( value < limit ) {
				throw new ArgumentException(msg);
			}
		}

		public static void NoDuplicates<T>(ICollection<T> items, string msg = "") where T:IComparable<T> {
			var tempSet = new HashSet<T>();
			foreach ( var item in items ) {
				var isItemAdded = tempSet.Add(item);
				if ( !isItemAdded ) {
					throw new ArgumentException(msg);
				}
			}
		}

		public static void Success(bool condition, string msg = "") {
			if ( !condition ) {
				throw new ArgumentException(msg);
			}
		}
	}
}
