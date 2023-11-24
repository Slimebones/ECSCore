using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Config
{
    public interface IConfigSpec<T>
    {
        public string Key { get; }
        /// <summary>
        /// Value that will be written to config if no spec's key exists
        /// there.
        /// </summary>
        public string DefaultValueStr { get; }
        public void OnChange(string value, World world);
        public T Parse(string value);
    }
}