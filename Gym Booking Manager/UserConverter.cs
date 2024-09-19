using Gym_Booking_Manager;
using Newtonsoft.Json;
using System;

public class UserConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(User);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jsonObject = Newtonsoft.Json.Linq.JObject.Load(reader);
        var userType = jsonObject["UserType"]?.ToString();  

        if (string.IsNullOrEmpty(userType))
        {
            throw new JsonSerializationException("UserType is missing or null in the JSON data.");
        }


        var name = jsonObject["Name"]?.ToString();
        var email = jsonObject["Email"]?.ToString();
        var phone = jsonObject["PhoneNumber"]?.ToString();

        User user;

        if (userType == nameof(Customer))
        {
            bool isMember = jsonObject["IsMember"]?.ToObject<bool>() ?? false;
            user = new Customer(name, email, phone, isMember);
        }
        else if (userType == nameof(Coach))
        {
            user = new Coach(name, email, phone);
        }
        else
        {
            throw new NotImplementedException($"Type {userType} is not supported");
        }

        serializer.Populate(jsonObject.CreateReader(), user);
        return user;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var user = (User)value;
        var jsonObject = Newtonsoft.Json.Linq.JObject.FromObject(user, serializer);
        jsonObject.AddFirst(new Newtonsoft.Json.Linq.JProperty("UserType", user.GetType().Name));
        jsonObject.WriteTo(writer);
    }
}

