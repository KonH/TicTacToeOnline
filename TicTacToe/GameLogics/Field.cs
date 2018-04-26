namespace GameLogics {
	public sealed class Field {
		public Cell[,] Cells { get; }

		public Field(int size) {
			Guard.NonLess(size, 1);
			Cells = new Cell[size, size];
		}
	}
}
