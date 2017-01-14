using System.Runtime.InteropServices;

namespace Ipt.Timer
{
    internal static class NativeMethods
    {
        internal delegate void MultimediaTimerCallback(uint id, uint msg, ref uint userCtx, uint rsv1, uint rsv2);

        [DllImport("winmm.dll", SetLastError = true, EntryPoint = "timeSetEvent")]
        internal static extern uint TimeSetEvent(uint msDelay, uint msResolution, MultimediaTimerCallback callback, ref uint userCtx, uint eventType);

        [DllImport("winmm.dll", SetLastError = true, EntryPoint = "timeKillEvent")]
        internal static extern void TimeKillEvent(uint uTimerId);
    }
}