# Tower Defense 2D Game

## Table of Contents
- [Abstract](#abstract)
- [Introduction](#introduction)
- [Preface](#preface)
- [Preliminary Design](#preliminary-design)
  - [Menu](#menu)
  - [Game Screen](#game-screen)
- [System Architecture](#system-architecture)
  - [High-level Overview](#high-level-overview)
  - [Deployment](#deployment)
  - [Technologies and Tools](#technologies-and-tools)
  - [Front-end](#front-end)
  - [Back-end](#back-end)
  - [Conceptual Modelling](#conceptual-modelling)
- [Functional Requirements](#functional-requirements)
  - [High Priority](#high-priority)
  - [Medium Priority](#medium-priority)
  - [Low Priority](#low-priority)
- [Non-functional Requirements](#non-functional-requirements)
  - [Compatibility](#compatibility)
  - [Reliability](#reliability)
  - [Security](#security)
  - [Performance](#performance)
- [Algorithms and Data Storage](#algorithms-and-data-storage)
  - [Algorithm Overview](#algorithm-overview)
  - [Real World Example](#real-world-example)
  - [Data Storage](#data-storage)
- [Testing](#testing)
  - [Unit Tests](#unit-tests)
  - [Integration Tests](#integration-tests)
  - [System Tests](#system-tests)
  - [GUI Tests](#gui-tests)
  - [Acceptance Tests](#acceptance-tests)
- [Competitive Analysis](#competitive-analysis)
  - [Analysis](#analysis)
    - ["Bloons TD 6" by Ninja Kiwi](#bloons-td-6-by-ninja-kiwi)
- [Conclusions and Recommendations](#conclusions-and-recommendations)


## Abstract
The poster presents "Monsters and Mages" - fantasy themed 2D tower defense game. Tower de-
fense is a popular game genre of real-time strategy which requires difficulty balancing and content
for replayability to make a fun player experience. First the famous “Bloons TD” games were
analyzed for inspiration on gamemodes, enemies and game mechanics. A game development en-
gine "Unity" will be used to make the 2D Tower defense game using C# Language for Windows
platforms. The Game will have PostgreSQL database to store player progress and a webserver to
connect this database with the game client. The expected result is to have a player vs AI game
where enemies walk in a given path and player has to use currency to defend against a horde of
enemy waves. There will be playable maps with many towers and enemy variations. You will
be allowed to pick a "Challenge" gamemode where you will be placed in a map with conditions,
where you can only use certain towers. Also a "Sandbox" gamemode where you will be allowed to
place as many towers as you want and and fight a ever increasing difficulty of monsters, just like
in the "Endless" gamemode, where you need to hold out as long as you can. There will be boss
monster that have special abilities like other, more weak monsters. Users will be able to save data
by creating an account and transferring data by logging in into another device. So player don’t
need to worry about losing their data.

## Introduction

This repository contains the source code for a Tower Defense 2D game with strategy and AI features. The game is developed using Unity Game Engine and C# programming language. This README provides an overview of the project structure, design choices, and functionality.
The video game is a top-down 2D Tower Defense game. Made for Windows desktop devices. The theme of our game is magic. The enemies of the player are fantasy monsters, whereas the towers are magical wizards. The Video game consists of many waves, the player loses the game if the monsters reach the end of the level, as one monster damages a specified amount of Health Points and if it reaches 0 the game ends. The enemies and wizards all have special abilities that either help the player or do not. 

## Preface

This project was done during the 3rd semester of the study programme Information Technologies
in the specialization of Innovative studies. We have chosen the topic Tower Defense. 2D game
with strategy and AI features. suggested during the project market on the first lecture of the subject
"Problem-Based Project". The topic seemed very interesting.
## Preliminary Design

### Menu

Upon opening the application, the user is presented with the main menu, providing options like "Play," "Sandbox," "Endless," and "Exit." Users can also log in or register to save their progress. The menu design allows for a seamless navigation experience.

### Game Screen

The game screen displays the map, health, and currency information. Users can strategically place towers to defend against waves of enemies. The game features various tower types, each with unique abilities. The architecture allows for easy map progression and saving/loading game states.

## System Architecture

### High-level Overview

The game architecture consists of the Video Game Application, Database, and Web Server. The Video Game Application enables players to play and interact, while the Database stores player data and progression. The Web Server connects the database and the client-side application, serving HTTP requests and ensuring secure communication.

### Deployment

The deployment utilizes a UML Deployment Diagram, showcasing the system's high-level interactions between components. The game runs on Unity Game Engine, with the web server using .Net Core framework and PostgreSQL for the database.

### Technologies and Tools

- Unity Game Engine (C#)
- .Net Core Framework
- PostgreSQL
- NGINX
- JetBrains Rider IDE

### Front-end

The front-end of the game application is designed in Unity, using a UML Diagram to illustrate the required variables and functions. The main menu, scene changing, and web scripts are notated in the diagrams.

### Back-end

The back-end consists of a web server and a PostgreSQL database. The web server handles data communication between the client-side app and the database. It generates JWT tokens for secure player authentication and includes functions for loading, adding, and modifying data in the database.

### Conceptual Modelling

The Entity Relation diagram illustrates the relationships in the database, including entities like player, challenge, map, passed_challenge, and beaten_map. Relationships ensure data integrity and support various game features.

## Functional Requirements

### High Priority

- Manually crafted waves of enemies (20 waves).
- Placeable towers with a shooting system.
- Playable maps.

### Medium Priority

- Unique tower abilities and upgrades.
- Saving function to save current map progress or beaten map progress.

### Low Priority

- Game state save and load function.
- Sandbox, Endless, and Challenge game modes.

## Non-functional Requirements

### Compatibility

The game should run on the majority of Windows devices.

### Reliability

The game should be free of critical bugs, providing a non-frustrating experience.

### Security

- Database and server deployed on separate virtual machines for security.
- JWT tokens for secure player authentication.

### Performance

The game should run at a consistent frame rate on recommended system requirements.

## Algorithms and Data Storage

### Algorithm Overview

The game utilizes pathfinding algorithms to determine enemy movement on the map. A* algorithm is employed for efficient pathfinding.

### Real World Example

The A* algorithm uses a heuristic to find the shortest path, similar to a GPS navigating a map to find the fastest route.

### Data Storage

The PostgreSQL database stores player data, maps, and game progress. It ensures data integrity through relationships between entities.

## Testing

### Unit Tests

Unit tests cover individual functions and methods in the game's codebase, ensuring they work as intended.

### Integration Tests

Integration tests check the interactions between various components, such as the game client, web server, and database.

### System Tests

System tests validate the overall functionality and user experience of the game, including game modes and map progression.

### GUI Tests

GUI tests focus on the graphical user interface, checking buttons, menus, and overall visual presentation.

### Acceptance Tests

Acceptance tests ensure that the game meets specified requirements and is ready for release.

## Competitive Analysis

### Analysis

#### "Bloons TD 6" by Ninja Kiwi

**Advantages:**
- Engaging tower upgrades and strategic depth.
- Longevity and continuous updates.
- Well-balanced difficulty for various player skill levels.

**Disadvantages:**
- Steep learning curve for new players.
- In-app purchases for additional content.

## Conclusions and Recommendations

To conclude this report, it needs to be said that it’s very important to have a final design in mind
and to communicate with the team about the projects future development so that new ideas don’t
pile on top, hindering the production of said project. That could be done by making a preliminary
design and talking about all the features with great detail. While also deviding the team to work on
different parts of the project to increase efficiency, but also starting at the basic parts of the project,
which will be needed to construct the ideas from ground up rather then starting at something
difficult that would make it harder to integrate the basic parts to the project.
Being able to finish most of the funtionalities satisfies the functional requirement priorities, there
are still parts that need to be improved and functionalities that still need to be made to contest
the more popular games of the same gienre. Functionalities like smarter targeting to help with
the towers letting through enemies, tower upgrades for more intresting gameplay for the user and
many quality of life content to help the new player understand the game, with tutorials or tooltips.
All of these ideas could improve the user enjoyment from the application.


## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
