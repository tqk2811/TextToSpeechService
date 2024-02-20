using System;
using TextToSpeechService.Enums;
using TextToSpeechService.Interfaces;

namespace TextToSpeechService.Classes
{
    internal class Voice : IVoice
    {
        public Voice(string name, string languageCode, Gender gender, Enums.TextToSpeechService service)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(languageCode)) throw new ArgumentNullException(nameof(languageCode));
            this.Name = name;
            this.LanguageCode = languageCode;
            this.Gender = gender;
            Service = service;
        }
        public string Name { get; }
        public string LanguageCode { get; }
        public Gender Gender { get; }
        public Enums.TextToSpeechService Service { get; }
    }
}
