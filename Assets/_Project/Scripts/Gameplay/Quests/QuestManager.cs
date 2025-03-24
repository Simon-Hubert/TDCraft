using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Quests
{
    public class QuestManager : MonoBehaviour
    {
        public event Action<Quest> OnQuestStarted;
        public event Action<Quest> OnQuestEnded;

        public Quest ActiveQuest { get; private set; }

        public void StartNewQuest(Quest quest) {
            ActiveQuest = quest;
            OnQuestStarted?.Invoke(ActiveQuest);
        }

        public void EndActiveQuest() {
            OnQuestEnded?.Invoke(ActiveQuest);
        }

    }
}
