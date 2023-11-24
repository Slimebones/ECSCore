namespace Slimebones.ECSCore.Utils.Parsing
{
    public interface IParseOpts<T>
    {
        public T Min { get; }
        public T Max { get; }
    }
}