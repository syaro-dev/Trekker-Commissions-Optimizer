using CommissionsOptimizerLib.Core.Enums;

namespace CommissionsOptimizerLib.Core.Models;

public sealed class OptimizerOptions
{
    /// <summary>
    /// Should all commissions focus on the specified materials?<br/>
    /// true = All commissions have to reward one of the specified materials, even if it sacrifices overall efficiency.<br/>
    /// false = Only one commission of each reward type, and the rest has to be the most efficient commissions.
    /// </summary>
    public required bool HyperFocus { get; set; }
    // code: false sets priority to be only used once per material type,
    // true keeps each priority so multiple commissions of that material type
    // will yield the same priority

    /// <summary>
    /// Group of materials and priority.
    /// </summary>
    public required List<MaterialPrioritySetting> MaterialsSelect { get; set; }

    /// <summary>
    /// Tyrant level - or Player Auth Level.<br/>
    /// Don't confuse this with Trekker Level!
    /// </summary>
    public required int TyrantLevel { get; set; }

    public class Builder
    {
        private bool hyperFocus = false;
        private int tyrantLevel = 40;
        private readonly List<MaterialPrioritySetting> materialsSelect = new();

        /// <summary>
        /// Should all commissions focus on the specified materials?<br/>
        /// true = All commissions have to reward one of the specified materials, even if it sacrifices overall efficiency.<br/>
        /// false = Only one commission of each reward type, and the rest has to be the most efficient commissions.
        /// </summary>
        public Builder SetHyperFocus(bool hyperFocus)
        {
            this.hyperFocus = hyperFocus;
            return this;
        }

        /// <summary>
        /// Sets the Tyrant level - or Player Auth Level.<br/>
        /// Don't confuse this with Trekker Level!
        /// </summary>
        public Builder SetTyrantLevel(int tyrantLevel)
        {
            this.tyrantLevel = tyrantLevel;
            return this;
        }

        /// <summary>
        /// Adds a new material to the priority list, or update the priority of this material.
        /// </summary>
        public Builder AddOrSetMaterialPriority(RewardType material, int priority)
        {
            var existing = materialsSelect.Find(x => x.Material == material);
            if (existing != null)
            {
                existing.Priority = priority;
            }
            else
            {
                materialsSelect.Add(new MaterialPrioritySetting() { Material = material, Priority = priority });
            }
            return this;
        }

        /// <summary>
        /// Creates an OptimizerOptions instance.
        /// </summary>
        public OptimizerOptions Build()
        {
            return new OptimizerOptions()
            {
                HyperFocus = hyperFocus,
                MaterialsSelect = materialsSelect,
                TyrantLevel = tyrantLevel
            };
        }
    }
}
