using Google.Cloud.TextToSpeech.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextToSpeechService.Classes;
using TextToSpeechService.Enums;
using TextToSpeechService.Interfaces;

namespace TextToSpeechService.Services
{
    public class GoogleService : ITextToSpeechService
    {
        readonly TextToSpeechClientBuilder _builder = new TextToSpeechClientBuilder();
        public GoogleService(string? jsonPath)
        {
            if (!File.Exists(jsonPath))
            {
                throw new FileNotFoundException(jsonPath);
            }
            _builder.CredentialsPath = jsonPath;
        }
        AudioConfig _audioConfig = new AudioConfig()
        {
            AudioEncoding = AudioEncoding.Mp3,
            SampleRateHertz = 24000,
            SpeakingRate = 1.0,
            VolumeGainDb = 0.0,
            Pitch = 0,
        };
        public void UpdateAudioConfig(AudioConfig audioConfig)
        {
            if (audioConfig is null) throw new ArgumentNullException(nameof(audioConfig));
            _audioConfig = new AudioConfig(audioConfig);
        }



        public async Task<IReadOnlyList<IVoice>> ListVoicesAsync(string languageCode, CancellationToken cancellationToken = default)
        {
            TextToSpeechClient client = await _builder.BuildAsync(cancellationToken);
            var res = await client.ListVoicesAsync(languageCode, cancellationToken);
            IReadOnlyList<IVoice> voices = res.Voices
                .Where(x => x.SsmlGender == SsmlVoiceGender.Male || x.SsmlGender == SsmlVoiceGender.Female)
                .Select(x => new Classes.Voice(
                    x.Name,
                    languageCode,
                    x.SsmlGender switch
                    {
                        SsmlVoiceGender.Male => Gender.Male,
                        SsmlVoiceGender.Female => Gender.Female,
                        SsmlVoiceGender.Neutral => Gender.Neutral,
                        _ => Gender.Unknow,
                    },
                    ServiceName.Google))
                .ToList();
            return voices;
        }

        public async Task<ReadOnlyMemory<byte>> TextToSpeechAsync(TextToSpeechInput textToSpeechInput, CancellationToken cancellationToken = default)
        {
            if (textToSpeechInput is null)
                throw new ArgumentNullException(nameof(textToSpeechInput));

            TextToSpeechClient client = await _builder.BuildAsync(cancellationToken);
            SynthesizeSpeechRequest request = new SynthesizeSpeechRequest()
            {
                AudioConfig = _audioConfig,
                Input = new SynthesisInput()
                {
                    Text = textToSpeechInput.Text,
                },
                Voice = new VoiceSelectionParams()
                {
                    LanguageCode = textToSpeechInput.LanguageCode,
                    Name = textToSpeechInput.VoiceName,
                    SsmlGender = textToSpeechInput.Gender switch
                    {
                        Gender.Male => SsmlVoiceGender.Male,
                        _ => SsmlVoiceGender.Female,
                    }
                }
            };
            var response = await client.SynthesizeSpeechAsync(request, cancellationToken);
            return response.AudioContent.Memory;
        }
    }
}
