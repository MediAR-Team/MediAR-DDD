using MediAR.Modules.Learning.Application.Contracts;
using System.Collections.Generic;

namespace MediAR.Modules.Learning.Application.Students
{
    public class GetStudentsQuery: QueryBase<IEnumerable<StudentDto>> {
      public string FirstName { get; }
      public string LastName { get; }
      public string UserName { get; }

      public GetStudentsQuery(string firstName, string lastName, string userName)
      {
          FirstName = firstName;
          LastName = lastName;
          UserName = UserName;
      }
    }
}