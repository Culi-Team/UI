using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EQX.UI.Converters
{
    public class RectangleConverter : JsonConverter<Rectangle>
    {
        public override Rectangle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var document = JsonDocument.ParseValue(ref reader);
            var root = document.RootElement;

            int x = GetInt32OrDefault(root, "X");
            int y = GetInt32OrDefault(root, "Y");
            int width = GetInt32OrDefault(root, "Width");
            int height = GetInt32OrDefault(root, "Height");

            if (root.TryGetProperty("Location", out var location))
            {
                x = GetInt32OrDefault(location, "X");
                y = GetInt32OrDefault(location, "Y");
            }

            if (root.TryGetProperty("Size", out var size))
            {
                width = GetInt32OrDefault(size, "Width");
                height = GetInt32OrDefault(size, "Height");
            }

            return new Rectangle(x, y, width, height);
        }

        public override void Write(Utf8JsonWriter writer, Rectangle value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("X", value.X);
            writer.WriteNumber("Y", value.Y);
            writer.WriteNumber("Width", value.Width);
            writer.WriteNumber("Height", value.Height);
            writer.WriteEndObject();
        }

        private static int GetInt32OrDefault(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out var property) && property.TryGetInt32(out var value))
            {
                return value;
            }

            return 0;
        }
    }
}
