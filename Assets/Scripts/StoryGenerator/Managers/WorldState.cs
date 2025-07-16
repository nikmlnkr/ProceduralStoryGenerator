using System.Collections.Generic;
using UnityEngine;

namespace StoryGenerator.Managers
{
    [System.Serializable]
    public class WorldState
    {
        [Header("Story Progress")]
        public Dictionary<string, string> storyFlags = new Dictionary<string, string>();
        public List<string> completedEvents = new List<string>();
        
        [Header("Character States")]
        public Dictionary<string, bool> characterStates = new Dictionary<string, bool>(); // alive/dead, present/absent
        public Dictionary<string, string> characterRelationships = new Dictionary<string, string>(); // relationship tracking
        
        [Header("Location States")]
        public List<string> visitedLocations = new List<string>();
        public Dictionary<string, string> locationStates = new Dictionary<string, string>(); // discovered secrets, changes
        
        [Header("Story Metrics")]
        public int storyProgression = 0; // 0-100 percentage
        public float morality = 0f; // -1.0 (evil) to 1.0 (good)
        public int tensionLevel = 0; // 0-10 story tension
        
        public WorldState()
        {
            storyFlags = new Dictionary<string, string>();
            completedEvents = new List<string>();
            characterStates = new Dictionary<string, bool>();
            characterRelationships = new Dictionary<string, string>();
            visitedLocations = new List<string>();
            locationStates = new Dictionary<string, string>();
            storyProgression = 0;
            morality = 0f;
            tensionLevel = 1;
        }
        
        // Story Flag Management
        public void SetFlag(string flagName, string value)
        {
            storyFlags[flagName] = value;
            Debug.Log($"World State: Set flag '{flagName}' to '{value}'");
        }
        
        public void SetFlag(string flagName, bool value)
        {
            SetFlag(flagName, value.ToString());
        }
        
        public string GetFlag(string flagName)
        {
            return storyFlags.ContainsKey(flagName) ? storyFlags[flagName] : "";
        }
        
        public bool GetFlagAsBool(string flagName)
        {
            string value = GetFlag(flagName);
            return value.ToLower() == "true" || value == "1";
        }
        
        public bool HasFlag(string flagName)
        {
            return storyFlags.ContainsKey(flagName);
        }
        
        public void RemoveFlag(string flagName)
        {
            if (storyFlags.ContainsKey(flagName))
            {
                storyFlags.Remove(flagName);
                Debug.Log($"World State: Removed flag '{flagName}'");
            }
        }
        
        // Event Management
        public void CompleteEvent(string eventName)
        {
            if (!completedEvents.Contains(eventName))
            {
                completedEvents.Add(eventName);
                Debug.Log($"World State: Completed event '{eventName}'");
            }
        }
        
        public bool IsEventCompleted(string eventName)
        {
            return completedEvents.Contains(eventName);
        }
        
        // Character State Management
        public void SetCharacterState(string characterName, bool state)
        {
            characterStates[characterName] = state;
            Debug.Log($"World State: Set character '{characterName}' state to {state}");
        }
        
        public bool GetCharacterState(string characterName)
        {
            return characterStates.ContainsKey(characterName) ? characterStates[characterName] : true;
        }
        
        public void SetCharacterRelationship(string characterA, string characterB, string relationshipType)
        {
            string key = $"{characterA}_{characterB}";
            characterRelationships[key] = relationshipType;
        }
        
        public string GetCharacterRelationship(string characterA, string characterB)
        {
            string key = $"{characterA}_{characterB}";
            return characterRelationships.ContainsKey(key) ? characterRelationships[key] : "neutral";
        }
        
        // Location Management
        public void VisitLocation(string locationName)
        {
            if (!visitedLocations.Contains(locationName))
            {
                visitedLocations.Add(locationName);
                Debug.Log($"World State: Visited location '{locationName}'");
            }
        }
        
        public bool HasVisitedLocation(string locationName)
        {
            return visitedLocations.Contains(locationName);
        }
        
        public void SetLocationState(string locationName, string state)
        {
            locationStates[locationName] = state;
        }
        
        public string GetLocationState(string locationName)
        {
            return locationStates.ContainsKey(locationName) ? locationStates[locationName] : "normal";
        }
        
        // Story Progression
        public void AdvanceStory(int points = 10)
        {
            storyProgression = Mathf.Clamp(storyProgression + points, 0, 100);
            Debug.Log($"World State: Story progression now at {storyProgression}%");
        }
        
        public void AdjustMorality(float adjustment)
        {
            morality = Mathf.Clamp(morality + adjustment, -1f, 1f);
            Debug.Log($"World State: Morality adjusted to {morality:F2}");
        }
        
        public void SetTensionLevel(int level)
        {
            tensionLevel = Mathf.Clamp(level, 0, 10);
            Debug.Log($"World State: Tension level set to {tensionLevel}");
        }
        
        // Utility Methods
        public void Reset()
        {
            storyFlags.Clear();
            completedEvents.Clear();
            characterStates.Clear();
            characterRelationships.Clear();
            visitedLocations.Clear();
            locationStates.Clear();
            storyProgression = 0;
            morality = 0f;
            tensionLevel = 1;
            Debug.Log("World State: Reset to initial state");
        }
        
        public Dictionary<string, object> GetSnapshot()
        {
            return new Dictionary<string, object>
            {
                {"storyFlags", new Dictionary<string, string>(storyFlags)},
                {"completedEvents", new List<string>(completedEvents)},
                {"storyProgression", storyProgression},
                {"morality", morality},
                {"tensionLevel", tensionLevel}
            };
        }
        
        public override string ToString()
        {
            return $"WorldState: {completedEvents.Count} events completed, {storyFlags.Count} flags set, {storyProgression}% progression";
        }
    }
} 