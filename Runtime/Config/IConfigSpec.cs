using Scellecs.Morpeh;
using Slimebones.ECSCore.Utils.Parsing;

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
        public IParseOpts<T> ParseOpts { get; }
        public void OnChange(T value, World world);
    }
}