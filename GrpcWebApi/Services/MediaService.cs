using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;  
using GrpcWebApi.Protos;       

namespace GrpcWebApi.Services
{
    public class MediaService : Media.MediaBase
    {
        private readonly ILogger<MediaService> _logger;
        private const int CHUNK_SIZE = 64 * 1024;

        public MediaService(ILogger<MediaService> logger)
        {
            _logger = logger;
        }

        public override async Task GetImage(
            Empty request,
            IServerStreamWriter<Chunk> responseStream,
            ServerCallContext context)
        {
            await StreamBytes(ApiCache.ImageData, responseStream, context);
        }

        public override async Task GetAudio(
            Empty request,
            IServerStreamWriter<Chunk> responseStream,
            ServerCallContext context)
        {
            await StreamBytes(ApiCache.AudioData, responseStream, context);
        }

        public override async Task GetVideo(
            Empty request,
            IServerStreamWriter<Chunk> responseStream,
            ServerCallContext context)
        {
            await StreamBytes(ApiCache.VideoData, responseStream, context);
        }

        private async Task StreamBytes(
            byte[] buffer,
            IServerStreamWriter<Chunk> stream,
            ServerCallContext context)
        {
            try
            {
                for (int offset = 0; offset < buffer.Length; offset += CHUNK_SIZE)
                {
                    int length = Math.Min(CHUNK_SIZE, buffer.Length - offset);
                    var chunk = new Chunk
                    {
                        Data = ByteString.CopyFrom(buffer, offset, length)
                    };
                    await stream.WriteAsync(chunk);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error streaming data");
                throw new RpcException(
                    new Status(StatusCode.Internal, "Streaming error"));
            }
        }
    }
}
