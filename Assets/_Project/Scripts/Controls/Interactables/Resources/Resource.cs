using UnityEngine;

public enum ResourceID
{
    Null,
    Wood,
};

[RequireComponent(typeof(CircleCollider2D))]
public class Resource : MonoBehaviour
{
    private int _amount;
    
    public int Amount { get => _amount; }
    
    public virtual ResourceID GetResourceID() => ResourceID.Null;
    
    public void AddAmount(int amount) => _amount += amount;
    public void RemoveAmount(int amount) => _amount -= amount;
}
