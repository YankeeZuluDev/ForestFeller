# ForestFeller
A repository for "Forest Feller" game source code

[:camera: **See Screenshots**](#screenshots)

[:movie_camera: **See gameplay video**](https://www.youtube.com/watch?v=EBuS0Xp_in8)

[:video_game: **Play**](https://play.google.com/store/apps/details?id=com.Yankeezulu.ForestFeller)

## About the game
Genre: hyper-casual, hybrid-casual

Mechanics: collecting, stacking, producing

Unity version: 2021.3.18f1 (LTS)

Accessibility: due to the use of some copyrighted assets in this project (Adobe Mixamo), it is temporarily cannot be freely explored in Unity

## Screenshots

<div style="display:flex;">
  <img src="https://github.com/YankeeZuluDev/ForestFeller/assets/129124150/6118a35f-9ad2-42e4-b560-680f06374ad2" alt="screenshot_1" width="270" height="480">
  <img src="https://github.com/YankeeZuluDev/ForestFeller/assets/129124150/915e1403-8b5b-415c-8d1b-f4408078ec91" alt="screenshot_2" width="270" height="480">
  <img src="https://github.com/YankeeZuluDev/ForestFeller/assets/129124150/74fe78a6-68e1-486a-b0d2-8672a7ac4066" alt="screenshot_3" width="270" height="480">
</div>

## Best сode practices in this project

### Composition over inheritance
This project implements the Composition Over Inheritance principle to create the system that relies on horizontal relations between different game parts. Rather than relying on inheritance from a base or parent class, the project is using composition to achieve polymorphic behavior and code reuse. In this project, composition is achieved by extensive use of interfaces and carefully structured entity systems. Entities are created by composing various little reusable components, enabling flexible and modular design. For instance, the Factory entity comprises the [ResourceReceiver](https://github.com/YankeeZuluDev/ForestFeller/blob/main/Assets/Scripts/ResourceProcessing/ResourceReceiver.cs), [Factory](https://github.com/YankeeZuluDev/ForestFeller/blob/main/Assets/Scripts/ResourceProcessing/Factory.cs), [Storage](https://github.com/YankeeZuluDev/ForestFeller/blob/main/Assets/Scripts/Storage/Storage.cs), and [ResourceProvider](https://github.com/YankeeZuluDev/ForestFeller/blob/main/Assets/Scripts/ResourceProcessing/ResourceProvider.cs) components. Similarly, the Player entity is composed of the [InteractableDetector](https://github.com/YankeeZuluDev/ForestFeller/blob/main/Assets/Scripts/Interaction/InteractableDetector.cs), [ResourceReceiver](https://github.com/YankeeZuluDev/ForestFeller/blob/main/Assets/Scripts/ResourceProcessing/ResourceReceiver.cs), [ResourceStack](https://github.com/YankeeZuluDev/ForestFeller/blob/main/Assets/Scripts/Stack/ResourceStack.cs), [Storage](https://github.com/YankeeZuluDev/ForestFeller/blob/main/Assets/Scripts/Storage/Storage.cs), and [ResourceProvider](https://github.com/YankeeZuluDev/ForestFeller/blob/main/Assets/Scripts/ResourceProcessing/ResourceProvider.cs) components. By adding or replacing these reusable components, we can achieve diverse and customizable behavior.

<div style="display:flex;">
  <img src="https://github.com/YankeeZuluDev/ForestFeller/assets/129124150/c41c39cc-6ecf-42ba-b748-a8779a95d441" alt="screenshot_4" width="334" height="632">
  <img src="https://github.com/YankeeZuluDev/ForestFeller/assets/129124150/ab668773-719a-4279-8ff4-44c25826caa4" alt="screenshot_5" width="334" height="632">
</div>

### DIP
This project [implements](https://github.com/YankeeZuluDev/ForestFeller/tree/main/Assets/Scripts/Interfaces) Dependency Inversion Principle. Thanks to thanks to the extensive use of interfaces, high level modules do not depend upon low level modules. Both depend upon abstractions.

### ISP
This project [implements](https://github.com/YankeeZuluDev/ForestFeller/tree/main/Assets/Scripts/Interfaces) Interface Segregation Principle. All interfaces are small and specific, keeping the code decoupled and thus easier to change and refactor.

### Object pooling
This game uses object pooling to efficiently manage and reuse gameobjects within the game. Object pooling minimizes the overhead of creating and destroying bullet objects dynamically, resulting in improved performance and reduced memory allocation. [Code](https://github.com/YankeeZuluDev/ForestFeller/blob/main/Assets/Scripts/ObjectPools/ResourcePools.cs)

### Split responsibilities
Class responsibilities in this project are well defined and separated. Each class is responsible for only one thing. [Code](https://github.com/YankeeZuluDev/ForestFeller/tree/main/Assets/Scripts/ResourceProcessing)

### Game event system
This game uses an event system to handle in-game events such as GameEndEvent or GameResetEvent. The event system is implemented uisng ScriptableObjects, making it simple, convenient and extendible. The event system consists of 2 classes: GameEvent and GameEventListener. [GameEvent class](https://github.com/YankeeZuluDev/ForestFeller/blob/main/Assets/Scripts/Events/GameEvent.cs) provides a way to create custom game events that can be triggered by different components. It allows for flexible event handling and communication between different parts of the game. [GameEventListener class](https://github.com/YankeeZuluDev/ForestFeller/blob/main/Assets/Scripts/Events/GameEventListener.cs) is responsible for listening to a specific GameEvent and triggering a UnityEvent response when that event is raised. GameEventListener can be attached to any gameobject. Event system is implemented using [observer pattern](https://en.wikipedia.org/wiki/Observer_pattern).

### Custom level constructor window
This project has [custom editor window](https://github.com/YankeeZuluDev/ForestFeller/blob/main/Assets/Editor/LevelConstructor.cs), that is used to construct and save levels. Upon dragging gameobjects to the scene, gameobjects with component derived from the Spawnable class will automatically have a corresponding data class generated and saved to level definition when the "Save Level Definition" button is pressed. This approach provides convinient and flexible way for level creation. Levels are stored as [LevelDefinition](https://github.com/YankeeZuluDev/ForestFeller/blob/main/Assets/Scripts/Level/LevelDefinition.cs) ScriptableObjects. LevelDefinition class consists of 3 parts: the length of the level, the width of the level and an array of [data classes for spawnables](https://github.com/YankeeZuluDev/ForestFeller/blob/main/Assets/Scripts/ResourceData/SpawnableData.cs), that contain all the necessary information to instantiate spawnables at runtime.

### Prefab-based project architecture
This project uses prefab-based design approach, utilizing the power of prefabs to create and organize all game parts. Prefab-based approach allows for reusability, efficiency, easier testing, better collaboration and significant time and effort savings when implementing new features to some game parts. Additionally it allows for more convenient game initialization and dependency resolving.
