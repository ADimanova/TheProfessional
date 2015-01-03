namespace Professional.Web.Infrastructure.Services.Base
{
    using Professional.Data;
    using Professional.Web.Infrastructure.Caching;

    public abstract class BaseServices
    {
        public BaseServices(IApplicationData data, ICacheService cache)
        {
            this.Data = data;
            this.Cache = cache;
        }

        protected IApplicationData Data { get; private set; }

        protected ICacheService Cache { get; private set; }
    }
}