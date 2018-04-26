namespace GameLogics {
	public sealed class Cell {
		public string Owner { get; }

		public Cell() {}

		public Cell(string owner) {
			Owner = owner;
		}
	}
}
