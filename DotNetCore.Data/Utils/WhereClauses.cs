using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetCore.Data.Utils
{
    public class WhereClauses : List<WhereClause>
    {
        public WhereClauses(IEnumerable<WhereClause> collection) : base(collection)
        {
        }

        public string Resolve()
        {
            var result = new StringBuilder();
            var orList = new List<WhereClause>();

            foreach (var where in this)
            {
                if (where.Join == WhereJoin.Or || where.Join == WhereJoin.OrNot)
                {
                    orList.Add(where);
                    continue;
                }

                if (result.Length > 0) result.Append(where.Join.GetDescription());
                result.Append(CreateWhereClause(where));
            }

            if (orList.Count > 0)
            {
                if (result.Length > 0) result.Append(WhereJoin.And.GetDescription());
                result.Append("(");

                foreach (var where in orList)
                {
                    if (result.Length > 0) result.Append(where.Join.GetDescription());
                    result.Append(CreateWhereClause(where));
                }
                result.Append(")");
            }

            return result.ToString();
        }

        public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>();

        private string CreateWhereClause(WhereClause where)
        {
            if (where.Parameters != null)
            {
                foreach (var param in where.Parameters)
                {
                    Parameters.TryAdd(param.Key, param.Value);
                }
            }

            return string.Concat(where.Field, where.Operator.GetDescription(), where.Value);
        }
    }
}
