using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public interface IFactsService
{
    UniTask<List<BreedData>> GetFactsAsync();
    UniTask<BreedData> GetFactDetailsAsync(string factId);
}