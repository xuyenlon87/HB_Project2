using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{
    public void ContinueButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        Close(0);
    }

    public void QuitButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        Close(0);
    }
}
