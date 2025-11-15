# Infinite Procedural Dungeon Generator (Scriptable Objects)
This is my first public open-source project. Feedback on code quality or structure is very welcome.
This project provides an infinite procedural dungeon generator for Unity using ScriptableObjects. It is designed to make procedural dungeons quickly accessible for demos, prototypes, tests, and beginner projects. A demo scene is included with a simple first-person shooter application to show its capabilities.

## Foreword
Infinite level generation normally requires a lot of setup and logic, even for basic implementations. This asset aims to remove that barrier by providing a complete, configurable generator that can be used immediately or customized through ScriptableObjects.
A working demo scene is included if you want to quickly test the system or have an example to built from.

## Setup and Implementation
Below is the required setup. Clarity feedback is appreciated.

### 1. Add the DungeonManager to Your Scene
Because this generator creates infinite layouts, it is recommended to start with an empty scene.
Drag the DungeonManager prefab into the scene.
It includes the default demo configuration and will generate a functional infinite dungeon as soon as you press Play. You can adjust parameters afterward.
The DungeonManager also contains a NavMeshSurface component that will dynamically update the Surface, you can delete this if you don't need it!

### 2. Prepare Your ScriptableObjects
The system relies on three ScriptableObject types:

#### DungeonDataSettings
Contains all dungeon-wide parameters, references, room & enemy collections and generator rules.
Replacing this ScriptableObject gives you a completely new dungeon configuration.

#### RoomSpawnData
- Defines an individual room type.
- Includes the room prefab and all room-specific settings.
- More on creating a correct room prefab later.

#### EnemySpawnData
- Defines an enemy type.
- Contains the enemy prefab, the DungeonEnemyScript, and all enemy-specific settings.
- Enemies can be made however you like, this ScriptableObject only helps with spawning in enemies!

*Note: If anyone has a clean architectural idea for linking RoomDataObjects and EnemyDataObjects under unified spawn conditions (for example: “this room only spawns this enemy” or “this enemy appears only in this room”) without writing dozens of if statements.*

### 3. Adding Your Own Rooms
Adding rooms is the most "difficult" part. You need to assemble room pieces, apply the correct components, and register the room in your RoomDataObject.
This process is straightforward (Things will be explained more clearly later):
- Place: floor, 4 identical walls, doorwalls, doors
- set up colliders
- add all required components
- Add the Prefab to the RoomSpawnerData ScriptableObject
The room will then be included in the algorithm.

*Note: I’m curious if it would be viable to have a room as scriptableObjects and then assembled in code, just add: floor, wall, doorwalls, door and code assembles it. Sounds sick I might get to that after my thesis! It would be a great way to make rooms have more dynamic possibilities!*

## To Be Continued
### More documentation will be added later, including:
- Picutures to visualise explanarions
- more clearn room explanarion
- Feature & settings list
- Probably more that I write down as I go
