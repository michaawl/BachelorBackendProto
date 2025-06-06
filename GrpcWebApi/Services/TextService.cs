using Common;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcWebApi.Protos;             // for Text.TextBase, Empty, TextResponse, PingResponse
using Microsoft.Extensions.Logging;  // for ILogger
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GrpcWebApi.Services
{
    // We name our class exactly "TextService" to match `service Text { … }` in the .proto.
    // Notice how we inherit from GrpcWebApi.Protos.Text.TextBase (no ambiguity, because of the using above).
    public class TextService : Text.TextBase
    {
        private readonly ILogger<TextService> _logger;

        // Constructor name must match the class name: "TextService"
        public TextService(ILogger<TextService> logger)
        {
            _logger = logger;
        }

        // Helper to read an embedded text file from the Common.Payload.Text assembly
        private string LoadEmbeddedText(string resourceName)
        {
            // Correct type to anchor the assembly is Common.Payload.Text.TextPayload
            var assembly = typeof(TextPayload).Assembly;
            var names = assembly.GetManifestResourceNames();
            _logger.LogInformation("Available resources:\n" + string.Join("\n", names));

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new Exception(
                    $"Resource '{resourceName}' not found. Available:\n{string.Join("\n", names)}"
                );
            }

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        // Override of the generated proto method: GetSmall(Empty) → TextResponse
        public override Task<TextResponse> GetSmall(Empty request, ServerCallContext context)
        {
            var content = LoadEmbeddedText("Common.Payload.Text.small.txt");
            var byteCount = Encoding.UTF8.GetByteCount(content);
            _logger.LogInformation($"[SMALL] Payload size: {byteCount} bytes");

            var reply = new TextResponse { Content = content };
            return Task.FromResult(reply);
        }

        // Override for GetMedium
        public override Task<TextResponse> GetMedium(Empty request, ServerCallContext context)
        {
            var content = LoadEmbeddedText("Common.Payload.Text.medium.txt");
            var byteCount = Encoding.UTF8.GetByteCount(content);
            _logger.LogInformation($"[MEDIUM] Payload size: {byteCount} bytes");

            var reply = new TextResponse { Content = content };
            return Task.FromResult(reply);
        }

        // Override for GetLarge
        public override Task<TextResponse> GetLarge(Empty request, ServerCallContext context)
        {
            var content = LoadEmbeddedText("Common.Payload.Text.large.txt");
            var byteCount = Encoding.UTF8.GetByteCount(content);
            _logger.LogInformation($"[LARGE] Payload size: {byteCount} bytes");

            var reply = new TextResponse { Content = content };
            return Task.FromResult(reply);
        }

        // Override for Ping
        public override Task<PingResponse> Ping(Empty request, ServerCallContext context)
        {
            var reply = new PingResponse { Message = "pong" };
            return Task.FromResult(reply);
        }
    }
}
