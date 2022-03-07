using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Data.MsSqlServer
{
    internal static class FilterProcessor
    {
        internal static Expression<Func<T, bool>> ExpressionFromFilters<T>(List<Filter> filters)
        {
            var expr = ExpressionFromFilter<T>(filters.First());

            foreach(var filter in filters.Skip(1))
            {
                switch (filter.FilterOp)
                {
                    case FilterOp.And:
                        expr = AndExpression(expr, ExpressionFromFilter<T>(filter));
                        break;
                    case FilterOp.Or:
                        expr = OrExpression(expr, ExpressionFromFilter<T>(filter));
                        break;
                    default:
                        break;
                }
            }

            return expr;
        }

        internal static Expression<Func<T, bool>> ExpressionFromFilter<T>(Filter filter)
        {
            var propInfo = typeof(T).GetProperty(filter.PropName);

            ParameterExpression param = Expression.Parameter(typeof(T), "prop");
            ConstantExpression value = Expression.Constant(Convert.ChangeType(filter.Value, propInfo.PropertyType), propInfo.PropertyType);
            Expression getProperty = Expression.Property(param, propInfo);
            Expression operation = MapOperations[filter.Operation](getProperty, value);
            Expression<Func<T, bool>> lambda =
                Expression.Lambda<Func<T, bool>>(
                    operation,
                    new ParameterExpression[] { param });

            return lambda;
        }

        private static Expression<Func<TEntity, bool>> AndExpression<TEntity>(
            Expression<Func<TEntity, bool>> left, Expression<Func<TEntity, bool>> right)
        {
            var visitor = new ParameterReplaceVisitor()
            {
                Target = right.Parameters[0],
                Replacement = left.Parameters[0],
            };

            var rewrittenRight = visitor.Visit(right.Body);
            var andExpression = Expression.AndAlso(left.Body, rewrittenRight);
            return Expression.Lambda<Func<TEntity, bool>>(andExpression, left.Parameters);
        }

        private static Expression<Func<TEntity, bool>> OrExpression<TEntity>(
            Expression<Func<TEntity, bool>> left, Expression<Func<TEntity, bool>> right)
        {
            var visitor = new ParameterReplaceVisitor()
            {
                Target = right.Parameters[0],
                Replacement = left.Parameters[0],
            };

            var rewrittenRight = visitor.Visit(right.Body);
            var andExpression = Expression.OrElse(left.Body, rewrittenRight);
            return Expression.Lambda<Func<TEntity, bool>>(andExpression, left.Parameters);
        }

        private static Dictionary<Operations, Func<Expression, Expression, Expression>> MapOperations = 
            new Dictionary<Operations, Func<Expression, Expression, Expression>>
        {
            [Operations.Eq] = Expression.Equal,
            [Operations.EqString] = Expression.Equal,
            [Operations.NotEq] = Expression.NotEqual,
            [Operations.NotEqString] = Expression.NotEqual,
            [Operations.EqOrGreater] = Expression.GreaterThanOrEqual,
            [Operations.EqOrLess] = Expression.LessThanOrEqual,
            [Operations.Greater] = Expression.GreaterThan,
            [Operations.Less] = Expression.LessThan,
            [Operations.Like] = (left, right) => Expression.Call(left, typeof(string).GetMethod(nameof(string.Contains)), right),
            [Operations.StartWith] = (left, right) => Expression.Call(left, typeof(string).GetMethod(nameof(string.StartsWith)), right),
            [Operations.EndWith] = (left, right) => Expression.Call(left, typeof(string).GetMethod(nameof(string.EndsWith)), right)
        };

        private class ParameterReplaceVisitor : ExpressionVisitor
        {
            public ParameterExpression Target { get; set; }
            public ParameterExpression Replacement { get; set; }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == Target ? Replacement : base.VisitParameter(node);
            }
        }
    }
}
