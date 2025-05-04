using Naninovel;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RequstCallsToNani : MonoBehaviour
{
    public static RequstCallsToNani Instance;

    public TextMeshProUGUI textEva;
    public TextMeshProUGUI textMia;
    public GameObject questIsCompletedEva;
    public GameObject questIsProcessedEva;
    public GameObject questIsCompletedMia;
    public GameObject questIsProcessedMia;

    public GameObject OnQuestMia;
    public GameObject OnQuestEva;

    public GameObject panelQuests;
    private void Awake()
    {
        Instance = this;
        OnQuestEva.SetActive(false);
        OnQuestMia.SetActive(false);
       
    }
    public void QuestReceiveNPC1()
    {
        textMia.text = "Quest Mia: go to Eva and get a task from her";
        panelQuests.SetActive(true);
        OnQuestMia.SetActive(true);
        questIsCompletedMia.SetActive(false);
        questIsProcessedMia.SetActive(true);
    }
    public void Quest—ompleteNPC1()
    {
        textMia.text = "<s>Quest Mia: go to Eva and get a task from her</s>";
        questIsCompletedMia.SetActive(true);
        questIsProcessedMia.SetActive(false);
    }
    public void QuestReceiveNPC2()
    {
        textEva.text = "Quest Eva: find item in next location and return to first location";
        questIsCompletedEva.SetActive(true);
        questIsProcessedEva.SetActive(false);
    }
    public void QuestCompleteNPC2()
    {
        panelQuests.SetActive(false);
        textEva.text = "<s>Quest Eva: find item in next location and return to first location</s>";
        questIsCompletedEva.SetActive(false);
        questIsProcessedEva.SetActive(true);
    }
    
}
