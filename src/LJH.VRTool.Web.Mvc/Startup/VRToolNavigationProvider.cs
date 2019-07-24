using Abp.Application.Navigation;
using Abp.Localization;
using LJH.VRTool.Authorization;

namespace LJH.VRTool.Web.Startup
{
    
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class VRToolNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            #region 系统菜单
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "home",
                        requiresAuthentication: true
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Tenants,
                        L("Tenants"),
                        url: "Tenants",
                        icon: "business",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "fa fa-user",
                        requiredPermissionName: PermissionNames.Pages_Users
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "Roles",
                        icon: "fa fa-users",
                        requiredPermissionName: PermissionNames.Pages_Roles
                    )
                )
                 .AddItem(
                    new MenuItemDefinition(
                        PageNames.Source,
                        L("Source"),
                        url: "Source",
                        icon: "fa fa-clone",
                        requiredPermissionName: PermissionNames.Pages_Source
                    )
                )
                 .AddItem(
                    new MenuItemDefinition(
                        PageNames.Panoram,
                        L("Panoram"),
                        url: "Panoram",
                        icon: "fa fa-youtube-play",
                        requiredPermissionName: PermissionNames.Pages_Panoram
                    )
                )
                  .AddItem(
                    new MenuItemDefinition(
                        PageNames.Video,
                        L("Video"),
                        url: "Video",
                        icon: "fa fa-tv",
                        requiredPermissionName: PermissionNames.Pages_Video
                    )
                )
                  .AddItem(
                    new MenuItemDefinition(
                        PageNames.Video,
                        L("WebSetting"),
                        url: "WebSetting",
                        icon: "fa fa-tv",
                        requiredPermissionName: PermissionNames.Pages_Video
                    )
                );
            //.AddItem(
            //    new MenuItemDefinition(
            //        PageNames.About,
            //        L("About"),
            //        url: "About",
            //        icon: "info"
            //    )
            //).AddItem( // Menu items below is just for demonstration!
            //    new MenuItemDefinition(
            //        "MultiLevelMenu",
            //        L("MultiLevelMenu"),
            //        icon: "menu"
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "AspNetBoilerplate",
            //            new FixedLocalizableString("ASP.NET Boilerplate")
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetBoilerplateHome",
            //                new FixedLocalizableString("Home"),
            //                url: "https://aspnetboilerplate.com?ref=abptmpl"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetBoilerplateTemplates",
            //                new FixedLocalizableString("Templates"),
            //                url: "https://aspnetboilerplate.com/Templates?ref=abptmpl"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetBoilerplateSamples",
            //                new FixedLocalizableString("Samples"),
            //                url: "https://aspnetboilerplate.com/Samples?ref=abptmpl"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetBoilerplateDocuments",
            //                new FixedLocalizableString("Documents"),
            //                url: "https://aspnetboilerplate.com/Pages/Documents?ref=abptmpl"
            //            )
            //        )
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "AspNetZero",
            //            new FixedLocalizableString("ASP.NET Zero")
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroHome",
            //                new FixedLocalizableString("Home"),
            //                url: "https://aspnetzero.com?ref=abptmpl"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroDescription",
            //                new FixedLocalizableString("Description"),
            //                url: "https://aspnetzero.com/?ref=abptmpl#description"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroFeatures",
            //                new FixedLocalizableString("Features"),
            //                url: "https://aspnetzero.com/?ref=abptmpl#features"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroPricing",
            //                new FixedLocalizableString("Pricing"),
            //                url: "https://aspnetzero.com/?ref=abptmpl#pricing"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroFaq",
            //                new FixedLocalizableString("Faq"),
            //                url: "https://aspnetzero.com/Faq?ref=abptmpl"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroDocuments",
            //                new FixedLocalizableString("Documents"),
            //                url: "https://aspnetzero.com/Documents?ref=abptmpl"
            //            )
            //        )
            //    )
            //); 
            #endregion
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, VRToolConsts.LocalizationSourceName);
        }
    }
}
