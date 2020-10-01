using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MonstersInc
{
    public class IntimidatorIntimidationsCacheChannel
    {
        private readonly Channel<int> _IntimidatorIntimidationsSummeryChannel;

        public IntimidatorIntimidationsCacheChannel()
        {
            var options = new BoundedChannelOptions(100)
            {
                SingleWriter = false,
                SingleReader = true
            };

            _IntimidatorIntimidationsSummeryChannel = Channel.CreateBounded<int>(options);
        }

        public async Task WriteToIntimidationsSummeryChannel(int intimidatorId, CancellationToken ct = default)
        {
            while (await _IntimidatorIntimidationsSummeryChannel.Writer.WaitToWriteAsync(ct) && !ct.IsCancellationRequested)
            {
                var added = _IntimidatorIntimidationsSummeryChannel.Writer.TryWrite(intimidatorId);

                if (added)
                    break;
            }
        }


        public IAsyncEnumerable<int> ReadAllAsync(CancellationToken ct = default)
       => _IntimidatorIntimidationsSummeryChannel.Reader.ReadAllAsync(ct);
    }
}
