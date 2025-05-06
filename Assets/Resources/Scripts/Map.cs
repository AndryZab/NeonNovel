using Naninovel;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public static Map Instance;
    public Button[] buttonLevels;
    public GameObject MapButton;
    private void Awake()
    {
        Instance = this;
    }
   
    public void LoadLevel()
    {
        if (QuestManager.Instance.QuestCount == 1)
        {
            var task = PlayScriptAsync("room2");
            MapButton.SetActive(true);
        }
        else if (QuestManager.Instance.QuestCount == 2)
        {
            var task = PlayScriptAsync("Final");
            MapButton.SetActive(true);

        }
        else if (QuestManager.Instance.QuestCount == 3)
        {
            var task = PlayScriptAsyncWithLabel("Final", "ReturnToPub");
            MapButton.SetActive(true);

        }
        else if (QuestManager.Instance.QuestCount == 4)
        {
            var task = PlayScriptAsyncWithLabel("Final", "LastGame");
            MapButton.SetActive(false);
        }
    }

    private async Task PlayScriptAsync(string level)
    {
        var player = Engine.GetService<IScriptPlayer>();
        await player.PreloadAndPlayAsync(level);
    }
    private async Task PlayScriptAsyncWithLabel(string level, string label)
    {
        var player = Engine.GetService<IScriptPlayer>();
        await player.PreloadAndPlayAsync(level, label);
    }
    public void SetActiveRooms(int room, bool state)
    {
        buttonLevels[room].interactable = state;
    }

}
