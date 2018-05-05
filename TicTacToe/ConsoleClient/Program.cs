using System;
using System.Linq;

namespace ConsoleClient {
	class MainClass {
		// url to use NetworkGameController, LocalGameController instead
		public static void Main(string[] args) {
			var ctrl = CreateController(args.FirstOrDefault());
			ctrl.Run();
			Console.ReadKey();
		}

		static IGameController CreateController(string url) {
			IGameController ctrl = null;
			if ( !string.IsNullOrEmpty(url) ) {
				Console.WriteLine($"Url: '{url}'");
				ctrl = new NetworkGameController(url);
			} else {
				ctrl = new LocalGameController(3, "X", "O");
			}
			Console.WriteLine($"{ctrl.GetType()} is used.");
			return ctrl;
		}
	}
}
