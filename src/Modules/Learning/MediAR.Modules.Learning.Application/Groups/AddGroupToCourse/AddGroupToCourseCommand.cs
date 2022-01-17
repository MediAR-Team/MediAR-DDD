using MediAR.Modules.Learning.Application.Contracts;

namespace MediAR.Modules.Learning.Application.Groups.AddGroupToCourse
{
  public class AddGroupToCourseCommand : CommandBase
  {
    public int GroupId { get; }
    public int CourseId { get; }

    public AddGroupToCourseCommand(int groupId, int courseId)
    {
      GroupId = groupId;
      CourseId = courseId;
    }
  }
}
