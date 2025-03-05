using System;
using System.Collections.Generic;
using Controls;
using UnityEngine;

[Serializable]
public struct Needings
{
    public string nResourceID;
    public int nAmount;

    public Needings(string nResourceID, int nAmount)
    {
        this.nResourceID = nResourceID;
        this.nAmount = nAmount;
    }
}

[CreateAssetMenu(fileName = "ModuleData", menuName = "Scriptable Objects/ModuleData")]
public class SO_ModuleData : ScriptableObject
{
    [SerializeField] List<Needings> _needings;

    [SerializeField] private string _moduleID;

    [SerializeField] private Sprite _spriteBuildable;
    //[SerializeField] UpgradesPlan[3] _upgradesPlan;
    
    public List<Needings> Needings { get => _needings;}
    public string ModuleID { get => _moduleID;}
    public Sprite SpriteBuildable { get => _spriteBuildable;}
    public bool Craftable = false;

    public SO_ModuleData(string moduleID, List<Needings> needings)
    {
        _moduleID = moduleID;
        _needings = needings;
    }
}
