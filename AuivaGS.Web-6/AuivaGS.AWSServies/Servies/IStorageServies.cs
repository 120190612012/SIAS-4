using AuivaGS.AWSServies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuivaGS.AWSServies.Servies
{
    public interface IStorageServies
    {
        Task<S3ResponesDTO> UplodeFileAsync(S3Object s3obj, AwsCredentials awsCredentials);
    }
}
