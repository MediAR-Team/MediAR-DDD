using MediAR.Coreplatform.Application.Exceptions;
using MediAR.Modules.Learning.Application.Configuration.Commands;
using MediAR.Modules.Learning.Application.ContentEntries;
using MediAR.Modules.Learning.Application.ContentEntries.TypeHandlers;
using Newtonsoft.Json;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.StudentSubmissions.CreateSubmission
{
  internal class CreateSubmissionCommandHandler : ICommandHandler<CreateSubmissionCommand, object>
  {
    private readonly IContentEntriesRepository _contentEntriesRepository;
    private readonly IContentEntryHandlerFactory _contentEntryHandlerFactory;

    public CreateSubmissionCommandHandler(IContentEntriesRepository contentEntriesRepository, IContentEntryHandlerFactory contentEntryHandlerFactory)
    {
      _contentEntriesRepository = contentEntriesRepository;
      _contentEntryHandlerFactory = contentEntryHandlerFactory;
    }

    public async Task<object> Handle(CreateSubmissionCommand request, CancellationToken cancellationToken)
    {
      var entry = await _contentEntriesRepository.GetContentEntry(request.EntryId);
      if (entry == null)
      {
        throw new NotFoundException("Entry not found");
      }

      var handler = await _contentEntryHandlerFactory.GetHandlerAsync(entry.TypeId);

      // TODO: move into constant
      var method = handler.GetType().GetMethod("CreateSubmission");
      var paramType = method.GetParameters().First().ParameterType;
      var param = JsonConvert.DeserializeObject(request.Payload.ToString(), paramType);

      var result = await (Task<dynamic>)method.Invoke(handler, new[] { param, request.EntryId });

      return result;
    }
  }
}
