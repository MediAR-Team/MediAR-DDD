﻿using System;

namespace MediAR.Modules.Membership.Domain.Users
{
  class User
  {
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid TenantIt { get; set; }
  }
}