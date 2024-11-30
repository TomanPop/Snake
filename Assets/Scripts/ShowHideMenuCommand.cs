public class ShowHideMenuCommand : ICommand
{
    private UIController _uiController;
    
    public ShowHideMenuCommand(UIController uiController)
    {
        _uiController = uiController;
    }

    public void Execute()
    {
        _uiController.ShowHideMenu();
    }
}