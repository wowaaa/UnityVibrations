using System;
using UnityEngine;

namespace Assets.Scripts.VibrationSystem {
    public partial class Vibrations {
        private class AndroidVibrations : IVibrationSys {
            private const string androidBuildClassName = "android.os.Build$VERSION";
            private const string sdkVersionFieldName = "SDK_INT";
            private const int minHapticSDKVersion = 26;
            private const string hasVibratorMethodName = "hasVibrator";
            private const string hasAmplitudeControlMethodName = "hasAmplitudeControl";
            private const string vibrateOnceMethod = "createOneShot";
            private const string waveformVibrationMethod = "createWaveform";
            private const string vibrateMethod = "vibrate";
            private const string cancelVibrationMethodName = "cancel";
            
            private const int NotSet = -1;
            private const int DefaultRepeat = -1;
            
            private AndroidJavaClass unityPlayer;
            private AndroidJavaObject vibrator;
            private AndroidJavaObject currentActivity;
            private AndroidJavaClass vibrationEffectClass;
            private int defaultAmplitude;
            
            private Action<Pattern> VibratePattern;
            private Action<Vibe> VibrateOnce;

            object[] parameters2 = new object[2];
            object[] parameters3 = new object[3];

            private int SDK_ver = NotSet;

            public AndroidVibrations() {
#if UNITY_ANDROID && !UNITY_EDITOR
            unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
            if ((SDK_ver = getSDKInt()) >= minHapticSDKVersion) {
                MonoBehaviour.print("Vibrations: has haptic sdk version");
                var amp = HasAmplituideControl();
                MonoBehaviour.print("Vibrations: has amplitude control - " + amp);
                vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
                defaultAmplitude = vibrationEffectClass.GetStatic<int>("DEFAULT_AMPLITUDE");
            
                VibrateOnce = HapticVibration;
                VibratePattern = HapticVibration;
            }
            else {
                MonoBehaviour.print("Vibrations: basics only");
                VibrateOnce = BasicVibration;
                VibratePattern = BasicVibration;
            }
#else
                VibrateOnce = Fallback;
                VibratePattern = Fallback;
#endif
            }

            private void HapticVibration(Vibe vibe) {
                CreateVibrationEffect(vibrateOnceMethod, vibe.GetDuration(), vibe.GetAmplitude() ?? defaultAmplitude);
            }

            private void BasicVibration(Vibe vibe) {
                OldVibrate(vibe);
            }

            private void Fallback(Vibe vibe) {
                Handheld.Vibrate();
            }

            private void HapticVibration(Pattern pattern) {
                CreateVibrationEffect(waveformVibrationMethod, pattern);
            }

            private void BasicVibration(Pattern pattern) {
                OldVibrate(pattern);
            }

            private void Fallback(Pattern pattern) {
                Handheld.Vibrate();
            }

            public void Play(Vibe vibe) {
                VibrateOnce(vibe);
            }

            public void Play(Pattern p) {
                VibratePattern(p);
            }

            private void CreateVibrationEffect(string function, Pattern p) {
                if (p.GetAmplitudes() == null) {
                    parameters2[0] = p.GetTimings();
                    parameters2[1] = p.GetRepeatIndex() ?? DefaultRepeat;
                    CreateVibrationEffect(function, parameters2);
                }
                else {
                    parameters3[0] = p.GetTimings();
                    parameters3[1] = p.GetAmplitudes();
                    parameters3[2] = p.GetRepeatIndex() ?? DefaultRepeat;
                    CreateVibrationEffect(function, parameters3);
                }
            }

            private void CreateVibrationEffect(string function, params object[] args) {
                using (AndroidJavaObject vibrationEffect = vibrationEffectClass.CallStatic<AndroidJavaObject>(function, args)) {
                    vibrator.Call(vibrateMethod, vibrationEffect);
                }
            }

            private void OldVibrate(Vibe v) {
                vibrator.Call(vibrateMethod, v.GetDuration());
            }
            private void OldVibrate(Pattern p) {
                vibrator.Call(vibrateMethod, p.GetTimings(), p.GetRepeatIndex());
            }

            public bool HasVibrator() {
                return vibrator.Call<bool>(hasVibratorMethodName);
            }

            public bool HasAmplituideControl() {
                if (SDK_ver >= minHapticSDKVersion) {
                    return vibrator.Call<bool>(hasAmplitudeControlMethodName);
                }
                else {
                    return false;
                }

            }

            public void Cancel() {
                if (vibrator != null)
                    vibrator.Call(cancelVibrationMethodName);
            }

            private static int getSDKInt() {
                using (var version = new AndroidJavaClass(androidBuildClassName)) {
                    return version.GetStatic<int>(sdkVersionFieldName);
                }
            }
            
            public static bool IsActive() {
#if UNITY_ANDROID && !UNITY_EDITOR
	    return true;
#else
                return false;
#endif
            }
        }
    }
}
