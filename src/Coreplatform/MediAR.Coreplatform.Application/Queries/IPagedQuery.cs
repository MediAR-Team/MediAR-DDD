namespace MediAR.Coreplatform.Application.Queries
{
  public interface IPagedQuery
  {
    int? Page { get; }
    int? PageSize { get; }
  }
}
