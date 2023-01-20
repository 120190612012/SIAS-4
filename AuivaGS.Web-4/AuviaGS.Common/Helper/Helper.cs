using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using AuivaGS.AWSServies.Models;
using AuivaGS.AWSServies.Servies;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AuviaGS.Common.Helper
{
    public class Helper
    {


        public static string SaveImage(string base64img, string baseFolder)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(baseFolder))
                {
                    throw new ServiceValidationException("Invalid folder name for upload images");
                }
                var wwwroot = AppSettingsHelper.GetWebHostEnvironment().WebRootPath;
                var folderPath = Path.Combine(wwwroot, baseFolder);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var base64Array = base64img.Split(";base64,");
                if (base64Array.Length < 1)
                {
                    return "";
                }
                
                base64img = base64Array[1];
                var fileName = $"{Guid.NewGuid()}{"Logo.png"}".Replace("-", string.Empty);

                if (!string.IsNullOrWhiteSpace(folderPath))
                {
                    var url = $@"{baseFolder}\{fileName}";
                    fileName = @$"{folderPath}\{fileName}";
                    File.WriteAllBytes(fileName, Convert.FromBase64String(base64img));
                    return url;
                }

                return "";
            }
            catch (Exception ex)
            {
                throw new ServiceValidationException(ex.Message);
            }
        }

        public static async Task<string> SaveImage1(string base64img, string baseFolder, IStorageServies _storageServies)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(baseFolder))
                {
                    throw new ServiceValidationException("Invalid folder name for upload images");
                }


                var base64Array = base64img.Split(";base64,");
                if (base64Array.Length < 1)
                {
                    return "";
                }

                base64img = base64Array[1];
                var fileName = $"{Guid.NewGuid()}{"Logo.png"}".Replace("-", string.Empty);


                byte[] bytes = Convert.FromBase64String(base64img);
                var memory = new MemoryStream(bytes);


                var s3obj = new S3Object()
                {
                    BucketName = baseFolder,
                    InputStream = memory,
                    Name = fileName
                };

                var cond = new AwsCredentials()
                {
                    AwsKey = AppSettingsHelper.Settings("AwsConfiguration:AWSAccessKey"),
                    AwsSecretKey = AppSettingsHelper.Settings("AwsConfiguration:AWSSecretKey")
                };

                var result = await _storageServies.UplodeFileAsync(s3obj, cond);

                var url = $@"{baseFolder}\{fileName}";

                return url;




            }
            catch (Exception ex)
            {
                throw new ServiceValidationException(ex.Message);
            }
        }



    }
}
