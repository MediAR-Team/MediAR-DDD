using FluentValidation;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.SubmissionTask.Commands
{
  public class CreateCommand
  {
    public string Title { get; set; }
    public int ModuleId { get; set; }
    public string TextData { get; set; }
    public int MaxMark { get; set; }
  }

  public class CreateCommandValidator : AbstractValidator<CreateCommand>
  {
    public CreateCommandValidator()
    {
      RuleFor(x => x.ModuleId).NotNull().WithMessage("Module id cannot be empty");
      RuleFor(x => x.TextData).NotEmpty().WithMessage("Text data cannot be empty");
      RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty");
      RuleFor(x => x.MaxMark).Must(x => x > 0).NotNull().WithMessage("Max mark cannot be null or less than 0");
    }
  }
}
