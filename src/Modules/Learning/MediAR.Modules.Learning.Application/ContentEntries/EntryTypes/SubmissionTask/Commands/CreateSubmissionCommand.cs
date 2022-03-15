using FluentValidation;
using MediAR.Coreplatform.Application.FielStorage;
using System.Collections.Generic;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.SubmissionTask.Commands
{
  public class CreateSubmissionCommand
  {
    public IEnumerable<FileUploadDto> Files { get; set; }
    public string Comment { get; set; }
  }

  public class CreateSubmissionValidator : AbstractValidator<CreateSubmissionCommand>
  {
    public CreateSubmissionValidator()
    {
      RuleFor(x => x.Files).NotEmpty().WithMessage("Upload at least one file");
    }
  }
}
