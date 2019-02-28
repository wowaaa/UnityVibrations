using Assets.Scripts.VibrationSystem;
using UnityEngine;

namespace Assets.Scripts {
    public class ExampleBehaviour : MonoBehaviour {
        Vibrations v;
        private void Start() {
            v = new Vibrations();
            print(v.HasAmplitudes() ? "We can use amplitude control inside of vibrations"
            : "Just simple vibrations without amplitude control supported");
        }

        Vibe oneTimeShort = Vibrations.Buzz(100);

        Vibe oneTimeLongWithAmplitude = Vibrations.Buzz(500).Amplitude(100);

        Pattern waveSimple = Vibrations.MakePattern(
            Vibrations.Buzz(50),
            Vibrations.Buzz(150).Delay(200),
            Vibrations.Buzz(500).Delay(300)
            );

        Pattern waveWithAmplitudesAndLoopedInTheMiddle = Vibrations.MakePattern(
            Vibrations.Buzz(50).Amplitude(10),
            Vibrations.Buzz(150).Delay(200),
            Vibrations.Buzz(500).Delay(300).Amplitude(250)
            ).LoopAt(1);

        public void VibrateSimple() {
            v.Vibrate(oneTimeShort);
        }

        public void VibrateSimpleAndAmplitude() {
            v.Vibrate(oneTimeShort);
        }

        public void VibrateSomeWave() {
            v.Vibrate(waveSimple);
        }

        public void VibrateLoopedWave() {
            v.Vibrate(waveWithAmplitudesAndLoopedInTheMiddle);
            Invoke("DoNotForgetToStopVibrationPatternLoops", 3);
        }

        void DoNotForgetToStopVibrationPatternLoops() {
            v.Stop();
        }
    }
}
