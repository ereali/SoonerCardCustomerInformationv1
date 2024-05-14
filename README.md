# Sooner Card Customer Information Console Application

## DEPRECATED APPLICATION - NO LONGER SUPPORTED

**Sooner Card Customer Info** is a proof-of-concept application developed by Edward Reali. It displays Transact Campus customer information by sending an assigned ISO ID through RESTful API requests, using either manual entry or a designated credential reader.

### Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Setup](#setup)
- [Usage](#usage)
- [Code Overview](#code-overview)
- [Contributing](#contributing)
- [Authors and Acknowledgments](#authors-and-acknowledgments)
- [License](#license)

### Features

1. **Display Customer Data**:
    - Basic customer info: first name, last name, customer number, email, timestamp.
    - Customer info with stored value account amounts.
    - Save customer info to a `.csv` file.
    - List of door access plans assigned to the customer.

2. **Selection Menu**:
    ```plaintext
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

3. **Security**:
    - Masked card numbers for security when entered manually.

### Technologies Used

- **C#** with **.NET Core 3.1**
- **Visual Studio** (primarily for MacOS)
- **RestSharp** for HTTP requests and OAuth 1.0a authentication

### Setup

1. **Clone the Repository**:
    ```bash
    git clone https://github.com/ereali/SoonerCardCustomerInformationv1.git
    cd SoonerCardCustomerInformationv1
    ```

2. **Configuration**:
    - Update `appsettings.json` with your OAuth API credentials from Transact:
      ```json
      {
        "APIConnect": {
          "OAuthSecretKey": "**************************************",
          "OAuthConsumerKey": "**************************************",
          "InstitutionRouteId": "**************************************",
          "MerchantAuthorization": "**************************************"
        }
      }
      ```

    - Update `APIGlobalVars` with your application server hostname:
      ```csharp
      public class APIGlobalVars
      {
          public readonly static string HostName = "hostname";
          public static string IncludeImageYes = "Include";
          public static string IncludeImageNo = "Exclude";
      }
      ```

3. **Build and Run**:
    ```bash
    dotnet build
    dotnet run
    ```

### Usage

1. **Starting the Application**:
    - Run the console application.
    - Follow the on-screen prompts to enter ISO IDs or tap the SoonerCard on the reader.

2. **Example Outputs**:
    - Basic Info:
      ```plaintext
      Tap SoonerCard, Phone or Watch on Reader

      Sooner 52 CardTest
      113456789
      2/23/2021 3:17:20 PM
      ```
    - Stored Value Info:
      ```plaintext
      Sooner Sense Balance: $0
      ```
    - Door Access Info:
      ```plaintext
      Test User | 113456789

      General Norman Student Access
      General Norman Staff Access

      Customer has 2 door access plans
      ```

### Code Overview

#### OAuth 1.0a Authentication
- **TempTokenRequestInit Method**:
  - Initializes an OAuth 1.0a request token.
  - Example snippet:
    ```csharp
    public bool TempTokenRequestInit()
    {
        var getSecrets = new ConfigBuilder();
        var getAPIVars = new APIGlobalVars();
        var client = new RestClient($"https://{APIGlobalVars.HostName}/transact/api/initiate");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        var tempSignature = OAuth1Authenticator.ForRequestToken(getSecrets.OAuthConsumerKey, getSecrets.OAuthSecretKey, RestSharp.Authenticators.OAuth.OAuthSignatureMethod.HmacSha1);
        request.AddParameter("text/plain", "", ParameterType.RequestBody);
        tempSignature.Authenticate(client, request);
        IRestResponse tempResponse = client.Execute(request);

        try
        {
            if (tempResponse.Content != "")
            {
                TempToken = tempResponse.Content.Split("&")[0].Split("=")[1];
                TempTokenSecret = tempResponse.Content.Split("&")[1].Split("=")[1];
                return true;
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

### Contributing

**Note**: This application is no longer supported. However, contributions are still welcome. For major changes, please open an issue first to discuss what you would like to change.

### Authors and Acknowledgments

- **Primary Developer**: Edward Reali

### License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
