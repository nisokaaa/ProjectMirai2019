Procedural Lightning for Unity 3D
© 2015 Digital Ruby, LLC – Created by Jeff Johnson – jeff@digitalruby.com
http://www.digitalruby.com

--------------------------------------------------
Version 2.4.4 - (See ChangeLog.txt for history)
--------------------------------------------------

**********************
*UNITY 4 SPECIAL NOTE*
I made everything in Unity 5, but have tried to make this Unity 4 compatible. Consequently, for Unity 4, there are no prefabs and you have to setup everything from scratch. It's very easy, but requires dragging script files on to an empty game object and setting up materials and textures. See the Getting Started tutorial.
**********************

Please read this entire file to get the most out of this asset. To view water in the demo scenes, please import Standard Assets -> Environment.

Procedural Lightning for Unity contains code to create 2D or 3D procedural lightning for use in your Unity game or app.

Tutorial Videos:

Getting Started: https://youtu.be/U7X1OPDZh2M
Chain Lightning: https://youtu.be/uTNxa901EZA
Spline Lightning: https://youtu.be/Od4AINfKE8k
Light: https://youtu.be/Wv-bP0qua0o
Turbulence and Jitter: https://youtu.be/g7ehEKRJwb4

Assets provided:

- Demo Scenes: (/Demo/Scenes)
	- Once you are familiar with the asset, if you don't need this, you can delete the Demo folder. Nothing relies on this folder existing.
	- DemoScene2D - shows sort layers and sprites
	- DemoScene2DXZ - shows orthographic mode using the XZ plane
	- DemoSceneConfigurePrafab - easily setup a lightning bolt prefab in the designer view
	- DemoSceneConfigurePrafabHDR - easily setup a lightning bolt prefab in the designer view for HDR
	- DemoSceneConfigureScript - configure the lightning for use in scripting
	- DemoSceneCustomTransform - shows how to rotate, scale and translate the lightning to track a start and end target for the lifetime of the bolt.
	- DemoSceneLight - Shows how to setup the lightning to use lights
	- DemoSceneLightningField - Shows the lightning field prefab in action
	- DemoSceneLightsabre - Shows a lightsabre in action using the lightning for a blade
	- DemoSceneManualAutomatic - Shows how to put the lightning script in manual mode vs. automatic mode. In manual mode you must call the Trigger function to create lightning.
	- DemoSceneMesh - Shows how to make lightning that follows the shape of a mesh
	- DemoSceneOutdoorsDay - Demo scene with a storm during the day
	- DemoSceneOutdoorsNight - Demo scene with a storm during the night
	- DemoScenePath - Shows how to create paths with the lightning
	- DemoScenePrefabTutorial - The example scene from my prefab demo tutorial video
	- DemoSceneShape - Shows cone and spherical lightning
	- DemoSceneSpells - Shows an assortment of very cool and fun spells such as force lightning, charged bolt and lightning whip
	- DemoSceneSpline - Shows how to make the lightning curve smoothly through a list of points
	- DemoSceneStrike - Shows how to use particle systems for emission and strike location effects
	- DemoSceneTriggerPath - Shows how to manually trigger path lightning with a list of points and optional spline
	- DemoSceneWhip - a 2D lightning whip

