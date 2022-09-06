# Sooner Card Cust Info

##DEPRICATED APPLICATION - NO LONGER SUPPORTED

Sooner Card Customer Info is an application that displays proprietary Transact customer information by sending an assigned ISO ID through REST API requests. Using either manual entry or an MRD5 credential reader.

This console application is meant as a framework for development using Transact Campus HTTP RESTful APIs and OAuth 1.0a. This application is a testing application and is not designed for deployment in a production environment.

This application is developed using **C# with .NET Core 3.1** primarily on Visual Studio for MacOS.

### Features of this application include:
***1) Different ways to display customer data including:***

   Basic customer info with first name, last name, customer number, email and a timestamp.
   
   Basic customer info with stored value account amounts.
   
   The ability to save customer info to a .csv file in a path that is specified.

   Basic customer info with a list of door access plans assigned to the customer.
   

Selection Menu:
```C#
**************************************************************
      Sooner Card Customer Info Tool                          
**************************************************************
      Display customer info in console                     : 1
      Display customer info in console with stored values  : 2
      Save customer info to .csv file                      : 3
      Display Customer Door Access Plans                   : 4
      Exit Application                                     : 5
Select an option: 
```
**Note that when a card is tapped or number is entered the number is masked for security.**

Basic Output:

```C#
Tap SoonerCard, Phone or Watch on Reader
                                                                                                                          
Sooner 52 CardTest
113456789
2/23/2021 3:17:20 PM
```
Stored Value Output:
```C#
Tap SoonerCard, Phone or Watch on Reader
                                                                                                                          
Sooner 52 CardTest
113458256
2/23/2021 3:30:48 PM

Sooner Sense Balance: $0
```

Door Access Output:

```C#
Tap SoonerCard, Phone or Watch on Reader
                                                                                                                          
Test User | 113456789

General Norman Student Access
General Norman Staff Access

Customer has 2 door access plans
```
***2) OAuth 1.0a one-legged authentication using [RestSharp](https://restsharp.dev/)***

The **TempTokenRequest class** is how an OAuth 1.0a request token is generated and saved in the instance of the class. Similar methods are used for the rest of the token generation and authentication in the application.
```C#
public bool TempTokenRequestInit()
        {
            var getSecrets = new ConfigBuilder();
            var getAPIVars = new APIGlobalVars();

            //RestClient setup using Temp Token specific URI
            var client = new RestClient($"https://{APIGlobalVars.HostName}/transact/api/initiate");

            client.Timeout = -1;

            //Setups REST API request
            var request = new RestRequest(Method.POST);

            //Passes in Consumer Key and Secret Key and then magically generates the rest of the auth blueprint
            var tempSignature = OAuth1Authenticator.ForRequestToken(getSecrets.OAuthConsumerKey, getSecrets.OAuthSecretKey, RestSharp.Authenticators.OAuth.OAuthSignatureMethod.HmacSha1);

            request.AddParameter("text/plain", "", ParameterType.RequestBody);

            //Does the authentication passthrough
            tempSignature.Authenticate(client, request);

            //Executes the API Request
            IRestResponse tempResponse = client.Execute(request);

            try
            {
                if (tempResponse.Content != "")
                {
                    //Make exception clause if not responding
                    TempToken = tempResponse.Content.Split("&")[0].Split("=")[1];
                    TempTokenSecret = tempResponse.Content.Split("&")[1].Split("=")[1];

                    return true;
                    //For debugging purposes, remove later
                    //Console.WriteLine($"Temp Token: {TempToken}\n\rTemp Secret: {TempTokenSecret}");
                }
                else
                {
                    return false;
                }
            }
            catch (SystemException)
            {
                return false;
            }
        }
```

## Getting Started

In the appsettings.json file put your OAuth API credentials from Transact which can be found in the app server web management portal:
```C#
{
  "APIConnect": {
    "OAuthSecretKey": "**************************************",
    "OAuthConsumerKey": "**************************************",
    "InstitutionRouteId": "**************************************",
    "MerchantAuthorization": "**************************************"
  }
}
```
In the APIGlobalVars class put in your application server hostname **without** "https://" included at the beginning:
```C#
 public class APIGlobalVars
    {
        public readonly static string HostName = "hostname";

        public static string IncludeImageYes = "Include";

        public static string IncludeImageNo = "Exclude";

        public APIGlobalVars()
        {
        }
    }
```

**Depending on the version of Transact that is being used API URIs might need to be modified, as of this time the URIs are stored per API request or authorization class.**

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change. This is meant to be a collaborative effort and improving the code is welcome.

## Authors and Acknowledgment
### Primary Developer: Edward Reali
