using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using StoryGenerator.Managers;
using StoryGenerator.Data;
using StoryGenerator.Core;

namespace StoryGenerator.Demo
{
    public class StoryGeneratorDemo : MonoBehaviour
    {
        [Header("References")]
        public StoryGeneratorManager storyGenerator;
        public StoryDataInitializer dataInitializer;
        
        [Header("UI References (Optional)")]
        public Button generateStoryButton;
        public Button testAIButton;
        public Text outputText;
        
        [Header("Demo Settings")]
        public bool autoInitialize = true;
        public bool createUIIfMissing = true;
        
        void Start()
        {
            InitializeDemo();
        }
        
        void InitializeDemo()
        {
            // Find or create StoryGeneratorManager
            if (storyGenerator == null)
            {
                storyGenerator = FindAnyObjectByType<StoryGeneratorManager>();
                if (storyGenerator == null)
                {
                    GameObject sgObject = new GameObject("Story Generator Manager");
                    storyGenerator = sgObject.AddComponent<StoryGeneratorManager>();
                    Debug.Log("Created new StoryGeneratorManager");
                }
            }
            
            // Initialize with sample data if enabled
            if (autoInitialize && dataInitializer != null)
            {
                dataInitializer.ApplyToStoryGenerator(storyGenerator);
            }
            
            // Create simple UI if missing and enabled
            if (createUIIfMissing && generateStoryButton == null)
            {
                CreateSimpleUI();
            }
            
            // Setup button listeners
            SetupUIListeners();
            
            Debug.Log("Story Generator Demo initialized successfully!");
        }
        
        void CreateSimpleUI()
        {
            // Create Canvas
            GameObject canvasObject = new GameObject("Demo Canvas");
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            
            // Create Generate Story Button
            GameObject buttonObject = new GameObject("Generate Story Button");
            buttonObject.transform.SetParent(canvasObject.transform);
            
            RectTransform buttonRect = buttonObject.AddComponent<RectTransform>();
            buttonRect.anchoredPosition = new Vector2(0, 100);
            buttonRect.sizeDelta = new Vector2(200, 50);
            
            Image buttonImage = buttonObject.AddComponent<Image>();
            buttonImage.color = Color.cyan;
            
            generateStoryButton = buttonObject.AddComponent<Button>();
            
            // Add button text
            GameObject textObject = new GameObject("Button Text");
            textObject.transform.SetParent(buttonObject.transform);
            
            RectTransform textRect = textObject.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.sizeDelta = Vector2.zero;
            textRect.anchoredPosition = Vector2.zero;
            
            Text buttonText = textObject.AddComponent<Text>();
            buttonText.text = "Generate Story";
            buttonText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            buttonText.fontSize = 16;
            buttonText.color = Color.black;
            buttonText.alignment = TextAnchor.MiddleCenter;
            
            // Create AI Test Button
            GameObject aiButtonObject = new GameObject("Test AI Button");
            aiButtonObject.transform.SetParent(canvasObject.transform);
            
            RectTransform aiButtonRect = aiButtonObject.AddComponent<RectTransform>();
            aiButtonRect.anchoredPosition = new Vector2(0, 40);
            aiButtonRect.sizeDelta = new Vector2(200, 50);
            
            Image aiButtonImage = aiButtonObject.AddComponent<Image>();
            aiButtonImage.color = Color.green;
            
            testAIButton = aiButtonObject.AddComponent<Button>();
            
            // Add AI button text
            GameObject aiTextObject = new GameObject("AI Button Text");
            aiTextObject.transform.SetParent(aiButtonObject.transform);
            
            RectTransform aiTextRect = aiTextObject.AddComponent<RectTransform>();
            aiTextRect.anchorMin = Vector2.zero;
            aiTextRect.anchorMax = Vector2.one;
            aiTextRect.sizeDelta = Vector2.zero;
            aiTextRect.anchoredPosition = Vector2.zero;
            
            Text aiButtonText = aiTextObject.AddComponent<Text>();
            aiButtonText.text = "Test AI";
            aiButtonText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            aiButtonText.fontSize = 16;
            aiButtonText.color = Color.black;
            aiButtonText.alignment = TextAnchor.MiddleCenter;
            
            // Create Output Text
            GameObject outputObject = new GameObject("Output Text");
            outputObject.transform.SetParent(canvasObject.transform);
            
            RectTransform outputRect = outputObject.AddComponent<RectTransform>();
            outputRect.anchoredPosition = new Vector2(0, -100);
            outputRect.sizeDelta = new Vector2(600, 200);
            
            outputText = outputObject.AddComponent<Text>();
            outputText.text = "Click 'Generate Story' to create a procedural story!";
            outputText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            outputText.fontSize = 12;
            outputText.color = Color.white;
            outputText.alignment = TextAnchor.UpperLeft;
            
            Debug.Log("Created simple UI for Story Generator Demo");
        }
        
