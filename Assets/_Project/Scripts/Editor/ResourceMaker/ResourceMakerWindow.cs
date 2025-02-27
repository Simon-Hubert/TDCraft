using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using Controls;

public class ResourceMakerWindow : EditorWindow
{
    private string _csvPathFile = "";
    private string _searchQuery = "";
    
    List<SO_ResourceType> _resourceTypes = new List<SO_ResourceType>();
    List<SO_FarmableType> _farmableTypes = new List<SO_FarmableType>();

    private enum View
    {
        ResourceGenerator,
        FarmableGenerator,
        DataEditor
    }

    private View _currentView = View.ResourceGenerator;


    [MenuItem("Tools/ResourceManager")]
    public static void ShowWindow() => GetWindow<ResourceMakerWindow>("ResourceManager");

    void OnGUI()
    {

        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        if (GUILayout.Toggle(_currentView == View.DataEditor, "Data Editor", EditorStyles.toolbarButton))
        {
            _currentView = View.DataEditor;
        }

        if (GUILayout.Toggle(_currentView == View.ResourceGenerator, "Resource Generator", EditorStyles.toolbarButton))
        {
            _currentView = View.ResourceGenerator;
        }

        if (GUILayout.Toggle(_currentView == View.FarmableGenerator, "Farmable Generator", EditorStyles.toolbarButton))
        {
            _currentView = View.FarmableGenerator;
        }

        GUILayout.EndHorizontal();

        switch (_currentView)
        {
            case View.ResourceGenerator:
                ShowResourceGenerator();
                break;
            case View.FarmableGenerator:
                ShowFarmableGenerator();
                break;
            case View.DataEditor:
                ShowDataEditor();
                break;
        }

    }
    
