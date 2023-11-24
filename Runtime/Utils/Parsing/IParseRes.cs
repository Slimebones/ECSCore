namespace Slimebones.ECSCore.Utils.Parsing
{
    public interface IParseRes<T>
        where T: struct
    {
        public T Value { get; set; }

        public bool IsOutAnyLimit { get; set; }
        public bool IsOutMaxLimit { get; set; }
        public bool IsOutMinLimit { get; set; }
    }
}