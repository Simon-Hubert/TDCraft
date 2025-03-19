using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using Controls;

public class ResourceMakerWindow : EditorWindow
{
    private string _csvPathFile = "";
    private string _searchQuery = "";
    private string _resourcesPath = "Assets/_Project/Scripts/ScriptableObjects/Resources/";
    Vector2 scrollPosH = Vector2.zero;
    
    List<SO_ResourceType> _resourceTypes = new List<SO_ResourceType>();
    List<SO_FarmableType> _farmableTypes = new List<SO_FarmableType>();
    List<SO_ModuleData> _moduleData = new List<SO_ModuleData>();

    private enum View
    {
        ResourceGenerator,
        FarmableGenerator,
        ModuleGenerator,
        DataEditor
    }

    private View _currentView = View.ResourceGenerator;


    [MenuItem("Tools/CraftManager")]
    public static void ShowWindow() => GetWindow<ResourceMakerWindow>("CraftManager");

    void OnGUI()
    {
        scrollPosH = EditorGUILayout.BeginScrollView(scrollPosH);
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

        if (GUILayout.Toggle(_currentView == View.ModuleGenerator, "Module Generator", EditorStyles.toolbarButton))
        {
            _currentView = View.ModuleGenerator;
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
            case View.ModuleGenerator:
                ShowModuleGenerator();
                break;
            case View.DataEditor:
                ShowDataEditor();
                break;
        }
        EditorGUILayout.EndScrollView();
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
            GUILayout.Label("Resources :", EditorStyles.boldLabel);
            GUILayout.Space(20);
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
            GUILayout.Label("Farmables :", EditorStyles.boldLabel);
            GUILayout.Space(20);
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                foreach (SO_FarmableType farmableType in _farmableTypes)
                {
                    LoadFarmable(farmableType);
                }
            }
        }

        if (_moduleData.Count > 0)
        {
            GUILayout.Label("Modules :", EditorStyles.boldLabel);
            GUILayout.Space(20);
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                foreach (SO_ModuleData moduleData in _moduleData)
                {
                    LoadModules(moduleData);
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

            LoadDataFromCSV(_csvPathFile, true, false);
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

            LoadDataFromCSV(_csvPathFile, false, false);
        }
    }
    private void ShowModuleGenerator()
    {
        GUILayout.Label("Select CSV File", EditorStyles.boldLabel);  
        
        if (GUILayout.Button("Select CSV File"))
        {
            string path = EditorUtility.OpenFilePanel("Select CSV File", "", "csv");
            if (!string.IsNullOrEmpty(path)) _csvPathFile = path;
        }

        if (!string.IsNullOrEmpty(_csvPathFile)) GUILayout.Label("Selected CSV : " + _csvPathFile);

        if (GUILayout.Button("Generate Modules"))
        {
            if (string.IsNullOrEmpty(_csvPathFile))
            {
                Debug.LogError("Please select a CSV File");
                return;
            }

            LoadDataFromCSV(_csvPathFile, false, true);
        }
    }
    #endregion

    #region DataEditor Methods

    void GetFilteredAssets()
    {
        _farmableTypes.Clear();
        _resourceTypes.Clear();
        _moduleData.Clear();
        
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
                else if (asset is SO_ModuleData)
                {
                    _moduleData.Add((SO_ModuleData)asset);
                }
            }
        }
    }

    void LoadFarmable(SO_FarmableType farmableType)
    {
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUILayout.Label(farmableType.name, EditorStyles.boldLabel);
            GUILayout.Space(10);
            farmableType.FarmableID = EditorGUILayout.TextField("ID",farmableType.FarmableID);
            farmableType.FarmableRarity = EditorGUILayout.TextField("Rarity" ,farmableType.FarmableRarity);
            farmableType.LifeMAX = EditorGUILayout.FloatField("LifeMAX" ,farmableType.LifeMAX);
        }
    }

    void LoadResource(SO_ResourceType resourceType)
    {
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUILayout.Label(resourceType.name, EditorStyles.boldLabel);
            GUILayout.Space(10);
            resourceType.ResourceID = EditorGUILayout.TextField("ID" ,resourceType.ResourceID);
            resourceType.ResourceRarity = EditorGUILayout.TextField( "Rarity" ,resourceType.ResourceRarity);
            resourceType.Value = EditorGUILayout.FloatField( "Value" ,resourceType.Value);
        }
    }

    void LoadModules(SO_ModuleData moduleData)
    {
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUILayout.Label(moduleData.name, EditorStyles.boldLabel);
            GUILayout.Space(10);
            GUILayout.Label("ModuleID:  " + moduleData.ModuleID);
            GUILayout.Space(10);
            GUILayout.Label("Needings", EditorStyles.boldLabel);
            foreach (Needings needing in moduleData.Needings)
            {
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Resource needed:  " + needing.nResourceID);
                    GUILayout.Label("Amount :  " + needing.nAmount);
                    GUILayout.EndHorizontal();
                }
            }
        }
    }

    #endregion

    #region CSV Methods

    void LoadDataFromCSV(string csvPath, bool isResources, bool isModules)
    {
        if (!File.Exists(csvPath))
        {
            Debug.LogError("The CSV File does not exist");
            return;
        }

        string[] csvLines = File.ReadAllLines(csvPath);

        if (isResources) LoadResources(csvLines);
        else if (isModules)
        {
            LoadModules(csvLines);
        }
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

            List<SO_ResourceType> droppedResources = GetResourcesTypes(resourceDroppedIDs);
            GenerateFarmableData(farmableID, farmableRarity, lifeMAX, droppedResources);
        }
    }
    
    void LoadModules(string[] csvLines)
    {
        int i = -1;
        foreach (string line in csvLines)
        {
            i++;
            if(i == 0) continue;
            string[] columns = line.Split(',');

            if (columns.Length < 2)
            {
                Debug.LogWarning("Skipping invalid line at " + line);
                continue;
            }

            string moduleID = columns[0];
            
            List<string> needings = new List<string>();
            foreach (string needing in columns[1].Split('_'))
            {
                needings.Add(needing);
            }
            GenerateModuleData(moduleID, needings);
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

    void GenerateFarmableData(string farmableID, string farmableRarity, float lifeMAX, List<SO_ResourceType> resourcesDropped)
    {
        if (string.IsNullOrEmpty(farmableID))
        {
            Debug.LogError("Farmable ID must be provided");
            return;
        }

        SO_FarmableType newFarmable = CreateInstance<SO_FarmableType>();
        newFarmable = new SO_FarmableType(farmableID, farmableRarity, lifeMAX, resourcesDropped);

        AssetDatabase.CreateAsset(newFarmable,
            "Assets/_Project/Scripts/ScriptableObjects/Farmables/" + farmableID + ".asset");
        EditorUtility.SetDirty(newFarmable);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Resource Data generated!");
    }

    void GenerateModuleData(string moduleID, List<string> needingsID)
    {
        if (string.IsNullOrEmpty(moduleID))
        {
            Debug.LogError("Module ID must be provided");
            return;
        }
        
        SO_ModuleData newModule = CreateInstance<SO_ModuleData>();
        List<Needings> needingsList = GetNeedings(needingsID);
        newModule = new SO_ModuleData(moduleID, needingsList);
        
        AssetDatabase.CreateAsset(newModule,
            "Assets/_Project/Scripts/ScriptableObjects/Modules/" + moduleID + ".asset");
        EditorUtility.SetDirty(newModule);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Resource Data generated!");
    }
    List<SO_ResourceType> GetResourcesTypes(List<string> resourceDroppedIDs)
    {
        List<SO_ResourceType> resourcesOut = new List<SO_ResourceType>();
        foreach (string droppedID in resourceDroppedIDs)
        {
            resourcesOut.Add(GetResourceType(droppedID));
        }
        return resourcesOut;
    }

    SO_ResourceType GetResourceType(string resourceID) => (SO_ResourceType)AssetDatabase.LoadAssetAtPath<SO_ResourceType>(_resourcesPath + resourceID + ".asset");

    List<Needings> GetNeedings(List<string> needingsID)
    {
        List<Needings> needingsList = new List<Needings>();
        foreach (string needingID in needingsID)
        {
            string amountID = "";
            amountID += needingID[0];
            
            if (string.IsNullOrEmpty(amountID))
            {
                Debug.LogError("Needings ID must be provided like this : nAmoutMyResourceNeeded");
                return null;
            }
            Debug.Log(needingID + " : " + amountID);
            Debug.Log(amountID);
            int amount = int.Parse(amountID);
            
            string resourceID = "";
            for (int i = 1; i < needingID.Length; i++)
            {
                resourceID += needingID[i];
            }

            Needings needing = new Needings(resourceID, amount);
            needingsList.Add(needing);
        }

        return needingsList;
    }
    #endregion
}
   
