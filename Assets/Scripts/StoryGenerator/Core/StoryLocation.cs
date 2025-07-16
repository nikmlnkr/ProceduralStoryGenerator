using System.Collections.Generic;
using UnityEngine;

namespace StoryGenerator.Core
{
    [System.Serializable]
    public enum LocationTone
    {
        Peaceful,
        Dangerous,
        Mysterious,
        Bustling,
        Abandoned,
        Sacred,
        Corrupted
    }

    [System.Serializable]
    public class StoryLocation
    {
        [Header("Basic Information")]
        public string name = "";
        
        [Header("Description")]
        [TextArea(3, 5)]
        public string description = "";
        
        [Header("Location Properties")]
        public LocationTone tone = LocationTone.Peaceful;
        public List<string> associatedEvents = new List<string>();
        public List<string> possibleActions = new List<string>();
        
        [Header("Story Integration")]
        public bool hasBeenVisited = false;
        public List<string> hiddenSecrets = new List<string>();
        public List<string> charactersPresentNames = new List<string>(); // Use names instead of direct references
        
        public StoryLocation()
        {
            associatedEvents = new List<string>();
            possibleActions = new List<string>();
            hiddenSecrets = new List<string>();
            charactersPresentNames = new List<string>();
        }
        
        public StoryLocation(string locationName, string locationDescription)
        {
            name = locationName;
            description = locationDescription;
            associatedEvents = new List<string>();
            possibleActions = new List<string>();
            hiddenSecrets = new List<string>();
            charactersPresentNames = new List<string>();
            hasBeenVisited = false;
        }
        
        public void AddEvent(string eventDescription)
        {
            if (!associatedEvents.Contains(eventDescription))
            {
                associatedEvents.Add(eventDescription);
            }
        }
        
        public void AddCharacter(string characterName)
        {
            if (!charactersPresentNames.Contains(characterName) && !string.IsNullOrEmpty(characterName))
            {
                charactersPresentNames.Add(characterName);
            }
        }
        
        public void AddCharacter(CharacterProfile character)
        {
            if (character != null)
            {
                AddCharacter(character.name);
            }
        }
        
        public void AddPossibleAction(string action)
        {
            if (!possibleActions.Contains(action))
            {
                possibleActions.Add(action);
            }
        }
        
        public void Visit()
        {
            hasBeenVisited = true;
        }
        
        public override string ToString()
        {
            return $"{name}: {description}";
        }
    }
} 