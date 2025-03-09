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
### Project Checkpoint 1-2: Basic Mechanics and Scripting (Ch 5-9)
1. Created prefabs for the robot, furniture, room, and trash.  
2. Implemented basic player movement using **WASD** controls.  
3. Enabled jumping to allow interaction with elevated surfaces.  
4. Implemented collision detection for trash collection.  
5. Developed basic game logic, including win and lose conditions.  
6. Created essential game scenes: **Room Scene**, **Win Scene**, and **Lose Scene**.  


Deferred Tasks:

Refining collision detection: Currently, the trash collection system works, but the collision detection needs improvement for smoother gameplay. This will be refined in the next submission.
UI Elements (Scoreboard & Timer): These were planned for this submission, but due to difficulties positioning UI elements correctly within the game scene, they will be completed in the next phase.
Additions
To enhance gameplay flow, we implemented a Game Manager script to handle win/lose conditions dynamically. Initially, we planned to rely on separate triggers for scene transitions, but consolidating them under a Game Manager allows for better control and scalability.

### Tasks Completed:
Created prefabs for the robot, furniture, and trash.
Implemented player movement, trash collection, and obstacle interaction using scripts.
Created a game manager that manages the win/lose condition of the game.
Implemented a “bad trash” that reduces the player score.
Implemented a UI that shows the countdown timer.
Imported models and textures to create a better visual experience.
Created a second game level with higher difficulty.
Enabled third-person following camera.


## Additions
we introduced a jump mechanic, which was not originally planned, to allow the robot to reach and clean trash placed on elevated surfaces such as sofas and tables. While initially implemented as a fun feature, it adds a new layer of challenge and interactivity to the game as suggested from our feedbacks for better gameplay experience.


### Project Part 2: 3D Scenes and Models (Ch 3+4, 10)
Refining Environment Design: The current game environment is built using basic primitives. For the next phase, we will replace placeholders with more detailed 3D models of furniture, household objects, and interactive elements.
Texture and Material Improvements: Applying textures to key objects (e.g., floors, walls, trash) to enhance realism and immersion.
Enhanced Collision System: Improving the physics interactions between the robot and objects, ensuring smoother movement and more accurate trash collection.
User Interface Development: Finalizing the implementation of the UI elements, including the score counter and timer, ensuring they are properly positioned and functional within the game scene.
Expanding Game Logic: Introducing a more structured game loop, where different levels or game scenarios can be loaded dynamically based on the player's progress.
Adding more stuff: we would want to add a obstacle, where upon the collision with the trash, the robot will slow down.
Add a bad trash, which the robot would avoid and prevent from colliding with

### Project Part 3: Visual Effects (Ch 11, 12, 13)
Lighting and Shadows: Instead of relying on the default lighting and shadows, we are going to apply different lighting methods to improve the aesthetics of the scenes. We are also going to add a night and day alternation. 
Particle System and Visual Effect Graph: We will use the particle system to create more furniture (e.g. fireplace and water sink) with vivid fire/water visual effects.
Post Processing: We will use tools such as the depth map to create better visual effects.
More levels: We plan to add more levels with more rooms to clean and higher difficulty to win.


## Game Development
### Project Checkpoint 1-2

- Implemented scripts for player actions.  
- Created prefabs for game objects (furniture, dust).  
- Designed a basic map for easy mode.  
- Implemented basic interactions between the player and the environment, including collision detection and trash collection.  

#### Work Completed

1. Created prefabs for the robot, furniture, room, and trash.  
2. Built game scenes: **Room Scene** (main gameplay area), **Win Scene**, and **Lose Scene**.  
3. Implemented basic interactions, including movement, collision detection (still needs improvement), and trash collection.  
4. Developed a Game Manager to handle win and lose conditions.  

### Checkpoint 1-2 details:
#### Basic Game Functions
We created the basic scene for the game, which is a small room with furnitures and trash(represented as round balls) scattered around the room. The robot is initiaded in the middle of the room. Movement is enabled through user input (wasd) via keyboard using the unity input system, input has not yet been fitted with other controls (such as mouse or gamepads). Gamelogic, including starting and ending the game, win and lose conditions, collection of trash is handled using C# scripts (CameraController, GameManager, PlayerMovement, RoundTrashCollectible, TrashSpawn, of which TrashSpawn is not yet used in the game). These script enabled moving, collecting and ending the game.
Lose Scene: collect enough trash in 3 minutes
![Lose Scene](Game_demo/LoseScene.png)
Win Scene (collect enough trash):
![Win Scene](Game_demo/WinScene.png)
Room Scene: 
![Game Play Scene](Game_demo/GamePlayScene.png)

