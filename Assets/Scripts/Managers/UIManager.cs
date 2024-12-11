using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button weatherButton;
    [SerializeField] private Button factsButton;

    [SerializeField] private GameObject weatherPanel; 
    [SerializeField] private GameObject factsPanel;  

    private ServiceManager _serviceManager;

    [Inject]
    public void Construct(ServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    private void Awake()
    {
        weatherButton.onClick.AddListener(SwitchToWeather);
        factsButton.onClick.AddListener(SwitchToFacts);
    }

    private void Start()
    {        
        _serviceManager.SetActiveService(ServiceManager.ActiveService.Facts);
        UpdatePanels();
    }
    
    public void SwitchToWeather()
    {
        _serviceManager.SetActiveService(ServiceManager.ActiveService.Weather);
        UpdatePanels();
    }

    public void SwitchToFacts()
    {
        _serviceManager.SetActiveService(ServiceManager.ActiveService.Facts);
        UpdatePanels();
    }    

    private void UpdatePanels()
    {
        weatherPanel.SetActive(_serviceManager.CurrentService == ServiceManager.ActiveService.Weather);
        factsPanel.SetActive(_serviceManager.CurrentService == ServiceManager.ActiveService.Facts);
    }
}
