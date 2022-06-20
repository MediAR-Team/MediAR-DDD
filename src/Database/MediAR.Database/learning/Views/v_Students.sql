CREATE VIEW [learning].[v_Students]
AS
  SELECT
    s.Id,
    s.TenantId,
    s.FirstName,
    s.LastName,
    s.Email,
    s.UserName
  FROM [learning].Students s
