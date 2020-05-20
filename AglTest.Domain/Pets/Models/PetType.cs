﻿using System.Text.Json.Serialization;

namespace AglTest.Domain.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PetType
    {
        Cat, Dog, Fish
    }
}