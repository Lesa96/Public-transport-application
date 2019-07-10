using BraintreeHttp;
using PayPalCheckoutSdk.Core;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace PayPal
{
    public class PayPalClient
    {
        /**
            Set up PayPal environment with sandbox credentials.
            In production, use LiveEnvironment.
         */
        public static PayPalEnvironment environment()
        {
            return new SandboxEnvironment("AcL_IC6Ezb4musJXtokXFakf3D-vBorL94XRPhhIdS3j-C8GLs75EH7MBmaFTsxLKssFRWklwWzheClY",
                                          "EDuzXHPIBJ2hpSP3RSKC9YiVICCX9e3Lvim5QeYZbUMHPxQZoXizl_DoygHk_NHlF68gzcXrORBa6Fqb");
        }

        /**
            Returns PayPalHttpClient instance to invoke PayPal APIs.
         */
        public static HttpClient client()
        {
            return new PayPalHttpClient(environment());
        }

        public static HttpClient client(string refreshToken)
        {
            return new PayPalHttpClient(environment(), refreshToken);
        }

        /**
            Use this method to serialize Object to a JSON string.
        */
        public static String ObjectToJSONString(Object serializableObject)
        {
            MemoryStream memoryStream = new MemoryStream();
            var writer = JsonReaderWriterFactory.CreateJsonWriter(
                        memoryStream, Encoding.UTF8, true, true, "  ");
            DataContractJsonSerializer ser = new DataContractJsonSerializer(serializableObject.GetType(),
                                                                            new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true });
            ser.WriteObject(writer, serializableObject);
            memoryStream.Position = 0;
            StreamReader sr = new StreamReader(memoryStream);
            return sr.ReadToEnd();
        }
    }
}