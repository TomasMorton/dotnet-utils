using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyInjection.CSharp.Tests
{
    public static class ControllerFinder
    {
        public static List<Type> FindControllers(Type baseInterface)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(SafelyGetTypes)
                .Where(x => InheritsInterface(x, baseInterface))
                .ToList();
        }

        /// <summary>
        /// Fix for https://github.com/microsoft/vstest/issues/2008
        /// </summary>
        private static IEnumerable<Type> SafelyGetTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Enumerable.Empty<Type>();
            }
        }

        private static bool InheritsInterface(Type typeToCheck, Type baseInterface)
        {
            return baseInterface.IsAssignableFrom(typeToCheck) && CanBeInstantiated(typeToCheck);
        }

        private static bool CanBeInstantiated(Type typeToCheck)
        {
            return typeToCheck.IsClass && !typeToCheck.IsAbstract;
        }
    }
}