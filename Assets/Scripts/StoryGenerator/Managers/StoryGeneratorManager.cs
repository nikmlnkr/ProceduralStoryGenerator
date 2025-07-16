using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using StoryGenerator.Core;
using StoryGenerator.Templates;

namespace StoryGenerator.Managers
{
    public class StoryGeneratorManager : MonoBehaviour
    {
        [Header("Story Templates")]
        public List<StoryTemplate> templates = new List<StoryTemplate>();
        
        [Header("Generation Data")]
        public List<string> names = new List<string>();
        public List<string> locations = new List<string>();
        public List<string> personalityTraits = new List<string>();
        
        [Header("World State")]
        public WorldState worldState;
        
        [Header("Generation Settings")]
        public int maxCharacters = 5;
        public int maxLocations = 3;
        public bool useAIGeneration = false;
        
        // Current story data
        private StoryTemplate currentTemplate;
        private List<CharacterProfile> currentCharacters = new List<CharacterProfile>();
        private List<StoryLocation> currentLocations = new List<StoryLocation>();
        private DialogueNode currentDialogue;
        
        void Start()
        {
            InitializeData();
            worldState = new WorldState();
        }
        
        void InitializeData()
        {
            // Initialize with sample data if lists are empty
            if (names.Count == 0)
            {
                names.AddRange(new string[] {
                    "Rhea", "Kai", "Zara", "Rho", "Nova", "Jax", "Luna", "Orion",
                    "Maya", "Dante", "Aurora", "Phoenix", "Sage", "River", "Storm"
                });
            }
            
            if (locations.Count == 0)
            {
                locations.AddRange(new string[] {
                    "The Grid", "The Core", "The Underbelly", "Neon District",
                    "The Wasteland", "Sky Gardens", "Underground Labs", "The Nexus",
                    "Abandoned Mall", "Rooftop Gardens", "The Harbor", "Old Metro"
                });
            }
            
            if (personalityTraits.Count == 0)
            {
                personalityTraits.AddRange(new string[] {
                    "brave", "impulsive", "sarcastic", "loyal", "cold", "logical",
                    "mysterious", "cheerful", "stubborn", "wise", "reckless", "protective",
                    "cunning", "honest", "volatile", "calm", "ambitious", "compassionate"
                });
            }
        }
        
        [ContextMenu("Generate Story")]
        public void GenerateStory()
        {
            Debug.Log("=== PROCEDURAL STORY GENERATOR ===");
            
            // Clear previous story
            currentCharacters.Clear();
            currentLocations.Clear();
            worldState.Reset();
            
            // Step 1: Pick a random template
            currentTemplate = GetRandomTemplate();
            if (currentTemplate == null)
            {
                Debug.LogError("No story templates available!");
                return;
            }
            
            // Step 2: Generate characters
            GenerateCharacters();
            
            // Step 3: Generate locations
            GenerateLocations();
            
            // Step 4: Generate story events
            GenerateStoryEvents();
            
            // Step 5: Generate sample dialogue
            GenerateDialogue();
            
            // Step 6: Print the complete story
            PrintStoryToConsole();
            
            Debug.Log("=== STORY GENERATION COMPLETE ===");
        }
        
        private StoryTemplate GetRandomTemplate()
        {
            if (templates.Count == 0)
            {
                // Create a default template if none exist
                StoryTemplate defaultTemplate = CreateDefaultTemplate();
                return defaultTemplate;
            }
            
            return templates[Random.Range(0, templates.Count)];
        }
        
        private StoryTemplate CreateDefaultTemplate()
        {
            StoryTemplate template = ScriptableObject.CreateInstance<StoryTemplate>();
            template.genre = "Cyberpunk";
            template.setting = "Ruined megacity";
            template.conflict = "The city's AI overlord has kidnapped the protagonist's brother";
            template.resolution = "The protagonist must choose between sacrificing themselves or exposing the city's secrets";
            template.tags.AddRange(new string[] { "cyberpunk", "family", "sacrifice", "technology" });
            return template;
        }
        
