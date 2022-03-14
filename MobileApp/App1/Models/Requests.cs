using System;
using System.Text.Json;

namespace App.Models
{
    public class Response
    {
        public Response() { }
        public Response(object errors = null, string parameter = null, string token = null)
        {
            if (errors is string[])
            {
                Errors = errors as string[];
            }
            else if (errors is string)
            {
                Errors = new string[] { errors as string };
            }
            else
            {
                Errors = Array.Empty<string>();
            }
            Parameter = parameter;
            NewAuthToken = token;
        }

        public string[] Errors { get; init; }
        public string Parameter { get; init; }
        public string NewAuthToken { get; init; }
    }


    public abstract class Request
    {
        public string Type { get; init; }

        public abstract string ToJson();
    }

    public class SignUpRequest : Request
    {
        public SignUpRequest(string username, string password)
        {
            Type = nameof(SignUpRequest);
            Username = username ?? string.Empty;
            Password = password ?? string.Empty;
        }
        public string Username { get; init; }
        public string Password { get; init; }

        public override string ToJson() => JsonSerializer.Serialize(this);
    }

    public class SignInRequest : Request
    {
        public SignInRequest(string username, string password)
        {
            Type = nameof(SignInRequest);
            Username = username ?? string.Empty;
            Password = password ?? string.Empty;
        }
        public string Username { get; init; }
        public string Password { get; init; }

        public override string ToJson() => JsonSerializer.Serialize(this);
    }

    public class GetCustomerInfoRequest : Request
    {
        public GetCustomerInfoRequest()
        {
            Type = nameof(GetCustomerInfoRequest);
        }

        public override string ToJson() => JsonSerializer.Serialize(this);
    }

    public class UpdateCustomerInfoRequest : Request
    {
        public UpdateCustomerInfoRequest(CustomerInfo customerInfo)
        {
            Type = nameof(UpdateCustomerInfoRequest);
            CustomerInfo = customerInfo;
        }
        public CustomerInfo CustomerInfo { get; init; }

        public override string ToJson() => JsonSerializer.Serialize(this);
    }

    public class GetRecommendationsRequest : Request
    {
        public GetRecommendationsRequest(Field[] targetFields)
        {
            Type = nameof(GetRecommendationsRequest);
            TargetFields = targetFields;
        }
        public GetRecommendationsRequest(Field targetField)
                : this(new Field[] { targetField }) { }

        public Field[] TargetFields { get; init; }

        public override string ToJson() => JsonSerializer.Serialize(this);
    }
}
