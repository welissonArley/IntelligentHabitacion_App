using Homuai.App.Services.Communication;
using Homuai.Communication.Error;
using Homuai.Exception;
using Homuai.Exception.ExceptionsBase;
using Newtonsoft.Json;
using Refit;
using System.Linq;
using System.Net.Http.Headers;

namespace Homuai.App.UseCases
{
    public class UseCaseBase
    {
        private readonly StringWithQualityHeaderValue _language = new StringWithQualityHeaderValue(System.Globalization.CultureInfo.CurrentCulture.ToString());
        private readonly string _baseAddres;

        public UseCaseBase(string controller)
        {
            _baseAddres = $"https://{RestEndPoints.BaseUrl}/api/v1/{controller}";
        }

        protected StringWithQualityHeaderValue GetLanguage()
        {
            return _language;
        }

        protected string BaseAddress()
        {
            return _baseAddres;
        }

        protected string GetTokenOnHeaderRequest(HttpResponseHeaders headers)
        {
            return headers.Contains("Tvih") ? headers.GetValues("Tvih")?.First() : null;
        }

        protected void ResponseValidate(IApiResponse responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorJson = JsonConvert.DeserializeObject<ErrorJson>(responseMessage.Error.Content);
                switch (responseMessage.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest:
                        {
                            throw new ResponseException
                            {
                                Exception = new ErrorOnValidationException(errorJson.Errors)
                            };
                        }
                    case System.Net.HttpStatusCode.NotFound:
                        {
                            throw new ResponseException
                            {
                                Exception = new NotFoundException(errorJson.Errors[0])
                            };
                        }
                    case System.Net.HttpStatusCode.Unauthorized:
                        {
                            if (errorJson.ErrorCode == ErrorCode.TokenExpired)
                                throw new TokenExpiredException();

                            throw new ResponseException
                            {
                                Exception = new HomuaiException(errorJson.Errors[0])
                            };
                        }
                    default:
                        {
                            throw new ResponseException
                            {
                                Exception = new HomuaiException(ResourceTextException.UNKNOW_ERROR)
                            };
                        }
                }
            }
        }
    }
}
