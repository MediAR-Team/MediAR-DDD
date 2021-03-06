using System;
using System.Collections.Generic;

namespace MediAR.Modules.Learning.Application.Courses.GetCourse
{
  public class CourseAggregateDto
  {
    public int Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string BackgroundImageUrl { get; set; }
    public List<ModuleDto> Modules { get; set; } = new List<ModuleDto>();
  }

  public class ModuleDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Ordinal { get; set; }
    public List<ContentEntryDto> ContentEntries { get; set; } = new List<ContentEntryDto>();
  }

  public class ContentEntryDto
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string TypeName { get; set; }
    public int Ordinal { get; set; }
  }
}
