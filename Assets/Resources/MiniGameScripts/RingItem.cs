using Naninovel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingItem : MonoBehaviour
{
    public async void RingIsClicked()
    {
        var player = Engine.GetService<IScriptPlayer>();
        await player.PreloadAndPlayAsync("Final RingIsFinded");
    }
}
