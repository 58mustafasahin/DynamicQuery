using DynamicQueryWebAPI.Entities;
using System.Linq.Expressions;
using System.Reflection;

namespace DynamicQueryWebAPI.Builder
{
    public class QueryBuilder<T>
    {
        public Expression<Func<T, bool>>? Build(QueryBuilderDto queryBuilderDto)
        {
            var parameterExpression = Expression.Parameter(typeof(T), "person"); // can write any word instead of person
            var binaryExpressions = new List<Expression>();

            foreach (var query in queryBuilderDto.QueryProperties)
            {
                var constant = ConstantExpression(query.FieldType, query.Value);
                var memberExpression = Expression.Property(parameterExpression, query.FieldName);

                var comparisonExp = ComparisonExpression(memberExpression, constant, query.ComparisonOperator, query.FieldType);
                binaryExpressions.Add(comparisonExp);
            }

            var resultExpression = LogicalExpression(queryBuilderDto.LogicalOperator, binaryExpressions);
            return Expression.Lambda<Func<T, bool>>(resultExpression, parameterExpression);
        }

        public static Expression? ComparisonExpression(MemberExpression memberExpression, ConstantExpression constant, string comparisonOperator, string FieldType)
        {
            if (FieldType.ToLower() == "string")
            {
                MethodInfo methodToLower = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                var toLowerMethodExp = Expression.Call(memberExpression, methodToLower);
                MethodInfo methodContains = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var containsMethodExp = Expression.Call(toLowerMethodExp, methodContains, constant);
                return comparisonOperator == "!=" ? Expression.Not(containsMethodExp) : containsMethodExp;
            }
            return comparisonOperator switch
            {
                ">=" => Expression.GreaterThanOrEqual(memberExpression, constant),
                ">" => Expression.GreaterThan(memberExpression, constant),
                "<=" => Expression.LessThanOrEqual(memberExpression, constant),
                "<" => Expression.LessThan(memberExpression, constant),
                "=" => Expression.Equal(memberExpression, constant),
                "!=" => Expression.NotEqual(memberExpression, constant),
                _ => throw new NotImplementedException(),
            };
        }

        public static Expression? LogicalExpression(string logicalOperator, List<Expression> expressions)
        {
            return logicalOperator.ToLower() switch
            {
                "and" => AndExpression(expressions),
                "or" => OrExpression(expressions),
                _ => throw new NotImplementedException()
            };
        }

        public static ConstantExpression? ConstantExpression(string FieldType, string FieldValue)
        {
            return FieldType.ToLower() switch
            {
                "guid" => Expression.Constant(Guid.Parse(FieldValue)),
                "int" => Expression.Constant(int.Parse(FieldValue)),
                "double" => Expression.Constant(double.Parse(FieldValue)),
                "float" => Expression.Constant(float.Parse(FieldValue)),
                "datetime" => Expression.Constant(DateTime.Parse(FieldValue)),
                "string" => Expression.Constant(FieldValue.ToLower()),
                _ => throw new NotImplementedException()
            };
        }

        public static Expression? AndExpression(List<Expression> binaryExpressions)
        {
            Expression binaryExpression = binaryExpressions.First();
            if (binaryExpressions.Count <= 1) return binaryExpression;
            for (int i = 1; i < binaryExpressions.Count; i++)
            {
                binaryExpression = Expression.AndAlso(binaryExpression, binaryExpressions[i]);
            }
            return binaryExpression;
        }

        public static Expression? OrExpression(List<Expression> binaryExpressions)
        {
            Expression binaryExpression = binaryExpressions.First();
            if (binaryExpressions.Count <= 1) return binaryExpression;
            for (int i = 1; i < binaryExpressions.Count; i++)
            {
                binaryExpression = Expression.OrElse(binaryExpression, binaryExpressions[i]);
            }
            return binaryExpression;
        }
    }
}
