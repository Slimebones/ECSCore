namespace Slimebones.ECSCore.Utils.Parsing
{
    public class IntParseRes: IParseRes<int>
    {
        private int value;

        private bool isOutAnyLimit = false;
        private bool isOutMaxLimit = false;
        private bool isOutMinLimit = false;

        public int Value {
            get => value;
            set => this.value = value;
        }

        public bool IsOutAnyLimit
        {
            get => isOutAnyLimit;
            set => isOutAnyLimit = value;
        }

        public bool IsOutMaxLimit
        {
            get => isOutMaxLimit;
            set => isOutMaxLimit = value;
        }

        public bool IsOutMinLimit
        {
            get => isOutMinLimit;
            set => isOutMinLimit = value;
        }
    }
}