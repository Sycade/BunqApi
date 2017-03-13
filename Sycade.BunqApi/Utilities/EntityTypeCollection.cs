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
            _entityTypes = (from t in Assembly.GetExecutingAssembly().GetTypes()
                            where typeof(BunqEntity).IsAssignableFrom(t)
                            select t).ToDictionary(t => t.Name, t => t);
        }

        public static Type FindByName(string name)
        {
            if (_entityTypes.ContainsKey(name))
                return _entityTypes[name];

            return null;
        }
    }
}
