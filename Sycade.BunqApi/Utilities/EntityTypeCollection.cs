using Sycade.BunqApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sycade.BunqApi.Utilities
{
    public static class EntityTypeCollection
    {
        private static Dictionary<string, Type> _entityTypes;

        static EntityTypeCollection()
        {
            _entityTypes = Assembly.GetExecutingAssembly().GetTypes()
                                                          .Where(t => typeof(BunqEntity).IsAssignableFrom(t))
                                                          .ToDictionary(t => t.Name, t => t);
        }

        public static Type FindByName(string name)
        {
            if (_entityTypes.ContainsKey(name))
                return _entityTypes[name];

            return null;
        }
    }
}
