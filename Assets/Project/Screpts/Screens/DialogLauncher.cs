using Services;
using UnityEngine;

namespace Project.Screpts.Screens
{
    public class DialogLauncher : MonoBehaviour, IService
    {
        [SerializeField] private MenuScreen _menuScreen;
        [SerializeField] private SettingsScreen _settingsScreen;
        [SerializeField] private GameScreen _gameScreen;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private CameraFollow _cameraFollow;


        private BaseScreen _activeScreen;

        private void Awake()
        {
            ServiceLocator.Init();
            ServiceLocator.Instance.AddService(this);
            ServiceLocator.Instance.AddService(_audioManager);
            ServiceLocator.Instance.AddService(_cameraFollow);
        }

        private void Start() => ShowMenuScreen();

        public void ShowMenuScreen()
        {
            _audioManager.PlayMenuMusick();
            ShowScreen(_menuScreen);
        }

        public void ShowSettingsScreen() => ShowScreen(_settingsScreen);

        public void ShowGameScreen()
        {
            _audioManager.PlayGame();
            ShowScreen(_gameScreen);
        }

        private void ShowScreen(BaseScreen screen)
        {
            _activeScreen?.Сlose();
            var instanceScreen = Instantiate(screen, transform);
            instanceScreen.Init();
            _activeScreen = instanceScreen;
        }
    }
}