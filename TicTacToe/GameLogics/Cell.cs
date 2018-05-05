namespace GameLogics {
	public sealed class Cell {
		public string Owner { get; set; }

		public Cell() {}

		public Cell(string owner) {
			Owner = owner;
		}
	}
}
