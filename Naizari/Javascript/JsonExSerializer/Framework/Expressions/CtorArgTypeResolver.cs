/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.ComponentModel;

namespace Naizari.Javascript.JsonExSerialization.Framework.Expressions
{
    public class CtorArgTypeResolver
    {
        private ComplexExpressionBase _expression;
        private Type[] _definedParameters;
        private SerializationContext _context;

        public CtorArgTypeResolver(ComplexExpressionBase Expression, SerializationContext Context, Type[] DefinedParameters)
        {
            if (Expression == null)
                throw new ArgumentNullException("Expression");
            _expression = Expression;
            _definedParameters = DefinedParameters ?? new Type[0];
            _context = Context;
        }

        public CtorArgTypeResolver(ComplexExpressionBase Expression, SerializationContext Context)
            : this(Expression, Context, new Type[0])
        {
        }

        public Type[] ResolveTypes()
        {
            List<ConstructorResult> constructors = GetConstructorList();
            constructors = FilterByArgCount(constructors);
            constructors.ForEach(ProcessConstructor);
            constructors.ForEach(ComputeScore);
            constructors.Sort();
            if (constructors.Count > 0)
                return constructors[0].GetFinalTypes();
            else
                throw new Exception("No suitable constructor found for " + _expression.ResultType);
        }

        private List<ConstructorResult> GetConstructorList()
        {
            List<ConstructorResult> ctors = new List<ConstructorResult>();
            foreach (ConstructorInfo ctorInfo in _expression.ResultType.GetConstructors())
                ctors.Add(new ConstructorResult(ctorInfo));
            return ctors;
        }

        private List<ConstructorResult> FilterByArgCount(List<ConstructorResult> ctors)
        {
            return ctors.FindAll(delegate(ConstructorResult ctor) { return ctor.types.Length == _expression.ConstructorArguments.Count; });
        }

        private void ProcessConstructor(ConstructorResult constructor)
        {
            ParameterInfo[] parms = constructor.constructor.GetParameters();

            for (int i = 0; i < constructor.types.Length; i++)
            {
                ParameterInfo parameter = parms[i];
                Expression value = _expression.ConstructorArguments[i];
                Type definedType = null;
                if (_definedParameters.Length > i)
                    definedType = _definedParameters[i];

                Type inferredType = null;
                if (value is ValueExpression)
                    inferredType = GetValueType(parameter, (ValueExpression)value);
                else if (value is ArrayExpression)
                {
                    if (_context.GetTypeHandler(parameter.ParameterType).IsCollection())
                        inferredType = parameter.ParameterType;
                }
                constructor.types[i] = GetBestMatch(parameter.ParameterType, definedType, inferredType);
            }
        }

        /// <summary>
        /// Returns the first type in the list that is compatible with TypeToMatch
        /// </summary>
        /// <param name="TypeToMatch"></param>
        /// <param name="TypesToCheck"></param>
        /// <returns></returns>
        private static Type GetBestMatch(Type TypeToMatch, params Type[] TypesToCheck)
        {
            foreach (Type checkType in TypesToCheck)
            {
                if (checkType == null)
                    continue;

                bool matched = IsTypeCompatible(TypeToMatch, checkType);
                // abstract types are not very useful for evaluating arguments so don't return them
                if (checkType.IsAbstract
                    || checkType.IsGenericTypeDefinition
                    || checkType.IsInterface)
                    matched = false;

                if (matched)
                {
                    return checkType;
                }
            }
            return null;
        }

        private static bool IsTypeCompatible(Type TypeToMatch, Type checkType)
        {
            bool matched = false;
            if (TypeToMatch == checkType)
                matched = true;
            else if (TypeToMatch.IsAssignableFrom(checkType))
                matched = true;

            return matched;
        }

        private static Type GetValueType(ParameterInfo parameter, ValueExpression value)
        {
            Type result = null;
            if (value is BooleanExpression)
                result = typeof(bool);
            else if (value is NumericExpression)
            {
                if (IsNumericType(parameter.ParameterType))
                    result = parameter.ParameterType;
                else if (((NumericExpression)value).IsFloatingPoint())
                    result = typeof(double);
                else
                    result = typeof(long);
            }
            else
            {
                // string
                result = typeof(string);
            }
            return result;
        }

        private static bool IsNumericType(Type t)
        {
            switch (Type.GetTypeCode(t))
            {
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                default:
                    return false;
            }
        }

        private void ComputeScore(ConstructorResult constructor)
        {
            ParameterInfo[] parms = constructor.constructor.GetParameters();
            // should have already been checked, but just double-check
            Debug.Assert(parms.Length == _expression.ConstructorArguments.Count, "Constructors with different argument counts should have been removed already");

            int score = 0;
            // check to make sure each arg type is compatible, if its set
            for (int i = 0; i < parms.Length; i++)
            {
                Type exprType = null;
                if (constructor.types[i] != null)
                    exprType = constructor.types[i];

                //+2 for each exact match
                if (exprType != null && parms[i].ParameterType == exprType)
                    score += 2;
                //+1 for each compatible match
                else if (exprType != null && parms[i].ParameterType.IsAssignableFrom(exprType))
                    score += 1;
                // make sure incompatible types are weeded out
                else if (exprType != null && !IsTypeCompatible(parms[i].ParameterType, exprType))
                    score -= parms.Length * 5;
            }
            constructor.score = score;
        }

        /// <summary>
        /// Data holder
        /// </summary>
        private class ConstructorResult : IComparable<ConstructorResult> {
            public int score;
            public ConstructorInfo constructor;
            public Type[] types;

            public ConstructorResult(ConstructorInfo ctor)
            {
                constructor = ctor;
                types = new Type[ctor.GetParameters().Length];
            }

            public Type[] GetFinalTypes()
            {
                ParameterInfo[] parms = constructor.GetParameters();
                for (int i = 0; i < parms.Length; i++)
                    types[i] = types[i] ?? parms[i].ParameterType;
                return types;
            }
            #region IComparable<ConstructorResult> Members

            public int CompareTo(ConstructorResult other)
            {
                return other.score - score;
            }

            #endregion
        }
    }
}
