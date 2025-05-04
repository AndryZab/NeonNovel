using Naninovel;
using System.Threading.Tasks;
using static Naninovel.FountainConverter;

[CommandAlias("runMyMethod")]
public class NaniRequest : Command
{
    [ParameterAlias("name")]
    public StringParameter MethodName;

    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        switch (MethodName.Value)
        {
            case "QuestReceiveNPC1":
                RequstCallsToNani.Instance.QuestReceiveNPC1();
                break;
            case "Quest—ompleteNPC1":
                RequstCallsToNani.Instance.Quest—ompleteNPC1();
                break;
            
        }

        return UniTask.CompletedTask;
    }
}
