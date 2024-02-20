using TextToSpeechService.Enums;

namespace TextToSpeechService.Interfaces
{
    public interface IVoice
    {
        string Name { get; }
        string LanguageCode { get; }
        Gender Gender { get; }
        Enums.TextToSpeechService Service { get; }
    }
}
