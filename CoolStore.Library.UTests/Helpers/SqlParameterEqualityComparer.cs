using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace CoolStore.Library.UTests
{
    public partial class ProductConnectedRepositoryTests
    {
        private class SqlParameterEqualityComparer : EqualityComparer<IEnumerable<IDbDataParameter>>
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

                return x.All(xItem => y.Any(yItem => yItem.Value!.Equals(xItem.Value) && yItem.ParameterName == xItem.ParameterName));
            }

            public override int GetHashCode([DisallowNull] IEnumerable<IDbDataParameter> obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
