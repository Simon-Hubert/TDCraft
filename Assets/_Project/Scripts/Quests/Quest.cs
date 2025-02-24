using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    [Serializable]
    public struct Quest
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private string _reward;
        [SerializeField] private int _difficulty;
        [SerializeField] private List<int> _requirements;
        
        public Quest(string name, string description, string reward, int difficulty, List<int> requirements) {
            _name = name;
            _description = description;
            _reward = reward;
            _difficulty = difficulty;
            _requirements = requirements;
        }

        public string Name => _name;

        public string Description => _description;

        public string Reward => _reward;

        public int Difficulty => _difficulty;

        public List<int> Requirements => _requirements;
    }
}
