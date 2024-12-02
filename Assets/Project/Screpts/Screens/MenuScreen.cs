namespace Project.Screpts.Screens
{
    public class MenuScreen : BaseScreen
    {
        public void OpenSetting()
        {
            AudioManager.PlayButtonClick();
            Dialog.ShowSettingsScreen();
        }

        public void ShowGameScreen()
        {
            AudioManager.PlayButtonClick();
            Dialog.ShowGameScreen();
        }
    }
}