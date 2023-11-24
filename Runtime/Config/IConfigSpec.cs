using Scellecs.Morpeh;
using Slimebones.ECSCore.Utils.Parsing;

namespace Slimebones.ECSCore.Config
{
    public interface IConfigSpec<T>
    {
        public string Key { get; }
        public string DefaultValueStr { get; }

        public void OnInit(Entity e, World world);
        public void OnChange(T value);

        public T PostParse(T value);
    }
}