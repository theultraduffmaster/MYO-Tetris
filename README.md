# Gesture Based UI Project 
### G00318025 
### Declan Duffy 
 
 
## Description 
My project for this module will be to use the Myo Armband in the creation process of a Tetris game and use it as its  
main controller but it will also support normal Keyboard inputs. Made in C# using Visual Studio and utilizing Unity for graphics. 
 
 
## Main Aim 
To make a game fully synced (if possible) with the Myo Armband and have all (if not most) gestures doing something  
within the game. For example: Wave In will move a block to the left while Wave Out will move it to the right 
 
 
## Gestures 
These are subject to change 

Wave In: Move block left 

Wave Out: Move block right 

Fingers-Spread: Rotate right 

Fist: Rotate left 

Double-Tap: Increase Speed 
 
 
## Development 
The first part of making this game starts with creating the blocks in certain shapes e.g L, J, T, Line, Square, etc. Which in the context of this game we will refer to these as Tetraminos (the official name including our ones that have 5 blocks which technically would not normally fit the definition for a Tetramino as it states it’s a Tetramino is a geometric shape composed of four Squares, connected orthogonally). All shapes appear in all colours so that the player doesn't become too used to one piece being associated with one colour. After making the tetraminos we’ll now make the grid which will define the playable area of the game. Once this is made C# scripts are then made in visual studio – 1 for the tetraminos and one for the game. All the tetraminos will then have a script which defines certain functions like rotations based on keyboard inputs. Functions also were added for features like holding a piece in place using space bar while using the left shift key in combination with other keys (including space bar) will speed the tetraminos up. This is helpful due to the fact that the game is designed around more score being awarded for the faster you place the tetraminos. Score is also awarded for clearing a whole line and include bonus points for clearing multiples lines all the way up to a double points bonus for clearing four lines (Called a Tetris). After that a GameOver scene had to be included which the game moves to when tetraminos fill up the full height of the grid and I included the score here through a globalClass so that it was accessable to all scenes in unity. Once the GameOver scene was made I decided I would include a Main Menu scene with play game and score buttons which moves to a scene for the appropriate button and play game will start the level scene containing the main game and score starts a scene with the latest score and a back button to return to the main menu. The GameOver scene also includes a back to main menu button. Once all these features were added MYO controls were then added to the app so that Wave In equates to the tetramino moving left and Wave out will move right (full list of gestures are provided above). I originally thought cause that I was using C# scripts I could use MYOsharp but it turned out pretty quickly that wouldn't work as the C# behind Unity was version 4 and I could not use async and await at all so instead I used the MYO for unity plugin which right now is giving me NullPointerExceptions. I realized during the development of my game that the controls are also not the clearest to the player so I included a textbox during the main game which lists all the controls on the left side of the game. The last thing I wanted to include in my project was some custom gestures but as of right now they are not included. 
 
## Known Issues 
* Right now some of the code is causing NullPointerExceptions - if (thalmicMyo.pose != _lastPose) specifically. 
* Score does not reset to 0 after reinitializing the game 
* When the play again button is pressed a warning about multiple Thalmic Hubs will pop up due to the scene being reloaded – it will delete extras ones however it causes focuse to be lost from the screen, there is warning on the right side of the game making this aware to players 
 
## References 
1. Tutorial for learning how to make Tetris in Unity - https://www.youtube.com/watch?v=aurEgWxDfQQ  
1. ThalmicLabs Myo examples for Unity - https://github.com/thalmiclabs/myo-unity  
1. If it gets implemented: Custom Gestures - http://answers.unity3d.com/questions/417026/need-advice-on-custom-finger-gesture-recognition.html and http://depts.washington.edu/madlab/proj/dollar/pdollar.html  
1. MYOSharp - https://github.com/tayfuzun/MyoSharp  
1. Myo SDK - https://developer.thalmic.com/docs/api_reference/platform/the-sdk.html  
 
