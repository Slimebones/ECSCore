namespace Slimebones.ECSCore.Input
{
    public enum InputEventType
    {
        /// <summary>
        /// Input is pressed this frame.
        /// </summary>
        Enter = 0,
        /// <summary>
        /// Input is still pressed.
        /// </summary>
        Stay = 1,
        /// <summary>
        /// Input is released this frame.
        /// </summary>
        Exit = 2
    }
}