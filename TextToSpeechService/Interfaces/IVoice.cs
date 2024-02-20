using TextToSpeechService.Enums;

namespace TextToSpeechService.Interfaces
{
    public interface IVoice
    {
        string Name { get; }
        string LanguageCode { get; }
        Gender Gender { get; }
        ServiceName Service { get; }
    }
}
