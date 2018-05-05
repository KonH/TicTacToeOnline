using System.Collections.Generic;

namespace GameLogics {
	public sealed class Field {
		public int        Size     { get; set; }
		public List<Cell> RawCells { get; set; }

		// Required for deserialization
		public Field() { }

		public Field(int size) {
			Guard.NonLess(size, 1);
			Size = size;
			RawCells = new List<Cell>(size * size);
			for ( var i = 0; i < RawCells.Capacity; i++ ) {
				RawCells.Add(new Cell());
			}
		}

		Field(int size, List<Cell> cells) {
			Size     = size;
			RawCells = cells;
		}

		internal Field ChangeOwner(int x, int y, string owner) {
			var newField = Clone();
			newField.SetCellAt(x, y, new Cell(owner));
			return newField;
		}

		Field Clone() {
			var cells = new List<Cell>(RawCells.Count);
			foreach ( var cell in RawCells ) {
				cells.Add(new Cell(cell.Owner));
			}
			var field = new Field(Size, cells);
			return field;
		}

		int GetFlatIndex(int x, int y) {
			return y * Size + x;
		}

		public Cell GetCellAt(int x, int y) {
			if ( (x >= 0) && (y >= 0) && (Size > x) && (Size > y) ) {
				return RawCells[GetFlatIndex(x, y)];
			}
			return null;
		}

		void SetCellAt(int x, int y, Cell cell) {
			RawCells[GetFlatIndex(x, y)] = cell;
		}
	}
}
