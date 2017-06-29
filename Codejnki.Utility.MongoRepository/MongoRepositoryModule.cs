using Autofac;
using System.Reflection;

namespace Codejnki.Utility.MongoRepository
{
  public class MongoRepositoryModule : Autofac.Module
  {
    /// <summary>
    /// Override to add registrations to the container.
    /// </summary>
    /// <param name="builder">The builder through which components can be
    /// registered.</param>
    /// <remarks>
    /// Note that the ContainerBuilder parameter is unique to this module.
    /// </remarks>
    protected override void Load(ContainerBuilder builder)
    {
      var assembly = typeof(MongoRepositoryModule).GetTypeInfo().Assembly;
      builder.RegisterAssemblyTypes(assembly).As(t => t.GetInterfaces()).SingleInstance();
    }
  }
}
