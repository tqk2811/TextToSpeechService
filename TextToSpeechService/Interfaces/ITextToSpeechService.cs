using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextToSpeechService.Classes;

namespace TextToSpeechService.Interfaces
{
    public interface ITextToSpeechService
    {
        Task<IReadOnlyList<IVoice>> ListVoicesAsync(string languageCode, CancellationToken cancellationToken = default);
        Task<ReadOnlyMemory<byte>> TextToSpeechAsync(TextToSpeechInput textToSpeechInput, CancellationToken cancellationToken = default);
    }
}
