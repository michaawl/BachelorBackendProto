using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcWebApi.Protos;             
namespace GrpcWebApi.Services
{
    public class TextService : Text.TextBase
    {
        private readonly ILogger<TextService> _logger;

        public TextService(ILogger<TextService> logger)
        {
            _logger = logger;
        }

        public override Task<TextResponse> GetSmall(Empty request, ServerCallContext context)
        {
            var content = ApiCache.SmallText;
            var reply = new TextResponse { Content = content };
            return Task.FromResult(reply);
        }

        public override Task<TextResponse> GetMedium(Empty request, ServerCallContext context)
        {
            var content = ApiCache.MediumText;
            var reply = new TextResponse { Content = content };
            return Task.FromResult(reply);
        }

        public override Task<TextResponse> GetLarge(Empty request, ServerCallContext context)
        {
            var content = ApiCache.LargeText;
            var reply = new TextResponse { Content = content };
            return Task.FromResult(reply);
        }
    }
}
