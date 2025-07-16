using System.Collections.Generic;
using UnityEngine;

namespace StoryGenerator.Templates
{
    [CreateAssetMenu(fileName = "New Story Template", menuName = "Story Generator/Story Template")]
    public class StoryTemplate : ScriptableObject
    {
        [Header("Story Template Configuration")]
        public string genre = "";
        public string setting = "";
        public string conflict = "";
        public string resolution = "";
        
        [Header("Tags")]
        public List<string> tags = new List<string>();
        
        [Header("Story Elements")]
        [TextArea(3, 5)]
        public string description = "";
        
        public override string ToString()
        {
            return $"Genre: {genre}\nSetting: {setting}\nConflict: {conflict}\nResolution: {resolution}";
        }
    }
} 