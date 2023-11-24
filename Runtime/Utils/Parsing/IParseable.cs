namespace Slimebones.ECSCore.Utils.Parsing
{
    public interface IParseable<T> where T: struct
    {
        public IParseOpts<T> ParseOpts { get; }
    }
}