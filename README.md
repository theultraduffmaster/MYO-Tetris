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
The first part of making this game starts with creating the blocks in certain shapes e.g L, J, T, Line, Square, etc. Which in the context of this game we will refer to these as Tetraminos (the official name including our ones that have 5 blocks which technically would not normally fit the definition for a Tetramino as it states it’s a Tetramino is a geometric shape composed of four Squares, connected orthogonally). All shapes appear in all colours so that the player doesn't become too used to one piece being associated with one colour. After making the tetraminos we’ll now make the grid which will define the playable area of the game. Once this is made C# scripts are then made in visual studio – 1 for the tetraminos and one for the game. All the tetraminos will then have a script which defines certain functions like rotations based on keyboard inputs. Functions also were added for features like holding a piece in place using space bar while using the left shift key in combination with other keys (including space bar) will speed the tetraminos up. This is helpful due to the fact that the game is designed around more score being awarded for the faster you place the tetraminos. Score is also awarded for clearing a whole line and include bonus points for clearing multiples lines all the way up to a double points bonus for clearing four lines (Called a Tetris). After that a GameOver scene had to be included which the game moves to when tetraminos fill up the full height of the grid and I included the score here through a globalClass so that it was accessable to all scenes in unity. Once the GameOver scene was made I decided I would include a Main Menu scene with play game and score buttons which moves to a scene for the appropriate button and play game will start the level scene containing the main game and score starts a scene with the latest score and a back button to return to the main menu. The GameOver scene also includes a back to main menu button. Once all these features were added MYO controls were then added to the app so that Wave In equates to the tetramino moving left and Wave out will move right (full list of gestures are provided above). I originally thought cause that I was using C# scripts I could use MYOsharp but it turned out pretty quickly that wouldn't work as the C# behind Unity was version 4 and I could not use async and await at all so instead I used the MYO for unity plugin which right now is giving me NullPointerExceptions. I realized during the development of my game that the controls are also not the clearest to the player so I included a textbox during the main game which lists all the controls on the left side of the game. The last thing I wanted to include in my project was some custom gestures but as of right now they are not included. After some help from Peers I learnt that my tetraminos do not support MYO controls due to the fact they are not actually a part of the scene I'm designing, instead they are randomly generated from the prefabs folder and this thusly renders each tetramino unable to see MYO in order for it to bind itself to the prefab for it. Instead I decided I'd have to bind the MYO to the grid and use the gestures to affect variables within the game itself. After some tinkering with my code I found out that if I bind the MYO game object to the Grid and give methods doing the same things as the keyboard inputs and call those method in my MYO script I could in fact use the Gestures I intended. 
 
