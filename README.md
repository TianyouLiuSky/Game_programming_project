# Cleaner Robot Simulator

## Team members:
Tianyou Liu, Nian Gao, Alina Pan

## Game summary
A sweeping robot simulator game where the player controls a sweeping robot to explore rooms, cleaning up trash while avoiding obstacles.

## Genres
- Simulator  
- Third-person  
- Exploration

## Inspiration

### 1. Robo Vacuum Simulator
Robo Vacuum Simulator is one of our primary inspirations. In this game, the player controls a cleaner robot to suck up garbage in a variety of settings. We want to add more elements to our game, including changes in weather/lighting, a point system for the garbage collected, and possible "enemies" such as pets that must be avoided. Our robot would ideally be round instead of square.  
![Pac-Man](https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/1766150/ss_c80181d661a4620c004ba7d01288945eeae4dcba.1920x1080.jpg?t=1636406330)  
Source: [Steam - Robo Vacuum Simulator](https://store.steampowered.com/app/1766150/Robo_Vacuum_Simulator/)

### 2. Roombo: First Blood
This 3D action game on Steam features a cleaner robot that uses various gadgets to defend a house from invaders. After the battle, the robot cleans up the mess left behind. Our focus is on the cleaning mechanics rather than combat. We aim to include a challenge element by introducing household pets as obstacles at higher difficulty levels.  
![Buckshot Roulette](https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/1004610/ss_74cc5b6877c7f57039cf178cc85791ed174809bc.1920x1080.jpg?t=1723606973)  
Source: [Steam - Roombo: First Blood](https://store.steampowered.com/app/1004610/Roombo_First_Blood/)

### 3. Rock Simulator
Rock Simulator is a unique game where players experience life as a rock, observing different environments and weather conditions. The idea of taking on the role of an everyday object is intriguing and inspires us to deliver an immersive simulation experience.  
![Game Screenshot](https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/1187510/ss_12f66bb828a296ff3e5e2431b218f9b36ead8918.1920x1080.jpg?t=1695638409)  
Source: [Steam - Rock Simulator](https://store.steampowered.com/app/1187510/Rock_Simulator/)

## Gameplay

- **Player Role:** Control an advanced robot vacuum cleaner in a detailed 3D home environment.
- **Objectives:**
  - **Easy Mode:**  
    - Navigate through various rooms to collect a specified amount of dust.
    - Overcome obstacles (e.g., jumping onto tables, avoiding collisions with sofas, maneuvering under furniture).
  - **Medium Mode:**  
    - In addition to dust collection, avoid interference from household pets like cats and dogs.
  - **Hard Mode:**  
    - Defend the home from intruders using built-in defensive mechanisms.
- **Mechanics:**
  - **Item Interaction:** Collect and use items (e.g., speed boosts, temporary shields).
  - **Time Management:** Complete tasks within set time limits to advance.
  - **Dynamic Environment:** Experience realistic day-night cycles and weather changes.
- **Graphics & Controls:**
  - First-person perspective with low-poly, stylized graphics.
  - Intuitive controls using keyboard (WASD for movement, Space for jump) or game controller.
  - On-screen displays include a dust collection meter, battery life indicator, and a mini-map.

## Development Plan  

### Project Checkpoint 1-2: Basic Mechanics and Scripting (Ch 5-9)

- **Tasks Completed:**
  - ~~Created prefabs for the robot, furniture, room, and trash.~~
  - ~~Implemented basic player movement using **WASD** controls.~~
  - ~~Enabled jumping to allow interaction with elevated surfaces.~~
  - ~~Implemented collision detection for trash collection.~~
  - ~~Developed basic game logic, including win and lose conditions.~~
  - ~~Created essential game scenes: **Room Scene**, **Win Scene**, and **Lose Scene**.~~

- **Deferred Tasks:**
  - **Refining Collision Detection:**  
    The trash collection system works, but collision detection needs further refinement for smoother gameplay.
  - **UI Elements (Scoreboard & Timer):**  
    Positioning issues with the UI have delayed full implementation; these will be completed in the next phase.

- **Additions:**
  - ~~Implemented a Game Manager script to handle win/lose conditions dynamically.~~
  - Added a “bad trash” mechanic that reduces the player’s score.
  - Introduced a jump mechanic to allow the robot to clean trash on elevated surfaces.

---

### Project Part 2: 3D Scenes and Models (Ch 3+4, 10)

- **Environment Design:**
  - ~~Replaced basic primitives with detailed 3D models for furniture and household objects.~~
  - Future work includes importing models with realistic proportions and textures to better simulate a home setting.
  
- **Texture and Material Improvements:**
  - ~~Enhanced realism by applying high-quality textures and materials to floors, walls, and trash.~~
  - Future plan: Incorporate materials with improved reflectivity, bump maps, and tiling patterns.

- **Enhanced Collision System:**
  - ~~Improved physics interactions between the robot and in-game objects using Unity’s advanced physics features.~~

- **Expanding Game Logic:**
  - ~~Introduced a more structured game loop to dynamically load different levels/scenarios.~~
  - Future plan: Add more levels to increase replayability.

- **Introducing New Gameplay Elements:**
  - **Obstacle Mechanics:**  
    On collision with a trash item, the robot experiences a temporary slowdown, requiring strategic movement.
  - **Bad Trash Mechanic:**  
    The robot is programmed to detect and avoid “bad trash” which negatively affects the score.

- **Additions:**
  - No additional tasks were made beyond the planned improvements.

---

### Project Part 3: Visual Effects (Ch 11, 12, 13)

- **Lighting and Shadows:**
  - Planned improvements include applying advanced lighting methods (e.g., night and day cycles) to enhance scene aesthetics.
  
- **Particle System and Visual Effects:**
  - Planned use of particle systems to create vivid visual effects (e.g., fire and water) for elements such as fireplaces and sinks.
  
- **Post Processing:**
  - Planned use of post-processing tools (e.g., depth map) for enhanced visual quality.
  
- **Additional Plans:**
  - Future work will include additional levels with increased difficulty and integrated score calculations via text UI.

- **Additions:**
  - No additional tasks were made beyond the planned visual enhancements.

---

## Development

### Project Checkpoint 1-2: Basic Mechanics and Scripting

- **Implemented Elements:**
  - Created prefabs for key game objects (robot, furniture, trash).
  - Developed scripts for player movement and interactions.
  - Built basic scenes with win/lose conditions.
  - Implemented collision detection and trash collection.
  - Added a countdown timer UI (partial implementation).
  
- **Screen Captures:**
  - **Lose Scene:**  
    ![Lose Scene](Game_demo/LoseScene.png)
  - **Win Scene:**  
    ![Win Scene](Game_demo/WinScene.png)
  - **Room Scene:**  
    ![Game Play Scene](Game_demo/GamePlayScene.png)

- **Additions:**
  - Added a countdown timer UI (partial implementation).

---

### Project Part 2: 3D Scenes and Models

- **Added Assets:**
  - **Player Object (Cleaner Robot):**  
    Source: [Xiaomi Robot Mop - Automatic Vacuum Cleaner](https://sketchfab.com/3d-models/xiaomi-robot-mop-automatic-vacuum-cleaner-01c808d0ab9f44e184ef3005cf0fe82d)
  - **Room and Furniture:**
    - **Apartment Kit:**  
      Source: [Apartment Kit on Unity Asset Store](https://assetstore.unity.com/packages/3d/environments/apartment-kit-124055)
    - **Vintage Retro Room Props:**  
      Source: [Vintage Retro Room Props](https://assetstore.unity.com/packages/3d/props/furniture/vintage-retro-room-props-built-in-pipeline-290031)
    - **Custom Models:**  
      Bed, Lamp, and Tables (created in-house).

- **Visual Enhancements:**
  - Updated textures and materials for floors, walls, and other surfaces.
  - Refined lighting using point and directional lights to enhance the interior ambiance.
  
- **Screen Captures:**
  - **Level 1:**  
    ![Updated Apartment Scene](Game_demo/level1.png)
  - **Level 2:**  
    ![Updated Apartment Scene](Game_demo/level2.png)

- **Additions:**
  - No additional tasks were made beyond the above implementations.

---

## Instructions for Testing the Project

- **Opening the Project:**
  - Open the Unity project in the Unity Editor.
  - In the **Project** window, navigate to the `Scenes` folder.
  - Load **RoomScene.unity** to begin testing the core gameplay.
  
- **Gameplay Controls:**
  - **W, A, S, D:** Move the robot.
  - **Space:** Jump (to reach trash on elevated surfaces).
  - The robot automatically collects trash upon collision.
  
- **Core Scenes & Their Purposes:**
  - **Room Scene:** Main gameplay area for moving and collecting trash.
  - **Win Scene:** Displays “You Win” after collecting the required number of trash items.
  - **Lose Scene:** Displays “You Lost” if the trash collection target isn’t met within 3 minutes.
  
- **Additional Notes:**
  - The UI for score and timer might require adjustments in the Canvas settings.
  - Some scripts (e.g., `TrashSpawn`) are not fully utilized yet but are included for future expansions.
  - The jump mechanic, though initially experimental, adds extra interactivity and fun based on team feedback.
  - Future updates may convert “bad trash” into useful objects (e.g., money, cosmetics) that reduce the score if collected.
