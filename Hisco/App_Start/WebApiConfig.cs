namespace Hisco
{
    using System.Net.Http.Headers;
    using System.Reflection;
    using System.Web.Http;
    using Autofac;
    using Autofac.Integration.WebApi;
    using Database;
    using Models;
    using Repositories;
    using Security;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("Get", "api/{controller}/{level}");
            config.Routes.MapHttpRoute("Post", "api/{controller}");

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // autofac config
            var builder = new ContainerBuilder();
            builder.RegisterInstance<IRepository<Entry>>(new EntryRepository(new MySqlDataAccessor<Entry>()));
            builder.RegisterInstance<ISecurity>(new BasicSecurity());
            builder.RegisterAssemblyTypes(
                Assembly.GetExecutingAssembly())
                .Where(t =>
                    !t.IsAbstract && typeof(ApiController).IsAssignableFrom(t))
                    .InstancePerMatchingLifetimeScope(AutofacWebApiDependencyResolver.ApiRequestTag);
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;
        }
    }
}