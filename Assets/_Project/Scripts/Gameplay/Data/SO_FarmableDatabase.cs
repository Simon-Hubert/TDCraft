using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "FarmableDatabase", menuName = "Scriptable Objects/FarmableDatabase")]
    public class SO_FarmableDatabase : ScriptableObject
    {
        [SerializeField] List<SO_FarmableType> farmableTypes;
    }
}
