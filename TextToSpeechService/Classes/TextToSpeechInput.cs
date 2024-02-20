using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechService.Enums;
using TextToSpeechService.Interfaces;

namespace TextToSpeechService.Classes
{
    public class TextToSpeechInput
    {
        public TextToSpeechInput(string text, IVoice voice)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException(nameof(text));
            if (voice is null) throw new ArgumentNullException(nameof(voice));
            this.Text = text;
            this.VoiceName = voice.Name;
            this.LanguageCode = voice.LanguageCode;
            this.Gender = voice.Gender;
        }
        public TextToSpeechInput(string text, string voiceName, string languageCode, Gender gender)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException(nameof(text));
            if (string.IsNullOrWhiteSpace(voiceName)) throw new ArgumentNullException(nameof(voiceName));
            if (string.IsNullOrWhiteSpace(languageCode)) throw new ArgumentNullException(nameof(languageCode));
            this.Text = text;
            this.VoiceName = voiceName;
            this.LanguageCode = languageCode;
            this.Gender = gender;
        }
        public string Text { get; }
        public string VoiceName { get; }
        public string LanguageCode { get; }
        public Gender Gender { get; }
    }
}
