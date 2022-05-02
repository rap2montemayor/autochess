# Aedin's Documentation

# INVENTORY

## Adding to Scene

For Inventory data / backend : add InventoryData prefab  anywhere in scene
For Inventory menu / front-end : add InventoryManager prefab to canvas

## Accessing the inventory backend

To access the inventory backend, use "Inventory.instance"
For example, this line gets the Inventory List for items:

InvList<Unit> units = Inventory.instance.units;

## Inventory backend functionality

You can access the units/bench/board/item lists via above method.
You can also move units between inventory, board, and bench via provided functions

## Inventory List / InvList

This is just a list with some extra features. Here are the methods:

1. LoadFrom - loads an array into this InvList
2. SaveTo - returns the internal array
3. Add(T obj, int index) - adds an object (unit or item) at that index
4. Add(T obj) - adds an object at the first empty / null index
5. Swap - swaps
6. Remove - removes object at index
7. At - gets object at index

You can ignore the ienumerator functions, they just let you do a for loop on the InvList

## Backend Use Example

Let's say you want to heal all units on the board, how would you do this?

- For loop through the board list

for (Unit unit : Inventory.instance.board){
	unit.hp_current = unit.hp_max;
}

** note : you may have to change the above fields to public 


## Accessing the inventory frontend

No static instance for this. You'll need to use an inspector reference or a FindObjectByType<InventoryManager>() to access this.

## How the Frontend works

The InventoryManager controls a set of InvWindows. Each window corresponds to an InvList in the Inventory backend. Use InvWindow's SetData() methods to choose the list it displays. 

There's also the enemyData list which stores an enemy's units

InvWindow has a SetInteractable method, it just determines whether you can drag / drop from the window's  slots or not.

Each InvWindow consists of a number of slots, one slot for each array index in the InvList.
You can drag/drop objects to each slot.

Slots contain a static / shared reference to the InventoryManager, so please only have one manager at all times. 

OnPointerClick() handles all the drag/dropping stuff. When a slot is clicked, a new gameObject containing a sprite of the object in the slot is created. That gameObject follows the mouse until you click on another slot. 

TryCommitToBackend() checks whether the slot you're trying to drop the object on is valid. Right now it's super hard-coded so it will only work specifically with unit, bench, board, enemy. When the right match is found, the method will access the inventory and perform the appropriate operation (swapping objects, moving objects, etc). 

Then the InventoryManager's display is refreshed / updated to show the changes.

DisplaySlotInfo(Slot slot) in the InventoryManager just shows the object description text; it is currently very hard-coded so if possible please fix.






# MISCELANEOUS 

## Dont Destroy on Load

This script prevents gameObjects from being destroyed when a new scene is loaded, useful for having continuous audio for example. 
Right now I use it in SceneLoader to have data transferred from one scene to the next (right now, only start location positions, see below for more detail)

## Fade

If you want a gameObject to fade / unfade, use this

SetVisibility for instant changes, FadeIn(float seconds)  / FadeOut(float seconds) for gradual

## FollowMousePosition

Used for temporary slot sprite objects when dragging, it literally just follows your mouse position

## TextBox

Flexible text box script. Use the TextBox prefab for easy use

ShowBox() / HideBox() for visibility

DisplayTextNow() to instantly display some text

DisplayTextSlowly() to write out each char in the texts

ShowTextDone() just says whether the DisplayTextSlowly() is done writing out the text

Supports rich text like <b></b>

# VisibleInEditorOnly

if you want a gameObject visible in editor only (like the door script) use this




 

# OVERWORLD

## Player Controller
your standard run-of-the-mill player movement script, use WASD / arrow keys to move. 
Currently the movement is jittery, please fix if you know how.

can also activate or deactivate the player controller - good for cutscenes or opened menus when you want to stop player movement. these are static functions so you can access them from anywhere via PlayerController.activate() or PlayerController.deactivate(). REMEMBER TO ACTIVATE AFTER DEACTIVATING 



## Dialogue Overview

Dialogue controls a certain TextBox to display text, go to the next line when spacebar is pressed, and more. 

There are two ways to display text.

public static void SayLine(string line) will say a single line of dialogue, then close the texbox when you press spacebar. 

