using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace ParkingSystem.Localization;

public static class ParkingSystemLocalizationConfigurer
{
    public static void Configure(ILocalizationConfiguration localizationConfiguration)
    {
        localizationConfiguration.Sources.Add(
            new DictionaryBasedLocalizationSource(ParkingSystemConsts.LocalizationSourceName,
                new XmlEmbeddedFileLocalizationDictionaryProvider(
                    typeof(ParkingSystemLocalizationConfigurer).GetAssembly(),
                    "ParkingSystem.Localization.SourceFiles"
                )
            )
        );
    }
}
