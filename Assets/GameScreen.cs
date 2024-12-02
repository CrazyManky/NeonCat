using System;
using Project.Screpts.Screens;
using Services;
using SO;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : BaseScreen
{
    [SerializeField] private PauseScreen _pauseScreen;
    [SerializeField] private PauseScreen _GameOverScreen;
    [SerializeField] private LevelsConfig _levelsConfig;
    [SerializeField] private LevelComplitedScreen _levelComplitedScreen;
    [SerializeField] private Button _moveLeft;
    [SerializeField] private Button _moveRight;

    private GameLevel _gameLevelInstance;
    private CameraFollow _cameraFollow;
    private bool GameAcitve = true;

    public override void Init()
    {
        base.Init();
        _cameraFollow = ServiceLocator.Instance.GetService<CameraFollow>();
        var level = _levelsConfig.GetLevel();
        InstaceLevel(level);
    }

    public void ShowPauseScreen()
    {
        var puaseScreen = Instantiate(_pauseScreen, transform);
        puaseScreen.Init();
    }

    public void ShowGameOverScreen()
    {
        var puaseScreen = Instantiate(_GameOverScreen, transform);
        puaseScreen.Init();
    }

    public void Update()
    {
        if (GameAcitve)
        {
            if (Vector3.Distance(_gameLevelInstance.transform.position,
                    _gameLevelInstance.CharacterMovement.transform.position) >= 30f)
            {
                GameAcitve = false;
                ShowGameOverScreen();
            }
        }
    }

    public void InstaceLevel(GameLevel _gameLevel)
    {
        _gameLevelInstance = Instantiate(_gameLevel);
        _gameLevelInstance.InitLevel();
        _cameraFollow.SetPlayer(_gameLevelInstance.CharacterMovement.transform);
        _moveLeft.onClick.AddListener(_gameLevelInstance.CharacterMovement.JumpLeft);
        _moveRight.onClick.AddListener(_gameLevelInstance.CharacterMovement.JumpRight);
        _gameLevelInstance.CounterInterectiveItems.OnMaxItems += ShowGameComlitedLevel;
    }

    public void ShowGameComlitedLevel()
    {
        var levelComlitedScren = Instantiate(_levelComplitedScreen, transform);
        levelComlitedScren.Init();
    }


    public override void Сlose()
    {
        _cameraFollow.OfFoolowTarget();
        _moveLeft.onClick.AddListener(_gameLevelInstance.CharacterMovement.JumpLeft);
        _moveRight.onClick.AddListener(_gameLevelInstance.CharacterMovement.JumpRight);
        Destroy(_gameLevelInstance.gameObject);
        Destroy(_gameLevelInstance.CharacterMovement.gameObject);
        _gameLevelInstance.CounterInterectiveItems.OnMaxItems -= ShowGameComlitedLevel;
        base.Сlose();
    }
}