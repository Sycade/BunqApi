using Sycade.BunqApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sycade.BunqApi.Utilities
{
    public static class ModelFinder
    {
        private static Dictionary<string, Type> _models { get; }

        public static Type FindByName(string name)
        {
            if (_models.ContainsKey(name))
                return _models[name];

            return null;
        }

        static ModelFinder()
        {
            var namespaceName = typeof(IBunqEntity).Namespace;

            _models = (from t in Assembly.GetExecutingAssembly().GetTypes()
                      where t.IsClass && t.Namespace == namespaceName
                      select t).ToDictionary(t => t.Name, t => t);
        }
    }
}
