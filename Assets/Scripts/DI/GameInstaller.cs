using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private FactView factPrefab;
    [SerializeField] private Transform factsContainer;

    public override void InstallBindings()
    {
        //Uitls
        Container.Bind<RequestQueue>().AsSingle();


        //Менеджеры
        Container.Bind<RequestManager>().AsSingle();
        Container.Bind<ServiceManager>().AsSingle();
        Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();

        // Сервисы
        Container.Bind<IWeatherService>().To<WeatherService>().AsSingle();
        Container.Bind<IFactsService>().To<FactsService>().AsSingle();

        //Модели
        Container.Bind<PopUpModel>().AsSingle();
        Container.Bind<WeatherModel>().AsSingle();

        // Контроллеры
        Container.Bind<WeatherController>().AsSingle();
        Container.Bind<FactsContainerController>().AsSingle();
        Container.Bind<PopUpController>().AsSingle();
        Container.BindFactory<string, PopUpController, FactController, FactControllerFactory>()
            .To<FactController>()
            .AsTransient();


        // Views
        Container.Bind<WeatherView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PopUpView>().FromComponentInHierarchy().AsSingle();

        // Контейнер Fact
        Container.Bind<FactsContainerModel>().FromComponentInHierarchy().AsSingle();

        // Пул FactView
        Container.Bind<FactViewPool>().AsSingle()
    .WithArguments(factPrefab, factsContainer);

    }
}
