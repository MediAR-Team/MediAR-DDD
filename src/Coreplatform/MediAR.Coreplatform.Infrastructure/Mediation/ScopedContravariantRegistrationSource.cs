using Autofac.Core;
using Autofac.Features.Variance;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediAR.Coreplatform.Infrastructure.Mediation
{
  public class ScopedContravariantRegistrationSource : IRegistrationSource
  {
    private readonly IRegistrationSource _child = new ContravariantRegistrationSource();
    private readonly List<Type> _types = new();

    public ScopedContravariantRegistrationSource(params Type[] types)
    {
      if (types == null)
      {
        throw new ArgumentNullException(nameof(types));
      }

      if (!types.All(x => x.IsGenericTypeDefinition))
      {
        throw new ArgumentException("Supplied types should be generic type definitions");
      }

      _types.AddRange(types);
    }

    public bool IsAdapterForIndividualComponents => _child.IsAdapterForIndividualComponents;

    public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
    {
      var registrations = _child.RegistrationsFor(service, registrationAccessor);

      foreach (var r in registrations)
      {
        var defs = r.Target.Services.OfType<TypedService>().Select(x => x.ServiceType.GetGenericTypeDefinition());

        if (defs.Any(_types.Contains))
        {
          yield return r;
        }
      }
    }
  }
}
