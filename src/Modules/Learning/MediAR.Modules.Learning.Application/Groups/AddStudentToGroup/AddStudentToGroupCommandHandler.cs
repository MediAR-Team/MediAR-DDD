﻿using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Domain;
using MediAR.Modules.Learning.Application.Configuration.Commands;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Groups.AddStudentToGroup
{
  class AddStudentToGroupCommandHandler : ICommandHandler<AddStudentToGroupCommand>
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public AddStudentToGroupCommandHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(AddStudentToGroupCommand request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();
      var queryParams = new
      {
        request.StudentId,
        request.StudentUserName,
        request.GroupId,
        TenantId = request.TenantId ?? _executionContextAccessor.TenantId
      };
      try
      {
        await connection.ExecuteAsync("[learning].[add_Student_to_Group]", queryParams, commandType: CommandType.StoredProcedure);
        return Unit.Value;
      }
      catch (SqlException ex)
      {
        throw new BusinessRuleValidationException(ex.Message);
      }
    }
  }
}