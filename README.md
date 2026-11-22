
# FPS Survival Game

FPS Survival Game is a 3D survival game developed using Unity. The project features an outdoor environment, character-based survival gameplay, combat elements, and a player HUD displaying health and stamina.

## Game Overview

The game takes place in an outdoor environment featuring trees, grass, rocks, and natural terrain. Players navigate the environment while managing their health and stamina.

The gameplay footage shows a player character equipped with a melee weapon, with the weapon visible during gameplay.

## Gameplay Features

- **3D Environment:** Outdoor environment featuring natural terrain, vegetation, trees, and rocks.
- **Player Character:** A controllable character navigating the game environment.
- **Health System UI:** A health bar displayed on the gameplay HUD.
- **Stamina System UI:** A separate stamina indicator displayed alongside the health bar.
- **Combat:** Melee weapon visible during gameplay.
- **Main Menu:** A dedicated main menu scene.
- **AI Navigation Support:** Unity AI Navigation tooling is present in the project environment.

## Technical Overview

| Component | Details |
|---|---|
| Game Engine | Unity |
| Unity Version | 2022.3.22f1 |
| Programming Language | C# |
| Game Dimension | 3D |
| Genre | Survival / FPS |
| Environment | Outdoor terrain |
| Development Platform | Unity Editor |

## Project Structure

The Unity project contains the following main directories:

```text
Assets/
├── _TerrainAutoUpgrade/
├── Animation/
├── Effects/
├── Materials/
├── Models/
├── Prefabs/
├── Scenes/
├── Scripts/
├── Shaders/
├── Sounds/
├── Terrain/
└── Textures/

Packages/
ProjectSettings/
```

## Scenes

The project view shows the following scene assets:

- `MainMenu`
- `SampleScene`

The Main Menu scene contains a Main Camera and Directional Light in its visible hierarchy.

## User Interface

The gameplay HUD includes:

- Health indicator
- Stamina indicator

These indicators provide the player with visible feedback about their survival status during gameplay.

## Getting Started

1. Clone or download the repository.
2. Open Unity Hub.
3. Add the downloaded project.
4. Open it using Unity `2022.3.22f1`.
5. Allow Unity to import assets and resolve project dependencies.
6. Open the `MainMenu` scene from the Scenes directory to inspect the main menu.

## Development Notes

- The project is built around a 3D outdoor survival environment.
- The visible gameplay includes a player character, melee combat equipment, and health and stamina HUD elements.
- Unity AI Navigation tooling is visible in the editor.
- Project assets are organized into dedicated folders for animation, effects, materials, models, prefabs, scenes, scripts, shaders, sounds, terrain, and textures.
