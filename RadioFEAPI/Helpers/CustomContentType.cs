using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Text;

namespace RadioFEAPI.Helpers
{
    public class CustomContentType : TextInputFormatter
    {
        public CustomContentType()
        {
            SupportedMediaTypes.Add("application/custom");
            SupportedEncodings.Add(Encoding.UTF8);
        }
        public override async  Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            try
            {
                // Create a reader for the context
                using var reader = context.ReaderFactory(context.HttpContext.Request.Body, encoding);
                
                // Read the Base&4 encoded string
                var raw = await reader.ReadToEndAsync();

                // Return null value if content is null
                if (string.IsNullOrEmpty(raw)) return await InputFormatterResult.NoValueAsync();

                // Convert the base64 string to bytes
                var bytes = Convert.FromBase64String(raw);
                // Convert the bytes to json string
                var convertedString = Encoding.UTF8.GetString(bytes);

                // Return json object deserialized to the model type of the context
                return InputFormatterResult.Success(JsonConvert.DeserializeObject(convertedString, context.ModelType));
            }
            catch (Exception e)
            {
                context.ModelState.TryAddModelError(string.Empty, e.Message);
                return await InputFormatterResult.FailureAsync();
            }
        }
    }
}
