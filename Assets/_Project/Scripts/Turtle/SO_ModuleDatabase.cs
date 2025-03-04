using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModuleDatabase", menuName = "Scriptable Objects/ModuleDatabase")]
public class SO_ModuleDatabase : ScriptableObject
{
    public List<SO_ModuleData> moduleDatas;
}
