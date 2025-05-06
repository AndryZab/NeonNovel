using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textQuest;
    [SerializeField] private GameObject questIsCompleted;
    [SerializeField] private GameObject questIsProcessed;
    [SerializeField] private GameObject panelQuests;
    [SerializeField] private GameObject onQuest;

    public void ShowQuest(Quest quest)
    {
        panelQuests.SetActive(true);
        onQuest.SetActive(true);

        if (quest.IsCompleted)
        {
            textQuest.text = $"<s>{quest.Description}</s>";
            questIsCompleted.SetActive(true);
            questIsProcessed.SetActive(false);
        }
        else
        {
            textQuest.text = quest.Description;
            questIsCompleted.SetActive(false);
            questIsProcessed.SetActive(true);
        }
    }
    public void InitializeQuestCanv()
    {
        panelQuests.SetActive(false);
        onQuest.SetActive(false);
    }
    public void Hide()
    {
        panelQuests.SetActive(false);
    }
}
