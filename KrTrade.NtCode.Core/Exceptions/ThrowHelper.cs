﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KrTrade.Nt.Core.Exceptions
{
    public static class ThrowHelper
    {

        public static void ThrowWrongKeyTypeArgumentException(object key, Type targetType)
        {
            throw new ArgumentException(); // SR.GetString("Arg_WrongType", key, targetType), "key");
        }

        public static void ThrowWrongValueTypeArgumentException(object value, Type targetType)
        {
            throw new ArgumentException(); // SR.GetString("Arg_WrongType", value, targetType), "value");
        }

        public static void ThrowKeyNotFoundException()
        {
            throw new KeyNotFoundException();
        }

        public static void ThrowArgumentException(ExceptionResource resource)
        {
            throw new ArgumentException(); //SR.GetString(GetResourceName(resource)));
        }

        public static void ThrowArgumentNullException(ExceptionArgument argument)
        {
            throw new ArgumentNullException(GetArgumentName(argument));
        }

        public static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
        {
            throw new ArgumentOutOfRangeException(GetArgumentName(argument));
        }

        public static void ThrowObjectDisposedException()
        {
            throw new ObjectDisposedException(string.Empty);
        }

        public static void ThrowArgumentOutOfRangeException(ExceptionArgument argument, ExceptionResource resource)
        {
            throw new ArgumentOutOfRangeException(); // GetArgumentName(argument), SR.GetString(GetResourceName(resource)));
        }

        public static void ThrowInvalidOperationException(ExceptionResource resource)
        {
            throw new InvalidOperationException(); // SR.GetString(GetResourceName(resource)));
        }

        public static void ThrowSerializationException(ExceptionResource resource)
        {
            throw new SerializationException(); // SR.GetString(GetResourceName(resource)));
        }

        public static void ThrowNotSupportedException(ExceptionResource resource)
        {
            throw new NotSupportedException(); // SR.GetString(GetResourceName(resource)));
        }

        public static void IfNullAndNullsAreIllegalThenThrow<T>(object value, ExceptionArgument argName)
        {
            if (value == null && default(T) != null)
            {
                ThrowArgumentNullException(argName);
            }
        }

        public static string GetArgumentName(ExceptionArgument argument)
        {
            switch (argument)
            {

                case ExceptionArgument.array: return "array";
                case ExceptionArgument.arrayIndex: return "arrayIndex";
                case ExceptionArgument.capacity: return "capacity";
                case ExceptionArgument.collection: return "collection";
                case ExceptionArgument.converter: return "converter";
                case ExceptionArgument.count: return "count";
                case ExceptionArgument.dictionary: return "dictionary";
                case ExceptionArgument.index: return "index";
                case ExceptionArgument.info: return "info";
                case ExceptionArgument.key: return "key";
                case ExceptionArgument.match: return "match";
                case ExceptionArgument.obj: return "obj";
                case ExceptionArgument.queue: return "queue";
                case ExceptionArgument.stack: return "stack";
                case ExceptionArgument.startIndex: return "startIndex";
                case ExceptionArgument.value: return "value";
                case ExceptionArgument.item: return "item";
                default: return string.Empty;
            }
        }

        public static string GetResourceName(ExceptionResource resource)
        {
            switch (resource)
            {

                case ExceptionResource.Argument_ImplementIComparable: return "Argument_ImplementIComparable";
                case ExceptionResource.Argument_AddingDuplicate: return "Argument_AddingDuplicate";
                case ExceptionResource.ArgumentOutOfRange_Index: return "ArgumentOutOfRange_Index";
                case ExceptionResource.ArgumentOutOfRange_NeedNonNegNum: return "ArgumentOutOfRange_NeedNonNegNum";
                case ExceptionResource.ArgumentOutOfRange_NeedNonNegNumRequired: return "ArgumentOutOfRange_NeedNonNegNumRequired";
                case ExceptionResource.ArgumentOutOfRange_SmallCapacity: return "ArgumentOutOfRange_SmallCapacity";
                case ExceptionResource.Arg_ArrayPlusOffTooSmall: return "Arg_ArrayPlusOffTooSmall";
                case ExceptionResource.Arg_RankMultiDimNotSupported: return "Arg_MultiRank";
                case ExceptionResource.Arg_NonZeroLowerBound: return "Arg_NonZeroLowerBound";
                case ExceptionResource.Argument_InvalidArrayType: return "Invalid_Array_Type";
                case ExceptionResource.Argument_InvalidOffLen: return "Argument_InvalidOffLen";
                case ExceptionResource.InvalidOperation_CannotRemoveFromStackOrQueue: return "InvalidOperation_CannotRemoveFromStackOrQueue";
                case ExceptionResource.InvalidOperation_EmptyCollection: return "InvalidOperation_EmptyCollection";
                case ExceptionResource.InvalidOperation_EmptyQueue: return "InvalidOperation_EmptyQueue";
                case ExceptionResource.InvalidOperation_EnumOpCantHappen: return "InvalidOperation_EnumOpCantHappen";
                case ExceptionResource.InvalidOperation_EnumFailedVersion: return "InvalidOperation_EnumFailedVersion";
                case ExceptionResource.InvalidOperation_EmptyStack: return "InvalidOperation_EmptyStack";
                case ExceptionResource.InvalidOperation_EnumNotStarted: return "InvalidOperation_EnumNotStarted";
                case ExceptionResource.InvalidOperation_EnumEnded: return "InvalidOperation_EnumEnded";
                case ExceptionResource.NotSupported_KeyCollectionSet: return "NotSupported_KeyCollectionSet";
                case ExceptionResource.NotSupported_SortedListNestedWrite: return "NotSupported_SortedListNestedWrite";
                case ExceptionResource.Serialization_InvalidOnDeser: return "Serialization_InvalidOnDeser";
                case ExceptionResource.Serialization_MissingValues: return "Serialization_MissingValues";
                case ExceptionResource.Serialization_MismatchedCount: return "Serialization_MismatchedCount";
                case ExceptionResource.NotSupported_ValueCollectionSet: return "NotSupported_ValueCollectionSet";
                default: return String.Empty;
            }
        }
    }
}
