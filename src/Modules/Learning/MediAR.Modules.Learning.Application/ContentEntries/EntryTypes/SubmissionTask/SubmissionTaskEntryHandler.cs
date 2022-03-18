using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Exceptions;
using MediAR.Coreplatform.Application.FielStorage;
using MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.SubmissionTask.Commands;
using MediAR.Modules.Learning.Application.ContentEntries.TypeHandlers;
using MediAR.Modules.Learning.Application.StudentSubmissions;
using MediAR.Modules.Learning.Application.StudentSubmissions.SubmissionTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.SubmissionTask
{
  internal class SubmissionTaskEntryHandler : IContentEntryHandler
  {
    private readonly IContentEntriesRepository _repo;
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly IFileStorage _fileStorage;
    private readonly IStudentSubmissionsRepository _submissionsRepository;

    public int TypeId => 2;

    public SubmissionTaskEntryHandler(IContentEntriesRepository repo, IExecutionContextAccessor executionContextAccessor, IFileStorage fileStorage, IStudentSubmissionsRepository submissionsRepository)
    {
      _repo = repo;
      _executionContextAccessor = executionContextAccessor;
      _fileStorage = fileStorage;
      _submissionsRepository = submissionsRepository;
    }

    [ContentEntryAction("create")]
    public async Task<dynamic> CreateEntry(CreateCommand command)
    {
      var entry = new SubmissionTaskContentEntry(null, _executionContextAccessor.TenantId, command.ModuleId, command.Title, new SubmissionTaskData(command.TextData), new SubmissionTaskConfiguration(command.MaxMark));

      await _repo.SaveEntryAsync(entry);
      return entry;
    }

    public async Task<dynamic> CreateSubmission(CreateSubmissionCommand command, int entryId)
    {
      var entry = await _repo.GetContentEntry(entryId);

      if (entry == null || entry.TypeId != TypeId)
      {
        throw new NotFoundException("Entry not found");
      }

      var submissionFiles = new List<(string, string)>();
      foreach (var f in command.Files)
      {
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(f.DisplayName);
        await _fileStorage.UploadAsync(f.B64File, _executionContextAccessor.TenantId.ToString(), fileName);
        submissionFiles.Add((fileName, f.DisplayName));
      }

      var submissionData = new SubmissionTaskSubmissionData(submissionFiles, command.Comment);
      var submission = SubmissionTaskSubmission.Create(_executionContextAccessor.TenantId, _executionContextAccessor.UserId, entryId, submissionData);

      await _submissionsRepository.SaveAsync(submission);

      return submission;
    }
  }
}
