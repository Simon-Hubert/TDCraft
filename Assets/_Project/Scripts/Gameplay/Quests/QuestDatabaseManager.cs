using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Quests
{
    public class QuestDatabaseManager : MonoBehaviour
    {
        [SerializeField] private QuestDatabase questDatabase;

        public Quest GetRandomQuest() {
            throw new NotImplementedException();
        }
    }
}
