using Common;              
namespace GraphQlApi.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class TextQuery
    {
        public TextPayload GetSmall() =>
            new TextPayload
            {
                Content = ApiCache.SmallText
            };

        public TextPayload GetMedium() =>
            new TextPayload
            {
                Content = ApiCache.MediumText
            };

        public TextPayload GetLarge() =>
            new TextPayload
            {
                Content = ApiCache.LargeText
            };
    }
}
