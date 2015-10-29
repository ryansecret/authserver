#region

using Autofac;

#endregion



namespace Ets.OAuthServer.Core.Infrastructure.DependencyManagement
{
    public interface IDependencyRegister
    {
        int Order { get; }
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);
    }
}