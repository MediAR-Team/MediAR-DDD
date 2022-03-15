using FluentValidation;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.SubmissionTask.Commands
{
  public class GetMySubmissionCommand
  {
    public int EntryId { get; }
  }

  public class GetMySubmissionValidator : AbstractValidator<GetMySubmissionCommand>
  {
    public GetMySubmissionValidator()
    {
      RuleFor(x => x.EntryId).NotEmpty().WithMessage("Entry id must be given");
    }
  }
}
