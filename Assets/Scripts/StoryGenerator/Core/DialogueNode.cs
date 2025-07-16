using System.Collections.Generic;
using UnityEngine;

namespace StoryGenerator.Core
{
    [System.Serializable]
    public enum DialogueType
    {
        Statement,
        Question,
        Response,
        Narration,
        Action
    }

    [System.Serializable]
    public class DialogueNode
    {
        [Header("Dialogue Content")]
        public string speaker = "";
        [TextArea(2, 4)]
        public string line = "";
        public DialogueType dialogueType = DialogueType.Statement;
        
        [Header("Branching")]
        [SerializeReference]
        public List<DialogueNode> responses = new List<DialogueNode>();
        
        [Header("Conditions & Effects")]
        public List<string> requiredFlags = new List<string>();
        public List<string> setFlags = new List<string>();
        public bool isEndNode = false;
        
        [Header("Metadata")]
        public string nodeId = "";
        public int emotionalWeight = 0; // -10 (very negative) to +10 (very positive)
        
        public DialogueNode()
        {
            responses = new List<DialogueNode>();
            requiredFlags = new List<string>();
            setFlags = new List<string>();
            nodeId = System.Guid.NewGuid().ToString();
        }
        
        public DialogueNode(string dialogueSpeaker, string dialogueLine, DialogueType type = DialogueType.Statement)
        {
            speaker = dialogueSpeaker;
            line = dialogueLine;
            dialogueType = type;
            responses = new List<DialogueNode>();
            requiredFlags = new List<string>();
            setFlags = new List<string>();
            nodeId = System.Guid.NewGuid().ToString();
            isEndNode = false;
        }
        
        public void AddResponse(DialogueNode response)
        {
            if (response != null && !responses.Contains(response))
            {
                responses.Add(response);
            }
        }
        
        public void AddResponse(string responseSpeaker, string responseLine, DialogueType type = DialogueType.Response)
        {
            DialogueNode newResponse = new DialogueNode(responseSpeaker, responseLine, type);
            AddResponse(newResponse);
        }
        
        public void AddRequiredFlag(string flag)
        {
            if (!requiredFlags.Contains(flag))
            {
                requiredFlags.Add(flag);
            }
        }
        
        public void AddSetFlag(string flag)
        {
            if (!setFlags.Contains(flag))
            {
                setFlags.Add(flag);
            }
        }
        
        public bool CanBeShown(Dictionary<string, string> worldFlags)
        {
            foreach (string requiredFlag in requiredFlags)
            {
                if (!worldFlags.ContainsKey(requiredFlag))
                {
                    return false;
                }
            }
            return true;
        }
        
        public void ApplyEffects(Dictionary<string, string> worldFlags)
        {
            foreach (string flag in setFlags)
            {
                worldFlags[flag] = "true";
            }
        }
        
        public List<DialogueNode> GetAvailableResponses(Dictionary<string, string> worldFlags)
        {
            List<DialogueNode> availableResponses = new List<DialogueNode>();
            
            foreach (DialogueNode response in responses)
            {
                if (response.CanBeShown(worldFlags))
                {
                    availableResponses.Add(response);
                }
            }
            
            return availableResponses;
        }
        
        public override string ToString()
        {
            return $"{speaker}: \"{line}\" [{responses.Count} responses]";
        }
    }
} 