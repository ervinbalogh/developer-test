using System.Xml.Serialization;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Helpers
{
    public static class Serializer
    {
        public static string SerializeToXml(VatRegistrationRequestModel request)
        {
            using (var stringWriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(VatRegistrationRequestModel));
                serializer.Serialize(stringWriter, request);
                return stringWriter.ToString();
            }
        }
    }
}
