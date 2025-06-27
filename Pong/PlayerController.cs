using System;
using static SDL2.SDL;

static class Player{
	public static  void GoUp(){
		if (Program.player.rect.y > 1){
			Program.player.rect.y -= 10;
		}
	}
	public static void GoDown(){
		if (Program.player.rect.y < 400){
			Program.player.rect.y += 10;
		}
	}
}
