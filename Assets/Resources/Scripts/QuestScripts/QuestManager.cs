using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    private QuestUI questUI;

    private Dictionary<string, Quest> quests = new();

    public int QuestCount;

    private void Awake()
    {
        Instance = this;

        questUI = GetComponent<QuestUI>();

        quests["Quest1"] = new Quest("Quest Mia", "Quest Mia: go to Eva and get a task from her");
        quests["Quest2"] = new Quest("Quest Eva", "Quest Eva: find ring in Audience");
        quests["Quest3"] = new Quest("Quest Eva", "Quest Eva: Return to pub");
        quests["Quest4"] = new Quest("Quest", "Quest: Go to Mia's place");
        questUI.InitializeQuestCanv();
    }

    public void ReceiveQuest(string questId)
    {
        if (quests.TryGetValue(questId, out var quest))
            questUI.ShowQuest(quest);
    }

    public void CompleteQuest(string questId)
    {
        if (quests.TryGetValue(questId, out var quest))
        {
            quest.Complete();
            questUI.ShowQuest(quest);
        }
    }

    public void HideQuests() => questUI.Hide();
}