## UI Screenshots  
![alt text](https://github.com/theultraduffmaster/MYO-Tetris/blob/master/Architecture/MainMenu.PNG "MainMenu")

![alt text](https://github.com/theultraduffmaster/MYO-Tetris/blob/master/Architecture/Score.PNG "Score")

![alt text](https://github.com/theultraduffmaster/MYO-Tetris/blob/master/Architecture/Rules.PNG "Rules")

![alt text](https://github.com/theultraduffmaster/MYO-Tetris/blob/master/Architecture/Level.PNG "Level")

![alt text](https://github.com/theultraduffmaster/MYO-Tetris/blob/master/Architecture/GameOver.PNG "GameOver")
 
## Known Issues 
* Right now some of the code is causing NullPointerExceptions - if (thalmicMyo.pose != _lastPose) specifically. 
* Score does not reset to 0 after reinitializing the game 
* When the play again button is pressed a warning about multiple Thalmic Hubs will pop up due to the scene being reloaded – it will delete extras ones however it causes focuse to be lost from the screen, there is warning on the right side of the game making this aware to players 

## Workaround for Issues 
* Due to help received in class on the 31st March 2017 I have come to realize the problem with the NullPointerException was due to MYO not being bound to any gameobjects in the scene so the MYO controls cannot for them. Instead the MYO controls will be used to affect variables in the game itself – Fallspeed, etc. 
* The warning message for the Thalmic Hub's duplication has been changed to warn players (a second time) that the focus is moved off the screen and the destroy function was removed due to it destroying valuable resources for allowing the MYO to work which would've caused the many errors in the console when the game is played through the play again button 
 
## New Gestures 
 
Wave In: Speeds a Tetramino up with each swipe until it hits 5 speed 

Wave Out: Slows a Tetramino down with each swipe until it hits 1 is as slow as it goes 

Fingers-Spread:  

Fist:  

Double-Tap: Spawns a new Tetramino 
 
## Update 
 
In the end it turns out that my original Myo script actually works perfectly it just needed to be bound to an object already on the screen. I bound it to my grid and I can control my tetraminos with the Myo so the Gestures will now be based on my original gestures above and not the ones called "New Gestures". 
 
## Purpose of the application 
 
The purpose of this game was to provide a fun way to use the MYO armband in conjunction with a well-known game that is available to near everyone so most people already know what the pull to play this game is. With a twist however as the MYO was to add new and fun ways on interacting with the game for anyone playing this game. Be it One player or Two where one plays the game and the other changes the variables on the fly to make the game harder / easier for them. First off, the main menu has three buttons – play game, rules and score. Play game starts the level scene where the Tetris game is played. Rules tells you how to play Tetris (for those who don't know) and score which shows some highscores. The UI is fairly simple in the level scene, it has a grid visible to the player as the game boundaries and tetraminos cannot go outside them. A tetramino will spawn and move down to the bottom of the screen. All controls all wrote on the left hand side of the grid so players can see them at any time. On the top right is the current score and below that is a warning message informing players that if they choose to replay the game the focus might not be on the screen so don't panic about not controlling the tetraminos.  

## Gestures identified as appropriate for this application  
 
I changed from the original idea of what the MYO controls could do due to the fact the tetraminos would not bind to the MYO for control to using Gestures to change variables in the game. These I believe to be appropriate due to the fact that speeding up and slowing down are seen as direct opposites so they needed to be represented by Gestures that were in of themselves, direct opposites so Wave In and Wave Out were chosen as the gestures for this. I also choose Double-Tap as the spawn a tetramino gesture dues to the fact Fist and Fingers Spread are sometimes detected when they player is not trying to do those Gestures and could cause the game to end prematurely. With the revelation that my original Gestures can work I have opted for them over the "New Gestures" as they made the game more interactive so Waving In (As in left) moves a tetramino left and Waving out will move it right. Fingers Spread and Fist to me are natural opposites as well so they fitted the rotate clockwise and counter clockwise methods which are polar opposites. Double-tap speeds up the tetramino which made sense to use as speed up for a gesture as it implies getting to something fast like clicking on a link. 
 
## Hardware used in creating the application  
 
For this project I solely used the MYO armband as the hardware to implement within my chosen softwares of Unity (chosen for Graphics and game objects with bindings to C# scripts) and Visual Studio (chosen due to my knowledge of it being a very good C# IDE). When considering the project I believed the MYO to be the best fit for playing tetris as Kinect would have people un-necssarily wasting energy trying to get the gestures going just to move the tetraminos slightly. Cortana didn't seem a right fit either as its speech recognition is off at the best of times and a piece turning the wrong way cause Cortana didn't understand you would be furstrating to no end. And when you're already shouting at Cortana this solution wouldn't keep blood pressure down. Leap motion was the only other sensible choice but in the end MYO was the option I opted for due to the gestures already being set up and easily recognised made it the best choice by far.
 
## Architecture for the solution  
![alt text](https://github.com/theultraduffmaster/MYO-Tetris/blob/master/Architecture/Architecture.png "Architecture")


## Conclusions & Recommendations 
 
In conclusion, using the MYO armband with Unity has taught me a lot about Unity and most of that is it's strictness when it comes to bindings game objects and prefabs. I would recommend to anyone trying this in the future to only use MYO controls on objects that are already created in the scene as I could not find a solution for binding it to randomly generated game objects but in the end of my research it turned it the MYO didn't need to be bound to any randomly generated objects instead it needed to just be bound to an object in the scene already. When looking back at my main aim for this project and taking into consideration what I've learnt now I would say that MYO and Unity are interactable but I would not recommend it over anything else you could pair the MYO with. Before settling on Unity as something I wanted to learn I was looking at using Windows Forms with MYOsharp to make this same game and while the MYO gestures seemed easier to pull off in that, the visuals of it were not nearly as good as Unity's and so it depends on what you'd want in your own application – a nice looking project with a tougher way to use MYO (in my opinion) or an okay looking project with an easier way to implement MYO (Again, my opinion).

## References 
1. Tutorial for learning how to make Tetris in Unity - https://www.youtube.com/watch?v=aurEgWxDfQQ  
1. ThalmicLabs Myo examples for Unity - https://github.com/thalmiclabs/myo-unity  
1. If it gets implemented: Custom Gestures - http://answers.unity3d.com/questions/417026/need-advice-on-custom-finger-gesture-recognition.html and http://depts.washington.edu/madlab/proj/dollar/pdollar.html  
1. MYOSharp - https://github.com/tayfuzun/MyoSharp  
1. Myo SDK - https://developer.thalmic.com/docs/api_reference/platform/the-sdk.html  
 
