using System;
using System.Collections.Generic;
using UnityEngine;

public class FactsContainerController
{
    private readonly IFactsService _factsService;
    private readonly FactsContainerModel _factsContainer;
    

    public FactsContainerController(IFactsService factsService, FactsContainerModel factsContainer)
    {
        _factsService = factsService;
        _factsContainer = factsContainer;       
    }

    public async void FetchFacts()
    {
        ShowLoader(true); 

        try
        {
            var breedDataList = await _factsService.GetFactsAsync();
            if (breedDataList != null)
            {
                var factModels = MapToFactModels(breedDataList);
                _factsContainer.UpdateFacts(factModels);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error loading facts: {ex.Message}");
        }
        finally
        {
            ShowLoader(false);
        }
    }
    private List<FactModel> MapToFactModels(List<BreedData> breedDataList)
    {
        var factModels = new List<FactModel>();
        foreach (var breedData in breedDataList)
        {
            factModels.Add(new FactModel
            {
                Id = breedData.Id,
                Name = breedData.Attributes.Name
            });
        }
        return factModels;
    }

    private void ShowLoader(bool show)
    {     
        
    }
}
