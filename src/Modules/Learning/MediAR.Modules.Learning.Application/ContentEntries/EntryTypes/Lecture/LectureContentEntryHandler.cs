using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.Lecture.Commands;
using MediAR.Modules.Learning.Application.ContentEntries.TypeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.Lecture
{
  class LectureContentEntryHandler : IContentEntryHandler
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly IContentEntriesRepository _contentEntriesRepository;

    public LectureContentEntryHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor, IContentEntriesRepository contentEntriesRepository)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
      _contentEntriesRepository = contentEntriesRepository;
    }

    [ContentEntryAction("create")]
    public async Task<dynamic> CreateAsync(CreateCommand command)
    {
      var lecture = new LectureContentEntry(null, _executionContextAccessor.TenantId, command.ModuleId, new LectureData(command.TextData), new LectureConfiguration());

      await _contentEntriesRepository.SaveEntryAsync(lecture);

      return null;
    }
  }
}
