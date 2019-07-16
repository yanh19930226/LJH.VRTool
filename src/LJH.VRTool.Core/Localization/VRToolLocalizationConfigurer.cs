using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace LJH.VRTool.Localization
{
    public static class VRToolLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(VRToolConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(VRToolLocalizationConfigurer).GetAssembly(),
                        "LJH.VRTool.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
