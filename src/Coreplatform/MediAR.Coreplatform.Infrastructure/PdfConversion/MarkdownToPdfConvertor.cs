using MediAR.Coreplatform.Application.PdfConversion;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace MediAR.Coreplatform.Infrastructure.PdfConversion
{
  public class MarkdownToPdfConvertor : IMarkdownToPdfConvertor
  {
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly MarkdownToPdfConfiguration _config;

    public MarkdownToPdfConvertor(IHttpClientFactory httpClientFactory, MarkdownToPdfConfiguration config)
    {
      _httpClientFactory = httpClientFactory;
      _config = config;
    }

    public Stream ConvertMarkdownToPdf(string markdown)
    {
      var httpClient = _httpClientFactory.CreateClient();

      var serializedBody = JsonConvert.SerializeObject(new { Data = markdown }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
      var content = new StringContent(serializedBody, Encoding.UTF8, "application/json");

      var result = httpClient.PostAsync($"{_config.BaseUrl}/api/md2pdf", content);

      var stream = result.GetAwaiter().GetResult().Content.ReadAsStream();
      return stream;
    }
  }
}