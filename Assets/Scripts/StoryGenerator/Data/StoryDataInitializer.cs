using System.Collections.Generic;
using UnityEngine;
using StoryGenerator.Templates;
using StoryGenerator.Managers;

namespace StoryGenerator.Data
{
    [CreateAssetMenu(fileName = "Story Data Initializer", menuName = "Story Generator/Data Initializer")]
    public class StoryDataInitializer : ScriptableObject
    {
        [Header("Sample Story Templates")]
        public List<StoryTemplate> sampleTemplates = new List<StoryTemplate>();
        
        [Header("Sample Names")]
        public List<string> characterNames = new List<string>();
        
        [Header("Sample Locations")]
        public List<string> locationNames = new List<string>();
        
        [Header("Sample Personality Traits")]
        public List<string> traits = new List<string>();
        
        [ContextMenu("Initialize Sample Data")]
        public void InitializeSampleData()
        {
            CreateSampleTemplates();
            PopulateCharacterNames();
            PopulateLocationNames();
            PopulateTraits();
            
            Debug.Log("Sample data initialized successfully!");
        }
        
        private void CreateSampleTemplates()
        {
            sampleTemplates.Clear();
            
            // Cyberpunk Template
            StoryTemplate cyberpunkTemplate = CreateInstance<StoryTemplate>();
            cyberpunkTemplate.genre = "Cyberpunk";
            cyberpunkTemplate.setting = "Ruined megacity controlled by AI";
            cyberpunkTemplate.conflict = "The city's AI overlord has kidnapped the protagonist's brother";
            cyberpunkTemplate.resolution = "The protagonist must choose between sacrificing themselves or exposing the city's secrets";
            cyberpunkTemplate.description = "A dark future where technology has enslaved humanity";
            cyberpunkTemplate.tags.AddRange(new string[] { "cyberpunk", "family", "sacrifice", "technology", "AI" });
            sampleTemplates.Add(cyberpunkTemplate);
            
            // Fantasy Template
            StoryTemplate fantasyTemplate = CreateInstance<StoryTemplate>();
            fantasyTemplate.genre = "Fantasy";
            fantasyTemplate.setting = "Ancient magical kingdom under siege";
            fantasyTemplate.conflict = "Dark forces have stolen the kingdom's protective artifact";
            fantasyTemplate.resolution = "The hero must unite the scattered clans to reclaim the artifact";
            fantasyTemplate.description = "A mystical realm where magic and politics intertwine";
            fantasyTemplate.tags.AddRange(new string[] { "fantasy", "magic", "kingdom", "unity", "artifact" });
            sampleTemplates.Add(fantasyTemplate);
            
            // Mystery Template
            StoryTemplate mysteryTemplate = CreateInstance<StoryTemplate>();
            mysteryTemplate.genre = "Mystery";
            mysteryTemplate.setting = "Victorian-era London shrouded in fog";
            mysteryTemplate.conflict = "A series of impossible murders plague the city";
            mysteryTemplate.resolution = "The detective must uncover the supernatural truth behind the crimes";
            mysteryTemplate.description = "A atmospheric tale of detection and supernatural intrigue";
            mysteryTemplate.tags.AddRange(new string[] { "mystery", "victorian", "supernatural", "detective", "murders" });
            sampleTemplates.Add(mysteryTemplate);
            
            // Space Opera Template
            StoryTemplate spaceTemplate = CreateInstance<StoryTemplate>();
            spaceTemplate.genre = "Space Opera";
            spaceTemplate.setting = "Galactic confederation on the brink of war";
            spaceTemplate.conflict = "Ancient alien technology threatens to destroy all life";
            spaceTemplate.resolution = "The crew must make first contact with the aliens to prevent annihilation";
            spaceTemplate.description = "An epic tale spanning star systems and species";
            spaceTemplate.tags.AddRange(new string[] { "space", "aliens", "war", "technology", "confederation" });
            sampleTemplates.Add(spaceTemplate);
            
            // Post-Apocalyptic Template
            StoryTemplate apocalypseTemplate = CreateInstance<StoryTemplate>();
            apocalypseTemplate.genre = "Post-Apocalyptic";
            apocalypseTemplate.setting = "Wasteland settlements struggling to survive";
            apocalypseTemplate.conflict = "A tyrant controls the last clean water source";
            apocalypseTemplate.resolution = "The wanderer must rally the settlements to overthrow the tyrant";
            apocalypseTemplate.description = "Survival and hope in a world reclaimed by nature";
            apocalypseTemplate.tags.AddRange(new string[] { "apocalypse", "survival", "water", "tyrant", "settlements" });
            sampleTemplates.Add(apocalypseTemplate);
        }
        
