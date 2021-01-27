using System;
using Newtonsoft.Json;

namespace ISOParse.API.APIObjects
{
    public class CustVerifyRoot
    {
        [JsonProperty("customerVerify")]
        public CustomerVerify CustomerVerify { get; set; }
    }

    public class Card
    {
        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("lastModifiedDateTimeUtc")]
        public DateTime LastModifiedDateTimeUtc { get; set; }

        [JsonProperty("lostIndicator")]
        public bool LostIndicator { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Person
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("gender")]
        public int Gender { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }
    }

    public class Customer
    {
        [JsonProperty("activeEndDate")]
        public DateTime ActiveEndDate { get; set; }

        [JsonProperty("activeIndicator")]
        public bool ActiveIndicator { get; set; }

        [JsonProperty("card")]
        public Card Card { get; set; }

        [JsonProperty("customerNumber")]
        public string CustomerNumber { get; set; }

        [JsonProperty("openDateTime")]
        public DateTime OpenDateTime { get; set; }

        [JsonProperty("person")]
        public Person Person { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("primaryEmailAddress")]
        public string PrimaryEmailAddress { get; set; }
    }

    public class Identification
    {
        [JsonProperty("method")]
        public string Method { get; set; }
    }

    public class Instrument
    {
        [JsonProperty("identification")]
        public Identification Identification { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Customer2
    {
        [JsonProperty("instrument")]
        public Instrument Instrument { get; set; }
    }

    public class ResponseOptions
    {
        [JsonProperty("image")]
        public string Image { get; set; }
    }

    public class Identification2
    {
        [JsonProperty("method")]
        public string Method { get; set; }
    }

    public class Terminal
    {
        [JsonProperty("identification")]
        public Identification2 Identification { get; set; }
    }

    public class CustomerVerifyRequest
    {
        [JsonProperty("customer")]
        public Customer2 Customer { get; set; }

        [JsonProperty("responseEchoLevel")]
        public string ResponseEchoLevel { get; set; }

        [JsonProperty("responseOptions")]
        public ResponseOptions ResponseOptions { get; set; }

        [JsonProperty("terminal")]
        public Terminal Terminal { get; set; }
    }

    public class Error
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Operation
    {
        [JsonProperty("customerVerifyRequest")]
        public CustomerVerifyRequest CustomerVerifyRequest { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }

        [JsonProperty("requestTimestampUtc")]
        public DateTime RequestTimestampUtc { get; set; }

        [JsonProperty("responseTimestampUtc")]
        public DateTime ResponseTimestampUtc { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }
    }

    public class Institution
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("requestScheme")]
        public string RequestScheme { get; set; }

        [JsonProperty("requestValue")]
        public string RequestValue { get; set; }
    }

    public class Merchant
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("requestValue")]
        public string RequestValue { get; set; }
    }

    public class Route
    {
        [JsonProperty("institution")]
        public Institution Institution { get; set; }

        [JsonProperty("merchant")]
        public Merchant Merchant { get; set; }
    }

    public class CustomerVerify
    {
        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("operation")]
        public Operation Operation { get; set; }

        [JsonProperty("route")]
        public Route Route { get; set; }
    }

}
