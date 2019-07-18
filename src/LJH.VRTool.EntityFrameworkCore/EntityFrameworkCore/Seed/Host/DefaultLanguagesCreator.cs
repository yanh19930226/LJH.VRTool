using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Localization;
using Abp.Configuration;

namespace LJH.VRTool.EntityFrameworkCore.Seed.Host
{
    public class DefaultLanguagesCreator
    {
        public static List<ApplicationLanguage> InitialLanguages => GetInitialLanguages();

        private readonly VRToolDbContext _context;

        private static List<ApplicationLanguage> GetInitialLanguages()
        {
            return new List<ApplicationLanguage>
            {
                new ApplicationLanguage(null, "en", "English", "famfamfam-flags gb"),
                new ApplicationLanguage(null, "ar", "العربية", "famfamfam-flags sa"),
                new ApplicationLanguage(null, "de", "German", "famfamfam-flags de"),
                new ApplicationLanguage(null, "it", "Italiano", "famfamfam-flags it"),
                new ApplicationLanguage(null, "fr", "Français", "famfamfam-flags fr"),
                new ApplicationLanguage(null, "pt-BR", "Português", "famfamfam-flags br"),
                new ApplicationLanguage(null, "tr", "Türkçe", "famfamfam-flags tr"),
                new ApplicationLanguage(null, "ru", "Русский", "famfamfam-flags ru"),
                new ApplicationLanguage(null, "zh-Hans", "简体中文", "famfamfam-flags cn"),
                new ApplicationLanguage(null, "es-MX", "Español México", "famfamfam-flags mx"),
                new ApplicationLanguage(null, "nl", "Nederlands", "famfamfam-flags nl"),
                new ApplicationLanguage(null, "ja", "日本語", "famfamfam-flags jp")
            };
        }

        public DefaultLanguagesCreator(VRToolDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            foreach (var language in InitialLanguages)
            {
                AddLanguageIfNotExists(language);
            }
            SetDefaultLanguageIfNotExist("zh-Hans");
        }
        private void SetDefaultLanguageIfNotExist(string lang)
        {
            if (_context.Settings.IgnoreQueryFilters().Any(s => s.Name == LocalizationSettingNames.DefaultLanguage && s.Value == lang))
                return;

            _context.Settings.Add(new Setting(null, null, LocalizationSettingNames.DefaultLanguage, "zh-Hans"));
            _context.SaveChanges();
        }
        private void AddLanguageIfNotExists(ApplicationLanguage language)
        {
            if (_context.Languages.IgnoreQueryFilters().Any(l => l.TenantId == language.TenantId && l.Name == language.Name))
            {
                return;
            }
            _context.Languages.Add(language);
            _context.SaveChanges();
        }
    }
}
