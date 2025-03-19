using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    [CreateAssetMenu(fileName = "ResourceType", menuName = "Scriptable Objects/ResourceType")]
    public class SO_ResourceType : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _resourceID;
        [SerializeField] private string _resourceRarity;
        public List<Sprite> Sprites;
        [SerializeField] private float _value;

        #endregion
        #region Properties
        public string ResourceID {get => _resourceID; set => _resourceID = value; }
        public string ResourceRarity {get => _resourceRarity; set => _resourceRarity = value; }
        public float Value {get => _value; set => _value = value; }
        #endregion

        public SO_ResourceType(string resourceID, string resourceRarity, float value, List<Sprite> sprites = null)
        {
            _resourceID = resourceID;
            _value = value;
            _resourceRarity = resourceRarity;
            if(sprites != null) Sprites = sprites;
        }
        
    }
}
