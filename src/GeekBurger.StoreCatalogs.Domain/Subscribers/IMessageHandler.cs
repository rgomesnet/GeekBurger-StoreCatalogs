namespace GeekBurger.StoreCatalogs.Domain.Subscribers
{
    public interface IMessageHandler<TModel>
    {
        Task HandleMessageAsync(TModel? message);
    }
}
