using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;

public class RequestQueue
{
    private readonly Subject<Func<CancellationToken, UniTask>> _requestSubject = new();
    private CancellationTokenSource _currentTokenSource;

    public RequestQueue()
    {
        _requestSubject
            .SelectMany(request => ProcessRequest(request).ToObservable()) 
            .Subscribe(
                _ => { },
                ex => UnityEngine.Debug.LogError($"Error in request: {ex.Message}")
            )
            .AddTo(new CompositeDisposable());
    }

    public void AddRequest(Func<CancellationToken, UniTask> request)
    {
        _requestSubject.OnNext(request);
    }

    public void CancelCurrentRequest()
    {
        _currentTokenSource?.Cancel();
        _currentTokenSource?.Dispose();
        _currentTokenSource = null;
    }

    public void ClearQueue()
    {
        CancelCurrentRequest();
        _requestSubject.OnCompleted(); 
    }

    private async UniTask ProcessRequest(Func<CancellationToken, UniTask> request)
    {
        _currentTokenSource = new CancellationTokenSource();

        try
        {
            await request(_currentTokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            UnityEngine.Debug.Log("Request canceled.");
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"Unhandled exception: {ex.Message}");
        }
        finally
        {
            _currentTokenSource.Dispose();
            _currentTokenSource = null;
        }
    }
}
