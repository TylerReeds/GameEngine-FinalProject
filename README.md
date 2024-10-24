# GameEngine-FinalProject

Description of the game:

It is a strategic 2D tile-based action game where you travel through a series of procedurally generated levels while slaying enemies. There will be different kinds of tiles where some can heal you and some others burn you, you will have to navigate between the different tiles while also planning how to attack your enemies.  You will move around left, right, up and down between each tile and enemies will move when you move. You can undo/redo any movement action you perform while the enemy does not move, which helps play into the strategy of navigating these levels, defeating enemies and surviving. The player is also able to remap their controls to best fit their playstyle and comfort. 

------

Information: 

![GameWorldBuilder](https://github.com/user-attachments/assets/758254c6-4e61-4cf9-8ece-19b921792bb6)

The above screenshot shows an example of a procedurally generated level and in the inspector, we can see that we can change the seed and also the chance each of the tiles can spawn. This helps add even more randomization to what the levels can be. 

------

Tyler Reeds - 100870679 - Programmer

Responsibilities 
- Input Manager(Singleton)
- Custom Button Mapping(Command)
- Procedurally Generated Levels(Factory)
- SFX that triggers based on death, win or kill(Observer)
- PlayerController
- Made SFX
- Made ReadMe File 

50% Contribution

Input Manager(Singleton)- 

![InputManager(Singleton) drawio](https://github.com/user-attachments/assets/e993e304-b95f-4743-bb9f-2b733c47486e)

Explanation: 

We used an input manager that is able to keep track of the player inputs. This sets a command based on what input key is pressed to manage the inputs for movement and attacks. It also has a handle input function to check what key is pressed and to execute the corresponding action. This allows us to have a single space where the inputs are managed to keep things organized and simple. 

Custom Button Mapping(Command)-

![Keybindings(Command) drawio](https://github.com/user-attachments/assets/3aaab762-c401-46c1-9ab1-b058060d693b)

Explanation:

We used the command pattern to handle custom button remapping for the controls of the game. This uses a Keybindings scriptable object that has a switch case statement for different cases, Up, Down, Left, Right, Attack, and Pause. We also have 4 scripts for each MoveDirectionCommand and an ICommand script that stores an execute function. This allows us to have custom button remapping from the unity inspector using the scriptable object. 

Procedurally Generated Levels(Factory)-

![WorldBuilder(Factory) drawio](https://github.com/user-attachments/assets/dfec4358-1cca-4e73-9617-2db8c1da99ce)

Explanation:

We implemented a factory pattern to procedurally generate all of our levels, we have a worldFactory interface that has a seed, gridsize and createtile functions that will help to build our level of 10x10 tiles using random tiles each time. The pattern also stores a chance variable that can be changed in the inspector for the percent chance that each tile can appear. It will then create a 10x10 tile of random colours/types of tiles. This uses a seed, so if you change the seed you can regenerate another level. This is helpful to our game as we want procedurally generated levels with a random order of tiles for the player to navigate and their game experience will never be the exact same as before. 

SFXObserver(Observer)- 

![SFXObserver(Observer) drawio](https://github.com/user-attachments/assets/c57cfcb0-c38b-4f14-a89d-53a36f6b5b9c)

Explanation:

We implemented a simple observer to play some SFX when some events happen in the game. We wanted an SFX to play if the player beats the level if the player dies and if the player kills an enemy. The observer looks at the GameManager script  to see if the beatlevel function is called, and then it will play the win SFX, it also looks at the PlayerController script to see if the playerDiedEvent is called or the enemyKilledEvent is called and then it will play the player died SFX and enemy killed SFX. 

------

Jayden Deller-Daoust - 100847599 - Programmer

Responsibilities 
- Game Manager(Singleton)
- Undo/Redo(Command)
- Different Kinds of Tiles(Factory)
- Checks if all enemies are cleared before loading the next level(Observer)
- Tiles
- Enemies 

50% Contribution

Game Manager(Singleton)

Explanation: 

Using a game manager allows us to simplify the overarching game mechanics of our project that do not rely on specific utilities from a player or an enemy. I created a script for the game manager to keep track of the level, and the amount of enemies left, and to have it regenerate a new level once conditions are met. This way, it makes the game mechanics simple to work around and implement without taking up too much space or too many scripts.

Undo/Redo(Command)

Explanation:

By storing the player inputs as ‘objects’, we can freely store the data of their inputs to use in the future. In using a stack, we can sequentially order player inputs in a way that allows us to easily undo/redo player input by manipulating the stack order to our advantage, as by nature objects in a stack are placed at the front, so most recent inputs are stored first.

Different Kinds of Tiles(Factory)

Explanation:

The original idea was to have different kinds of enemies with varying stats to them. However, as we began developing the game, we realized it would be better and far simpler to have different kinds of tiles for the levels, as that functionality was practically already implemented by the time we got to enemies. So, we created a base tile script, along with override functions for each tile so that they keep the same base while having completely different utilities altogether.

Check if all enemies are cleared before loading the next level(Observer)

Explanation: 

While a simple implementation of an observer, it was necessary to allow our game to function. We needed the enemies, as they died, to update the Game Manager once they did. Enemy death is an event that requires the number of total enemies in a given level to be reduced so that the conditions to progress levels are met.