    #region Switching View Methods
    void ShowDataEditor()
    {
        _resourceTypes.Clear();
        _farmableTypes.Clear();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Search Asset", GUILayout.Width(120));
        _searchQuery = EditorGUILayout.TextField(_searchQuery);
        GUILayout.EndHorizontal();
        
            GetFilteredAssets();
        
        if (_resourceTypes.Count > 0)
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                foreach (SO_ResourceType resourceType in _resourceTypes)
                {
                    LoadResource(resourceType);
                } 
            }
        }

        if (_farmableTypes.Count > 0)
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                foreach (SO_FarmableType farmableType in _farmableTypes)
                {
                    LoadFarmable(farmableType);
                }
            }
        }
        else
        {
            GUILayout.Label("No Assets Found");
        }
    }

    void ShowResourceGenerator()
    {
        GUILayout.Label("Select CSV File", EditorStyles.boldLabel);

        if (GUILayout.Button("Select CSV File"))
        {
            string path = EditorUtility.OpenFilePanel("Select CSV File", "", "csv");
            if (!string.IsNullOrEmpty(path)) _csvPathFile = path;
        }

        if (!string.IsNullOrEmpty(_csvPathFile)) GUILayout.Label("Selected CSV : " + _csvPathFile);

        if (GUILayout.Button("Generate Resources"))
        {
            if (string.IsNullOrEmpty(_csvPathFile))
            {
                Debug.LogError("Please select a CSV File");
                return;
            }

            LoadDataFromCSV(_csvPathFile, true);
        }
    }

    void ShowFarmableGenerator()
    {
        GUILayout.Label("Select CSV File", EditorStyles.boldLabel);

        if (GUILayout.Button("Select CSV File"))
        {
            string path = EditorUtility.OpenFilePanel("Select CSV File", "", "csv");
            if (!string.IsNullOrEmpty(path)) _csvPathFile = path;
        }

        if (!string.IsNullOrEmpty(_csvPathFile)) GUILayout.Label("Selected CSV : " + _csvPathFile);

        if (GUILayout.Button("Generate Farmables"))
        {
            if (string.IsNullOrEmpty(_csvPathFile))
            {
                Debug.LogError("Please select a CSV File");
                return;
            }

            LoadDataFromCSV(_csvPathFile, false);
        }
    }
    #endregion

    #region DataEditor Methods

    void GetFilteredAssets()
    {
        _farmableTypes.Clear();
        _resourceTypes.Clear();
        
        string basePath = "Assets/_Project/Scripts/ScriptableObjects";
        string[] guids = AssetDatabase.FindAssets(_searchQuery, new string[] { basePath });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string assetName = Path.GetFileNameWithoutExtension(path);
            
            var asset = AssetDatabase.LoadAssetAtPath<Object>(path);

            if (assetName.ToLower().Contains(_searchQuery.ToLower()))
            {
                if (asset is SO_FarmableType)
                {
                    _farmableTypes.Add((SO_FarmableType)asset);
                }
                else if (asset is SO_ResourceType)
                {
                    _resourceTypes.Add((SO_ResourceType)asset);
                }
            }
        }
    }

    void LoadFarmable(SO_FarmableType farmableType)
    {
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUILayout.Label(farmableType.name, EditorStyles.boldLabel);
            farmableType.FarmableID = EditorGUILayout.TextField(farmableType.FarmableID);
            farmableType.FarmableRarity = EditorGUILayout.TextField(farmableType.FarmableRarity);
            farmableType.LifeMAX = EditorGUILayout.FloatField(farmableType.LifeMAX);
        }
    }

    void LoadResource(SO_ResourceType resourceType)
    {
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUILayout.Label(resourceType.name, EditorStyles.boldLabel);
            resourceType.ResourceID = EditorGUILayout.TextField(resourceType.ResourceID);
            resourceType.ResourceRarity = EditorGUILayout.TextField(resourceType.ResourceRarity);
            resourceType.Value = EditorGUILayout.FloatField(resourceType.Value);
        }
    }

    #endregion

    #region CSV Methods

    void LoadDataFromCSV(string csvPath, bool isResources)
    {
        if (!File.Exists(csvPath))
        {
            Debug.LogError("The CSV File does not exist");
            return;
        }

        string[] csvLines = File.ReadAllLines(csvPath);

        if (isResources) LoadResources(csvLines);
        else
        {
            LoadFarmables(csvLines);
        }

    }

    void LoadResources(string[] csvLines)
    {
        foreach (string line in csvLines)
        {
            string[] columns = line.Split(',');

            if (columns.Length < 3)
            {
                Debug.LogWarning("Skipping invalid line at " + line);
                continue;
            }

            string resourceID = columns[0];
            string resourceRarity = columns[1];
            float resourceValue;
            if (!float.TryParse(columns[2], out resourceValue))
            {
                Debug.LogWarning("Invalid value for resourceValue in line: " + line);
                continue; // Saute cette ligne si la conversion échoue
            }

            GenerateResourceData(resourceID, resourceRarity, resourceValue);
        }
    }

    void LoadFarmables(string[] csvLines)
    {
        foreach (string line in csvLines)
        {
            string[] columns = line.Split(',');

            if (columns.Length < 4)
            {
                Debug.LogWarning("Skipping invalid line at " + line);
                continue;
            }

            string farmableID = columns[0];
            string farmableRarity = columns[1];
            float lifeMAX;

            if (!float.TryParse(columns[2], out lifeMAX))
            {
                Debug.LogWarning("Invalid value for lifeMAX in line: " + line);
                continue; // Saute cette ligne si la conversion échoue
            }

            List<string> resourceDroppedIDs = new List<string>();
            foreach (string resourceDroppedID in columns[3].Split('_'))
            {
                if (!resourceDroppedIDs.Contains(resourceDroppedID)) resourceDroppedIDs.Add(resourceDroppedID);
            }

            GenerateFarmableData(farmableID, farmableRarity, lifeMAX, resourceDroppedIDs);
        }
    }

    private void GenerateResourceData(string resourceID, string resourceRarity, float resourceValue)
    {
        if (string.IsNullOrEmpty(resourceID))
        {
            Debug.LogError("Resource ID must be provided");
            return;
        }

        SO_ResourceType newResource = CreateInstance<SO_ResourceType>();
        newResource = new SO_ResourceType(resourceID, resourceRarity, resourceValue);

        AssetDatabase.CreateAsset(newResource,
            "Assets/_Project/Scripts/ScriptableObjects/Resources/" + resourceID + ".asset");
        EditorUtility.SetDirty(newResource);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Resource Data generated!");
    }

    void GenerateFarmableData(string farmableID, string farmableRarity, float lifeMAX, List<string> resourceDroppedIDs)
    {
        if (string.IsNullOrEmpty(farmableID))
        {
            Debug.LogError("Farmable ID must be provided");
            return;
        }

        SO_FarmableType newFarmable = CreateInstance<SO_FarmableType>();
        newFarmable = new SO_FarmableType(farmableID, farmableRarity, lifeMAX, resourceDroppedIDs);

        AssetDatabase.CreateAsset(newFarmable,
            "Assets/_Project/Scripts/ScriptableObjects/Farmables/" + farmableID + ".asset");
        EditorUtility.SetDirty(newFarmable);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Resource Data generated!");
    }

    #endregion
}
   
