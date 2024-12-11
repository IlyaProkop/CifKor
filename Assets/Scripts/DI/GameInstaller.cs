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


        //���������
        Container.Bind<RequestManager>().AsSingle();
        Container.Bind<ServiceManager>().AsSingle();
        Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();

        // �������
        Container.Bind<IWeatherService>().To<WeatherService>().AsSingle();
        Container.Bind<IFactsService>().To<FactsService>().AsSingle();

        //������
        Container.Bind<PopUpModel>().AsSingle();
        Container.Bind<WeatherModel>().AsSingle();

        // �����������
        Container.Bind<WeatherController>().AsSingle();
        Container.Bind<FactsContainerController>().AsSingle();
        Container.Bind<PopUpController>().AsSingle();
        Container.BindFactory<string, PopUpController, FactController, FactControllerFactory>()
            .To<FactController>()
            .AsTransient();


        // Views
        Container.Bind<WeatherView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PopUpView>().FromComponentInHierarchy().AsSingle();

        // ��������� Fact
        Container.Bind<FactsContainerModel>().FromComponentInHierarchy().AsSingle();

        // ��� FactView
        Container.Bind<FactViewPool>().AsSingle()
    .WithArguments(factPrefab, factsContainer);

    }
}
