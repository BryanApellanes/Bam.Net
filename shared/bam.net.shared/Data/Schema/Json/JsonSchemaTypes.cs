using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Bam.Net.Data.Schema.Json
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum JsonSchemaTypes
    {
        [EnumMember(Value = "null")]
        Null,

        [EnumMember(Value = "boolean")]
        Boolean,

        [EnumMember(Value = "object")]
        Object,

        [EnumMember(Value = "array")]
        Array,

        [EnumMember(Value = "number")]
        Number,

        [EnumMember(Value = "string")]
        String,

        [EnumMember(Value = "integer")]
        Integer
    }
}
