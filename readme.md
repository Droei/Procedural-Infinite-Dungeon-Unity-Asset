# Infinite Procedural Dungeon Generator (Scriptable Objects)
This is my first public open-source project. Feedback on code quality or structure is very welcome.
This project provides an infinite procedural dungeon generator for Unity using ScriptableObjects. It is designed to make procedural dungeons accessible for demos, prototypes, tests, and beginner projects. A demo scene is included with a simple first-person setup to show the generator in action.

## Foreword
Infinite level generation normally requires a lot of setup and logic, even for basic versions. This asset aims to remove that barrier by providing a complete, configurable generator that can be used immediately or customized through ScriptableObjects.
A working demo scene is included if you want to quickly test the system.

## Setup and Implementation
Below is the required setup. Clarity feedback is appreciated.

### 1. Add the DungeonManager to Your Scene
Because this generator creates infinite layouts, it is recommended to start with an empty scene.
Drag the DungeonManager prefab into the scene.
It includes the default demo configuration and will generate a functional infinite dungeon as soon as you press Play. You can adjust parameters afterward.

### 2. Prepare Your ScriptableObjects
The system relies on three ScriptableObject types:

#### DungeonSettings
Contains all dungeon-wide parameters, references, collections, and generator rules.
Replacing this ScriptableObject gives you a completely new dungeon configuration.

#### RoomDataObject
Defines an individual room type.
Includes the room prefab and all room-specific settings required for infinite procedural placement.
More on creating a correct room prefab later.

#### EnemyDataObject
Defines an enemy type.
Contains the enemy prefab, the DungeonEnemyScript, and all enemy-specific settings.

Note: If anyone has a clean architectural idea for linking RoomDataObjects and EnemyDataObjects under unified spawn conditions (for example, “this room only spawns this enemy” or “this enemy appears only in this room”) without writing dozens of if statements, feedback is appreciated.

### 3. Adding Your Own Rooms
Adding rooms is the most hands-on part. You need to assemble room pieces, apply the correct components, and register the room in your RoomDataObject.
If your models are prepared cleanly, this process is straightforward:
- Place all room elements
- Add the required components
- Assign the prefab to a RoomDataObject
- Add the RoomDataObject to your DungeonSettings
The room will then be included in infinite generation.

Note: In the future, a system that assembles rooms dynamically from ScriptableObjects (floor, walls, door walls, door prefabs) may be explored. This could allow for highly modular room creation.

# To Be Continued
## More documentation will be added later, including:
- Picutures to visualise explanarions
- more clearn room explanarion
- Feature & settings list
- Probably more that I write down as I go
