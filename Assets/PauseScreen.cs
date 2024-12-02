using System;
using System.Collections;
using System.Collections.Generic;
using Project.Screpts.Screens;
using UnityEngine;

public class PauseScreen : BaseScreen
{
    public override void Init()
    {
        base.Init();
        Time.timeScale = 0;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Dialog.ShowGameScreen();
    }

    public void BackMenu()
    {
        Dialog.ShowMenuScreen();
        Time.timeScale = 1;
    }
}