namespace Slimebones.ECSCore.Utils.Parsing
{
    public interface IParser<T>
    {
        public T Parse(string valueStr);
    }
}