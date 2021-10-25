using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MediAR.Coreplatform.Infrastructure.Serializaton
{
  public class AllPropertiesContractResolver : DefaultContractResolver
  {
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
      var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        .Select(x => CreateProperty(x, memberSerialization))
        .ToList();

      props.ForEach(x =>
      {
        x.Readable = true;
        x.Writable = true;
      });

      return props;
    }
  }
}
