namespace Assets.Scripts.VibrationSystem {
    public class Pattern {
        Vibe[] vibes;
        private long[] timings;
        private int[] amplitudes;
        private int? repeatIndex;

        private Pattern() { }

        public Pattern(params Vibe[] vibes) {
            this.vibes = vibes;

            timings = new long[vibes.Length * 2];
            amplitudes = new int[vibes.Length * 2];
            var amplitudesCount = 0;
            for (var i = 0; i < vibes.Length; i++) {
                var delayIndex = i * 2;
                var valueIndex = i * 2 + 1;

                timings[delayIndex] = vibes[i].GetDelay();
                timings[valueIndex] = vibes[i].GetDuration();

                if (vibes[i].GetAmplitude().HasValue) {
                    amplitudesCount++;
                }

                amplitudes[delayIndex] = 0;
                amplitudes[valueIndex] = vibes[i].GetAmplitude() ?? -1;
            }

            if (amplitudesCount == 0) amplitudes = null;
        }

        public Pattern LoopAt(int repeatIndex) {
            this.repeatIndex = repeatIndex;
            return this;
        }

        public long[] GetTimings() => timings;
        public int[] GetAmplitudes() => amplitudes;
        public int? GetRepeatIndex() => repeatIndex;

        public override string ToString() {
            var res = "Pattern of " + vibes.Length + " Elements: \n";
            for (int i = 0; i < vibes.Length; i++) {
                res += (i) + ". " + vibes[i].ToString() +
                    ((i == repeatIndex) ? " <Looped here" : "") + "\n";
            }
            return res;
        }
    }
}
