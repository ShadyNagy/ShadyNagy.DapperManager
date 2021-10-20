using ShadyNagy.DapperManager.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace ShadyNagy.DapperManager.Oracle
{
    public class OracleSyntexBuilder: ISyntexBuilder
    {
        private const string SELECT = "SELECT";
        private const string FROM = "FROM";
        private const string ALL = "*";

        public StringBuilder Syntex { get; private set; } = new StringBuilder();

        public ISyntexBuilder Reset()
        {
            Syntex = new StringBuilder();

            return this;
        }

        public ISyntexBuilder SelectAllFrom(string name)
        {
            this
                .Select()
                .All()
                .From(name);

            return this;
        }

        public ISyntexBuilder SelectColumnsFrom(string name, List<string> columnsNames)
        {
            this
                .Select(columnsNames)
                .From(name);

            return this;
        }

        public ISyntexBuilder All()
        {
            Syntex = new StringBuilder($"{ALL} ");

            return this;
        }

        public ISyntexBuilder Select()
        {
            Syntex = new StringBuilder($"{SELECT} ");

            return this;
        }

        public ISyntexBuilder Select(List<string> columnsNames)
        {
            Syntex = new StringBuilder($"{SELECT} ");
            Syntex.Append(string.Join(" ", columnsNames.ToArray()));
            Syntex.Append(" ");

            return this;
        }

        public ISyntexBuilder From(string name)
        {
            Syntex.Append($"{FROM} {name}");

            return this;
        }

        public string Build()
        {
            return Syntex.ToString();
        }
    }
}
