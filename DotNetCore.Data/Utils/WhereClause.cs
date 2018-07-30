using System.Collections.Generic;

namespace DotNetCore.Data.Utils
{
    public class WhereClause
    {
        public WhereClause() { }

        public WhereClause(WhereJoin join, string field, WhereOperator op, string value) : this(join, field, op, value, null) { }

        public WhereClause(WhereJoin join, string field, WhereOperator op, string value, IDictionary<string, object> param)
        {
            Join = join;
            Field = field;
            Operator = op;
            Value = value;

            if (param != null)
            {
                foreach (var kvp in param)
                {
                    Parameters.TryAdd(kvp.Key, kvp.Value);
                }
            }
        }

        public WhereJoin Join { get; set; }
        public string Field { get; set; }
        public WhereOperator Operator { get; set; }
        public string Value { get; set; }
        public IDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }
}
