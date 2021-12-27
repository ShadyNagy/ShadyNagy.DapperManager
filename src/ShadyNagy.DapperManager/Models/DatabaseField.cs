namespace ShadyNagy.DapperManager.Models
{
  public class DatabaseMapField
  {
    public DatabaseMapFieldDirection DatabaseFieldDirection { get; set; }
    public DatabaseMapFieldType DatabaseFieldType { get; set; }
    public string FieldName { get; set; }
    public object Value { get; set; }

    public DatabaseMapField(DatabaseMapFieldDirection databaseFieldDirection, string fieldName, DatabaseMapFieldType databaseFieldType, object value)
    {
      DatabaseFieldDirection = databaseFieldDirection;
      DatabaseFieldType = databaseFieldType;
      FieldName = fieldName;
      Value = value;
    }

    public DatabaseMapField(string fieldName, object value)
    {
      FieldName = fieldName;
      Value = value;
    }
  }
}
