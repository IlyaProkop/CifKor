using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FactView : MonoBehaviour
{
    [SerializeField] private Text factId;
    [SerializeField] private Text factName;

    private string _factId;
    private FactController _factController;

    private FactControllerFactory _factControllerFactory;
    private PopUpController _popUpController;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnFactClicked);
    }

    [Inject]
    public void Construct(FactControllerFactory factControllerFactory, PopUpController popUpController)
    {
        _factControllerFactory = factControllerFactory;
        _popUpController = popUpController;
    }

    public void UpdateView(int factNumber, string factName, string factId)
    {
        this.factId.text = factNumber.ToString();
        this.factName.text = factName;
        _factId = factId;
        
        _factController = _factControllerFactory.Create(_factId, _popUpController);
    }

    public void OnFactClicked()
    {
        _factController.FetchFactDetails();
    }
}
