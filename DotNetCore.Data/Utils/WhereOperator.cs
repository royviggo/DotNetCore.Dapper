using System.ComponentModel;

namespace DotNetCore.Data.Utils
{
    public enum WhereOperator
    {
        [Description(" = ")]
        Equal = 1,

        [Description(" <> ")]
        NotEqual = 2,

        [Description(" > ")]
        GreaterThan = 3,

        [Description(" >= ")]
        GreaterThanOrEqual = 4,

        [Description(" < ")]
        LessThan = 5,

        [Description(" <= ")]
        LessThanOrEqual = 6,

        [Description(" IN ")]
        In = 7,
    }
}
