using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Authenticators.OAuth2;

namespace AIImageRevitPlugin
{
    internal class StableDiffusionAPIConnector
    {
        public string prompt = "default prompt";
        public string name;
        public string filePath;
        public RestClient client;

        public StableDiffusionAPIConnector(string prompt, string name)
        {
            var fileName = "exported_image.jpg";
            this.prompt = prompt;
            this.name = name;
            this.filePath = Environment.CurrentDirectory + @"\Data\" + fileName;

            var authenticatior = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            var options = new RestClientOptions("https://api.twitter.com/1.1")
            {
                Authenticator = authenticatior,
            };
            client = new RestClient(options);


        }

        public void fetchImageToImage()
        {

            Console.WriteLine("Succesfully Configured API Connection");
        }
    }
}
}
