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

	void SetRectY(){
		rect.y = (480 - rect.h) / 2;
	}

}

class Ball{
	public SDL_Rect rect = new SDL_Rect{x = (640 - 30) / 2, y = (480 - 30) / 2, w = 30, h = 30};

	int speed = 1;

	Vector2 direction;
	Vector2 position;
	

	int ballXbegin;
	int ballXend;
		
	int ballYbegin;
	int ballYend;

	System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

	public Ball(){
		direction = new Vector2(-1, 0);
		position = new Vector2(rect.x, rect.y);

		ballXbegin = rect.x;
		ballXend = rect.x + rect.w;

		ballYbegin = rect.y;
		ballYend = rect.y + rect.h;

		stopwatch.Start();

	}


	public void Moving(){

		position.X = rect.x;
		position.Y = rect.y;

		if (!Colliding() && (float)stopwatch.Elapsed.TotalMilliseconds > 10){
			position += direction * speed;
			SetPosition();
			stopwatch.Restart();
		}
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
		return false;
	}


}

static class ObjectDisplayLogic{
	public static void Render(){
		SDL_SetRenderDrawColor(Program.GameWindow.renderer, 255, 255, 255, 255);

		rPallet(ref Program.player.rect, ref Program.enemy.rect);
		rBall(ref Program.ball.rect);

		SDL_SetRenderDrawColor(Program.GameWindow.renderer, 0, 0, 0, 255);
	}

	static void rPallet(ref SDL_Rect player, ref SDL_Rect enemy){

		SDL_RenderFillRect(Program.GameWindow.renderer, ref player);
		SDL_RenderDrawRect(Program.GameWindow.renderer, ref player);

		SDL_RenderFillRect(Program.GameWindow.renderer, ref enemy);
		SDL_RenderDrawRect(Program.GameWindow.renderer, ref enemy);

	}

	static void rBall(ref SDL_Rect ball){
		SDL_RenderFillRect(Program.GameWindow.renderer, ref ball);
		SDL_RenderDrawRect(Program.GameWindow.renderer, ref ball);
	}
}
