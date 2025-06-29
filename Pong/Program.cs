class Program{
	
	public static Window GameWindow = new Window();

	public static int playerScore = 0;
	public static int enemyScore = 0;

	public static Pallet player = new Pallet(true);
	public static Pallet enemy = new Pallet(false);

	public static ScoreDisplay playerScoredisplay = new ScoreDisplay();
	public static ScoreDisplay enemyScoredisplay = new ScoreDisplay();


	public static Ball ball = new Ball();

	public static void Main(){
		Console.WriteLine("Hey");

		GameWindow.Setup();
		ScoreDisplay.MainEntry();
		playerScoredisplay.Setup(playerScore, true);
		enemyScoredisplay.Setup(enemyScore, false);
		Enemy.Entry();

		GameWindow.MainLoop();

		playerScoredisplay.Exit();
		enemyScoredisplay.Exit();
		ScoreDisplay.MainExit();
		return;
	}
}

