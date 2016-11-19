using EPiServer.Core;

namespace SampleWebApp.Models.Pages
{
    public interface IHasRelatedContent
    {
        ContentArea RelatedContentArea { get; }
    }
}
