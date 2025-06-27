using System;

class Program{
	
	public static Window GameWindow = new Window();

	public static Pallet player = new Pallet(true);
	public static Pallet enemy = new Pallet(false);

	static void Main(){
		Console.WriteLine("Hey");
		GameWindow.Setup();
		GameWindow.MainLoop();
	}
}

