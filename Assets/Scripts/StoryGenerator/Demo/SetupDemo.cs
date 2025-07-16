using UnityEngine;
using StoryGenerator.Managers;
using StoryGenerator.Data;

namespace StoryGenerator.Demo
{
    /// <summary>
    /// Simple setup script to automatically configure a scene for Story Generator testing.
    /// Add this to an empty GameObject in your scene and it will set everything up.
    /// </summary>
    public class SetupDemo : MonoBehaviour
    {
        [Header("Auto Setup")]
        [SerializeField] private bool setupOnStart = true;
        [SerializeField] private bool createDataInitializer = true;
        [SerializeField] private bool createStoryManager = true;
        [SerializeField] private bool createDemoUI = true;
        
        [Header("Created Objects (Auto-populated)")]
        [SerializeField] private StoryGeneratorManager storyManager;
        [SerializeField] private StoryDataInitializer dataInitializer;
        [SerializeField] private StoryGeneratorDemo demoScript;
        
        void Start()
        {
            if (setupOnStart)
            {
                SetupDemoScene();
            }
        }
        
        [ContextMenu("Setup Demo Scene")]
        public void SetupDemoScene()
        {
            Debug.Log("Setting up Story Generator Demo Scene...");
            
            // Create Story Data Initializer
            if (createDataInitializer && dataInitializer == null)
            {
                dataInitializer = ScriptableObject.CreateInstance<StoryDataInitializer>();
                dataInitializer.name = "Story Data Initializer";
                
                // Initialize with sample data
                dataInitializer.InitializeSampleData();
                
                Debug.Log("✓ Created Story Data Initializer with sample data");
            }
            
            // Create Story Generator Manager
            if (createStoryManager && storyManager == null)
            {
                GameObject managerObject = new GameObject("Story Generator Manager");
                storyManager = managerObject.AddComponent<StoryGeneratorManager>();
                
                // Apply sample data if available
                if (dataInitializer != null)
                {
                    dataInitializer.ApplyToStoryGenerator(storyManager);
                }
                
                Debug.Log("✓ Created Story Generator Manager");
            }
            
            // Create Demo UI
            if (createDemoUI && demoScript == null)
            {
                GameObject demoObject = new GameObject("Story Generator Demo");
                demoScript = demoObject.AddComponent<StoryGeneratorDemo>();
                
                // Link references
                demoScript.storyGenerator = storyManager;
                demoScript.dataInitializer = dataInitializer;
                
                Debug.Log("✓ Created Demo UI with automatic references");
            }
            
            Debug.Log("=== DEMO SETUP COMPLETE ===");
            Debug.Log("Press SPACE to generate a story, or use the UI buttons!");
            Debug.Log("Check the Console for detailed story output.");
        }
        
        [ContextMenu("Test Story Generation")]
        public void TestStoryGeneration()
        {
            if (storyManager == null)
            {
                Debug.LogError("No StoryGeneratorManager found! Run 'Setup Demo Scene' first.");
                return;
            }
            
            Debug.Log("Generating test story...");
            storyManager.GenerateStory();
        }
        
        [ContextMenu("Reset Demo")]
        public void ResetDemo()
        {
            if (dataInitializer != null) DestroyImmediate(dataInitializer);
            if (storyManager != null) DestroyImmediate(storyManager.gameObject);
            if (demoScript != null) DestroyImmediate(demoScript.gameObject);
            
            dataInitializer = null;
            storyManager = null;
            demoScript = null;
            
            Debug.Log("Demo reset. Run 'Setup Demo Scene' to recreate.");
        }
        
        void OnValidate()
        {
            // Auto-find existing components
            if (storyManager == null)
                storyManager = FindAnyObjectByType<StoryGeneratorManager>();
            
            if (dataInitializer == null)
                dataInitializer = FindAnyObjectByType<StoryDataInitializer>();
                
            if (demoScript == null)
                demoScript = FindAnyObjectByType<StoryGeneratorDemo>();
        }
    }
} 