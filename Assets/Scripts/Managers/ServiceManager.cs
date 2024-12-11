public class ServiceManager
{
    private readonly WeatherController _weatherController;
    private readonly FactsContainerController _factsContainerController;
    private readonly RequestManager _requestManager;

    public enum ActiveService
    {
        None,
        Weather,
        Facts
    }

    public ActiveService CurrentService { get; private set; } = ActiveService.None;

    public ServiceManager(WeatherController weatherController, FactsContainerController factsContainerController, RequestManager requestManager)
    {
        _weatherController = weatherController;
        _factsContainerController = factsContainerController;
        _requestManager = requestManager;
    }

    public void SetActiveService(ActiveService service)
    {
        if (CurrentService == service)
            return;


        _requestManager.CancelCurrentRequest();


        switch (CurrentService)
        {
            case ActiveService.Weather:
                _weatherController.StopWeatherUpdates();
                break;
            case ActiveService.Facts:

                break;
        }


        CurrentService = service;


        switch (service)
        {
            case ActiveService.Weather:
                _weatherController.StartWeatherUpdates();
                break;
            case ActiveService.Facts:
                _factsContainerController.FetchFacts();
                break;
        }
    }
}
