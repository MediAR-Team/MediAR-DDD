using FluentValidation;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.Lecture.Commands
{
  internal class UpdateCommand
  {
    public int EntryId { get; set; }
    public string TextData { get; set; }
  }

  internal class UpdateCommandValidator : AbstractValidator<UpdateCommand>
  {
    public UpdateCommandValidator()
    {
      RuleFor(c => c.EntryId).NotNull().WithMessage("Entry id must be specified");
      RuleFor(c => c.TextData).NotNull().NotEmpty().WithMessage("Text data cannot be empty");
    }
  }
}