        private void GenerateCharacters()
        {
            // Generate protagonist
            CharacterProfile protagonist = new CharacterProfile(
                GetRandomName(),
                CharacterRole.Protagonist,
                "Save their kidnapped brother and uncover the truth"
            );
            protagonist.AddPersonalityTrait(GetRandomTrait());
            protagonist.AddPersonalityTrait(GetRandomTrait());
            currentCharacters.Add(protagonist);
            
            // Generate antagonist
            CharacterProfile antagonist = new CharacterProfile(
                "Rho", // AI name
                CharacterRole.Antagonist,
                "Maintain control over the city and eliminate threats"
            );
            antagonist.AddPersonalityTrait("cold");
            antagonist.AddPersonalityTrait("logical");
            currentCharacters.Add(antagonist);
            
            // Generate sidekick
            CharacterProfile sidekick = new CharacterProfile(
                GetRandomName(),
                CharacterRole.Sidekick,
                "Help the protagonist and provide moral support"
            );
            sidekick.AddPersonalityTrait("sarcastic");
            sidekick.AddPersonalityTrait("loyal");
            currentCharacters.Add(sidekick);
            
            // Establish relationships
            protagonist.AddRelationship(sidekick);
            sidekick.AddRelationship(protagonist);
            worldState.SetCharacterRelationship(protagonist.name, sidekick.name, "allies");
            worldState.SetCharacterRelationship(protagonist.name, antagonist.name, "enemies");
        }
        
        private void GenerateLocations()
        {
            List<string> availableLocations = new List<string>(locations);
            int targetLocationCount = Mathf.Min(maxLocations, availableLocations.Count);
            
            // Ensure we generate the required number of locations
            while (currentLocations.Count < targetLocationCount && availableLocations.Count > 0)
            {
                int randomIndex = Random.Range(0, availableLocations.Count);
                string locationName = availableLocations[randomIndex];
                
                StoryLocation newLocation = new StoryLocation(locationName, $"A mysterious place known as {locationName}");
                newLocation.tone = (LocationTone)Random.Range(0, System.Enum.GetValues(typeof(LocationTone)).Length);
                currentLocations.Add(newLocation);
                
                // Remove from available list to prevent duplicates
                availableLocations.RemoveAt(randomIndex);
            }
            
            // If we still don't have enough locations, add fallback locations
            if (currentLocations.Count < 3)
            {
                string[] fallbackLocations = { "The Unknown Place", "The Mysterious Area", "The Hidden Realm", "The Secret Chamber", "The Lost Zone" };
                
                for (int i = 0; i < fallbackLocations.Length && currentLocations.Count < 3; i++)
                {
                    StoryLocation fallbackLocation = new StoryLocation(fallbackLocations[i], $"A place that emerged from the void of necessity");
                    fallbackLocation.tone = LocationTone.Mysterious;
                    currentLocations.Add(fallbackLocation);
                }
            }
        }
        
        private void GenerateStoryEvents()
        {
            if (currentCharacters.Count == 0)
            {
                Debug.LogError("No characters available for story events!");
                return;
            }
            
            if (currentLocations.Count < 3)
            {
                Debug.LogError($"Insufficient locations for story events! Found {currentLocations.Count}, need at least 3.");
                return;
            }
            
            string protagonistName = currentCharacters[0].name;
            
            // Event 1: Setup
            string setupEvent = $"{protagonistName} discovers their brother is missing.";
            worldState.CompleteEvent("brother_missing_discovered");
            worldState.SetFlag("brother_kidnapped", true);
            
            // Event 2: Conflict
            string conflictEvent = $"{protagonistName} hacks into {currentLocations[0].name} and finds clues about {currentLocations[1].name}.";
            worldState.CompleteEvent("clues_discovered");
            worldState.VisitLocation(currentLocations[0].name);
            worldState.AdvanceStory(30);
            
            // Event 3: Resolution
            string resolutionEvent = $"Final showdown at {currentLocations[2].name} with a moral dilemma.";
            worldState.CompleteEvent("final_confrontation");
            worldState.VisitLocation(currentLocations[2].name);
            worldState.AdvanceStory(50);
            worldState.SetTensionLevel(10);
        }
        
        private void GenerateDialogue()
        {
            if (currentCharacters.Count < 2)
            {
                Debug.LogError($"Insufficient characters for dialogue generation! Found {currentCharacters.Count}, need at least 2.");
                return;
            }
            
            string protagonistName = currentCharacters[0].name;
            string antagonistName = currentCharacters[1].name;
            
            // Create dialogue tree
            currentDialogue = new DialogueNode(protagonistName, $"I'm not afraid of you, {antagonistName}.", DialogueType.Statement);
            currentDialogue.emotionalWeight = 5; // Defiant
            
            DialogueNode antagonistResponse = new DialogueNode(antagonistName, "You should be. But fear is irrelevant.", DialogueType.Response);
            antagonistResponse.emotionalWeight = -3; // Threatening
            currentDialogue.AddResponse(antagonistResponse);
            
            // Add player response options
            DialogueNode defiantResponse = new DialogueNode(protagonistName, "I'll never give up!", DialogueType.Response);
            defiantResponse.emotionalWeight = 8;
            defiantResponse.AddSetFlag("player_defiant");
            
            DialogueNode negotiateResponse = new DialogueNode(protagonistName, "Maybe we can work together?", DialogueType.Response);
            negotiateResponse.emotionalWeight = 2;
            negotiateResponse.AddSetFlag("player_diplomatic");
            
            antagonistResponse.AddResponse(defiantResponse);
            antagonistResponse.AddResponse(negotiateResponse);
        }
        
