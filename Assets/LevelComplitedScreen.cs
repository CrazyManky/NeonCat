using System.Collections;
using System.Collections.Generic;
using Project.Screpts.Screens;
using SO;
using UnityEngine;

public class LevelComplitedScreen : BaseScreen
{
    [SerializeField] private LevelsConfig _levelsConfig;

    public override void Init()
    {
        base.Init();
        _levelsConfig.LevelComplited();
    }

    public void LoadNextLevel()
    {
        Dialog.ShowGameScreen();
    }

    public void BackToMenu()
    {
        Dialog.ShowMenuScreen();
    }
}