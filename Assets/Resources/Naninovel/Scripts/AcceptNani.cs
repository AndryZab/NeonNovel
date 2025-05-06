using Naninovel;
using System.Threading.Tasks;

[CommandAlias("setIntQuest")]
public class SetLevelCommand : Command
{
    [ParameterAlias("val")]
    public IntegerParameter Level;

    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        if (QuestManager.Instance != null)
            QuestManager.Instance.QuestCount = Level;
      

        return UniTask.CompletedTask;
    }
}
