using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EagleRock.Bs.Grid
{
    public class BoundColumn<TModel, TValue> : ColumnBase<TModel>, IColumn where TModel : class
    {
        #region variables

        private static readonly IDictionary<string, Func<TModel, TValue>> expressionCache = (IDictionary<string, Func<TModel, TValue>>)new Dictionary<string, Func<TModel, TValue>>();
        private static readonly ReaderWriterLockSlim syncLock = new ReaderWriterLockSlim();

        #endregion

        #region Properties

        public Expression<Func<TModel, TValue>> Expression { get; private set; }
        public ModelMetadata Metadata { get; private set; }
        public Type MemberType { get; set; }

        #endregion

        #region Constructor

        public BoundColumn(Grid<TModel> grid, Expression<Func<TModel, TValue>> expression)
            : base (grid, ColumnType.Bound)
        {
            this.Expression = expression;

            this.Member = ExpressionHelper.GetExpressionText(expression);

            this.MemberType = ((LambdaExpression)expression).ReturnType;

            string key = expression.ToString();

            this.Metadata = ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, new ViewDataDictionary<TModel>());
            this.MemberType = this.Metadata.ModelType;
            this.Format = this.Metadata.DisplayFormatString;
            this.Hidden = !this.Metadata.ShowForDisplay;
        }

        #endregion
    }
}