#### Movement and Scenes
Movement is enabled by the keyboard, via wsad for front, back, left and right for the robot. At this point the robot only moves without turning around. The robot can also jump by hitting Space key, which allows the robot to clean trash on the bed and sofa (we intend this to be for fun :)) On collision with trash, trash will be "collected", dissapprearing and incrementing points. If ten trash are collected in 3 minutes, win scene, which simply displayes You Win is initiated, else Lose Scene, which is just You Lost, will be displayed. Score board and timer countdown are planned to be implemented via UI, but implementation was not very successfull (unable to locate the UI in the correct position in the screen scene. 



### Project Part 2: 3D Scenes and Models

In this phase of development, we focused on enhancing our game’s 3D environment by incorporating new models, textures, and furniture assets to create a more realistic and engaging space for the player.

#### Added Assets

- **Player Object (Cleaner Robot)**
  - **Source**: [Xiaomi Robot Mop - Automatic Vacuum Cleaner](https://sketchfab.com/3d-models/xiaomi-robot-mop-automatic-vacuum-cleaner-01c808d0ab9f44e184ef3005cf0fe82d)  
  - We imported this model as our main player character, giving the game a more recognizable robotic vacuum cleaner look.

- **Room and Furniture**
  - **Apartment Kit**
    - **Source**: [Apartment Kit on Unity Asset Store](https://assetstore.unity.com/packages/3d/environments/apartment-kit-124055)  
    - Used for the dining table and various textures (e.g., floor, walls, windows), replacing placeholder geometry with more realistic meshes and materials.
  - **Vintage Retro Room Props**
    - **Source**: [Vintage Retro Room Props](https://assetstore.unity.com/packages/3d/props/furniture/vintage-retro-room-props-built-in-pipeline-290031)  
    - Added retro-style furniture pieces (e.g., sofa, additional décor) to bring variety and detail to the interior design.
  - **Custom Models** (Created by our team)
    - **Bed, Lamp, and Tables**
      - Modeled and textured in-house to showcase our own contributions. These custom assets are placed around the room to provide interactive surfaces and enhance the overall aesthetic.

#### Visual Enhancements

- **Textures and Materials**: We replaced basic Unity materials with higher-quality textures from the Apartment Kit and Vintage Retro Room Props. Marble walls and tiled flooring give the environment a more modern and polished look.
- **Lighting**: We refined the lighting to emphasize the interior space, using point lights and directional lights to illuminate furniture and highlight key areas where the robot will be navigating.

#### Current Progress

Below is a screenshot illustrating the updated scene with our imported and custom-made assets:

Level 1:
![Updated Apartment Scene](Game_demo/level1.png)

Level 2:
![Updated Apartment Scene](Game_demo/level2.png)

In the image, you can see:
- The *cleaner robot* (player) placed near the center of the room.
- A *bed* and *lamp* modeled by our team.
- A *dining table* and *chairs* from the Apartment Kit, complete with applied textures.
- A *vintage sofa* and other decorative props from the Retro Room Props package.
- Updated floor and wall materials to create a more cohesive and realistic interior environment.

By integrating these assets, we aimed to make the apartment feel more “lived-in” and visually appealing, laying the groundwork for future gameplay enhancements and interactive elements.

#### Next Steps

- **Collision & Interaction**: Continue refining collision detection so the robot can smoothly navigate around furniture and obstacles.
- **Additional Props**: Incorporate more decorative objects or “clutter” items to populate the environment, providing extra challenges and a sense of realism; create more rooms for players to explore such as a kitchen/bathroom; if possible, also create a trash spawner or similar object to randomly generate trash.
- **UI Overlays**: Integrate the updated UI (scoreboard, timer) so that it visually complements the new 3D scene without obstructing gameplay.





### Instructions for Testing the Project

Below are the steps to play and test our Cleaner Robot Simulator, along with notes on which scenes and features to explore:

- **Opening the Project**
  - Open the Unity project in the Unity Editor.
  - In the **Project** window, navigate to the `Scenes` folder.
  - Load the **Room Scene** (`RoomScene.unity`) to begin testing the core gameplay.

- **Gameplay Controls**
  - **W, A, S, D**: Move the robot forward, left, backward, and right.
  - **Space**: Jump (useful for reaching trash on elevated surfaces).
  - The robot automatically collects trash upon collision.

- **Core Scenes & Their Purposes**
  - **Room Scene**: Main gameplay area. Move around, collect trash, and observe collision mechanics.
  - **Win Scene**: Displays the “You Win” message once 10 trash items are collected within 3 minutes.
  - **Lose Scene**: Displays the “You Lost” message if the player fails to collect 10 trash items within 3 minutes.

- **How to Test Win/Lose Conditions**
  - In the **Room Scene**, move around and collect trash. Each piece of trash adds to the score. Note: bad trash will reduce the total points by 1; the obstacle slows down the moving speed of the robot for 3 seconds.
  - After collecting 10 trash items, the scene will transition to the **Win Scene**.
  - If 3 minutes pass without collecting enough trash, the scene will transition to the **Lose Scene**.

- **Additional Notes**
  - The UI for score and timer is partially implemented; you may need to tweak the Canvas settings in the Inspector if the elements are not visible or incorrectly positioned on your screen.
  - Some scripts (e.g., `TrashSpawn`) are not yet fully utilized but are included for future expansions.
  - The jump mechanic is an experimental feature added to increase interactivity and fun.
  - We intend to update the bad trash to useful objects of the owner (money, cosmetics, etc) so that if picked by the robot, the total score reduces








