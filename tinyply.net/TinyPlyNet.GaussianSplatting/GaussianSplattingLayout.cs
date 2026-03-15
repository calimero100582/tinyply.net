using TinyPlyNet.HighLevel;

namespace TinyPlyNet.GaussianSplatting
{
    /// <summary>
    /// Resolved layout for a supported Gaussian Splatting vertex element.
    /// </summary>
    public sealed class GaussianSplattingLayout
    {
        /// <summary>
        /// Creates a resolved layout.
        /// </summary>
        public GaussianSplattingLayout(ElementBindingPlan bindingPlan)
        {
            BindingPlan = bindingPlan;
        }

        /// <summary>
        /// Generic binding plan used for reading the file.
        /// </summary>
        public ElementBindingPlan BindingPlan { get; }
    }
}
