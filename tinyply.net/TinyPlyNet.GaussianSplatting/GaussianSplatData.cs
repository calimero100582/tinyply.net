using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TinyPlyNet.GaussianSplatting
{
    /// <summary>
    /// MVP typed representation of imported Gaussian splat data.
    /// </summary>
    public sealed class GaussianSplatData
    {
        /// <summary>
        /// Creates imported Gaussian splat data.
        /// </summary>
        public GaussianSplatData(
            IEnumerable<float> positions,
            IEnumerable<float> scales,
            IEnumerable<float> rotations,
            IEnumerable<float> opacities,
            IEnumerable<float> dcFeatures)
        {
            Positions = new ReadOnlyCollection<float>(positions.ToList());
            Scales = new ReadOnlyCollection<float>(scales.ToList());
            Rotations = new ReadOnlyCollection<float>(rotations.ToList());
            Opacities = new ReadOnlyCollection<float>(opacities.ToList());
            DcFeatures = new ReadOnlyCollection<float>(dcFeatures.ToList());
        }

        /// <summary>
        /// Flattened xyz position values.
        /// </summary>
        public IReadOnlyList<float> Positions { get; }

        /// <summary>
        /// Flattened scale triplets.
        /// </summary>
        public IReadOnlyList<float> Scales { get; }

        /// <summary>
        /// Flattened rotation quaternion values.
        /// </summary>
        public IReadOnlyList<float> Rotations { get; }

        /// <summary>
        /// Opacity values.
        /// </summary>
        public IReadOnlyList<float> Opacities { get; }

        /// <summary>
        /// Flattened DC feature triplets.
        /// </summary>
        public IReadOnlyList<float> DcFeatures { get; }

        /// <summary>
        /// Number of splats in the imported data.
        /// </summary>
        public int Count => Opacities.Count;
    }
}
