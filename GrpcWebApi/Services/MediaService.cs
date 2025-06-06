using Common;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcWebApi.Protos;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace GrpcWebApi.Services
{
    public class MediaService : Media.MediaBase
    {
        private readonly ILogger<MediaService> _logger;

        public MediaService(ILogger<MediaService> logger)
        {
            _logger = logger;
        }

        private (byte[], string) LoadMedia(string resourceName, string contentType)
        {
            var assembly = typeof(TextPayload).Assembly;
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                var resources = string.Join("\n", assembly.GetManifestResourceNames());
                throw new FileNotFoundException($"Resource '{resourceName}' not found. Available:\n{resources}");
            }
            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            return (ms.ToArray(), contentType);
        }

        public override Task<MediaResponse> GetImage(Empty request, ServerCallContext context)
        {
            try
            {
                var (bytes, contentType) = LoadMedia("Common.Payload.Media.foto.jpg", "image/jpeg");
                return Task.FromResult(new MediaResponse
                {
                    ContentType = contentType,
                    Data = Google.Protobuf.ByteString.CopyFrom(bytes)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load image resource!");
                throw new RpcException(new Status(StatusCode.NotFound, $"Could not load image: {ex.Message}"));
            }
        }

        public override Task<MediaResponse> GetAudio(Empty request, ServerCallContext context)
        {
            var (bytes, contentType) = LoadMedia("Common.Payload.Media.music.wav", "audio/wav");
            return Task.FromResult(new MediaResponse
            {
                ContentType = contentType,
                Data = Google.Protobuf.ByteString.CopyFrom(bytes)
            });
        }

        public override Task<MediaResponse> GetVideo(Empty request, ServerCallContext context)
        {
            var (bytes, contentType) = LoadMedia("Common.Payload.Media.video.mp4", "video/mp4");
            return Task.FromResult(new MediaResponse
            {
                ContentType = contentType,
                Data = Google.Protobuf.ByteString.CopyFrom(bytes)
            });
        }
    }
}
