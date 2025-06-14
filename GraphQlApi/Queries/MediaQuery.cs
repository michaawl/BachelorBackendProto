namespace GraphQlApi.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class MediaQuery
    {
        public byte[] GetImage() => ApiCache.ImageData;

        public byte[] GetAudio() => ApiCache.AudioData;

        public byte[] GetVideo() => ApiCache.VideoData;
    }
}
