using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AkamaiOpen;
using AkamaiOpen.Exception;

namespace AkamaiOpen
{
    public static class CredentialFactory
    {
        public static Credential CreateFromEnvironment() {
            string clientToken = "";
            string accessToken = "";
            string clientSecret = "";

            try {
                clientToken = Environment.GetEnvironmentVariable("AKAMAI_CLIENT_TOKEN");
                accessToken = Environment.GetEnvironmentVariable("AKAMAI_ACCESS_TOKEN");
                clientSecret = Environment.GetEnvironmentVariable("AKAMAI_CLIENT_SECRET");

            } catch(System.Exception SecurityException){
                Console.Write(SecurityException.Message);
            }

           return new Credential(clientToken, accessToken, clientSecret); 
        }

        public static Credential CreateFromEdgeRcFile(string path, string section = "default") {
            
            string clientToken = "";
            string accessToken = "";
            string clientSecret = ""; 

            if(File.Exists(path)) 
            {

            }else{
                
            }  
           return new Credential(clientToken, accessToken, clientSecret); 
        }  

        private static Dictionary<string,string> parseEdgeRcFile(string path, string section = "default"){
            
            string clientToken = "";
            string accessToken = "";
            string clientSecret = ""; 
            Dictionary<string, string> parsedCredentials = new Dictionary<string, string>();
            
            string[] edgeRcFileArray = System.IO.File.ReadAllLines(path);
            string sectionToBeSearched = "[" + section + "]";
            int numberOfRemainingArgs = 4;
            bool isSectionFound = false;

            if(edgeRcFileArray.Length > 0){
                foreach(string line in edgeRcFileArray)  {
                    if (sectionToBeSearched.Equals(line.Trim()) && !isSectionFound){
                        isSectionFound = true;
                    }

                    if(numberOfRemainingArgs > 0 && isSectionFound){
                       --numberOfRemainingArgs;
                       addUniqueCredentialProperty(ref parsedCredentials, line, numberOfRemainingArgs);
                    }                   
                }
            }
            return parsedCredentials; 
        }

        private static void addUniqueCredentialProperty(ref Dictionary<string, string> credentialParams, string line, int numberOfRemainingArgs){
            
            string[] propertiesToFind = new string[] { "client_secret", "host", "access_token", "client_token" }; 
            string trimmedLine = line.Trim().Replace(" ","");
            string key = "";
            string value = "";
            string textToMatch = "";

            foreach(string property in propertiesToFind) {
                
                textToMatch = property + "=";
                
                if(trimmedLine.Contains(textToMatch)){
                    key = property;
                    value = trimmedLine.Split(textToMatch)[1];
                    credentialParams.Add(key,value);
                }
            }

            if(numberOfRemainingArgs == 1 && credentialParams.Count < 4){
                //throw exception
            }

        }

        private static void validateCredentialParams (Dictionary<string, string> credentialProperty){
            
            string[] propertyToValidate = new string[] { "client_secret", "host", "access_token", "client_token" }; 
            
            foreach( string property in propertyToValidate) {
                if (credentialProperty.ContainsKey(property)) {
                    if(string.IsNullOrWhiteSpace(credentialProperty[property])){
                        //throw exception
                    }
                }else{
                        //throw exception
                    throw new CredentialPropertyNotFoundException("Error: "+ property + " could not be loaded from the edgerc file. Possible problems: syntax error, unexpected property inside the section or missing property inside file");
                }
            }
        }

    }
}