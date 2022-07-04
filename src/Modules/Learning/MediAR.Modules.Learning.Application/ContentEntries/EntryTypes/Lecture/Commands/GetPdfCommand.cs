using FluentValidation;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.Lecture.Commands
{
  public class GetPdfCommand {
    public int EntryId { get; set; }
  }

  internal class GetPdfCommandValidator : AbstractValidator<GetPdfCommand> {
    public GetPdfCommandValidator()
    {
      RuleFor(x => x.EntryId).NotEqual(0).WithMessage("Set the entry id");
    }
  }
}