using System.Collections.Generic;

namespace ShadyNagy.DapperManager.Models
{
  public class DatabaseTable
  {
    public string Schema { get; set; }
    public string Name { get; set; }
    public string TableType { get; set; }
    public string Catalog { get; set; }
    public string TableSpaceName { get; set; }
    public string ClusterName { get; set; }
    public string IotName { get; set; }
    public string Status { get; set; }
    public int PctFree { get; set; }
    public int PctUsed { get; set; }
    public int IniTrans { get; set; }
    public int MaxTrans { get; set; }
    public int InitialExtent { get; set; }
    public int NextExtent { get; set; }
    public int MinExtents { get; set; }
    public int MaxExtents { get; set; }
    public int PctIncrease { get; set; }
    public int FreeLists { get; set; }
    public int FreeListGroups { get; set; }
    public string Logging { get; set; }
    public string BackedUp { get; set; }
    public int NumRows { get; set; }
    public int Blocks { get; set; }
    public int EmptyBlocks { get; set; }
    public int AvgSpace { get; set; }
    public int ChainCnt { get; set; }
    public int AvgRowLen { get; set; }
    public int AvgSpaceFreeListBlocks { get; set; }
    public int NumFreeListBlocks { get; set; }
    public int Degree { get; set; }
    public int Instances { get; set; }
    public string Cache { get; set; }
    public string TableLock { get; set; }
    public int SampleSize { get; set; }
    public string LastAnalyzed { get; set; }
    public string Partitioned { get; set; }
    public string IotType { get; set; }
    public string Temporary { get; set; }
    public string Secondary { get; set; }
    public string Nested { get; set; }
    public string BufferPool { get; set; }
    public string FlashCache { get; set; }
    public string CellFlashCache { get; set; }
    public string RowMovement { get; set; }
    public string GlobalStats { get; set; }
    public string UserStats { get; set; }
    public string Duration { get; set; }
    public string SkipCorrupt { get; set; }
    public string Monitoring { get; set; }
    public string ClusterOwner { get; set; }
    public string Dependencies { get; set; }
    public string Compression { get; set; }
    public string CompressionFor { get; set; }
    public string Dropped { get; set; }
    public string ReadOnly { get; set; }
    public string SegmentCreated { get; set; }
    public string ResultCache { get; set; }
    public List<DatabaseColumn> Columns { get; set; } = new List<DatabaseColumn>();
  }
}
