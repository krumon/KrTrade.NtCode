using NinjaTrader.NinjaScript;
using System;
using System.Reflection;

namespace KrTrade.Nt.Core.Helpers
{
    public static class ThrowHelper
    {
        /// <summary>
        /// Throw exceptions when any service is executed when the state is not valid.
        /// </summary>
        /// <param name="invalidState">The current state that is not valid.</param>
        public static void ThrowOnBarUpdateInvalidStateException(State invalidState) 
            => ThrowInvalidStateException(MethodBase.GetCurrentMethod().DeclaringType.Name, invalidState, State.Historical, State.Transition, State.Realtime);

        /// <summary>
        /// Throw exceptions when any service is executed when the state is not valid.
        /// </summary>
        /// <param name="serviceName">The service that has been executed.</param>
        /// <param name="invalidState">The current state that is not valid.</param>
        public static void ThrowInvalidStateException(string serviceName, State invalidState) 
            => ThrowInvalidStateException(serviceName, invalidState, null);

        /// <summary>
        /// Throw exceptions when any service is executed when the state is not valid.
        /// </summary>
        /// <param name="serviceName">The service that has been executed.</param>
        /// <param name="invalidState">The current state that is invalid.</param>
        /// <param name="validStates">The valid states. The service must be executed when in any of this states.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void ThrowInvalidStateException(string serviceName, State invalidState, params State[] validStates)
        {
            string validSatesString = string.Empty;
            if (validStates != null && validStates.Length > 0)
                for (int i = 0; i < validStates.Length; i++)
                {
                    validSatesString += validStates[i].ToString();
                    if (i !=  validStates.Length - 1)
                        validSatesString += ", " ;
                }

            string errorString;
            if (validSatesString != string.Empty)
                errorString = $"The {serviceName.ToUpper()} state is invalid. The current state is {invalidState} and the state must be {validSatesString}.";
            else
                errorString = $"The {serviceName.ToUpper()} state is invalid. The cannot be {invalidState}.";

            throw new InvalidOperationException(errorString);
        }

        //public static string GetArgumentName(ExceptionArgument argument)
        //{
        //    switch (argument)
        //    {

        //        case ExceptionArgument.array: return "array";
        //        case ExceptionArgument.arrayIndex: return "arrayIndex";
        //        case ExceptionArgument.capacity: return "capacity";
        //        case ExceptionArgument.collection: return "collection";
        //        case ExceptionArgument.converter: return "converter";
        //        case ExceptionArgument.count: return "count";
        //        case ExceptionArgument.dictionary: return "dictionary";
        //        case ExceptionArgument.index: return "index";
        //        case ExceptionArgument.info: return "info";
        //        case ExceptionArgument.key: return "key";
        //        case ExceptionArgument.match: return "match";
        //        case ExceptionArgument.obj: return "obj";
        //        case ExceptionArgument.queue: return "queue";
        //        case ExceptionArgument.stack: return "stack";
        //        case ExceptionArgument.startIndex: return "startIndex";
        //        case ExceptionArgument.value: return "value";
        //        case ExceptionArgument.item: return "item";
        //        default: return string.Empty;
        //    }
        //}

        //public static string GetResourceName(ExceptionResource resource)
        //{
        //    switch (resource)
        //    {

        //        case ExceptionResource.Argument_ImplementIComparable: return "Argument_ImplementIComparable";
        //        case ExceptionResource.Argument_AddingDuplicate: return "Argument_AddingDuplicate";
        //        case ExceptionResource.ArgumentOutOfRange_Index: return "ArgumentOutOfRange_Index";
        //        case ExceptionResource.ArgumentOutOfRange_NeedNonNegNum: return "ArgumentOutOfRange_NeedNonNegNum";
        //        case ExceptionResource.ArgumentOutOfRange_NeedNonNegNumRequired: return "ArgumentOutOfRange_NeedNonNegNumRequired";
        //        case ExceptionResource.ArgumentOutOfRange_SmallCapacity: return "ArgumentOutOfRange_SmallCapacity";
        //        case ExceptionResource.Arg_ArrayPlusOffTooSmall: return "Arg_ArrayPlusOffTooSmall";
        //        case ExceptionResource.Arg_RankMultiDimNotSupported: return "Arg_MultiRank";
        //        case ExceptionResource.Arg_NonZeroLowerBound: return "Arg_NonZeroLowerBound";
        //        case ExceptionResource.Argument_InvalidArrayType: return "Invalid_Array_Type";
        //        case ExceptionResource.Argument_InvalidOffLen: return "Argument_InvalidOffLen";
        //        case ExceptionResource.InvalidOperation_CannotRemoveFromStackOrQueue: return "InvalidOperation_CannotRemoveFromStackOrQueue";
        //        case ExceptionResource.InvalidOperation_EmptyCollection: return "InvalidOperation_EmptyCollection";
        //        case ExceptionResource.InvalidOperation_EmptyQueue: return "InvalidOperation_EmptyQueue";
        //        case ExceptionResource.InvalidOperation_EnumOpCantHappen: return "InvalidOperation_EnumOpCantHappen";
        //        case ExceptionResource.InvalidOperation_EnumFailedVersion: return "InvalidOperation_EnumFailedVersion";
        //        case ExceptionResource.InvalidOperation_EmptyStack: return "InvalidOperation_EmptyStack";
        //        case ExceptionResource.InvalidOperation_EnumNotStarted: return "InvalidOperation_EnumNotStarted";
        //        case ExceptionResource.InvalidOperation_EnumEnded: return "InvalidOperation_EnumEnded";
        //        case ExceptionResource.NotSupported_KeyCollectionSet: return "NotSupported_KeyCollectionSet";
        //        case ExceptionResource.NotSupported_SortedListNestedWrite: return "NotSupported_SortedListNestedWrite";
        //        case ExceptionResource.Serialization_InvalidOnDeser: return "Serialization_InvalidOnDeser";
        //        case ExceptionResource.Serialization_MissingValues: return "Serialization_MissingValues";
        //        case ExceptionResource.Serialization_MismatchedCount: return "Serialization_MismatchedCount";
        //        case ExceptionResource.NotSupported_ValueCollectionSet: return "NotSupported_ValueCollectionSet";
        //        default: return String.Empty;
        //    }
        //}
    }
}
