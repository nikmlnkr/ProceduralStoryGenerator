# AI Procedural Story Generator for Unity

A comprehensive Unity system for generating procedural narratives, characters, locations, and branching dialogues with AI integration support.

## 🎯 Features

### Core Systems
- **Story Template System** - Flexible ScriptableObject-based story templates
- **Character Generator** - Dynamic character creation with personality traits and relationships
- **Location Generator** - Rich location system with atmosphere and events
- **Branching Dialogue** - Tree-based conversation system with conditional logic
- **World State Manager** - Comprehensive state tracking and story progression
- **AI Integration** - Placeholder system ready for OpenAI, Anthropic, or custom AI providers

### Generation Capabilities
- Procedural story generation from templates
- Character relationships and personality traits
- Location atmosphere and hidden secrets
- Branching dialogue with emotional weights
- Story progression tracking
- Morality and tension systems

## 🚀 Quick Start

### 1. Setup
1. Open Unity 6000.1.5f1 or later
2. All scripts are located in `Assets/Scripts/StoryGenerator/`
3. The system is organized into namespaces for clean organization

### 2. Basic Usage
```csharp
// Create a StoryGeneratorManager in your scene
GameObject storyObject = new GameObject("Story Generator");
StoryGeneratorManager manager = storyObject.AddComponent<StoryGeneratorManager>();

// Generate a story
manager.GenerateStory();
```

### 3. Demo Scene
Add the `StoryGeneratorDemo` component to a GameObject for instant testing:
- **Space** - Generate new story
- **A** - Test AI integration
- **H** - Show help

## 📁 Project Structure

```
Assets/Scripts/StoryGenerator/
├── Core/                     # Core classes
│   ├── CharacterProfile.cs   # Character system
│   ├── StoryLocation.cs      # Location system
│   ├── DialogueNode.cs       # Dialogue system
│   └── AIIntegration.cs      # AI placeholder system
├── Templates/                # ScriptableObjects
│   └── StoryTemplate.cs      # Story template system
├── Managers/                 # Main systems
│   ├── StoryGeneratorManager.cs  # Core manager
│   └── WorldState.cs         # State tracking
├── Data/                     # Sample data
│   └── StoryDataInitializer.cs   # Sample templates and data
└── Demo/                     # Demo and testing
    └── StoryGeneratorDemo.cs     # Demo script with UI
```

## 🎮 Usage Examples

### Generate a Story
```csharp
StoryGeneratorManager manager = FindObjectOfType<StoryGeneratorManager>();
manager.GenerateStory();  // Check Console for output
```

### Create Custom Characters
```csharp
CharacterProfile hero = new CharacterProfile("Nova", CharacterRole.Protagonist, "Save the city");
hero.AddPersonalityTrait("brave");
hero.AddPersonalityTrait("impulsive");
```

### Build Dialogue Trees
```csharp
DialogueNode root = new DialogueNode("Hero", "I won't let you destroy this city!");
DialogueNode response = new DialogueNode("Villain", "You're too late!");
root.AddResponse(response);

DialogueNode playerChoice1 = new DialogueNode("Hero", "We'll see about that!");
DialogueNode playerChoice2 = new DialogueNode("Hero", "Maybe we can find another way?");
response.AddResponse(playerChoice1);
response.AddResponse(playerChoice2);
```

### Track World State
```csharp
WorldState world = new WorldState();
world.SetFlag("hero_met_mentor", true);
world.CompleteEvent("tutorial_completed");
world.AdvanceStory(25);  // 25% progression
world.AdjustMorality(0.3f);  // Slightly more good
```

### AI Integration (Placeholder)
```csharp
// Configure AI
AIIntegration.ConfigureAI(AIProvider.OpenAI, "your-api-key", true);

// Generate content
string narrative = await AIIntegration.GetNarrativeFromAI("A cyberpunk story about rebellion");
CharacterProfile character = await AIIntegration.GenerateCharacterWithAI("dystopian setting", CharacterRole.Protagonist);
```

## 🔧 Customization

### Create Story Templates
1. Right-click in Project → Create → Story Generator → Story Template
2. Fill in genre, setting, conflict, resolution, and tags
3. Add to StoryGeneratorManager's templates list

### Custom Data Initializer
1. Right-click in Project → Create → Story Generator → Data Initializer
2. Run "Initialize Sample Data" from context menu
3. Use "Apply To Story Generator" to load data

### AI Provider Integration
The system includes placeholders for multiple AI providers:

```csharp
// OpenAI (placeholder)
AIIntegration.ConfigureAI(AIProvider.OpenAI, "sk-...", true);

// Anthropic Claude (placeholder)
AIIntegration.ConfigureAI(AIProvider.Anthropic, "your-key", true);

// Local AI (Ollama, etc.)
AIIntegration.ConfigureAI(AIProvider.Local, "", true);
AIIntegration.SetCustomEndpoint("http://localhost:11434");

// Custom endpoint
AIIntegration.ConfigureAI(AIProvider.Custom, "", true);
AIIntegration.SetCustomEndpoint("https://your-ai-api.com");
```

## 📊 Sample Output

```
=== GENERATED STORY ===
Genre: Cyberpunk
Setting: Ruined megacity controlled by AI
Main Character: Rhea, a brave, impulsive cyberpunk character
Conflict: The city's AI overlord has kidnapped the protagonist's brother
Resolution: The protagonist must choose between sacrificing themselves or exposing the city's secrets

Characters:
- Rhea: Protagonist, brave, impulsive
- Rho: Antagonist, cold, logical
- Kai: Sidekick, sarcastic, loyal

Locations:
- The Grid (Dangerous)
- The Core (Mysterious)
- The Underbelly (Abandoned)

Story Beats:
1. Rhea discovers their brother is missing.
2. They hack into The Grid and find clues about The Core.
3. Final showdown at The Underbelly with a moral dilemma.

Dialogue Sample:
Rhea: "I'm not afraid of you, Rho."
  Rho: "You should be. But fear is irrelevant."
    Rhea: "I'll never give up!"
    Rhea: "Maybe we can work together?"
```

## 🤖 AI Integration Roadmap

### Phase 1: Foundation (Complete)
- ✅ Core story generation system
- ✅ Modular architecture
- ✅ AI placeholder framework
- ✅ Multiple provider support

### Phase 2: AI Implementation (TODO)
- OpenAI GPT integration
- Anthropic Claude integration
- Local AI model support (Ollama)
- Response parsing and validation

### Phase 3: Advanced Features (TODO)
- Visual dialogue editor
- Story template marketplace
- Real-time story adaptation
- Player choice tracking
- Dynamic quest generation

## 🛠️ Technical Details

### Requirements
- Unity 6000.1.5f1+
- .NET 4.x equivalent
- Input System package (included)

### Performance
- Lightweight generation (< 1ms for basic stories)
- Memory-efficient character/location storage
- Async AI integration ready
- No runtime allocations during generation

### Extensibility
- Interface-based design
- Event-driven architecture
- ScriptableObject templates
- Modular AI providers

## 📝 License

This project is licensed under the GNU GPL v3 License - see the [LICENSE](LICENSE) file for details.

## 🤝 Contributing

1. Fork the project
2. Create feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 🐛 Known Issues

- AI integration is currently placeholder-only
- Some UI elements may need manual setup
- No save/load functionality yet (planned)

## 📞 Support

- Check Console for detailed story output
- Use Context Menu options for quick testing
- Enable debug logs for troubleshooting

---

**Built with ❤️ for Unity developers and storytellers** 