public static void StartDialogue(string inkDialogueEntry) starts a process that allows for multiple lines, choice, a whole bunch of other things. it's the best thing ever

## accessing dialogue

SayLine and StartDialogue are both static methods, for example:
Dialogue.SayLine("hello there") 


## InkStory

Inkle is the dialogue engine used to power award-winning mobile game "80 Days". Lucky for us, the company has made it free to use by anyone. The engine / script is pretty good.

The InkStory script forms a wrapper around their API. The only thing it should ever interact with is the Dialogue script. 

Ink story script is found in the .ink files in the Dialogue file. There's an ink extension on vscode for syntax / highlighting, I think the one made by a certain "Bruno Dias". 

To learn the scripting language:
- https://github.com/inkle/ink/blob/master/Documentation/WritingWithInk.md

If you want to extend the InkStory wrapper class:
- https://github.com/inkle/ink/blob/master/Documentation/RunningYourInk.md

## StartDialogue()

I've arranged the ink script so that each "conversation" / set of dialogue has its own entry. StartDialogue(inkDialogueEntry) will go to that entry and display it on the TextBox.

the inkDialogueEntry string should be the same as the name of the dialogue entry in the .ink file you're trying to go to. Look at those files for examples.

## NextDialogue()

this is triggered in the Update() loop when spacebar is pressed and there is currently an active dialogue entry. It will proceed to the next line of dialogue in the ink entry and display it.

If the next dialogue entry is in fact a choice, it sets inChoice to true and displays the first option. 

If there are no more available lines of dialogue it closes the textbox and ends things.

## Choice()

In the update loop, if inChoice is true and an arrow key / WASD is pressed, Choice will switch the currently selected Choice and display that one.

## InputDelayIsDone()

determines if a certain amount of time has passed since the last time this function was called. this is just so that the same spacebar press will not trigger NextDialogue() 10 frames in a row. 

## GetSpeakerName()

for ActivatorSpeaker (more on that later), this just displays the name of the speaker in a certain color

## ProcessTags()

this is cool. you can add tags to inkle script dialogue and read them here. 
right now there are only two tags: NOSKIP and SETSPEAKER. NOSKIP prevents you from skipping dialogue - cool for high-impact lines, but it also fuels Counting Person. SetSpeaker, look at ActivatorSpeaker for details.
You can do a lot more with this though. for example a tag to add items to inventory, or give you gold, or activate a script that unblocks a pathway. We just need to implement all the cool stuff.


## Activators and the Selector
An activator is a gameObject that you can "activate" when your character is nearby and you press spacebar. All types of activators derive from the Activator class.

The Selector is just a collider on the player that will select the nearest Activator object and activate() it when spacebar is pressed.

## ActivatorAddItem

a limited test script, but it is useful as it teachs how to use the inventory backend, and how activators work, and using Dialogue.SayLine()

## ActivatorDialogue

This will start a dialogue, based on the inkDialogueEntry provided

## ActivatorSpeaker

same thing as activatorDialogue except it also provides a speaker. the speaker consists of two things - speaker name and speaker name color. the name is always displayed at the start of dialogue. 
They are arrays so that you can have multiple speakers - useful for a conversation for example.
 
SET SPEAKER tag from ProcessTags just changes the current speaker, using an int to select the wanted index from the array of speakers



## Loading new scenes

involves four scripts - Door, PlayerStartLoc, PLayerStartLocBoss, SceneLoader


## Start Locations via PlayerStartLoc

where will the player start when they enter a scene? you can set these start locations with the PlayerStartLoc script. 
Each start location has an index. DO NOT use an index of 0, this is reserved for the original start loc (where you put the player in the editor)

PlayerStartLocBoss will get all of the start locations and place the player at the location at the index provided by SceneLoader

## SceneLoader

loads a new scene. takes in a scene name, and a optionally start location index. the player will spawn in the new scene at the location of the start location with the given index.

## Door

this consists of two things

first - a red area, if you touch it you get teleported to another scene via SceneLoader
second, a white area which serves as a start location. 

DON'T put the white area on the red area, if you spawn on the white area you will be insta-teleported back by the red area. also be careful so that they're not too near each other.

red/white areas are invisible in play mode









