using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TinyPlyNet.GaussianSplatting
{
    /// <summary>
    /// Imports supported Gaussian Splatting PLY files into a typed model.
    /// </summary>
    public sealed class GaussianSplattingImporter
    {
        private readonly GaussianSplattingValidator _validator = new GaussianSplattingValidator();

        /// <summary>
        /// Imports Gaussian splat data from a PLY stream.
        /// </summary>
        public GaussianSplatData Import(Stream stream)
        {
            var file = new PlyFile(stream);
            var validation = _validator.Validate(file);
            if (!validation.IsValid || validation.Layout == null)
            {
                throw new InvalidOperationException(
                    "The provided PLY file does not match a supported Gaussian Splatting layout: " +
                    string.Join("; ", validation.Report.Issues.Select(x => x.Message)));
            }

            var bindingPlan = validation.Layout.BindingPlan;
            var positions = new List<float>();
            var scales = new List<float>();
            var rotations = new List<float>();
            var opacities = new List<float>();
            var dcFeatures = new List<float>();

            file.RequestPropertyFromElement(
                bindingPlan.ElementName,
                bindingPlan.GetGroup("position").Properties.Select(x => x.Property.Name),
                positions);
            file.RequestPropertyFromElement(
                bindingPlan.ElementName,
                bindingPlan.GetGroup("scale").Properties.Select(x => x.Property.Name),
                scales);
            file.RequestPropertyFromElement(
                bindingPlan.ElementName,
                bindingPlan.GetGroup("rotation").Properties.Select(x => x.Property.Name),
                rotations);
            file.RequestPropertyFromElement(
                bindingPlan.ElementName,
                bindingPlan.GetGroup("dc_features").Properties.Select(x => x.Property.Name),
                dcFeatures);
            file.RequestPropertyFromElement(
                bindingPlan.ElementName,
                new[] { bindingPlan.GetScalar("opacity").Property.Name },
                opacities);

            file.Read(stream);
            return new GaussianSplatData(positions, scales, rotations, opacities, dcFeatures);
        }
    }
}
