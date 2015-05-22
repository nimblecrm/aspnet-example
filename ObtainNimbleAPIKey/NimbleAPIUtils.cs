using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ObtainNimbleAPIKey
{
    public class NimbleAuthServerDescription
    {
        public static Uri AuthorizationEndpoint = new Uri("https://api.nimble.com/oauth/authorize");
        public static Uri TokenEndpoint = new Uri("https://api.nimble.com/oauth/token");
    }

    public class NimbleAPIClient
    {
        public static string nimbleClientId = ConfigurationManager.AppSettings["nimbleClientId"];
        public static string nimbleSecret = ConfigurationManager.AppSettings["nimbleSecret"];
        public static string RedirectUri = ConfigurationManager.AppSettings["redirectUri"];


        public static string AuthorizationGrantCodeUri()
        {
            return String.Format(@"{0}?client_id={1}&redirect_uri={2}&response_type=code",
                               NimbleAuthServerDescription.AuthorizationEndpoint.OriginalString,
                               nimbleClientId,
                               RedirectUri
                              );
        }

        public static string AccessTokenUri(string grandCode)
        {
            return String.Format(@"client_id={0}&client_secret={1}&grant_type=authorization_code&redirect_uri={2}&code={3}",
                               nimbleClientId,
                               nimbleSecret,
                               RedirectUri,
                               grandCode
                            );
        }

        public static AccessToken RequestAccessToken(string grandCode)
        {
            byte[] dataStream = Encoding.UTF8.GetBytes(NimbleAPIClient.AccessTokenUri(grandCode));

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(NimbleAuthServerDescription.TokenEndpoint);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Accept = "application/json";
            webRequest.ContentLength = dataStream.Length;
            Stream newStream = webRequest.GetRequestStream();
            newStream.Write(dataStream, 0, dataStream.Length);
            newStream.Close();
            using (WebResponse webResponse = webRequest.GetResponse())
            {
                using (StreamReader loResponseStream = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    string jsonString = loResponseStream.ReadToEnd();
                    using (MemoryStream stream = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AccessToken));
                        return (AccessToken)serializer.ReadObject(stream);
                    }
                }
            }
        }
    }

    [DataContract]
    public class AccessToken
    {
        [DataMember]
        public string access_token { get; set; }
        [DataMember]
        public string expires_in { get; set; }
        [DataMember]
        public string refresh_token { get; set; }
    }
}