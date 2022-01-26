using FluentValidation;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.Lecture.Commands
{
  class CreateCommand
  {
    public int ModuleId { get; set; }

    public string Title { get; set; }

    public string TextData { get; set; }
  }

  internal class CreateCommandValidator : AbstractValidator<CreateCommand>
  {
    public CreateCommandValidator()
    {
      RuleFor(c => c.ModuleId).NotNull().WithMessage("Module id is required");
      RuleFor(c => c.ModuleId).NotEmpty().WithMessage("Title cannot be empty");
      RuleFor(c => c.TextData).NotNull().NotEmpty().WithMessage("Text data id is required");
    }
  }
}
