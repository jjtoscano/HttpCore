# HttpCore

HttpCore is a service layer that implements an HTTP client (HttpClient). This service layer facilitates and accelerates the use of the .NET http client to consume and integrate different APIs.

### Installation

HttpCore is available via:

* NuGet Official Releases: [![NuGet](https://img.shields.io/nuget/vpre/HttpCore.svg?label=NuGet)](https://www.nuget.org/packages/HttpCore)

```
Install-Package HttpCore -Version 0.9.0
```

## Supported platforms

| Platform  | Version       |
| ------------- | ------------- |
| .NET Core  | 2.0  |
| .NET Framework | 4.6.1  |
| Mono  | 5.4  |
| Xamarin.IOS  | 10.14  |
| Xamarin.Mac  | 3.8  |
| Xamarin.Android  | 8.0  |
| Universal Windows Platform  | 10.0.16299 |
| Unity  | 2018.1  |

## Built With

* .NET Standard 2.0
* Newtonsoft.Json (>= 12.0.1)

## Getting Started

Once you have added the package to your project, you just have to instantiate the HttpService class and use one of the following methods:

- **SendAsync<T<T>>**. This method is designed to make **GET**, **POST**, **PUT**, **DELETE** requests.

- **SendObjectAsync<T<T>>**. This method is intended for requests in which data has to be sent (**POST**, **PUT**, **PATCH**).

The generic T that is included in the call represents the type of the error entity that is going to capture our HttpService instance. For this, the **HttpServiceException** type is provided to capture exceptions produced in the HttpService layer.

### Example SendAsync to get a oAuth token from twitter API

```C#
using(HttpService httpService = new HttpService())
{
    if (!string.IsNullOrEmpty(url))
    {
        ...
        try
        {
          var response = await httpService.SendAsync<ErrorsList>(CustomHttpMethod.Post, url, "application/x-www-form-urlencoded", param, headers);

          if (!string.IsNullOrEmpty(response))
          {
              oAuthToken = JsonConvert.DeserializeObject<OAuthToken>(response);
          }
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
```

### Example SendObjectAsync

```C#
...
public class Request
{
    [JsonProperty("question")]
    public string Question { get; set; }

    [JsonProperty("top")]
    public int Top { get; set; }
}
...
using(HttpService httpService = new HttpService())
{
    if (!string.IsNullOrEmpty(url))
    {
        ...
        try
        {
            var request = new Request { Question = "Test", Top = 5 };
            var response = await httpConnector.SendObjectAsync<ErrorResponse>(CustomHttpMethod.Post, url, request, "application/json", headers);

            if (!string.IsNullOrEmpty(response))
            {
                qnaResponse = JsonConvert.DeserializeObject<Response>(response);
            }
        }
        catch (HttpServiceException<ErrorResponse> apiEx)
        {
            Console.WriteLine("Http service exception:");
            apiEx.Error.Errors.ForEach(error => Console.WriteLine($"Label:{error.Label} Error: {error.Message} StatusCode: {error.Code}"));
        }
        catch(Exception ex) 
        {
            Console.WriteLine($"Unexpected error: {ex.ToString()}");
        }
    }
}
```

## Authors

* **José Juan Toscano Cuenca** - *Initial work* - [jjtoscano](https://github.com/jjtoscano)

See also the list of [contributors](https://github.com/jjtoscano/HttpCore/contributors) who participated in this project.

## License

MIT License

Copyright (c) 2019 jjtoscano

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.