using Scellecs.Morpeh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slimebones.ECSCore {
    /// <summary>
    /// Represents Filter returning special combination of Components which
    /// define some logical object.
    /// </summary>
    /// <example>
    /// For CargoCategory, it would be a Filter returning Entities with
    /// Components `Price` and `SoldOnFinish`.
    /// </example>
    public interface ICategory: IFilterExtension {
        public bool In(Entity entity);
    }
}
