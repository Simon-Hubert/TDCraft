using Controls;
using UnityEngine;
using UnityEngine.Events;

public class Farmable : IInteractable
{
    #region Fields
    [SerializeField] private float _life;
    [SerializeField] private Resource _droppedResource;
    #endregion
    
    #region UnityEvents
    public UnityEvent OnDamaged;
    public UnityEvent OnDropResource;
    #endregion
    public void Interact()
    {
        OnDamaged?.Invoke();
        _life--;
        if(_life <= 0) DropResource();
    }

    public virtual void DropResource()
    {
        OnDropResource?.Invoke();
    }
}
