using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MediAR.Modules.Learning.Application.StudentSubmissions.SubmissionTypes
{
  public class SubmissionTaskSubmission : IStudentSubmission<SubmissionTaskSubmissionData>
  {
    public int? Id { get; private set; }

    public Guid TenantId { get; private set; }

    public Guid StudentId { get; private set; }

    public int EntryId { get; private set; }

    public SubmissionTaskSubmissionData Data { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? ChangedAt { get; private set; }

    public int ContentEntryType => 2;

    public static SubmissionTaskSubmission Create(Guid tenantId, Guid studentId, int entryId, SubmissionTaskSubmissionData data)
    {
      return new SubmissionTaskSubmission
      {
        TenantId = tenantId,
        StudentId = studentId,
        EntryId = entryId,
        Data = data
      };
    }
  }

  public class SubmissionTaskSubmissionData : IXmlSerializable
  {
    public IEnumerable<(string Name, string DisplayName)> FileNames { get; set; }
    public string Comment { get; set; }

    public SubmissionTaskSubmissionData(IEnumerable<(string, string)> fileNames, string comment)
    {
      FileNames = fileNames;
      Comment = comment;
    }

    public SubmissionTaskSubmissionData(){
      FileNames = new List<(string, string)>();
    }

    public XmlSchema GetSchema()
    {
      return null;
    }

    public void ReadXml(XmlReader reader)
    {
      reader.MoveToContent();
      Comment = reader.GetAttribute("comment") ?? string.Empty;
      reader.ReadToFollowing("file");
      var fileN = new List<(string Name, string DisplayName)>();
      do
      {
        if (!string.IsNullOrEmpty(reader.GetAttribute("name")))
        {
          fileN.Add((reader.GetAttribute("name"), reader.GetAttribute("displayname")));
        }
      } while (reader.ReadToNextSibling("file"));

      FileNames = fileN;
    }

    public void WriteXml(XmlWriter writer)
    {
      writer.WriteAttributeString("comment", Comment ?? string.Empty);

      writer.WriteStartElement("files");

      foreach (var f in FileNames)
      {
        writer.WriteStartElement("file");
        writer.WriteAttributeString("name", f.Name);
        writer.WriteAttributeString("displayname", f.DisplayName);
        writer.WriteEndElement();
      }

      writer.WriteEndElement();
    }
  }
}
