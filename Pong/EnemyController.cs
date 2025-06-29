class Enemy{
	static System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
	public static void Entry(){
		stopwatch.Start();
	}
	public static void TrackBall(){
		if (Program.ball.direction.Y < 0 && stopwatch.Elapsed.TotalMilliseconds > 50 && Program.enemy.rect.y > 1){
			Program.enemy.rect.y -= 5;
			stopwatch.Restart();
		}else if (Program.ball.direction.Y > 0 && stopwatch.Elapsed.TotalMilliseconds > 50 && Program.enemy.rect.y < 400){
			Program.enemy.rect.y += 5;
			stopwatch.Restart();
		}
	}
}
