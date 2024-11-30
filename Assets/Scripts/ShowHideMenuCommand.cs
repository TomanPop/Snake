public class ShowHideMenuCommand : ICommand
{
    private IUIController _uiController;
    
    public ShowHideMenuCommand(IUIController uiController)
    {
        _uiController = uiController;
    }

    public void Execute()
    {
        _uiController.ShowHideMenu();
    }
}