using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.VibrationSystem {
    public partial class Vibrations {
        class DefaultVibrationsAsLog : IVibrationSys {
            public void Cancel() => MonoBehaviour.print("Stopped vibrations");
            
            public bool HasAmplituideControl() {
                MonoBehaviour.print("Checked amplitude");
                return false;
            }

            public void Play(Pattern p) => MonoBehaviour.print("Played " + p);

            public void Play(Vibe p) => MonoBehaviour.print("Played single:" + p);
        }
    }
}
