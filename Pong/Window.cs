using static SDL2.SDL;

class Window{

	bool running;

	public IntPtr window;
	public IntPtr renderer;

	public void Setup(){
		running = true;

		if(SDL_Init(SDL_INIT_VIDEO) < 0){
			Console.WriteLine($"There was a problem initialising sdl: {SDL_GetError()}");
		}

		window = SDL_CreateWindow("Budget Pong", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, 640, 480, SDL_WindowFlags.SDL_WINDOW_SHOWN);

		if (window == IntPtr.Zero) Console.WriteLine($"There was a problem initialising the window: {SDL_GetError()}");

		renderer = SDL_CreateRenderer(window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

		if (renderer == IntPtr.Zero) Console.WriteLine($"There was a problem initialising the renderer {SDL_GetError()}");

	}

	public void MainLoop(){
		while (running){
			Render();
			PollEvents();
			Program.ball.Moving();
		}
		Deconstruct();
	}

	void Render(){
		SDL_RenderClear(renderer);
		ObjectDisplayLogic.Render();
		SDL_RenderPresent(renderer);
	}

	void PollEvents(){
		while(SDL_PollEvent(out SDL_Event e) == 1){
			switch(e.type){
				case SDL_EventType.SDL_QUIT:
					running = false;
				break;

				case SDL_EventType.SDL_KEYDOWN:
					if(e.key.keysym.sym == SDL_Keycode.SDLK_UP){
						Player.GoUp();
					}
					else if(e.key.keysym.sym == SDL_Keycode.SDLK_DOWN){
						Player.GoDown();
					}else if (e.key.keysym.sym == SDL_Keycode.SDLK_r){
						running = false;
						Deconstruct();
						Program.GameWindow = new Window();
						Program.Main();
						return;
					}
				break;
			}
		}
	}

	void Deconstruct(){
		SDL_DestroyWindow(window);
		SDL_DestroyRenderer(renderer);
		SDL_Quit();
		return;
	}
}
