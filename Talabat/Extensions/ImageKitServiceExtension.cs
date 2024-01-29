
using Imagekit.Sdk;

namespace Talabat.Extensions
{
    public static class ImageKitServiceExtension
    {
        public static void ConfigureImageKitService(
            this IServiceCollection services,
            IConfiguration configuration
        )

        {
            /*var config = configuration.GetSection("Bucket");
            var publicKey = config.GetValue<string>("publicKey");
            var privateKey = config.GetValue<string>("privateKey");
            var urlEndPoint = config.GetValue<string>("urlEndPoint");*/
            services.AddSingleton<ImagekitClient>(new ImagekitClient(
                "public_fqeiQOMGFPY6xKNcluh4VDF4zl4=",
                "private_aaC2To7UocRrXBRWDwCMACgxnDE=",
                "https://ik.imagekit.io/gigh0swt4n"
            ));
        }

    }
}
