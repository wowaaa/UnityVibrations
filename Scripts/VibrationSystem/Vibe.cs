namespace Assets.Scripts.VibrationSystem {
    public class Vibe {
        private long startDelayMs;
        private long durationMs;
        private byte? amplitude;

        public long GetDelay() => startDelayMs;

        public long GetDuration() => durationMs;

        public byte? GetAmplitude() => amplitude;

        private Vibe() { }

        public Vibe(long durationMs) {
            this.durationMs = durationMs;
        }

        public Vibe Amplitude(byte amplitude) {
            this.amplitude = amplitude;
            return this;
        }

        public Vibe Delay(long delayInPatternMs) {
            startDelayMs = delayInPatternMs;
            return this;
        }

        public override string ToString() => 
            $"{{{startDelayMs}>{durationMs}:{amplitude?.ToString() ?? "Default"}}}";
    }
}
