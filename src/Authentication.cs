using System;
using AkamaiOpen;

namespace AkamaiOpen
{
    public class Authentication
    {
        
        public static void Main(){
		
        string ClientToken = "lala";
		string AccessToken = "mema";
		string ClientSecret = "dej9d";        
        
        Credential credential = new Credential(ClientToken, AccessToken, ClientSecret);

            bool areCredentialsValid = credential.isValid;

            Console.Write("Press any key to continue...");
            Console.ReadKey(true);


        } 

    }
}
