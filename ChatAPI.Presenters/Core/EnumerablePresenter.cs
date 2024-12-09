namespace ChatAPI.Presenters.Core;

public class EnumerablePresenter<TViewModel, TResult>(IPresenter<TViewModel, TResult> elemPresenter) : IPresenter<IEnumerable<TViewModel>, IEnumerable<TResult>>
{
    public IEnumerable<TViewModel> Present(IEnumerable<TResult> result)
    {
        return result.Select(elem => elemPresenter.Present(elem));
    }
}
