namespace ShadyNagy.DapperManager.Models
{
  public class DatabaseColumn
  {
    public string TableCatalog { get; set; }
    public string TableSchema { get; set; }
    public string TableName { get; set; }
    public string Name { get; set; }
    public string OrdinalPosition { get; set; }
    public string ColumnDefault { get; set; }
    public bool IsNullable { get; set; }
    public string DataType { get; set; }
    public int? CharacterMaximumLength { get; set; }
    public int? CharacterOctetLength { get; set; }
    public int? NumericPrecision { get; set; }
    public int? NumericPrecisionRadix { get; set; }
    public int? NumericScale { get; set; }
    public string DateTimePrecision { get; set; }
    public string CharacterSetCatalog { get; set; }
    public string CharacterSetSchema { get; set; }
    public string CharacterSetName { get; set; }
    public string CollationCatalog { get; set; }
    public string CollationSchema { get; set; }
    public string CollationName { get; set; }
    public string DomainCatalog { get; set; }
    public string DomainSchema { get; set; }
    public string DomainName { get; set; }
  }
}
