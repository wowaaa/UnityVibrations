namespace Assets.Scripts.VibrationSystem {
    public interface IVibrationSys {
        bool HasAmplituideControl();

        void Play(Pattern p);
        void Play(Vibe p);

        void Cancel();
    }
}
