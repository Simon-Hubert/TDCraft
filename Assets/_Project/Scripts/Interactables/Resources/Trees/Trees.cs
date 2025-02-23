using Controls;
using UnityEngine;

public class Trees : Farmable, IInteractable
{
    public void Interact()
    {
        OnDamaged?.Invoke();
        _currentLife--;
        if(_currentLife <= 0) DropResource();
    }
}
