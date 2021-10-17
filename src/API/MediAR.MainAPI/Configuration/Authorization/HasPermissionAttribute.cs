using Microsoft.AspNetCore.Authorization;
using System;

namespace MediAR.MainAPI.Configuration.Authorization
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
  internal class HasPermissionAttribute : AuthorizeAttribute
  {
    internal const string HasPermissionPolicyName = "HasPermission";

    public HasPermissionAttribute(string name): base(HasPermissionPolicyName)
    {
      Name = name;
    }

    public string Name { get; }
  }
}
