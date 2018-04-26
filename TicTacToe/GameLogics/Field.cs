namespace GameLogics {
	public sealed class Field {
		public Cell[,] Cells { get; }

		public Field(int size) {
			Guard.NonLess(size, 1);
			Cells = new Cell[size, size];
			for ( var i = 0; i < Cells.GetLength(0); i++ ) {
				for ( var j = 0; j < Cells.GetLength(1); j++ ) {
					Cells[i, j] = new Cell();
				}
			}
		}

		Field(Cell[,] cells) {
			Cells = cells;
		}

		internal Field ChangeOwner(int x, int y, string owner) {
			var newField = Clone();
			newField.Cells[x, y] = new Cell(owner);
			return newField;
		}

		Field Clone() {
			var cells = Cells.Clone() as Cell[,];
			var field = new Field(cells);
			return field;
		}
	}
}
