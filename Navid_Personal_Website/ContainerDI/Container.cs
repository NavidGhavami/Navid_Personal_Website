using Domain.Layer.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Navid_Personal_Website.ContainerDI
{
    public static class Container
    {
        public static void RegisterService(this IServiceCollection services)
        {
            #region Repositories

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #endregion

            #region General Services



            #endregion



            #region Common Services

            services.AddHttpContextAccessor();
            services.AddSingleton<HtmlEncoder>(
                HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));
            //services.AddTransient<IPasswordHasher, PasswordHasher>();

            #endregion
        }
    }
}
