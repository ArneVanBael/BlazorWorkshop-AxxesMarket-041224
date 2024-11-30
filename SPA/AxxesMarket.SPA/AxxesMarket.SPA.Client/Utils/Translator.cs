using Microsoft.Extensions.Localization;

namespace AxxesMarket.SPA.Client.Utils;

public class Translator : StringLocalizer<Translations>
{
    public Translator(IStringLocalizerFactory factory) : base(factory)
    {
    }
}
