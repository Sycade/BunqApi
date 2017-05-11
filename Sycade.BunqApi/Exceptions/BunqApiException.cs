using Sycade.BunqApi.Model;
using System;

namespace Sycade.BunqApi.Exceptions
{
    public class BunqApiException : Exception
    {
        public string TranslatedMessage { get; set; }

        public BunqApiException(Error error)
            : base(error.ErrorDescription)
        {
            TranslatedMessage = error.ErrorDescriptionTranslated;
        }

        public BunqApiException(string message)
            : base(message) { }
    }
}
