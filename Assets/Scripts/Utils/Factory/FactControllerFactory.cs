using Zenject;

public class FactControllerFactory : PlaceholderFactory<string, PopUpController, FactController>
{
    private readonly DiContainer _container;

    public FactControllerFactory(DiContainer container)
    {
        _container = container;
    }

    public override FactController Create(string factId, PopUpController popUpController)
    {
        var factController = _container.Instantiate<FactController>();
        factController.Initialize(factId, popUpController);
        return factController;
    }
}
