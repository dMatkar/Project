using System;
using System.Collections.Generic;
using System.Reflection;

namespace Project.Core.Infrastructure
{
    /*
      1] When i want to find types in assemblies there two main task that should completed :
          a] I need to get asseblies.
          b]  Validate if i get the correct types from that assemblies.
     */

    public interface ITypeFinder
    {
        IEnumerable<Assembly> GetAssemblies();
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreateClasses = true);
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreateClasses = true);
        IEnumerable<Type> FindClassesOfType<T>(bool onlyConstreateClasses = true);
        IEnumerable<Type> FindClassesType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreateClasses = true);
    }
}
