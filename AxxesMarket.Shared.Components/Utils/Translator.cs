using Microsoft.Extensions.Localization;

namespace AxxesMarket.Shared.Components.Utils;

public class Translator : StringLocalizer<Translations>
{
    public Translator(IStringLocalizerFactory factory) : base(factory)
    {
    }
}
