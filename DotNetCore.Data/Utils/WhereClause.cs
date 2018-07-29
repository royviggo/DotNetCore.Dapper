using System.Collections.Generic;

namespace DotNetCore.Data.Utils
{
    public class WhereClause
    {
        public WhereJoin Join { get; set; }
        public string Field { get; set; }
        public WhereOperator Operator { get; set; }
        public string Value { get; set; }
        public IDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }
}
