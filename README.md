# Charm Diamond

Unbreakable: while the Diamond is in your pocket, valuables you hold in your hands take no damage.

> **Everyone in the lobby must install this mod, together with Pockets and CharmCore.** The power changes what your own character can do and its state shows on your inventory slot, so friends without the mods would see an empty slot and be unable to use the pocket.

## How to Use

1. Find the **Diamond** valuable and store it in a free inventory slot with [Pockets](https://thunderstore.io/c/repo/p/OsmarBriones/Pockets/) (grab it, press the slot key).
2. Pick up any other valuable and carry it in your hands: bumps, drops and knocks no longer reduce its value while you hold it.
3. Take the valuable out of the pocket and the power stops.

## Features

- **Unbreakable:** valuables you hold in your hands take no damage while the Diamond is in your pocket. Valuables you only carry in pockets, or that someone else holds, are not protected.
- **It costs the Diamond:** every hit the power prevents wears away a share of the Diamond's value (a quick series of knocks on the same item counts once), and a worn-out Diamond breaks.
- **Everything is configurable:** wear and the pause between wear charges. The host's choices apply to everyone.

## Requirements
- [BepInEx Pack for R.E.P.O.](https://thunderstore.io/c/repo/p/BepInEx/BepInExPack/)
- [Pockets](https://thunderstore.io/c/repo/p/OsmarBriones/Pockets/)
- [CharmCore](https://thunderstore.io/c/repo/p/OsmarBriones/CharmCore/)

## Installation
1. Install through r2modman / Thunderstore Mod Manager (Pockets and CharmCore come with it), or place `CharmDiamond.dll` next to `Pockets.dll` and `CharmCore.dll` in `BepInEx/plugins`. **All players need it.**
2. Launch the game once; the configuration file is created inside `BepInEx/config`.

## Configuration
Settings are in `com.osmar.CharmDiamond.cfg` inside `BepInEx/config`, section `Charm.Diamond`. The host's choices apply to everyone.

- `Enabled` (default `true`): turn this charm on or off.
- `WearPercent` (default `8`): share of the Diamond's original value lost for each prevented hit.
- `WearCooldownSeconds` (default `0.5`, 0 to 10): minimum time between wear charges for the same held valuable. Hits are still prevented inside that time, they just cost nothing extra. `0` charges every prevented hit.

Section `Debug`: `DebugMode` (default `false`) places a Diamond on the floor in front of you when a level starts (host or singleplayer) and writes extra log lines. For testing.

## Issues & Bug Reports
The official way to report issues, suggest improvements, or submit feedback is by opening an issue on the official GitHub repository:
👉 [GitHub Issues](https://github.com/OsmarBriones/REPO-CharmDiamond/issues)

## Credits
Developed by **Osmar Briones**
