# Risk of Rain 2 Arena Mod

Arena is a [Risk of Rain 2](https://en.wikipedia.org/wiki/Risk_of_Rain_2) mod for fighting your friends at the end of each stage. See the [mod's readme](src/Assets/README.md) for more details.

## Development

Make sure you are familiar with the [modding wiki.](https://github.com/risk-of-thunder/R2Wiki/wiki)

To be able to reload the mod while developing without restarting the game (which could take a significant amount of time), follow these steps:

1. Follow the setup instructions (the steps before "Getting the boilerplate") [here.](https://github.com/risk-of-thunder/R2Wiki/wiki/First-Mod)
2. For easier debugging also install these mods:
   * [DebugToolkit](https://thunderstore.io/package/IHarbHD/DebugToolkit/)
   * [PlayerBots](https://thunderstore.io/package/Meledy/PlayerBots/)
3. Install [ScriptEngine.](https://github.com/BepInEx/BepInEx.Debug#scriptengine)
4. Change the second argument of the copy command in the post-build event in the [Arena.csproj](src/Arena/Arena.csproj) file to use the correct path to your `BepInEx\scripts` folder. [More info.](https://github.com/risk-of-thunder/R2Wiki/wiki/Build-Events#copy-output-dll=)
5. Start the game modded.
6. Build the solution.
7. Load the mod by pressing `F6` in the main menu.

Whenever you make changes to the code, repeat step 4 and 5. Make sure you reload the mod only on the main menu, not during a run, otherwise (because of the way the mod is structured) you could end up with some unexpected behavior.
