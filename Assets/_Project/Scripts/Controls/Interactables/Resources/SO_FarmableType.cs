using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FarmableType", menuName = "Scriptable Objects/FarmableType")]
public class SO_FarmableType : ScriptableObject
{
    #region Fields
    [SerializeField] private string _farmableID;
    [SerializeField] private string _farmableRarity;
    public List<Sprite> Sprites;
    [SerializeField] float _lifeMAX;
    //[SerializeField] GameObject _droppedResource;
    public List<string> DroppedResourceIDs;
    #endregion
    #region Properties
    public string FarmableID { get => _farmableID; set => _farmableID = value; }
    public string FarmableRarity { get => _farmableRarity; set => _farmableRarity = value; }
    public float LifeMAX { get => _lifeMAX; set => _lifeMAX = value; }
    //public GameObject DroppedResource { get => _droppedResource; }
    #endregion

    public SO_FarmableType(string farmableID, string farmableRarity, float lifeMAX, 
        List<string> droppedResourceIDs, List<Sprite> sprites = null)
    {
        _farmableID = farmableID;
        _farmableRarity = farmableRarity;
        _lifeMAX = lifeMAX;
        DroppedResourceIDs = droppedResourceIDs;
        if(sprites != null) Sprites = sprites;
    }
}
