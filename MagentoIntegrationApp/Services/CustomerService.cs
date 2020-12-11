using Geocoding.Yahoo;
using MagentoIntegrationApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MagentoIntegrationApp.Services
{
    public class CustomerService : ICustomer
    {

        private string baseUrl;
        private string consumerKey;
        private string consumerSecret;
        private string accessToken;
        private string tokenSecret;
        string sign;
        string oauth_sign;
        string oauth_nonce;
        string oauth_timeStamp;
        // private HttpClient _client;
        public CustomerService(IConfiguration config)
        {
            baseUrl = config.GetValue<string>("EnvVariables:BaseURI");
            consumerKey= config.GetValue<string>("EnvVariables:consumerKey");
            consumerSecret = config.GetValue<string>("EnvVariables:consumerSecret");
            accessToken = config.GetValue<string>("EnvVariables:accessToken");
            tokenSecret = config.GetValue<string>("EnvVariables:tokenSecret");

        }
        public async Task<bool> Delete(int customerId)
        {
            throw new NotImplementedException();
        }


        
        public HttpClient createClient()
        {
            string accessToken = "p8u4y59m8vtzaw7aznwzbbf85gf8fah8";
            string tokenSecret = "d1r02yc5ko06eyeaanht619276vtg0oq";
            string consumerKey = "9sbpacku1gj98zonh5jqumk6uchydydb";
            string consumerSecret = "62551lhv0h83bech9txqvdcn1c09lgub";
            string credentials = String.Format("{0}:{1}:{2}:{3}", consumerKey, consumerSecret, accessToken, tokenSecret);


            /////get sign
            ///
            var strUri = "http://dev-warmglass.focallabs.co.uk/V1/customers/1";
            var uri = new Uri(strUri);
            string url, param;
            var oAuth = new OAuthBase();
            var nonce = oAuth.GenerateNonce();
            var timeStamp = oAuth.GenerateTimeStamp();


            var signature = oAuth.GenerateSignature(uri, consumerKey,
                consumerSecret, string.Empty, string.Empty, "GET", timeStamp, nonce,
                OAuthBase.SignatureTypes.HMACSHA1, out url, out param);

            sign = string.Format("{0}?{1}&oauth_signature={2}", url, param, signature);

            oauth_sign = "\"" + signature + "\"";
            oauth_nonce = "\"" + nonce + "\"";
            oauth_timeStamp = "\"" + timeStamp + "\"";

            /////get sign





           var client = new HttpClient();


           client.BaseAddress = new Uri("http://dev-warmglass.focallabs.co.uk/rest");

            return client;
            
            
        }

        public async Task GetAsync(int customerId)
        {


                var client = createClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "V1/customers/1");

                var formatted = string.Format("OAuth oauth_consumer_key=\"9sbpacku1gj98zonh5jqumk6uchydydb\",oauth_token=\"p8u4y59m8vtzaw7aznwzbbf85gf8fah8\",oauth_signature_method=\"HMAC-SHA1\",oauth_timestamp={0},oauth_nonce={1},oauth_version=\"1.0\",oauth_signature={2}", oauth_timeStamp, oauth_nonce, oauth_sign);
                request.Headers.Add("Authorization", formatted);
                var response = client.SendAsync(request);

                response.Wait();
                var result = response.Result;



                if (result.IsSuccessStatusCode)
                {

                    //I'm expecting this to return a customer object so that I can now convert to the desired format
                    var con = await result.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
                
                



           
            
        }




        public Task<bool> Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
