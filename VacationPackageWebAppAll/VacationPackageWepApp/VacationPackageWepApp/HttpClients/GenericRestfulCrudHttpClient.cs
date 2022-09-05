using System.Net.Http.Json;
using System.Text.Json;
using Newtonsoft.Json;

namespace HttpClients;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// Credits to http://www.matlus.com/a-generic-restful-crud-httpclient/
/// <summary>
/// This class exposes RESTful CRUD functionality in a generic way, abstracting
/// the implementation and usage details of HttpClient, HttpRequestMessage,
/// HttpResponseMessage, ObjectContent, Formatters etc. 
/// </summary>
/// <typeparam name="T">This is the Type of Resource you want to work with, such as Customer, Order etc.</typeparam>
/// <typeparam name="TResourceIdentifier">This is the type of the identifier that uniquely identifies a specific resource such as Id or Username etc.</typeparam>
public class GenericRestfulCrudHttpClient<T, TResourceIdentifier> : IDisposable where T : class
{
    private bool disposed = false;
    private HttpClient httpClient;
    protected readonly string serviceBaseAddress;
    public string addressSuffix;
    private readonly string jsonMediaType = "application/json";

    /// <summary>
    /// The constructor requires two parameters that essentially initialize the underlying HttpClient.
    /// In a RESTful service, you might have URLs of the following nature (for a given resource - Member in this example):<para />
    /// 1. http://www.somedomain/api/members/<para />
    /// 2. http://www.somedomain/api/members/jdoe<para />
    /// Where the first URL will GET you all members, and allow you to POST new members.<para />
    /// While the second URL supports PUT and DELETE operations on a specifc member.
    /// </summary>
    /// <param name="serviceBaseAddress">As per the example, this would be "http://www.somedomain"</param>
    /// <param name="addressSuffix">As per the example, this would be "api/members/"</param>

    public GenericRestfulCrudHttpClient(string serviceBaseAddress, string addressSuffix)
    {
        this.serviceBaseAddress = serviceBaseAddress;
        this.addressSuffix = addressSuffix;
        httpClient = MakeHttpClient(serviceBaseAddress);
    }

    protected virtual HttpClient MakeHttpClient(string serviceBaseAddress)
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(serviceBaseAddress);
        httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(jsonMediaType));
        return httpClient;
    }

    public async Task<IEnumerable<T>> GetManyAsync()
    {
        var responseMessage = await httpClient.GetAsync(addressSuffix);
        responseMessage.EnsureSuccessStatusCode();
        return await responseMessage.Content.ReadAsAsync<IEnumerable<T>>();
    }
     
    public async Task<T> GetAsync(TResourceIdentifier identifier)
    {
        var responseMessage = await httpClient.GetAsync(addressSuffix + identifier.ToString());
        responseMessage.EnsureSuccessStatusCode();
        return await responseMessage.Content.ReadAsAsync<T>();
    }

    public async Task<TA?> PostAsync<TA>(T model)
    {
        var requestMessage = new HttpRequestMessage();
        var objectContent = CreateJsonObjectContent(model);
        var responseMessage = await httpClient.PostAsync(addressSuffix, objectContent);
        
        return await responseMessage.Content.ReadFromJsonAsync<TA>();
    }

    public async Task PutAsync(TResourceIdentifier identifier, T model)
    {
        var requestMessage = new HttpRequestMessage();
        var objectContent = CreateJsonObjectContent(model);
        var responseMessage = await httpClient.PutAsync(addressSuffix + identifier.ToString(), objectContent);
    }

    public async Task DeleteAsync(TResourceIdentifier identifier)
    {
        var r = await httpClient.DeleteAsync(addressSuffix + identifier.ToString());
    }

    private HttpContent CreateJsonObjectContent(T model)
    {
        MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
        HttpContent content = new ObjectContent<T>(model, jsonFormatter);
        return content;
    }

    #region IDisposable Members

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            if (httpClient != null)
            {
                var hc = httpClient;
                httpClient = null;
                hc.Dispose();
            }
            disposed = true;
        }
    }

    #endregion IDisposable Members
}
