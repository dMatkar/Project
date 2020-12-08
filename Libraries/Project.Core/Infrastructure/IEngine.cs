using Project.Core.Infrastructure.DependencyMangement;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Project.Core.Infrastructure
{
    public interface IEngine
    {
        /// <summary>
        /// Get container.
        /// </summary>
        ContainerManager Container { get; }

        /// <summary>
        /// Setup all infrastructure services.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Resolve.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();

        /// <summary>
        /// Resolve.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Resolve(Type type);

        /// <summary>
        /// Resolve All.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> ResolveAll<T>();
    }
}