        void SetupUIListeners()
        {
            if (generateStoryButton != null)
            {
                generateStoryButton.onClick.AddListener(OnGenerateStoryClicked);
            }
            
            if (testAIButton != null)
            {
                testAIButton.onClick.AddListener(OnTestAIClicked);
            }
        }
        
        public void OnGenerateStoryClicked()
        {
            if (storyGenerator != null)
            {
                storyGenerator.GenerateStory();
                UpdateOutputText("Story generated! Check the Console for details.");
            }
            else
            {
                Debug.LogError("StoryGeneratorManager not found!");
                UpdateOutputText("Error: StoryGeneratorManager not found!");
            }
        }
        
        public async void OnTestAIClicked()
        {
            UpdateOutputText("Testing AI integration...");
            
            try
            {
                // Test AI narrative generation
                string narrative = await AIIntegration.GetNarrativeFromAI("A cyberpunk story about a hacker");
                UpdateOutputText($"AI Test Result: {narrative}");
                
                Debug.Log($"AI Test completed: {narrative}");
            }
            catch (System.Exception ex)
            {
                UpdateOutputText($"AI Test failed: {ex.Message}");
                Debug.LogError($"AI Test error: {ex.Message}");
            }
        }
        
        void UpdateOutputText(string message)
        {
            if (outputText != null)
            {
                outputText.text = message;
            }
        }
        
        // Public methods for external testing
        [ContextMenu("Generate Test Story")]
        public void GenerateTestStory()
        {
            OnGenerateStoryClicked();
        }
        
        [ContextMenu("Test AI Integration")]
        public void TestAIIntegration()
        {
            OnTestAIClicked();
        }
        
        [ContextMenu("Enable AI (Placeholder Mode)")]
        public void EnableAIPlaceholderMode()
        {
            AIIntegration.ConfigureAI(AIProvider.None, "", true);
            Debug.Log("AI enabled in placeholder mode");
            UpdateOutputText("AI enabled in placeholder mode");
        }
        
        [ContextMenu("Configure OpenAI")]
        public void ConfigureOpenAI()
        {
            // This would normally require an API key
            AIIntegration.ConfigureAI(AIProvider.OpenAI, "your-api-key-here", false);
            Debug.Log("OpenAI configured (disabled - add real API key to enable)");
            UpdateOutputText("OpenAI configured (add API key to enable)");
        }
        
        void Update()
        {
            // Keyboard shortcuts for testing using new Input System
            if (Keyboard.current != null)
            {
                if (Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    OnGenerateStoryClicked();
                }
                
                if (Keyboard.current.aKey.wasPressedThisFrame)
                {
                    OnTestAIClicked();
                }
                
                if (Keyboard.current.hKey.wasPressedThisFrame)
                {
                    ShowHelp();
                }
            }
        }
        
        void ShowHelp()
        {
            string helpText = @"
=== STORY GENERATOR DEMO CONTROLS ===
SPACE - Generate new story
A - Test AI integration
H - Show this help

Features:
- Procedural story generation
- Character creation with traits
- Location generation
- Branching dialogue system
- World state tracking
- AI integration placeholder

Check the Console for detailed story output!
";
            Debug.Log(helpText);
            UpdateOutputText("Help shown in Console (Press H for help)");
        }
        
        void OnValidate()
        {
            // Auto-find components if missing
            if (storyGenerator == null)
            {
                storyGenerator = FindAnyObjectByType<StoryGeneratorManager>();
            }
            
            if (dataInitializer == null)
            {
                dataInitializer = FindAnyObjectByType<StoryDataInitializer>();
            }
        }
    }
} 