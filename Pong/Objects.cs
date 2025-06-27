using static SDL2.SDL;
using static SDL2.SDL_ttf;

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

static class ObjectDisplayLogic{
	public static void Render(){
		rPallet(ref Program.player.rect, ref Program.enemy.rect);
	}

	static void rPallet(ref SDL_Rect player, ref SDL_Rect enemy){
		SDL_SetRenderDrawColor(Program.GameWindow.renderer, 255, 255, 255, 255);

		SDL_RenderFillRect(Program.GameWindow.renderer, ref player);
		SDL_RenderDrawRect(Program.GameWindow.renderer, ref player);

		SDL_RenderFillRect(Program.GameWindow.renderer, ref enemy);
		SDL_RenderDrawRect(Program.GameWindow.renderer, ref enemy);

		SDL_SetRenderDrawColor(Program.GameWindow.renderer, 0, 0, 0, 255);
	}
}
