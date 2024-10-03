namespace GestionPedidosAPIREST.Services
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.Json;
    using Microsoft.IdentityModel.Tokens;

    public class JwtServices
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtServices(IConfiguration configuration)
        {
            _secretKey = configuration.GetValue<string>("Jwt:Key");
            _issuer = configuration.GetValue<string>("Jwt:Issuer");
            _audience = configuration.GetValue<string>("Jwt:Audience");
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crear el payload y headers manualmente
            var header = new Dictionary<string, object>
            {
                { "alg", "HS256" },
                { "typ", "JWT" }
            };

            var payload = claims.ToDictionary(c => c.Type, c => (object)c.Value);

            // Serializar usando System.Text.Json
            var headerJson = JsonSerializer.Serialize(header);
            var payloadJson = JsonSerializer.Serialize(payload);

            // Codificar el encabezado y el contenido en Base64
            var headerBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(headerJson));
            var payloadBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(payloadJson));

            // Crear la firma usando el algoritmo HMAC-SHA256
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey)))
            {
                var signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes($"{headerBase64}.{payloadBase64}"));
                var signatureBase64 = Convert.ToBase64String(signatureBytes);

                // Formar el token final
                return $"{headerBase64}.{payloadBase64}.{signatureBase64}";
            }
        }
    }

}
