namespace Slimebones.ECSCore.Utils.Parsing
{
    public interface IParseOpts<T>
        where T : struct
    {
        public T? Min { get; }
        public T? Max { get; }
        public int? Precision { get; }
    }
}