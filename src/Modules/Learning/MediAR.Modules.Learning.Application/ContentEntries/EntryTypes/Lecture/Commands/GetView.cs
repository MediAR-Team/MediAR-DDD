using FluentValidation;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.Lecture.Commands
{
  public class GetView
  {
    public int EntryId { get; set; }
  }

  public class GetViewValidator : AbstractValidator<GetView>
  {
    public GetViewValidator()
    {
      RuleFor(x => x.EntryId).NotNull().WithMessage("Entry id cannot be null");
    }
  }
}
