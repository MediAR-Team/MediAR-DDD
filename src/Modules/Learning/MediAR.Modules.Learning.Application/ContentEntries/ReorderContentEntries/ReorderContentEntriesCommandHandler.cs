using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Domain;
using MediAR.Modules.Learning.Application.Configuration.Commands;
using MediatR;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.ContentEntries.ReorderContentEntries
{
  internal class ReorderContentEntriesCommandHandler : ICommandHandler<ReorderContentEntriesCommand>
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public ReorderContentEntriesCommandHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(ReorderContentEntriesCommand request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();
      var orderDt = new DataTable();
      orderDt.Columns.Add("Id");
      orderDt.Columns.Add("Ordinal");

      foreach (var orderItem in request.NewOrder)
      {
        orderDt.Rows.Add(orderItem.Id, orderItem.Ordinal);
      }

      var queryParams = new DynamicParameters();
      queryParams.Add("NewOrder", orderDt.AsTableValuedParameter("learning.ut_OrdinalUpdate"));
      queryParams.Add("TenantId", _executionContextAccessor.TenantId);
      try
      {
        await connection.ExecuteAsync("[learning].[upd_ReorderContentEntries]", queryParams, commandType: CommandType.StoredProcedure);
      }
      catch (SqlException ex)
      {
        throw new BusinessRuleValidationException(ex.Message);
      }


      return Unit.Value;
    }
  }
}
