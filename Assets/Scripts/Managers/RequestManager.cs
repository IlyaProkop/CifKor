using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public class RequestManager
{
    private readonly RequestQueue _requestQueue;

    public RequestManager(RequestQueue requestQueue)
    {
        _requestQueue = requestQueue;
    }

    public void AddRequest(Func<CancellationToken, UniTask> request)
    {
        _requestQueue.AddRequest(request);
    }

    public void CancelCurrentRequest()
    {
        _requestQueue.CancelCurrentRequest();
    }

    public void ClearAllRequests()
    {
        _requestQueue.ClearQueue();
    }

    public async UniTask<T> ExecuteRequest<T>(Func<CancellationToken, UniTask<T>> request, Action onLoadingStart = null, Action onLoadingEnd = null)
    {
        var taskCompletionSource = new UniTaskCompletionSource<T>();

        AddRequest(async token =>
        {
            try
            {
                onLoadingStart?.Invoke();
                var result = await request(token);
                taskCompletionSource.TrySetResult(result);
            }
            catch (Exception ex)
            {
                taskCompletionSource.TrySetException(ex);
            }
            finally
            {
                onLoadingEnd?.Invoke();
            }
        });

        return await taskCompletionSource.Task;
    }
}
