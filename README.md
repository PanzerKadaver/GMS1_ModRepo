# GMS1_ModRepo
A repository to store sources &amp; distribute mods written for Gold Mining Simulator

## Warning

Mods, as well as associate sources, are provided as is. I'm not responsible if you use mods in ladderboard season and get banned because of it.

Also, if you encounter a bug and if you think it's related to one of my mod, do not report it on GMS Discord. The official Discord do not provide support for modded game.

## How to install mods

1. Download [BepInEx 5.4.23.4](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.4) and follow the installation procedure 
2. Download my mod pack from the [Releases](https://github.com/PanzerKadaver/GMS1_ModRepo/releases) page of this repo
3. Extract the archive and move the mods dll you wish to install to `<GameFolder>/BepInEx/plugins` folder

## How to report a bug with a mod

1. In your game folder, locate the file named `doorstop_config.ini` and inside of it, change the value of the parameter `redirect_output_log` from `false` to `true`.
2. Re-launch the game and reproduce the bug you've encountered
3. Open an issue on this repo and provide as many information as you can : steps to reproduce, screenshots, details, etc.
4. Attach to the issue the log file `output_log.txt`

## List of mods

### Correct Axis Inverser

**Description :** A pretty straightforward mod. It make sure that even with X and/or Y axis inverted toggle, that inversion only apply to Controller (instead of Controller AND Mouse), allowing you to freely switch between KBM and Controller as you see fit.

**Safe to add/remove from your actual save :** Yes

### Lossless Blacksmith

**Description :** Modify the Blacksmith to remove gold losses when the crucible is fully upgraded. The new losses per level is the following :

| Level  | Losses |
| - | - |
| 1 | 10% |
| 2 | 5% |
| 3 | 2.5% |
| 4 | 0% |

**Safe to add/remove from your actual save :** Yes

### Mega Front-Loader

**Description :** Turn the Front-Loader into a real mining machine by increase it's capacity to 40m3. To balance a bit these changes, the fuel consumption is multiplied by 3 (60l/h => 180l/h) and price is multiplied by 4.

The Dump Truck have also seen his fuel consumption have been updated (48l/h => 100l/h) and price multiplied by 3 to be inline with the Front-Loader and other machinery price.

**Safe to add/remove from your actual save :**
* Adding : Yes, but for balance reason I suggest that you sell/return all your actual Front-Loader/Dump Truck before enabling this mod.
* Removing : Make sure that all Front-Loader bucket's and Dump Trucks bed's are empty.