namespace ShadyNagy.DapperManager.Models
{
  public class DatabaseField
  {
    public DatabaseFieldDirection DatabaseFieldDirection { get; set; }
    public DatabaseFieldType DatabaseFieldType { get; set; }
    public string FieldName { get; set; }
    public object Value { get; set; }

    public DatabaseField(DatabaseFieldDirection databaseFieldDirection, string fieldName, DatabaseFieldType databaseFieldType, object value)
    {
      DatabaseFieldDirection = databaseFieldDirection;
      DatabaseFieldType = databaseFieldType;
      FieldName = fieldName;
      Value = value;
    }

    public DatabaseField(string fieldName, object value)
    {
      FieldName = fieldName;
      Value = value;
    }
  }
}
