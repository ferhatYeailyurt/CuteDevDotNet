using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace CuteDev
{
    public static class ExpressionBuilder
    {
        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private static MethodInfo startsWithMethod =
        typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static MethodInfo endsWithMethod =
        typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });


        public static Expression<Func<T, bool>> GetExpression<T>(IList<Filter> filters)
        {
            if (filters.Count == 0)
                return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            if (filters.Count == 1)
                exp = GetExpression<T>(param, filters[0]);
            else if (filters.Count == 2)
                exp = GetExpression<T>(param, filters[0], filters[1]);
            else
            {
                while (filters.Count > 0)
                {
                    var f1 = filters[0];
                    var f2 = filters[1];

                    if (exp == null)
                        exp = GetExpression<T>(param, filters[0], filters[1]);
                    else
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));

                    filters.Remove(f1);
                    filters.Remove(f2);

                    if (filters.Count == 1)
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression<T>(ParameterExpression param, Filter filter)
        {
            MemberExpression member = Expression.Property(param, filter.PropertyName);
            if (filter.Value.GetType() != member.Type)
                filter.Value = TryTypeConvert(filter.Value, member);

            Expression constant = Expression.Constant(filter.Value);

            if (IsNullableType(member.Type) && !IsNullableType(constant.Type))
                constant = Expression.Convert(constant, member.Type);

            switch (filter.Operation)
            {
                case Op.Equals:
                    return Expression.Equal(member, constant);

                case Op.NotEquals:
                    return Expression.NotEqual(member, constant);

                case Op.GreaterThan:
                    return Expression.GreaterThan(member, constant);

                case Op.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);

                case Op.LessThan:
                    return Expression.LessThan(member, constant);

                case Op.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, constant);

                case Op.Contains:
                    return Expression.Call(member, containsMethod, constant);

                case Op.NotContains:
                    return Expression.Not(Expression.Call(member, containsMethod, constant));

                case Op.StartsWith:
                    return Expression.Call(member, startsWithMethod, constant);

                case Op.EndsWith:
                    return Expression.Call(member, endsWithMethod, constant);
            }

            return null;
        }

        static void MyGreaterThan(Expression e1, Expression e2)
        {
            if (IsNullableType(e1.Type) && !IsNullableType(e2.Type))
                e2 = Expression.Convert(e2, e1.Type);
            else if (!IsNullableType(e1.Type) && IsNullableType(e2.Type))
                e1 = Expression.Convert(e1, e2.Type);
            //return Expression.GreaterThan(e1, e2);
        }
        static bool IsNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        private static object TryTypeConvert(object p, MemberExpression member)
        {
            if (member.Type == typeof(Boolean))
            {
                Boolean result = new Boolean();
                result = (p.ToString() == "True");
                return result;
            }

            if (member.Type == typeof(Decimal?) && p.GetType() == typeof(string))
            {
                Decimal outValue;
                return Decimal.TryParse(p.ToString(), out outValue) ? (Decimal?)outValue : null;
            }

            if (member.Type == typeof(Decimal) && p.GetType() == typeof(string))
            {
                return Decimal.Parse(p.ToString());
            }

            if (member.Type == typeof(Int32) && p.GetType() == typeof(string))
            {
                return Int32.Parse(p.ToString());
            }

            if (member.Type == typeof(Int32?) && p.GetType() == typeof(string))
            {
                int outValue;
                return int.TryParse(p.ToString(), out outValue) ? (int?)outValue : null;
            }

            if (member.Type == typeof(Int64) && p.GetType() == typeof(string))
            {
                return Int64.Parse(p.ToString());
            }

            if (member.Type == typeof(Byte) && p.GetType() == typeof(string))
            {
                return Byte.Parse(p.ToString());
            }

            if (member.Type == typeof(DateTime?) && p.GetType() == typeof(string))
            {
                DateTime outValue;
                return DateTime.TryParse(p.ToString(), out outValue) ? (DateTime?)outValue : null;
            }

            if (member.Type == typeof(DateTime) && p.GetType() == typeof(string))
            {
                return DateTime.Parse(p.ToString());
            }
            
            return p;
        }

        private static BinaryExpression GetExpression<T>
        (ParameterExpression param, Filter filter1, Filter filter2)
        {
            Expression bin1 = GetExpression<T>(param, filter1);
            Expression bin2 = GetExpression<T>(param, filter2);

            return Expression.AndAlso(bin1, bin2);
        }
    }


    public class Filter
    {
        public string PropertyName { get; set; }
        public Op Operation { get; set; }
        public object Value { get; set; }
    }

    public enum Op
    {
        Equals,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith,
        NotEquals,
        NotContains
    }
}
