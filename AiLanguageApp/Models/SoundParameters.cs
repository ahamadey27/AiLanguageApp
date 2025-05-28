namespace AiLanguageApp.Models
{
    public class SoundParameters
    {
        public int Frequency { get; set; } //defines frequecny in Hz
        public int Duration { get; set; } //Defines duration of sound in ms
        public string? Waveform { get; set; } //corresponds to the OscillatorNode.type (which will be Sine)
    }
}