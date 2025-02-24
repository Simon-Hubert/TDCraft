using UnityEngine;

[CreateAssetMenu(fileName = "FarmableType", menuName = "Scriptable Objects/FarmableType")]
public class SO_FarmableType : ScriptableObject
{
    #region Fields
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] float _lifeMAX;
    [SerializeField] GameObject _droppedResource;
    #endregion
    #region Properties
    public string Name { get => _name; }
    public Sprite Sprite { get => _sprite; }
    public float LifeMAX { get => _lifeMAX; }
    public GameObject DroppedResource { get => _droppedResource; }
    #endregion
}
