Play From Here Button for Unity3D

![](https://i.imgur.com/HXmaLVl.png)

1. Get ToolbarExtender: https://github.com/marijnz/unity-toolbar-extender
2. Get my PlayFromHereButton.cs script (put it in an **Editor** folder)
3. Change the last line in `spawnPlayerAtCam` function to handle your player-spawning.
4. Change or comment the `.OpenScene` Line in OnToolbarGUI. This is where I load my Player Basics Scene (Player Prefab, Main Menu, Cameras, Particle Effects) which I keep in a separate scene, I load my levels in "additive" mode so I can have multiple scenes loaded. This is optional, so if you don't need this, just comment it out. 
5. Remember to use "Play Unfocused" or "Play Focused" but not "Play Maximized" (otherwise it will use Main Camera instead of Scene camera


![](https://i.imgur.com/QcHyNwr.png)
