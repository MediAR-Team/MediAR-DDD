using FluentValidation;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.SubmissionTask.Commands
{
  public class GetViewCommand {
    public int EntryId { get; set; }
  }

  public class GetViewValidator : AbstractValidator<GetViewCommand> {
    public GetViewValidator()
    {
        RuleFor(x => x.EntryId).NotEqual(0);
    }
  }
}