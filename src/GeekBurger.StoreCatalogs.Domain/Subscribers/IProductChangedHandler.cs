namespace GeekBurger.StoreCatalogs.Domain.Subscribers
{
    public interface IProductChangedHandler : IMessageHandler<ProductChanged>
    {
    }
}
