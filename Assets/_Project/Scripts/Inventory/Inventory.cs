using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    Resource[] _resources;
    [SerializeField] private int _inventorySize;

    public UnityEvent OnAddResource;

    private void Awake()
    {
        Init();
    }

    public void AddResource(Resource resource)
    {
        for (int i = 0; i < _inventorySize; i++)
        {
            if (_resources[i].GetResourceID() == resource.GetResourceID())
            {
                _resources[i].AddAmount(resource.Amount);
                OnAddResource?.Invoke();
                break;
            }

            if (_resources[i] == null)
            {
                _resources[i] = resource;
                OnAddResource?.Invoke();
                break;
            }
        }
        
        Debug.LogError("CANT ADD RESOURCE, INVENTORY FULL");
    }

    public void TryRemoveResource(Resource resource, int amount)
    {
        for (int i = 0; i < _inventorySize; i++)
        {
            if (_resources[i].GetResourceID() == resource.GetResourceID() && _resources[i].Amount >= amount)
            {
                RemoveResource(resource, amount, i);
                return;
            }
        }
        
        Debug.LogError("CANT REMOVE RESOURCE, NOT ENOUGH RESOURCE AMOUNT");
    }

    void Init()
    {
        _resources = new Resource[_inventorySize];
    }
    void RemoveResource(Resource resource, int amount, int index)
    {
        _resources[index].RemoveAmount(amount);
        if (_resources[index].Amount <= 0)
        {
            _resources[index] = null;
        }
    }

    public bool IsInInventory(List<Needings> needings)
    {
        bool result = true;
        foreach (Needings needing in needings)
        {
            if (!result) return false;
            foreach (Resource resource in _resources)
            {
                if (resource.GetResourceID() == needing.nResourceID &&
                    resource.Amount >= needing.nAmount)
                {
                    result = true;
                    break;
                }
                result = false;

            }
        }
        return true;
    }
}
