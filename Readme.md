# Disclaimer
All assets used in this project were sourced from the Unity Asset Store for free. Full credit goes to the original creators of these assets. If you are interested in using these assets, you can find them directly on the Unity Asset Store by following these links:

1. https://assetstore.unity.com/packages/tools/utilities/health-system-for-dummies-215755
2. https://assetstore.unity.com/packages/2d/textures-materials/brick/high-quality-bricks-walls-49581
3. https://assetstore.unity.com/packages/2d/free-stylized-2d-space-shooter-pack-245185
4. https://assetstore.unity.com/packages/2d/gui/gold-mining-game-2d-mine-ui-tilset-263856
5. https://assetstore.unity.com/packages/vfx/particles/spells/slash-effects-free-295209
   
Feel free to search for these assets on the Unity Asset Store to explore or use them in your own projects.

# Unity Game Project

This Project is a 2D Adventure Game using the assets forom App 1 and some more assets from unity store.
I extended on App 1 to build a 2D Adventure Game where you have to fix and collect questions to go onto the next stage.
The final stage is a boss fight, to test you skills, beat it without and power ups, reminder you one have 30 HP, while every projectile/boss collision does -5 HP.
You do have invincibility is you use your dash ability, although it is very short, kinda like iFrames in souls like games.

# Gameplay

Throughout the game you will need to fix robots just shooting projectiles to "fix" them into a dancing animiation and collect golden keys to unlock the next entrance path
They can also stun you if hit by their attack

Stage One
6 Enemies & 2 Golden Keys
Walk into the giant house to go to next stage

Stage Two
5 Enemies & 2 Golden Keys
Walkinto the Trap door to go to next stage

Teleporter Stage
Work thorugh a bunch of teleported to open the gate to the new level

Boss Fight
Beat the boss with or without the power ups
Boss shoots of 3 different moves, 2 of which follow you, the blue missiles and air waves, the final red missile is a 8 missile attack in a radial pattern so make sure to dodge quick

## Modifications

### Randomized SpeedBoost of Collectible Health Pickups

In addition to following the tutorial, I implemented a feature that added the randomization of health pickups, which can grant a speed boost for 5 seconds. The speed boost effect cannot be stacked.

## Keybinds In Game

Shoot Projectiles at Enemies Moving - Press RightMouseButton
Heal - Press H (full hp on normal stages, 10 hp for boss fight)
Interact with NPC Frog - Press F
Dash - Press Space
Pull Box - Press B
Pause Game - Press Escape

BossFight Has Power Ups
RapidFire - Hold Right Mouse Button down to shoot instead of clicking
MultiShot - Shoot two projectiles instead of one

# Leaderboard Manager

Use the following secret key to add existing leaderboard to following website https://danqzq.itch.io/leaderboard-creator

secret key = e3e20859c133d9295b6116f69d2380d8bbf991f1d75672f6ae8f9ca2d4363a6b36a3ddb617ff41a8c66baf391adfffaabb61aa5995e7f524e95899d258cba9622d636d7d68121327d91b14128dbd570f28b09518b4c7d882f70ac08e1fd54111ec7c00334a595ed1e886ce859da6663e7ce95b4d945001c8d20e821d8ff86314

- I will clear the leaderboard so you can test it from scratch, it is set in ascending order and only unique name entries so no duplicates happen, also lowest scores will make it on to the leaderboard only
  - ex. 7 will show above 35

# Features added

Dash
Heal
PowerUps
Stun - remove tnt explosion
Teleporter
Gate
Button
Boss movement
Boss attacks (3), wind, tracking missile, straight missle
Boss healthbar
Golden keys, used to unlock the next stage in early levels
Pullbox
i-frames when dashing
