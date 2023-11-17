using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Config
{
    public interface IConfigSpec
    {
        public string Key { get; }
        /// <summary>
        /// Value that will be written to config if no spec's key exists
        /// there.
        /// </summary>
        public string DefaultValue { get; }
        public void OnChange(string value, World world);
    }
}