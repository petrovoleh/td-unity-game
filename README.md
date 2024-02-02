# Tower Defense 2D Game

## Table of Contents
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

## Introduction

This repository contains the source code for a Tower Defense 2D game with strategy and AI features. The game is developed using Unity Game Engine and C# programming language. This README provides an overview of the project structure, design choices, and functionality.

## Preface

The `preface.tex` file contains configurations for the report, such as the language (Lithuanian or English) and the need for a preface. Additionally, the `macros.tex` file includes custom macros used throughout the project.

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

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
