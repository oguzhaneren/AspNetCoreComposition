using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Shared.Mvc
{
    public class AssemblyBasedControllerFeatureProvider
       : ControllerFeatureProvider
    {
        private readonly Assembly[] _assemblies;

        public AssemblyBasedControllerFeatureProvider(Assembly[] assemblies)
        {
            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));
            _assemblies = assemblies;
        }

        protected override bool IsController(TypeInfo typeInfo)
        {
            if (_assemblies.Contains(typeInfo.Assembly))
            {
                return base.IsController(typeInfo);
            }

            return false;
        }
    }
}
