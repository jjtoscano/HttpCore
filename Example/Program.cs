using System;
using System.Threading.Tasks;
using Example.Twitter;
using Example.Twitter.Entities;
using HttpCore.Entities;

namespace Example
{
    class Program
    {
        private const string CONSUMER_KEY = "A";

        private const string SECRET_KEY = "A";

        static void Main(string[] args)
        {
            AsyncMain().Wait();
        }

        static async Task AsyncMain()
        {
            try
            {
                Console.WriteLine("Sending oauth request for twitter...");

                OAuthToken token = await TwitterService.OAuth(CONSUMER_KEY, SECRET_KEY);

                Console.WriteLine($"OAuth success: {token.Token}");
            }
            catch (HttpServiceException<ErrorsList> exTwitter)
            {
                Console.WriteLine("Http service exception:");
                exTwitter.Error.Errors.ForEach(error => Console.WriteLine($"Label:{error.Label} Error: {error.Message} StatusCode: {error.Code}"));
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Unexpected error: {ex.ToString()}");
            }
        }
    }
}
