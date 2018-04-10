using System;
using AkamaiOpen;
using AkamaiOpen.Authentication;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace AkamaiOpen
{
    public class test
    {
        private static readonly HttpClient client = new HttpClient();

        public static void Main(string[] args){
		ProcessRepositories().Wait();

        string ClientToken = "lala";
		string AccessToken = "mema";
		string ClientSecret = "dej9d";        
        
    
        AkamaiOpen.Authentication.Credential credentialEnv = AkamaiOpen.Authentication.CredentialFactory.CreateFromEnvironment();
        AkamaiOpen.Authentication.Credential credentialFile = AkamaiOpen.Authentication.CredentialFactory.CreateFromEdgeRcFile("default", "/Users/miguel.chang/Documents/AkamaiOpen/auth.edgerc");
        AkamaiOpen.Authentication.Credential credential = new AkamaiOpen.Authentication.Credential(ClientToken, AccessToken, ClientSecret);

            bool areCredentialsValid = credential.isValid;
            var serializer = new DataContractJsonSerializer(typeof(AkamaiOpen.Authentication.Credential));

            Console.Write("Press any key to continue...");
            Console.ReadKey(true);

        }

        private static async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            var msg = await stringTask;
            Console.Write(msg);
        }

    }
}
