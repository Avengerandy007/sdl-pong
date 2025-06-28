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

		SetPosition();

		if (!Colliding() && (float)stopwatch.Elapsed.TotalMilliseconds > 10){
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
		int factor = r.Next(0, 2);
		if (position.Y == 479 || position.Y == 1){
			direction = new Vector2(-direction.X, direction.Y);
		}else direction = new Vector2(-direction.X, -direction.Y + factor);

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
			if (ballYbegin > Program.player.rect.y && ballYend < (Program.player.rect.y + Program.player.rect.h)){
				return true;
			}
			return false;
		}else if (ballXend == 550){
			if(ballYbegin > Program.enemy.rect.y && ballYend < (Program.enemy.rect.y + Program.enemy.rect.h)){
				return true;
			}
			return false;
		}else if (ballYbegin == 1){
			return true;
		}else if (ballYend == 479) return true;

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
