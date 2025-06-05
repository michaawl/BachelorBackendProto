using Common;
using System.Reflection;

namespace GraphQlApi.Queries;

[ExtendObjectType(typeof(Query))]
public class MediaQuery
{
    private byte[] LoadBinary(string resourceName)
    {
        var assembly = typeof(TextPayload).Assembly;
        using var stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new FileNotFoundException($"Resource '{resourceName}' not found.");
        using var ms = new MemoryStream();
        stream.CopyTo(ms);
        return ms.ToArray(); // Will be returned as base64 in GraphQL
    }

    public byte[] GetImage() =>
        LoadBinary("Common.Payload.Media.foto.jpg");

    public byte[] GetAudio() =>
        LoadBinary("Common.Payload.Media.music.wav");

    public byte[] GetVideo() =>
        LoadBinary("Common.Payload.Media.video.mp4");
}
