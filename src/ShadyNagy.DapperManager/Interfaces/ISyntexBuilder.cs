using System.Collections.Generic;
using System.Data;

namespace ShadyNagy.DapperManager.Interfaces
{
    public interface ISyntexBuilder
    {
        string Build();
        ISyntexBuilder Reset();
        ISyntexBuilder SelectAllFrom(string name);
        ISyntexBuilder SelectColumnsFrom(string name, List<string> columnsNames);
        ISyntexBuilder All();
        ISyntexBuilder Select();
        ISyntexBuilder Select(List<string> columnsNames);
        ISyntexBuilder From(string name);        
    }
}
