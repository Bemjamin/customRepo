public class AddJsonSerializer
    {
        private static readonly JsonSerializerSettings _settings;
        private static readonly JsonSerializer _serializer;

        static AddJsonSerializer()
        {
            _settings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
            };
            _serializer = JsonSerializer.Create(_settings);
        }

        public static void Serialize(TextWriter writer, Object data)
        {
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                _serializer.Serialize(jsonWriter, data);
            }
            
        }

        public static string SerializeObject(Object obj)
        {
            return JsonConvert.SerializeObject(obj, _settings);
        }
    }