### SDL-PONG
Is supposed to be a clone of the hit classic Pong made entirely in SDL2 with C#.

### Build instructions
If you want to try it out you can build the code using dotnet build or publish. You will need to add the "Sayers.SDL2.Core" NuGet package to the project. This contains all the bindings and dependencies for SDL2.
ALternatively, you can download the SDL2-CS bindings and the official SDL2 dlls (if you want to do this you only need the main one and SDL_ttf for displaying text).

If you wish "peruse" the code I warn you that it is not documented at all and you are on your own.

#### Playing
You can press the Up or Down arrow keys to control your pallet and 'r' to reset the positions of all the objects.

#### !!Note: It sadly only works on Windows because I am too lazy to make it search for fonts on all systems. The good part is that isn't hard to fix, you can just go to Objects.cs, in the ScoreDisplay class and change the font path to your desired one.
