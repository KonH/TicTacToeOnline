using System;

namespace ConsoleClient {
	class MainClass {
		public static void Main(string[] args) {
			var ctrl = new GameController(3, "X", "O");
			ctrl.Run();
			Console.ReadKey();
		}
	}
}
