using static SDL2.SDL;
using static SDL2.SDL_ttf;
using System.Numerics;

class Pallet{
	
	public SDL_Rect rect = new SDL_Rect{w = 40, h = 80}; 
	
	public static SDL_Color color = new SDL_Color{r = 255, g = 255, b = 255};


	public Pallet(bool isPlayer){
		if (isPlayer){
			rect.x = 50;
		}else{
			rect.x = 550;
		}
		SetRectY();
		SDL_RenderFillRect(Program.GameWindow.renderer, ref rect);
	}

	public void SetRectY(){
		rect.y = (480 - rect.h) / 2;
	}

}

class Ball{
	public SDL_Rect rect = new SDL_Rect{x = (640 - 30) / 2, y = (480 - 30) / 2, w = 30, h = 30};

	int speed = 1;

	public Vector2 originalPos = new Vector2(((640 - 30) / 2), ((480 - 30) / 2));

	public Vector2 direction;
	Vector2 position;
	

	int ballXbegin;
	int ballXend;
		
	int ballYbegin;
	int ballYend;

	System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
	System.Diagnostics.Stopwatch reflectStop = new System.Diagnostics.Stopwatch();

	public Ball(){
		direction = new Vector2(-1, 0);
		direction = Vector2.Normalize(direction);
		position = new Vector2(rect.x, rect.y);

		ballXbegin = rect.x;
		ballXend = rect.x + rect.w;

		ballYbegin = rect.y;
		ballYend = rect.y + rect.h;

		stopwatch.Start();
		reflectStop.Start();

	}


	public void Moving(){

		IfGoal();
		SetPosition();


		if (!Colliding() && (float)stopwatch.Elapsed.TotalMilliseconds > 5){
			position += direction * speed;
			stopwatch.Restart();
		}else if (Colliding()){
			if ((float)reflectStop.Elapsed.TotalMilliseconds > 100){
				reflectStop.Restart();
				Reflect();
			}
			position += direction * speed;
		}
	}

	void Reflect(){
		Random r = new Random();
		int factor = r.Next(1, 2);
		Random plusMinus = new Random();
		if (position.Y >= 449 || position.Y <= 31){
			direction = new Vector2(direction.X, -direction.Y);
		}else direction = plusMinus.Next(0, 1) == 0 ? new Vector2(-direction.X, direction.Y + factor) : new Vector2(-direction.X, direction.Y - factor);

		direction = Vector2.Normalize(direction);
	}

	void SetPosition(){
		rect.x = (int)position.X;
		rect.y = (int)position.Y;

		ballXbegin = rect.x;
		ballXend = rect.x + rect.w;

		ballYbegin = rect.y;
		ballYend = rect.y + rect.h;

	}

	bool Colliding(){
		if (ballXbegin == 90){
			if (SemiCollision(Program.player.rect)){
				return true;
			}
			return false;
		}else if (ballXend == 550){
			if(SemiCollision(Program.enemy.rect)){
				return true;
			}
			return false;
		}else if (ballYbegin == 1){
			return true;
		}else if (ballYend == 479) return true;

		return false;
	}

	bool SemiCollision(SDL_Rect rect){
		if (ballYbegin < rect.y && ballYbegin > rect.y + rect.h) return true;
		if (ballYend > rect.y && ballYend < rect.y + rect.h) return true;
		return false;
	}

	void IfGoal(){
		if (rect.x <= 1){
			direction = new Vector2(1, 0);
			Program.enemyScore++;
			position = originalPos;
			Program.player.SetRectY();
			Program.enemy.SetRectY();
		}else if (rect.x >= 600){
			direction = new Vector2(-1, 0);
			Program.playerScore++;
			position = originalPos;
			Program.player.SetRectY();
			Program.enemy.SetRectY();
		}
	}


}

class ScoreDisplay{

	public static IntPtr font = TTF_OpenFont("C:/Windows/Fonts/comic.ttf", 40);
	public IntPtr surface;
	public static SDL_Color white = new SDL_Color{r = 255, g = 255, b = 255, a = 255};
	public IntPtr texture;
	public SDL_Rect rect = new SDL_Rect{y = 350, w = 100, h = 50};

	public static void MainEntry(){
		TTF_Init();
	}

	public static void MainExit(){
		TTF_CloseFont(font);
		TTF_Quit();
	}

	public void Setup(int score, bool isPlayer){
		if (isPlayer){
			rect.x = ((640 - 30) / 2) - 150;
		}else rect.x = ((640 - 30) / 2) + 50;

	}

	public void Exit(){
		SDL_DestroyTexture(texture);
	}


}

static class ObjectDisplayLogic{
	public static void Render(){
		SDL_SetRenderDrawColor(Program.GameWindow.renderer, 255, 255, 255, 255);

		rPallet(ref Program.player.rect, ref Program.enemy.rect);
		rBall(ref Program.ball.rect);
		rText();

		SDL_SetRenderDrawColor(Program.GameWindow.renderer, 0, 0, 0, 255);
	}

	static void rPallet(ref SDL_Rect player, ref SDL_Rect enemy){

		SDL_RenderFillRect(Program.GameWindow.renderer, ref player);
		SDL_RenderDrawRect(Program.GameWindow.renderer, ref player);

		SDL_RenderFillRect(Program.GameWindow.renderer, ref enemy);
		SDL_RenderDrawRect(Program.GameWindow.renderer, ref enemy);

	}

	static void rText(){
		Program.playerScoredisplay.surface = TTF_RenderText_Solid(ScoreDisplay.font, $"Score: {Program.playerScore}", ScoreDisplay.white);
		SDL_DestroyTexture(Program.playerScoredisplay.texture);
		Program.playerScoredisplay.texture = SDL_CreateTextureFromSurface(Program.GameWindow.renderer, Program.playerScoredisplay.surface);
		SDL_FreeSurface(Program.playerScoredisplay.surface);

		Program.enemyScoredisplay.surface = TTF_RenderText_Solid(ScoreDisplay.font, $"Score: {Program.enemyScore}", ScoreDisplay.white);
		SDL_DestroyTexture(Program.enemyScoredisplay.texture);
		Program.enemyScoredisplay.texture = SDL_CreateTextureFromSurface(Program.GameWindow.renderer, Program.enemyScoredisplay.surface);
		SDL_FreeSurface(Program.enemyScoredisplay.surface);


		SDL_RenderCopy(Program.GameWindow.renderer, Program.playerScoredisplay.texture, IntPtr.Zero, ref Program.playerScoredisplay.rect);
		SDL_RenderCopy(Program.GameWindow.renderer, Program.enemyScoredisplay.texture, IntPtr.Zero, ref Program.enemyScoredisplay.rect);
	}

	static void rBall(ref SDL_Rect ball){
		SDL_RenderFillRect(Program.GameWindow.renderer, ref ball);
		SDL_RenderDrawRect(Program.GameWindow.renderer, ref ball);
	}
}
