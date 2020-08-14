# Epidemic Choices - Educational Interactive Unity Visualization

An interactive visualization made in Unity, made to show all the possible problems that can come from dealing with an epidemic. The visualization shows how hard it is to strike a balance between hospital space, prevention budget, people's mental health and the risk of deaths. The visualization is made to run for a 100 days, with the user affecting different parts of the simulation, by choosing to close schools, prevent people from going outside, stopping outdoor sports or mandating how many times a day children have to wash and sanitize their hands.

The simulation can have multiple different outcomes, depending on users' choices. Once the visualization is over, a screen is shown with an overview of user's choices and what it ment for the simulation. Through this, a user can have reflection on the experience and try again - choosing a different path.

The game is made for Unity WebGL and is currently in alpha state. It currently contains only hospitals, schools and houses. But additional places can be added like supermarkets, parks, workplaces, etc.

The game is still in development. All the scripts are commented, with additional in Inspector comments. The code is quite messy and lacks a lot of refactoring and optimizations. In currently relies on variable reflection assigments and contains redundancies, which can be fixed with better exploiting of class inheritance. Global classes are set as singletons, but the code still lacks a lot of proper encapsulation.

## Try the Game
The game can be played directly on the [itch.io page ]()
<!-- [Image Start Screen](docs/CONTRIBUTING.md)-->

## Getting Started
The repo contains the core Unity code for the Infection Defender game, with all used fonts, audio and model assets. For the appropriate licenses look at the License part of the readme. To get started, follow the instructions below:
1.  [Make sure you have all Requirements](#-requirements)
2.  [Download Source Code](#-installing)
3.  Run the project in Unity and Enjoy!

### Requirements
For getting started with the project you will need:

 - [Unity Game Engine ](https://unity3d.com/) 2019.3.7f1 or later
 - WebGL exporter installed, if you want to build it
 - Basic knowledge of Unity and C#
 - **Additionally**, install Visual Studio 2017 or newer or Visual Studio Code



### Installing
There are a number of ways to get started with the Unity project:

 - Clone the repo
```
git clone https://github.com/COVID-19-WP4/covid19-unity.git
```
 - Download the newest release: [LINK]()

## Code Structure:

Global Classes:

   - **Setup Resources** - setup all the necessary initial resources, like number of regions, number of hospitals, schools, houses, people, etc.
   - **Global Timer** - a global timer counting passed hours and days. It also sends events on day passed and when specific hours are reached every day
   - **SEIR Implementation** - implementation of the SEIR model for infection spread, using susceptible, exposed, infected and recovered people. The class also takes care of the other "resources" that change every day - number of deaths, prevention budget and overall mental health
   - **Global Events** - initialization of the global events like day passed, go to school, open/close schools, going out, sports, hand sanitazation change, etc.
   - **Global Map Rules** -keeps track, which classes are opened, as well as which other activities are open/closed every day
   - **Check End Conditions** - keeps track, if the any of the end game states are reached
   - **Debriefing Gather** - gathers data each day for the debriefing part of the visualization, once the game is over. Examples of data - maximum exposed, maximum infected, all dead, used budget, minimum mental health, days closed for each class, etc.
   
	 
Game Object Worker Classes:

  - **RTS Camera** - a ready made RTS camera movement script for smooth pan, tilt and zooming of a top down camera.
  - **Building** - main parent class for all buildings, created in game. Contains maximum residents, curr residents, building address and building type
  - **House** - house class, child of Building. Contains the state of the house, if there is infected, exposed, dead, etc. It also contains a list of the residents both adults and children
  - **Hospital** - hospital class, child of Building. Contains, who can go to the hospital
  - **School** - school class, child of Building. Contains a list of enrolled students
  - **Person** - main parent class for all people, created in game. Contains a reference to the house object, address, current state and current place through the day. It also contains the reference to the hospital to which it needs to go, it's age and a reference to if it is moving or not. It contains methods for moving it and changing its color
  - **Child** - child class, child of Person. Contains the reference to the school address, the school object, the grade.
  - **Adult** - adult class, child of Person. Contains work address and work object references.
	 
UI Menu Worker Classes:

  - **Infected Bar** - script for showing the infection bar, showing how many people are in the hospital
  - **Economics Bar** - script for showing the economics bar, showing how much of the budget is left
  - **Mental Health Bar** - script for showing the mental health bar, showing how much mental health is left
  - **GUI Interactions** - main GUI class containing methods for visualizations and events for buttons, sliders and toggles
  - **Restart Game** - script containing the restart functionality
  - **Window Graph** - script containing all the necessary methods for visualizing the real-time graph. The graph shows all the parts of the SEIR model
  - **Change School Toggle** - script for detecting which class has been opened and closed. It changes the mental health for each class, which can change the overall mental health.
  
## Authors

 -  **Ivan Nikolov**  -  _Main Development and 2D art assets_  -  [University PhD Profile](https://vbn.aau.dk/en/persons/136267)
 -  **Bastian Ilso**  -  _3D art assets_  -  [University Profile](https://vbn.aau.dk/en/persons/137891)
 - Additional input and ideas:
	 - **Claus Madsen**
	 - **Thomas Moeslund**
	 - **Hendrik Knoche**
	 - **Martin Kraus**


## License

This project is licensed under the MIT License. Acknowledgement to [Aalborg University](https://www.en.aau.dk/) will also be greatly appreciated.

## Acknowledgments

-   Brackeys  [Unity Health Bar](https://youtu.be/BLfNP4Sc_iA)
-   Code Monkey [Building 2D graphs in Unity](https://youtu.be/CmU5-v-v1Qo)
-   Christian Hubbs [SEIR Model Overview and Python Code] (https://towardsdatascience.com/social-distancing-to-slow-the-coronavirus-768292f04296)
-   Denis Sylkin [Unity RTS Camera](https://assetstore.unity.com/packages/tools/camera/rts-camera-43321)
