using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using AuivaGS.AWSServies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuivaGS.AWSServies.Servies
{
    public class StorageServies : IStorageServies
    {
        public async Task<S3ResponesDTO> UplodeFileAsync(S3Object s3obj, AwsCredentials awsCredentials)
        {
            //Adding AWS credentials
            var credentials = new BasicAWSCredentials(awsCredentials.AwsKey, awsCredentials.AwsSecretKey);

            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.EUWest3,
            };

            var response = new S3ResponesDTO();

            try
            {
                var uploadRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = s3obj.InputStream,
                    Key = s3obj.Name,
                    BucketName = s3obj.BucketName,
                    CannedACL = S3CannedACL.NoACL
                };

                using var client = new AmazonS3Client(credentials, config);

                var transferUtility = new TransferUtility(client);

                await transferUtility.UploadAsync(uploadRequest);

                response.StatusCode = 200;

                response.Message = $"{s3obj.Name} has been uploaded succesfully";
            }
            catch (AmazonS3Exception ex)
            {

                response.StatusCode = (int)ex.StatusCode;

                response.Message = ex.Message;
            }
            catch (Exception e)
            {
                response.StatusCode = 500;

                response.Message = e.Message;
            }
            return response;
        }
    }
}
