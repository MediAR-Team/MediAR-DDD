using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Exceptions;
using MediAR.Coreplatform.Domain;
using MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.Lecture.Commands;
using MediAR.Modules.Learning.Application.ContentEntries.TypeHandlers;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.Lecture
{
  class LectureContentEntryHandler : IContentEntryHandler
  {
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly IContentEntriesRepository _contentEntriesRepository;

    public int TypeId => 2;

    public LectureContentEntryHandler(IExecutionContextAccessor executionContextAccessor, IContentEntriesRepository contentEntriesRepository)
    {
      _executionContextAccessor = executionContextAccessor;
      _contentEntriesRepository = contentEntriesRepository;
    }

    [ContentEntryAction("create")]
    public async Task<dynamic> CreateAsync(CreateCommand command)
    {
      var lecture = new LectureContentEntry(null, _executionContextAccessor.TenantId, command.ModuleId, command.Title, new LectureData(command.TextData), new LectureConfiguration());

      await _contentEntriesRepository.SaveEntryAsync(lecture);

      return lecture;
    }

    [ContentEntryAction("update")]
    public async Task<dynamic> UpdateAsync(UpdateCommand command)
    {
      var lecture = new LectureContentEntry(command.EntryId, _executionContextAccessor.TenantId, default(int), command.Title, new LectureData(command.TextData), new LectureConfiguration());

      await _contentEntriesRepository.UpdateEntryAsync(lecture);

      return lecture;
    }

    [ContentEntryAction("getview")]
    public async Task<dynamic> GetView(GetViewCommand command)
    {
      var entry = await _contentEntriesRepository.GetContentEntry(command.EntryId);

      if (entry == null)
      {
        throw new NotFoundException($"Entry with id {command.EntryId} not found");
      }

      if (entry.TypeId != TypeId)
      {
        throw new BusinessRuleValidationException("Type mismatch");
      }

      var (data, config) = EntryMapper.MapEntry(entry);

      return data;
    }
  }
}
