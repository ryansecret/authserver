﻿#region

using System;
using System.Collections.Generic;

#endregion

namespace Ets.OAuthServer.Core.Infrastructure
{
    /// <summary>
    ///     A statically compiled "singleton" used to store objects throughout the
    ///     lifetime of the app domain. Not so much singleton in the pattern's
    ///     sense of the word as a standardized way to store single instances.
    /// </summary>
    /// <typeparam name="T">The type of object to store.</typeparam>
    /// <remarks>Access to the instance is not synchrnoized.</remarks>
    public class Singleton<T> : Singleton
    {
        private static T instance;

        /// <summary>
        ///     The singleton instance for the specified type T. Only one instance (at the time) of this object for each type
        ///     of T.
        /// </summary>
        public static T Instance
        {
            get { return instance; }
            set
            {
                instance = value;
                AllSingletons[typeof (T)] = value;
            }
        }
    }

    /// <summary>
    ///     Provides access to all "singletons" stored by <see cref="Singleton{T}" />.
    /// </summary>
    public class Singleton
    {
        private static readonly IDictionary<Type, object> allSingletons;

        static Singleton()
        {
            allSingletons = new Dictionary<Type, object>();
        }

        /// <summary>Dictionary of type to singleton instances.</summary>
        public static IDictionary<Type, object> AllSingletons
        {
            get { return allSingletons; }
        }
    }
}