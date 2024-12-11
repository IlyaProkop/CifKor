using System;
using System.Threading;
using Cysharp.Threading.Tasks;

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

    public async UniTask<T> ExecuteRequest<T>(Func<CancellationToken, UniTask<T>> request)
    {
        var taskCompletionSource = new UniTaskCompletionSource<T>();

        AddRequest(async token =>
        {
            try
            {
                var result = await request(token);
                taskCompletionSource.TrySetResult(result);
            }
            catch (Exception ex)
            {
                taskCompletionSource.TrySetException(ex);
            }
        });

        return await taskCompletionSource.Task;
    }
}
