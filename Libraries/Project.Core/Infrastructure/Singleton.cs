using System;
using System.Collections.Generic;

namespace Project.Core.Infrastructure
{
    public class Singleton<T> : Singleton
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                return _instance;
            }
            set
            {
                _instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }

    public class SingletonList<T> : Singleton<IList<T>>
    {
        static SingletonList()
        {
            Singleton<IList<T>>.Instance = new List<T>();
        }

        public static new IList<T> Instance
        {
            get => Singleton<IList<T>>.Instance;
        }
    }

    public class SingletonDictionary<T> : Singleton<IDictionary<Type, object>>
    {
        static SingletonDictionary()
        {
            Singleton<IDictionary<Type, object>>.Instance = new Dictionary<Type, object>();
        }

        public static new IDictionary<Type, object> Instance
        {
            get => Singleton<IDictionary<Type, object>>.Instance;
        }
    }

    public class Singleton
    {
        private static readonly IDictionary<Type, object> _dictionary;

        static Singleton()
        {
            _dictionary = new Dictionary<Type, object>();
        }

        public static IDictionary<Type, object> AllSingletons
        {
            get => _dictionary;
        }
    }
}
