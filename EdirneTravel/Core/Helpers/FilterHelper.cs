using EdirneTravel.Models.Enums;
using EdirneTravel.Models.Utilities.Filtering;
using System.Linq.Expressions;

namespace EdirneTravel.Core.Helpers
{
    public class FilterHelper<T>
    {
        public static IQueryable<T> ApplyOrderBy(IQueryable<T> query, string property, OrderTypeEnum orderType)
        {
            var orderByExpression = GetOrderByExpression(property);
            switch (orderType)
            {
                case OrderTypeEnum.ASC:
                    return query.OrderBy(orderByExpression);
                case OrderTypeEnum.DESC:
                    return query.OrderByDescending(orderByExpression);
                default:
                    return query;
            }
        }

        public static Expression<Func<T, object>> GetOrderByExpression(string property)
        {
            var parameter = Expression.Parameter(typeof(T), "p");
            var propertyExpression = Expression.Property(parameter, property);
            var conversionExpression = Expression.Convert(propertyExpression, typeof(object));
            return Expression.Lambda<Func<T, object>>(conversionExpression, parameter);
        }

        public static Expression<Func<T, bool>> BuildFilterExpression<T>(Filter filter)
        {

            Expression<Func<T, bool>> filterExpression = null;
            var parameterExpression = Expression.Parameter(typeof(T), "p");

            switch (filter.Property)
            {
                case string propertyName when typeof(T).GetProperty(propertyName) != null:
                    var propertyInfo = typeof(T).GetProperty(propertyName);
                    var propertyExpression = Expression.Property(parameterExpression, propertyName);

                    if (propertyInfo.PropertyType.IsEnum)
                    {
                        var enumValue = Enum.Parse(propertyInfo.PropertyType, filter.Value.ToString());
                        filterExpression = Expression.Lambda<Func<T, bool>>(
                            Expression.Equal(propertyExpression, Expression.Constant(enumValue)),
                            parameterExpression);
                    }
                    else
                    {
                        switch (propertyInfo.PropertyType.ToString())
                        {
                            case "System.Byte":
                                filterExpression = Expression.Lambda<Func<T, bool>>(
                                   Expression.Equal(propertyExpression, Expression.Convert(Expression.Constant(Convert.ToSByte(filter.Value)), typeof(int))),
                                   parameterExpression);
                                break;
                            case "System.Int16":
                                filterExpression = Expression.Lambda<Func<T, bool>>(
                                   Expression.Equal(propertyExpression, Expression.Convert(Expression.Constant(Convert.ToInt16(filter.Value)), typeof(int))),
                                   parameterExpression);
                                break;
                            case "System.Int32":
                                filterExpression = Expression.Lambda<Func<T, bool>>(
                                   Expression.Equal(propertyExpression, Expression.Convert(Expression.Constant(Convert.ToInt32(filter.Value)), typeof(int))),
                                   parameterExpression);
                                break;
                            case "System.Int64":
                                filterExpression = Expression.Lambda<Func<T, bool>>(
                                    Expression.Equal(propertyExpression, Expression.Convert(Expression.Constant(Convert.ToInt64(filter.Value)), typeof(int))),
                                    parameterExpression);
                                break;
                            case "System.Double":
                                filterExpression = Expression.Lambda<Func<T, bool>>(
                                   Expression.Equal(propertyExpression, Expression.Convert(Expression.Constant(Convert.ToDouble(filter.Value)), typeof(double))),
                                   parameterExpression);
                                break;

                            case "System.Nullable`1[System.Int32]":
                                filterExpression = Expression.Lambda<Func<T, bool>>(
                                    Expression.Equal(propertyExpression, Expression.Convert(Expression.Constant(Convert.ToInt64(filter.Value)), typeof(int?))),
                                    parameterExpression);
                                break;
                            case "System.Decimal":
                                filterExpression = Expression.Lambda<Func<T, bool>>(
                                    Expression.Equal(propertyExpression, Expression.Convert(Expression.Constant(Convert.ToDecimal(filter.Value)), typeof(double))),
                                    parameterExpression);
                                break;

                            case "System.Boolean":
                                filterExpression = Expression.Lambda<Func<T, bool>>(
                                    Expression.Equal(propertyExpression, Expression.Constant(Convert.ToBoolean(filter.Value))),
                                    parameterExpression);
                                break;

                            case "System.Nullable`1[System.Boolean]":
                                filterExpression = Expression.Lambda<Func<T, bool>>(
                                    Expression.Equal(propertyExpression, Expression.Convert(Expression.Constant(Convert.ToBoolean(filter.Value)), typeof(bool?))),
                                    parameterExpression);
                                break;

                            case "System.DateTime":
                                filterExpression = Expression.Lambda<Func<T, bool>>(
                                    Expression.Equal(propertyExpression, Expression.Convert(Expression.Constant(Convert.ToDateTime(filter.Value)), typeof(DateTime))),
                                    parameterExpression);
                                break;

                            case "System.String":
                                filterExpression = Expression.Lambda<Func<T, bool>>(
                                    Expression.Equal(propertyExpression, Expression.Convert(Expression.Constant(filter.Value), typeof(string))),
                                    parameterExpression);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
            }

            return filterExpression;
        }
    }
}
