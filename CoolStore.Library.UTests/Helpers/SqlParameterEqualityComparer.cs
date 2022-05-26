using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace CoolStore.Library.UTests.Helpers
{
    public class SqlParameterEqualityComparer : EqualityComparer<IEnumerable<IDbDataParameter>>
    {
        public override bool Equals(IEnumerable<IDbDataParameter>? x, IEnumerable<IDbDataParameter>? y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }
            //var result = x.All(xItem => y.Any(yItem => yItem.Value!.Equals(xItem.Value) && yItem.ParameterName == xItem.ParameterName));
            foreach(var xItem in x)
            {
                bool isEqual = false;
                foreach (var yItem in y)
                {
                    bool values = yItem.Value!.Equals(xItem.Value);
                    bool names = yItem.ParameterName == xItem.ParameterName;
                    if (values && names)
                    {
                        isEqual = true;
                        break;
                    }
                }
               if (!isEqual)
                {
                    return false;
                }

            }
            return true;
        }

        public override int GetHashCode([DisallowNull] IEnumerable<IDbDataParameter> obj)
        {
            throw new NotImplementedException();
        }
    }
}
