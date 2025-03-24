using UnityEngine;

namespace Gameplay
{
    [System.Serializable]

    [RequireComponent(typeof(CircleCollider2D))]
    public class Resource : MonoBehaviour
    {
        [SerializeField] int _amount;
        [SerializeField] SO_ResourceType _type;
        public int Amount { get => _amount; }
    
        public string GetResourceID() => _type.ResourceID;
    
        public void AddAmount(int amount) => _amount += amount;
        public void RemoveAmount(int amount) => _amount -= amount;
    }
}
