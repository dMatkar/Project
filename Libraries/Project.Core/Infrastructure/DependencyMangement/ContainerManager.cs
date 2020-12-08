using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Integration.Mvc;
using Project.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Web;

namespace Project.Core.Infrastructure.DependencyMangement
{
    public class ContainerManager
    {
        #region Fields

        private readonly IContainer _container;

        #endregion

        #region Properties

        public virtual IContainer Container
        {
            get => _container;
        }

        #endregion

        #region Constructor

        public ContainerManager(IContainer container)
        {
            _container = container;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resolve.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public virtual T Resolve<T>(string key = "", ILifetimeScope scope = null)
        {
            scope = scope ?? GetScope();
            if (string.IsNullOrWhiteSpace(key))
            {
                return scope.Resolve<T>();
            }
            return scope.ResolveKeyed<T>(key);
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <param name="type"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public virtual object Resolve(Type type, ILifetimeScope scope = null)
        {
            scope = scope ?? GetScope();
            return scope.Resolve(type);
        }

        /// <summary>
        /// Get All.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll<T>(string key, ILifetimeScope scope)
        {
            scope = scope ?? GetScope();
            if (string.IsNullOrWhiteSpace(key))
            {
                return scope.ResolveKeyed<IEnumerable<T>>(key);
            }
            return scope.Resolve<IEnumerable<T>>();
        }

        /// <summary>
        /// Try resolve type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scope"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual bool TryResolve<T>(ILifetimeScope scope, out T instance) where T : class
        {
            scope = scope ?? GetScope();
            return scope.TryResolve(out instance);
        }

        /// <summary>
        /// Check type is registred or not.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scope"></param>
        /// <returns></returns>
        public virtual bool IsRegistred<T>(ILifetimeScope scope)
        {
            scope = scope ?? GetScope();
            return scope.IsRegistered<T>();
        }

        /// <summary>
        /// Get Scope
        /// </summary>
        /// <returns></returns>
        public virtual ILifetimeScope GetScope()
        {
            try
            {
                if (HttpContext.Current != null)
                    return AutofacDependencyResolver.Current.RequestLifetimeScope;

                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
            catch
            {
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
        }

        #endregion
    }
}
