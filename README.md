# Cleaner Robot Simulator

## Team members:
Tianyou Liu, Nian Gao, Alina Pan

## Game summary
A sweeping robot simulator game where the player controls a sweeping robot to explore rooms, cleaning up trash while avoiding obstacles.

## Genres
Simulator
First-person
Exploration

## Inspiration
### 1. Robo Vacuum Simulator
Robo Vacuum Simulator is one of our primary inspirations. The player player a cleaner robot to suck up garbage in a variety of settings. We want to add more elements to the game, including change of weather/lighting, a point system for the garbages gathered, and possible "enemies" such as pets that you need to avoid. Our robot would also ideally be round instead of square
![Pac-Man](https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/1766150/ss_c80181d661a4620c004ba7d01288945eeae4dcba.1920x1080.jpg?t=1636406330)
Source: https://store.steampowered.com/app/1766150/Robo_Vacuum_Simulator/


### 2. Roombo: First Blood
This 3D action game on Steam features a cleaner robot that utilizes various gadgets to defend a house from invaders. After the battle, the robot diligently cleans up both the house and the mess caused by the fight. Our focus, however, is on the cleaning aspect rather than the combat. We aim to draw inspiration from the robot’s cleaning mechanics while introducing a challenge element: at higher difficulty levels, players must navigate around pets in the house, simulating the real-life obstacles a robotic vacuum might encounter.
![Buckshot Roulette](https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/1004610/ss_74cc5b6877c7f57039cf178cc85791ed174809bc.1920x1080.jpg?t=1723606973)
Source: https://store.steampowered.com/app/1004610/Roombo_First_Blood/



### 3. Rock Simulator
 Rock Simulator is a well-known game on Steam, offering a unique experience where players embody a rock, simply existing and witnessing different environments and weather conditions. The concept of a "simulator" that allows players to take on the role of an everyday object is particularly intriguing to us, as it provides a fresh and immersive perspective on the world.
![Game Screenshot](https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/1187510/ss_12f66bb828a296ff3e5e2431b218f9b36ead8918.1920x1080.jpg?t=1695638409)
Source: https://store.steampowered.com/app/1187510/Rock_Simulator/

## Gameplay:

Assume control of an advanced robot vacuum cleaner within a meticulously detailed 3D home environment.
Player Role: Assume control of an advanced robot vacuum cleaner within a meticulously detailed 3D home environment.

### Objectives:

#### Easy Mode: 
Navigate through various rooms to collect a specified amount of dust while overcoming obstacles such as jumping onto tables, avoiding collisions with sofas, and maneuvering under furniture.
Medium Mode: In addition to dust collection, avoid interference from household pets like cats and dogs, which can hinder your progress.
Hard Mode: Defend the home from intruders by utilizing built-in defensive mechanisms, showcasing the vacuum's advanced home security features.
Mechanics:

#### Item Interaction: 
Collect and utilize various items to enhance performance, such as speed boosts or temporary shields.
Time Management: Complete tasks within set time limits to advance to subsequent levels.
Dynamic Environment: Experience realistic day-night cycles and weather changes that affect visibility and navigation.

### Graphics:

The game features a first-person perspective with low-poly graphics, creating a stylized yet immersive environment.
Visual Style: The game features a first-person perspective with low-poly graphics, creating a stylized yet immersive environment.

Lighting and Effects: High-contrast lighting and particle systems enhance the visual experience, especially during interactions like dust collection and obstacle navigation.

This design aims to provide a realistic and engaging simulation of a robot vacuum cleaner's operations within a dynamic home setting.

### User Interface and Controls:

The game employs an intuitive user interface designed for ease of navigation and control. Players can choose between keyboard controls, using the WASD keys for movement, or a game controller's analog stick for a more immersive experience. Actions such as jumping, activating defensive mechanisms, and utilizing collected items are mapped to easily accessible buttons.

On-screen displays provide real-time feedback, including a dust collection meter to track progress, a battery life indicator to monitor energy levels, and a mini-map highlighting the home's layout, current objectives, and locations of obstacles and pets. These elements work together to offer a seamless and engaging user experience, ensuring players remain informed and in control as they navigate the challenges of each level.

## Development Plan
### Project Checkpoint 1-2:
Implement scripts of player actions
Create prefabs for game objects (furnitures, dusts)
Create a basic map for the easy mode
Implement basic interactions between player and environment (collision, collection)
Work done: 
1. created prefabs for cobot, furnitures, room and trash
2. built scenes room (the room being played in), win scene and loose scene
3. implemented basic interaction, including movement, collision (need futher working on), and collection.
4. created game manager for winning and loose condition
### Checkpoint 1-2 details:
#### Basic Game Functions
We created the basic scene for the game, which is a small room with furnitures and trash(represented as round balls) scattered around the room. The robot is initiaded in the middle of the room. Movement is enabled through user input (wasd) via keyboard using the unity input system, input has not yet been fitted with other controls (such as mouse or gamepads). Gamelogic, including starting and ending the game, win and lose conditions, collection of trash is handled using C# scripts (CameraController, GameManager, PlayerMovement, RoundTrashCollectible, TrashSpawn, of which TrashSpawn is not yet used in the game). These script enabled moving, collecting and ending the game.

![Lose Scene](Game_demo/LoseScene.png)
![Win Scene](Game_demo/WinScene.png)
![Game Play Scene](Game_demo/GamePlayScene.png)

#### Movement and Scenes
Movement is enabled by the keyboard, via wsad for front, back, left and right for the robot. At this point the robot only moves without turning around. The robot can also jump by hitting Space key, which allows the robot to clean trash on the bed and sofa (we intend this to be for fun :)) On collision with trash, trash will be "collected", dissapprearing and incrementing points. If ten trash are collected in 3 minutes, win scene, which simply displayes You Win is initiated, else Loose Scene, which is just You Lost, will be displayed. Score board and timer countdown are planned to be implemented via UI, but implementation was not very successfull (unable to locate the UI in the correct position in the screen scene. 



