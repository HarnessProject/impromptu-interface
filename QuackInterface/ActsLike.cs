﻿

namespace ImpromptuInterface
{
    using System;

    /// <summary>
    /// This interface can be used on your custom dynamic objects if you want impromptu interfaces
    /// </summary>
    public interface IActsLike
    {
        TInterface ActsLike<TInterface>(params Type[] otherInterfaces);
    }

    public static class ObjectExtensions
    {
        /// <summary>
        /// Wraps an existing object with an Explicit interface definition
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="originalDynamic">The original object can be annoymous type, System.DynamicObject as well as any others.</param>
        /// <param name="otherInterfaces">The other interfaces.</param>
        /// <returns></returns>
        public static TInterface ActsLike<TInterface>(this Object originalDynamic, params Type[]otherInterfaces)where TInterface:class
        {
            var tType = originalDynamic.GetType();

            var tProxy = BuildProxy.BuildType(tType,typeof(TInterface), otherInterfaces);

              return (TInterface)Activator.CreateInstance(tProxy, originalDynamic);
        }
    }

    public static class Dynamic
    {
        /// <summary>
        /// When using this method you must explicitly type the result otherwise the compiler will infer it as dynamic.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="originalDynamic">The original dynamic object.</param>
        /// <param name="otherInterfaces">The other interfaces.</param>
        /// <returns></returns>
        public static TInterface ActsLike<TInterface>(dynamic originalDynamic, params Type[] otherInterfaces) where TInterface : class
        {
            var tType = originalDynamic.GetType();

            var tProxy = BuildProxy.BuildType(tType, typeof(TInterface), otherInterfaces);

            return (TInterface)Activator.CreateInstance(tProxy, (object)originalDynamic);
        }
    }
}
