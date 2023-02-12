// namespace SparkPlug.Persistence.MongoDb;

// public class ObjectIdJsonConverter : JsonConverter<ObjectId>
// {
//     public override ObjectId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//     {
//         var value = reader.GetString();
//         return ObjectId.Parse(value);
//     }

//     public override void Write(Utf8JsonWriter writer, ObjectId value, JsonSerializerOptions options)
//     {
//         writer.WriteStringValue(value.ToString());
//     }
// }
