using System.Collections.Generic;
using UnityEngine;

namespace StoryGenerator.Core
{
    [System.Serializable]
    public enum CharacterRole
    {
        Protagonist,
        Antagonist,
        Sidekick,
        Mentor,
        LoveInterest,
        Neutral,
        MinorCharacter
    }

    [System.Serializable]
    public class CharacterProfile
    {
        [Header("Basic Information")]
        public string name = "";
        public CharacterRole role = CharacterRole.Neutral;
        
        [Header("Character Details")]
        public List<string> personalityTraits = new List<string>();
        [TextArea(2, 3)]
        public string motivation = "";
        
        [Header("Relationships")]
        public List<string> relationshipNames = new List<string>(); // Use names instead of direct references
        
        [Header("Story Integration")]
        public List<string> associatedEvents = new List<string>();
        public bool isAlive = true;
        public bool hasBeenIntroduced = false;
        
        public CharacterProfile()
        {
            personalityTraits = new List<string>();
            relationshipNames = new List<string>();
            associatedEvents = new List<string>();
        }
        
        public CharacterProfile(string characterName, CharacterRole characterRole, string characterMotivation)
        {
            name = characterName;
            role = characterRole;
            motivation = characterMotivation;
            personalityTraits = new List<string>();
            relationshipNames = new List<string>();
            associatedEvents = new List<string>();
            isAlive = true;
            hasBeenIntroduced = false;
        }
        
        public void AddPersonalityTrait(string trait)
        {
            if (!personalityTraits.Contains(trait))
            {
                personalityTraits.Add(trait);
            }
        }
        
        public void AddRelationship(string characterName)
        {
            if (!relationshipNames.Contains(characterName) && characterName != this.name && !string.IsNullOrEmpty(characterName))
            {
                relationshipNames.Add(characterName);
            }
        }
        
        public void AddRelationship(CharacterProfile character)
        {
            if (character != null)
            {
                AddRelationship(character.name);
            }
        }
        
        public override string ToString()
        {
            string traits = personalityTraits.Count > 0 ? string.Join(", ", personalityTraits) : "None";
            return $"{name}: {role}, {traits}";
        }
    }
} 