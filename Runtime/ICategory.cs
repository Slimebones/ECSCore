using Scellecs.Morpeh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetPants.ClumsyDelivery.Core {
    /// <summary>
    /// Represents Filter returning special combination of Components which
    /// define some logical object.
    /// </summary>
    /// <example>
    /// For CargoCategory, it would be a Filter returning Entities with
    /// Components `Price` and `SoldOnFinish`.
    /// </example>
    public interface ICategory {
        public Filter Get(World world);
    }
}