        private void PrintStoryToConsole()
        {
            Debug.Log($"\n=== GENERATED STORY ===");
            Debug.Log($"Genre: {currentTemplate.genre}");
            Debug.Log($"Setting: {currentTemplate.setting}");
            
            // Safe character access
            if (currentCharacters.Count > 0)
            {
                Debug.Log($"Main Character: {currentCharacters[0].name}, a {string.Join(", ", currentCharacters[0].personalityTraits)} {currentTemplate.genre.ToLower()} character");
            }
            else
            {
                Debug.Log("Main Character: [No characters generated]");
            }
            
            Debug.Log($"Conflict: {currentTemplate.conflict}");
            Debug.Log($"Resolution: {currentTemplate.resolution}");
            
            Debug.Log($"\nCharacters:");
            if (currentCharacters.Count > 0)
            {
                foreach (var character in currentCharacters)
                {
                    Debug.Log($"- {character.name}: {character.role}, {string.Join(", ", character.personalityTraits)}");
                }
            }
            else
            {
                Debug.Log("- [No characters generated]");
            }
            
            Debug.Log($"\nLocations:");
            if (currentLocations.Count > 0)
            {
                foreach (var location in currentLocations)
                {
                    Debug.Log($"- {location.name} ({location.tone})");
                }
            }
            else
            {
                Debug.Log("- [No locations generated]");
            }
            
            Debug.Log($"\nStory Beats:");
            if (currentCharacters.Count > 0 && currentLocations.Count >= 3)
            {
                Debug.Log($"1. {currentCharacters[0].name} discovers their brother is missing.");
                Debug.Log($"2. They hack into {currentLocations[0].name} and find clues about {currentLocations[1].name}.");
                Debug.Log($"3. Final showdown at {currentLocations[2].name} with a moral dilemma.");
            }
            else
            {
                Debug.Log("1. [Story events could not be generated - insufficient characters or locations]");
            }
            
            Debug.Log($"\nDialogue Sample:");
            if (currentDialogue != null)
            {
                PrintDialogueTree(currentDialogue, 0);
            }
            else
            {
                Debug.Log("[No dialogue generated]");
            }
            
            Debug.Log($"\nWorld State: {worldState}");
        }
        
        private void PrintDialogueTree(DialogueNode node, int depth)
        {
            if (node == null) return;
            
            string indent = new string(' ', depth * 2);
            Debug.Log($"{indent}{node.speaker}: \"{node.line}\"");
            
            if (node.responses != null)
            {
                foreach (var response in node.responses)
                {
                    PrintDialogueTree(response, depth + 1);
                }
            }
        }
        
        private string GetRandomName()
        {
            return names[Random.Range(0, names.Count)];
        }
        
        private string GetRandomTrait()
        {
            return personalityTraits[Random.Range(0, personalityTraits.Count)];
        }
        
        // AI Integration Placeholder
        public async Task<string> GetNarrativeFromAI(string context)
        {
            // Placeholder for OpenAI API integration
            Debug.Log($"AI Request: {context}");
            
            // Simulate async operation
            await Task.Delay(100);
            
            // Return placeholder response
            return "Generated narrative text from AI based on: " + context;
        }
        
        // Public methods for external use
        public CharacterProfile GetCharacterByName(string name)
        {
            foreach (var character in currentCharacters)
            {
                if (character.name == name)
                    return character;
            }
            return null;
        }
        
        public StoryLocation GetLocationByName(string name)
        {
            foreach (var location in currentLocations)
            {
                if (location.name == name)
                    return location;
            }
            return null;
        }
        
        public List<CharacterProfile> GetAllCharacters()
        {
            return new List<CharacterProfile>(currentCharacters);
        }
        
        public List<StoryLocation> GetAllLocations()
        {
            return new List<StoryLocation>(currentLocations);
        }
        
        // Save/Load functionality placeholders
        public void SaveStoryToJson(string filePath)
        {
            // TODO: Implement JSON serialization
            Debug.Log($"Story would be saved to: {filePath}");
        }
        
        public void LoadStoryFromJson(string filePath)
        {
            // TODO: Implement JSON deserialization
            Debug.Log($"Story would be loaded from: {filePath}");
        }
    }
} 