        private void PopulateCharacterNames()
        {
            characterNames.Clear();
            characterNames.AddRange(new string[] {
                // Cyberpunk Names
                "Rhea", "Kai", "Zara", "Nova", "Jax", "Nyx", "Orion", "Vector",
                "Cypher", "Matrix", "Ghost", "Phoenix", "Echo", "Nexus",
                
                // Fantasy Names
                "Aria", "Theron", "Luna", "Gareth", "Elara", "Darius", "Lyra", "Soren",
                "Kael", "Vera", "Magnus", "Iris", "Rowan", "Sage",
                
                // Mystery Names
                "Victoria", "Edmund", "Cordelia", "Jasper", "Ophelia", "Reginald",
                "Beatrice", "Montgomery", "Isadora", "Percival",
                
                // Space Names
                "Zeph", "Astrid", "Cosmo", "Stella", "Atlas", "Vega", "Sirius", "Andromeda",
                
                // Universal Names
                "River", "Storm", "Sage", "Hunter", "Sky", "Raven", "Dawn", "Ash"
            });
        }
        
        private void PopulateLocationNames()
        {
            locationNames.Clear();
            locationNames.AddRange(new string[] {
                // Cyberpunk Locations
                "The Grid", "The Core", "The Underbelly", "Neon District", "Data Haven",
                "The Nexus", "Chrome Tower", "Underground Labs", "The Matrix", "Digital Depths",
                
                // Fantasy Locations
                "The Ancient Grove", "Dragon's Peak", "Mystic Library", "Crystal Caverns",
                "The Lost Temple", "Moonstone Valley", "Whispering Woods", "Tower of Stars",
                
                // Mystery Locations
                "The Old Manor", "Foggy Docks", "The Gentleman's Club", "Abandoned Theatre",
                "The Library", "Dark Alley", "The Observatory", "Underground Tunnels",
                
                // Space Locations
                "The Command Bridge", "Alien Ruins", "Space Station Omega", "The Nebula",
                "Crystal Mines", "The Mothership", "Binary Stars", "Quantum Laboratory",
                
                // Post-Apocalyptic Locations
                "The Wasteland", "Ruined City", "The Oasis", "Scrapyard", "The Bunker",
                "Toxic Swamps", "The Settlement", "Radiation Zone"
            });
        }
        
        private void PopulateTraits()
        {
            traits.Clear();
            traits.AddRange(new string[] {
                // Positive Traits
                "brave", "loyal", "wise", "compassionate", "honest", "protective",
                "determined", "charismatic", "intelligent", "creative", "patient",
                "humble", "optimistic", "generous", "reliable", "curious",
                
                // Negative Traits
                "arrogant", "greedy", "cruel", "cowardly", "dishonest", "jealous",
                "impatient", "vindictive", "selfish", "reckless", "pessimistic",
                
                // Neutral/Complex Traits
                "mysterious", "sarcastic", "impulsive", "logical", "cold", "volatile",
                "stubborn", "ambitious", "calculating", "unpredictable", "reserved",
                "passionate", "methodical", "eccentric", "pragmatic"
            });
        }
        
        public void ApplyToStoryGenerator(StoryGeneratorManager manager)
        {
            if (manager == null)
            {
                Debug.LogError("StoryGeneratorManager is null!");
                return;
            }
            
            // Apply templates
            manager.templates.Clear();
            manager.templates.AddRange(sampleTemplates);
            
            // Apply names
            manager.names.Clear();
            manager.names.AddRange(characterNames);
            
            // Apply locations
            manager.locations.Clear();
            manager.locations.AddRange(locationNames);
            
            // Apply traits
            manager.personalityTraits.Clear();
            manager.personalityTraits.AddRange(traits);
            
            Debug.Log($"Applied {sampleTemplates.Count} templates, {characterNames.Count} names, {locationNames.Count} locations, and {traits.Count} traits to Story Generator Manager.");
        }
        
        void OnValidate()
        {
            // Auto-initialize if lists are empty
            if (sampleTemplates.Count == 0 || characterNames.Count == 0 || locationNames.Count == 0 || traits.Count == 0)
            {
                InitializeSampleData();
            }
        }
    }
} 