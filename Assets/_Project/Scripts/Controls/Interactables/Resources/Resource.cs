using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

[RequireComponent(typeof(CircleCollider2D))]
public class Resource : MonoBehaviour
{
    private int _amount;
    public int Amount { get => _amount; }
    
    public virtual List<string> GetResourceID() => new List<string>();
    
    public void AddAmount(int amount) => _amount += amount;
    public void RemoveAmount(int amount) => _amount -= amount;
}
