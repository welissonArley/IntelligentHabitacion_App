namespace Useful.ToTests.Builders.TokenController
{
    public class TokenControllerBuilder
    {
        private static TokenControllerBuilder _instance;
        private readonly Homuai.Application.Services.Token.TokenController _service;

        private TokenControllerBuilder()
        {
            if (_service == null)
                _service = new Homuai.Application.Services.Token.TokenController(4320, "Sk1ZUVBOQ1dRTlFJRVpNWkJQWFVPTFdNSUFTQVlURklIREFYUUNUSVBLUlVLRlRMVVJISkRFWE1GUE5ES1NMUldMUE5PSlhGVU9BR1RVU09NQlpRRldETE1RT1hLQlBITkZXRFBTSUw=");
        }

        public static TokenControllerBuilder Instance()
        {
            _instance = new TokenControllerBuilder();
            return _instance;
        }

        public Homuai.Application.Services.Token.TokenController Build()
        {
            return _service;
        }
    }
}
