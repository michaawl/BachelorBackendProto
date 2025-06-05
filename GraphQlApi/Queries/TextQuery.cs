using Common;              
using System.IO;
using System.Reflection;
using System.Text;
using HotChocolate;

namespace GraphQlApi.Queries
{
    // Hook into HotChocolate’s Query root
    [ExtendObjectType(typeof(Query))]
    public class TextQuery
    {
        private string LoadEmbeddedText(string resourceName)
        {
            var assembly = typeof(TextPayload).Assembly;
            using var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new FileNotFoundException($"Resource '{resourceName}' not found.");

            using var reader = new StreamReader(stream, Encoding.UTF8);
            return reader.ReadToEnd();
        }

        // Already working “getSmall”
        public TextPayload GetSmall() =>
            new TextPayload
            {
                Content = LoadEmbeddedText("Common.Payload.Text.small.txt")
            };

        // Add “getMedium” exactly like REST’s /text/medium
        public TextPayload GetMedium() =>
            new TextPayload
            {
                Content = LoadEmbeddedText("Common.Payload.Text.medium.txt")
            };

        // Add “getLarge” exactly like REST’s /text/large
        public TextPayload GetLarge() =>
            new TextPayload
            {
                Content = LoadEmbeddedText("Common.Payload.Text.large.txt")
            };
    }
}
