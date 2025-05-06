using Naninovel;
using System.Threading.Tasks;

[CommandAlias("runC#Method")]
public class NaniRequest : Command
{
    [ParameterAlias("id")]
    public StringParameter QuestId;

    [ParameterAlias("room")]
    public IntegerParameter roomID;

    [ParameterAlias("state")]
    public BooleanParameter stateButtonMap;

    [ParameterAlias("action")]
    public StringParameter Action;

    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        switch (Action.Value.ToLower())
        {
            case "receive":
                QuestManager.Instance.ReceiveQuest(QuestId);
                break;
            case "complete":
                QuestManager.Instance.CompleteQuest(QuestId);
                break;
            case "hide":
                QuestManager.Instance.HideQuests();
                break;
            case "rooms":
                Map.Instance.SetActiveRooms(roomID, stateButtonMap);
                break;

        }

        return UniTask.CompletedTask;
    }
}
