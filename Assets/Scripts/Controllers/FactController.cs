using UnityEngine;
using Zenject;

public class FactController
{
    private readonly IFactsService _factsService;
    private PopUpController _popUpController;

    private string _factId;

    public FactController(IFactsService factsService)
    {
        _factsService = factsService;
        
    }   

    public void Initialize(string factId, PopUpController popUpController)
    {
        _factId = factId;
        _popUpController = popUpController;
    }

    public async void FetchFactDetails()
    {        
        var factDetails = await _factsService.GetFactDetailsAsync(_factId);

        if (factDetails != null)
        {
            _popUpController.ShowPopUp(factDetails.Attributes.Name, factDetails.Attributes.Description);       
        }
    }
}
