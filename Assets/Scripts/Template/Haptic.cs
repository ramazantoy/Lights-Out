using UnityEngine;

namespace LeonBrave
{
    public static class Haptic 
    {
   
        public static void Vibrate(HapticType type)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidVibrate(type);
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                iOSVibrate(type);
            }
        }

        private static void AndroidVibrate(HapticType type)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

            long[] pattern = GetVibrationPattern(type);
            vibrator.Call("vibrate", pattern, -1);
        }

        private static void iOSVibrate(HapticType type)
        {
            int vibrationTime = GetVibrationTime(type);
            Handheld.Vibrate();
        }

        private static int GetVibrationTime(HapticType type)
        {
            switch (type)
            {
                case HapticType.Light:
                    return 50;
                case HapticType.Medium:
                    return 100;
                case HapticType.Hard:
                    return 150;
                default:
                    return 50;
            }
        }

        private static long[] GetVibrationPattern(HapticType type)
        {
            switch (type)
            {
                case HapticType.Light:
                    return new long[] { 0, 50 };
                case HapticType.Medium:
                    return new long[] { 0, 100 };
                case HapticType.Hard:
                    return new long[] { 0, 150 };
                default:
                    return new long[] { 0, 50 };
            }
        }
    }

    public enum HapticType
    {
        Light,
        Medium,
        Hard
    }
}