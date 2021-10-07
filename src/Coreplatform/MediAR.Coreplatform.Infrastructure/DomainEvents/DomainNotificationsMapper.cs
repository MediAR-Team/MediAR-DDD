using System;

namespace MediAR.Coreplatform.Infrastructure.DomainEvents
{
  class DomainNotificationsMapper : IDomainNotificationsMapper
  {
    private readonly BiDictionary<string, Type> _map;

    public DomainNotificationsMapper(BiDictionary<string, Type> map)
    {
      _map = map;
    }

    public string GetName(Type type)
    {
      return _map.TryGetBySecond(type, out var name) ? name : null;
    }

    public Type GetType(string name)
    {
      return _map.TryGetByFirst(name, out var type) ? type : null;
    }
  }
}
