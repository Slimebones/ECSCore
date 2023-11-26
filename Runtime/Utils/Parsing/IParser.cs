namespace Slimebones.ECSCore.Utils.Parsing
{
    public interface IParser<T>
    {
        public T ParseIn(string valueStr);
        public string ParseOut(T value);
    }
}