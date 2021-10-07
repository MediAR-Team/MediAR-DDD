using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAR.Coreplatform.Infrastructure.DomainEvents
{
  interface IDomainNotificationsMapper
  {
    string GetName(Type type);

    Type GetType(string name);
  }
}
