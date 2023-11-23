using UnityEngine;

namespace Slimebones.ECSCore.Input
{
    public static class CursorUtils
    {
        public static float mouseSensitivityX = 1f;
        public static float mouseSensitivityY = 1f;

        public static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public static void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        public static Vector3 GetDelta()
        {
            return new Vector3(
                UnityEngine.InputSystem.Mouse.current.delta.ReadValue().x
                    * mouseSensitivityX,
                UnityEngine.InputSystem.Mouse.current.delta.ReadValue().y
                    * mouseSensitivityY,
                0f
            );
        }
    }
}
