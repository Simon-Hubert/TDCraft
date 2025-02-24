using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FarmableDatabase", menuName = "Scriptable Objects/FarmableDatabase")]
public class SO_FarmableDatabase : ScriptableObject
{
    [SerializeField] List<SO_FarmableType> farmableTypes;
}
