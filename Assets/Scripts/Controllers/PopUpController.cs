public class PopUpController
{
    private readonly PopUpModel _popUpModel;
    private readonly PopUpView _popUpView;

    public PopUpController(PopUpModel popUpModel, PopUpView popUpView)
    {
        _popUpModel = popUpModel;
        _popUpView = popUpView;

        _popUpView.Initialize(HidePopUp);
    }

    public void ShowPopUp(string title, string description)
    {
        SetModel(title, description);
        SetView();        
        _popUpView.Show();
    }
    private void SetModel(string title, string description)
    {
        _popUpModel.Title = title;
        _popUpModel.Description = description;
    }
    private void SetView()
    {
        _popUpView.UpdateView(_popUpModel.Title, _popUpModel.Description);
    }

    public void HidePopUp()
    {
        _popUpView.Hide();
    }
}
