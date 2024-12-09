namespace ChatAPI.Presenters.Core;

public interface IPresenter<TViewModel, TResult>
{
    TViewModel Present(TResult result);
}
