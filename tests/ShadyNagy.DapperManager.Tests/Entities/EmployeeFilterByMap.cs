using ShadyNagy.DapperManager.Models;

namespace ShadyNagy.DapperManager.Tests.Entities
{
  class EmployeeFilterByMap
  {
    public DatabaseField SerialNumber { get; set; } = new DatabaseField(DatabaseFieldDirection.In, "SER_NO", DatabaseFieldType.Varchar2, null);
    public DatabaseField ProjectNumber { get; set; } = new DatabaseField(DatabaseFieldDirection.In, "PROJ_NO", DatabaseFieldType.Varchar2, null);
    public DatabaseField Status { get; set; } = new DatabaseField(DatabaseFieldDirection.In, "STATUS", DatabaseFieldType.Varchar2, null);
    public DatabaseField TicketTypeCode { get; set; } = new DatabaseField(DatabaseFieldDirection.In, "TICKET_TYPE_CODE", DatabaseFieldType.Varchar2, null);
    public DatabaseField TicketCategory { get; set; } = new DatabaseField(DatabaseFieldDirection.In, "TICKET_CATEGORY", DatabaseFieldType.Varchar2, null);
    public DatabaseField TicketSubCategory { get; set; } = new DatabaseField(DatabaseFieldDirection.In, "TICKET_SUB_CATEGORY", DatabaseFieldType.Varchar2, null);
    public DatabaseField Title { get; set; } = new DatabaseField(DatabaseFieldDirection.In, "DESCR", DatabaseFieldType.Varchar2, null);
    public DatabaseField Result { get; set; } = new DatabaseField(DatabaseFieldDirection.Out, "P_RESULT", DatabaseFieldType.RefCursor, null);
    public DatabaseField EnglishMessage { get; set; } = new DatabaseField(DatabaseFieldDirection.Out, "P_EN_MESS", DatabaseFieldType.Varchar2, null);
    public DatabaseField ArabicMessage { get; set; } = new DatabaseField(DatabaseFieldDirection.Out, "P_AR_MESS", DatabaseFieldType.Varchar2, null);
  }
}
