using System.IO;

namespace MediAR.Coreplatform.Application.PdfConversion
{
  public interface IMarkdownToPdfConvertor {
    Stream ConvertMarkdownToPdf(string markdown);
  }
}