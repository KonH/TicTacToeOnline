namespace GameLogics {
	public sealed class Field {
		public int Size { get; }

		Cell[,] Cells { get; }

		public Field(int size) {
			Guard.NonLess(size, 1);
			Size = size;
			Cells = new Cell[size, size];
			for ( var i = 0; i < size; i++ ) {
				for ( var j = 0; j < size; j++ ) {
					SetCellAt(i, j, new Cell());
				}
			}
		}

		Field(int size, Cell[,] cells) {
			Size = size;
			Cells = cells;
		}

		internal Field ChangeOwner(int x, int y, string owner) {
			var newField = Clone();
			newField.SetCellAt(x, y, new Cell(owner));
			return newField;
		}

		Field Clone() {
			var cells = Cells.Clone() as Cell[,];
			var field = new Field(Size, cells);
			return field;
		}

		public Cell GetCellAt(int x, int y) {
			if ( (x >= 0) && (y >= 0) && (Size > x) && (Size > y) ) {
				return Cells[y, x];
			}
			return null;
		}

		void SetCellAt(int x, int y, Cell cell) {
			Cells[y, x] = cell;
		}
	}
}
