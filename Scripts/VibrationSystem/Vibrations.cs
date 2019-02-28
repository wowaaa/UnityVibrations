using System;
using UnityEngine;

namespace Assets.Scripts.VibrationSystem {
    public partial class Vibrations {
        IVibrationSys vibro;
        public Vibrations() {
            try {
                if (AndroidVibrations.IsActive()) {
                    vibro = new AndroidVibrations();
                }
                else {
                    vibro = new DefaultVibrationsAsLog();
                    MonoBehaviour.print("Initialized default log vibration system");
                }
            }
            catch (Exception ex) {
                throw new Exception("Vibrations constructor must be called after the Player is initialized", ex);
            }
        }

        public bool HasAmplitudes() {
            return vibro.HasAmplituideControl();
        }

        public void Vibrate(Vibe v) {
            vibro.Play(v);
        }

        public void Vibrate(Pattern p) {
            vibro.Play(p);
        }

        public void Stop() {
            vibro.Cancel();
        }

        public static Vibe Buzz(long durationMs) => new Vibe(durationMs);

        public static Pattern MakePattern(params Vibe[] vibes) => new Pattern(vibes);
    }
}
