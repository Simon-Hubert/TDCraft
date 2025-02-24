using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Quests
{
    [CreateAssetMenu(fileName = "QuestDatabase", menuName = "Quest/QuestDatabase")]
    public class QuestDatabase : ScriptableObject
    {
        [SerializeField] private List<Quest> _quests;
    }
}