- Prefabs (/Prefab)
	- DarkCloudHeavyParticleSystem - Creates very nice looking and dark storm clouds in the sky
	- LightningBoltBasePrefab - The bare bones script without any extra scripting properties. You'll generally want LightningBoltPrefab for your game instead.
	- LightningBoltPathPrefab - Use this to create lightning that travels through a list of points
	- LightningBoltPrefab - This will be the prefab you use most of the time. Please see the getting started tutorial video (link at the top of this document) for full details.
	- LightningBoltPrefabHDR - HDR prefab with nice glowing effect. Please see the getting started tutorial video (link at the top of this document) for full details.
	- LightningBoltShapeConePrefab - Creates a cone of lightning
	- LightningBoltShapeSpherePrefab - Creates a sphere of lightning
	- LightningBoltSplinePrefab - Use this to create lightning that curves through a list of points smoothly
	- LightningFieldPrefab - Creates chaotic lightning in a certain bounds
	- LightningLightsabrePrefab - Shows how the lightning can be used for a lightsabre blade
	- LightningMeshSurfacePrefab - Creates lightning that attempts to follow the shape of a mesh using the triangles. The full triangle is used, so even a simple cube can have good coverage.
	- LightningWhipPrefab - A simple 2D whip showing how to use spline lightning, physics and sounds
	- ThunderAndLightningPrefab - Drop this in your scene to get automatic, random storm lightning
	- Spells (/Prefab/Spells
		- ChargedBoltSpell - Creates lots of chaotic lightning paticles that explode on impact
		- ForceLightningSpell - Emit lightning from your fingertips!
		- LightningBallSpell - Create a big ball of lightning that explodes on impact
		- LightningRaySpell - Create a beam of lightning / energy that mows down anything in it's path
		- LightningWhipSpell - Imagine being a balrog with a large whip, but instead of fire it's lightning

- Sprite sheet maker (/Prefab/Scenes/LightningSpriteSheetCreator)
	- Sprite maker allows you to put anything in your scene and create a sprite sheet out of it. Simply press play and then click export and a PNG will get created. Use the parameters of the SpriteSheetCreator to setup your sprite sheet. I made this for creating lightning sprite sheets, but you can put anything animated in your scene.
	- I made the charged bolt and lightning ball sprite sheet using this tool

- Sounds
	- Thunder (/Prefab/ThunderSounds)
		- 17 high def and awesome thunder claps for your game
	- Spells and Effects (/Prefab/Sounds)
		- Contains an assortment of shock, electricity and other spell effects

- Textures (/Prefab/Textures)
	- Contains a collection of lightning textures, sprite sheets and cloud textures

- Materials (/Prefab/Material)
	- Contains shaders, material and physics material

- Configuration scene overview (DemoSceneConfigureScript)
This scene allows tweaking of many of the parameters for your lightning. Press SPACE BAR to create a lightning bolt, and drag the circle and anchor to change where your bolt goes.

The goal here is to get a style of bolt that will work for your game or app. Once you have a style you like, you can click "copy" in the bottom right. This will put a C# script on your clipboard which you can paste into your app. It will also put the current "seed" for the lightning into the text box in the bottom right which means the structure of your bolt will be the same each time it is emitted. This is great for cut-scenes or other scenarios where you have an exact style bolt you want. Simply click "clear" to get a random seed again.

The possible parameters are:
- Generations: The number of splits, or details in the lightning. Higher numbers yield higher quality looking lightning, at the cost of additional CPU time to create the lightning bolt. Usually 5-6 generations is good enough, unless you want really nice looking bolts, in which case 8 works great - but remember to only do this on capable hardware if you are generating lots of bolts. This value can be 0 for point light only lightning, otherwise it's clamped between 4-8.
- Bolt count: The number of lightning bolts to shoot out at once. There is a slight delay before successive bolts are sent out. This delay can be changed and will be a variable when you copy the script for your lightning bolt.
- Duration: The total time in seconds that the bolts will take to emite and dissipate. When sending out multiple bolts, each bolt will appear for a percentage of this time (about duration divided by count seconds).
- Start Distance: This moves the source of the bolt closer or further away from the camera. For best results, keep this close to the end distance.
- End Distance: This moves the end of the bolt closer or further away from the camera. For best results, keep this close to the start distance.
- Chaos Factor: As this value is increased, the lightning bolt main trunk spreads out more and become more chaotic and cover more distance. In my testing, 0.1 to 0.25 are good ranges.
- Chaos Factor Forks: As this value is increased, the lightning bolt forks spread out more and become more chaotic and cover more distance. In my testing, 0.1 to 0.25 are good ranges.
- Trunk Width: How wide the main trunk of the lightning bolt will be int Unity units.
- Forkedness: How many forks or splits will your lightning bolt have? If this value is 0, none. If it is 1, lots!
- Intensity: Add additional brightness to the structure of the lightning - useful in HDR only, leave as 1 otherwise.
- Glow Intensity: How bright the glow of the lightning will be. Set to 0 to remove the glow.
- Glow Width Multiplier: Spreads the glow out more. Set to 0 to remove the glow.
- Fade Percent: 0.0 to 0.5, how long the lightning takes to fade in and out during it's lifetime (percent).
- Growth multiplier: How slowly the lightning should grow, 0 for instant, 1 for slow (0 - 1).

*Performance*
Lowering your generations can really improve performance. Every step up in generations is a squared order of increased computation.
The lightning script QualitySetting parameter can be used to tie into the player quality setting, or to use the generations value from your script.
Large trunk widths at close range can really bog down the GPU, so ensure your trunk width is as small as possible.
You can also try turning on the MultiThreaded property on the lightning script. This will generate the lightning in a background thread, freeing your main thread for other tasks. Note that Unity does not yet support creating meshes in a background thread, so sadly this still has to be done on the foreground thread.
Depending on your platform, background thread lightning creation might cause crashes or glitches, so turn it off if you see problems and email me at support@digitalruby.com.
Vote for Unity adding background thread mesh creation here: https://feedback.unity3d.com/suggestions/read-slash-write-mesh-on-background-thread
Set LevelOfDetailDistance to a value to lower the generations automatically as the lightning is further from the camera. Each amount of LevelOfDetailDistance the lightning is away from the camera, the generations are reduced automatically internally.

*Positioning and scaling the lightning*
LightningBoltParameters has a static Scale field that can globally change the scale of a few properties, such as trunk width.
If you are using world space coordinates (the default), the start and end coordinates are assumed to be in world space.
If you change to local coordinates, then your start and end coordinates are assumed to be relative to the parent game object.
For an example of local coordinates, see DemoSceneShape. The lightning is affected by the transform of any parent game objecst.

*Scene view*
Here is how to rapidly prototype and design lightning in your scene:

- Drag one of the prefabs into your scene
- Press play
- Tweak the values to your liking
- Copy the root game object of the prefab
- Stop play
- Paste in the new object
- *Important* The parent object and object with the lightning script attached should have position and rotation values of 0, and scale of 1. See the DemoScenePath for an example.

*Rendering*
LightningBoltScript.cs has a quality setting option. This can be:
- Use Script
	Whatever settings specified in the script will be used, regardless of quality setting
- Use Quality Settings
	The global quality setting determines maximum generations, light count and shadow casting lights. By default this will work with Unity's 6 default quality levels. If you have set up your own levels, you may want to re-populate the QualityMaximums of the LightningBoltParameters class	in LightningBoltScript.cs to be appropriate for your custom quality levels.
- Lightning lights, shadows and soft particles work best with Deferred rendering

*Lighting*
Lightning can emit lights. See the LightningLightParameters class, as well as the DemoSceneLight for examples of how this works. Please review the light parameter documentation carefully as lighting can impact performance, especially in forward rendering.
See LightningBoltScript.cs and the maxLightCount constant to determine how many lights lightning can generate at maximum. If you are using lights with an orthographic camera, they will have a z value set to the camera z position.

*Spells*
I've put together a nifty spell system for creating spells. The spell system will work for non-lightning spells too if you want. DemoSceneSpells shows them all in action.

The work-horse of the spells is LightningSpellScript. All spells should inherit from this class.

LightningBeamSpellScript is for spells that use the lightning bolt script and shoot in a generally straight line, instantly.

LightningParticleSpellScript is for lightning that shoots out projectiles, like the lightning ball and charged bolt.

LightningWhipSpell is a specialized script that lets you wield a whip made out of lightning!

If you need to make a spell, look at how the spell prefabs are done. Depending on the spell you want, it's probably easiest to clone either the beam or projectile prefab and start your spell from it. Most spells can be made without additional scripting.

The spells apply forces upon collision but there is no damage or other health altering system built in. To apply damage or other extra effects, you will need to hook into the collision callback of the spell script and do what is appropriate for your game.

*Notifications*
Set LightningStartedCallback and LightningEndedCallback on lightning bolt script to be notified when bolts are started and ended.

*Orthographic Camera*
Procedural lightning supports perspective cameras, but also supports orthographic cameras and even supports the XZ plane with an orthographic camera. Set the CameraRenderMode property of the script if needed. Auto usually works fine unless you need the XZ plane.

*Pooling and enabling / disabling game objects*
Procedural Lightning does it's own caching and pooling. When you load a new scene, the cache is cleared.

If you need to pool lightning objects or turn off the lightning, don't set active or enabled / disabled states. Rather, just set the script to be in manual mode until you need to use it again, then set it back to automatic. This allows the lightning animation to start and stop cleanly.

*Misc*
- LightningBoltParameters has a Random property. You can use this if you want to create a bolt that will look the same every time. Use the System.Random constructor that takes a seed property. The default is a new System.Random.
- Lightning glow requires an extra pass in the shader, so you may want to disable this on lower end mobile devices by setting the glow intensity or the glow width to 0.
- LightningBoltScript.cs, class LightningBolt has public static variables that control limits for lighting
- LightningBoltScript contains a source and destination blend mode. This can be used to change from the additive shader (default) to other kinds of blending, like aplha blending.
- Clear the cache by calling LightningBolt.ClearCache() - this is automatically called on scene load.

*Troubleshooting*
- Unity 5.6.2 is the most recent version of this asset which has tons of improvements and performance enhancements. I *HIGHLY* recommend using at least this Unity version with the latest version of the asset.
- Otherwise if you are still having problems, send me an email.

*Credits*
http://www.freesfx.co.uk

Not everything may be covered in this readme and this asset contains a lot of options and code. If you are confused, have a question or need guidance, please contact me. I'm always happy to answer questions. Please email me at support@digitalruby.com and I'll be happy to assist you.

Thanks for purchasing Procedural Lightning for Unity.

- Jeff Johnson, CEO Digital Ruby, LLC
support@digitalruby.com
http://www.digitalruby.com
