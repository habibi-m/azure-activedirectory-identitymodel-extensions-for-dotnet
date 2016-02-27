//------------------------------------------------------------------------------
//
// Copyright (c) Microsoft Corporation.
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace System.IdentityModel.Tokens.Jwt
{
    /// <summary>
    /// A <see cref="SecurityTokenHandler"/> designed for creating and validating Json Web Tokens. See: http://tools.ietf.org/html/rfc7519 and http://www.rfc-editor.org/info/rfc7515
    /// </summary>
    public class JwtSecurityTokenHandler : SecurityTokenHandler, ISecurityTokenValidator
    {
        private delegate bool CertMatcher(X509Certificate2 cert);

        // Summary:
        //     The claim properties namespace.
        private const string Namespace = "http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties";
        private static string shortClaimTypeProperty = Namespace + "/ShortTypeName";
        private static string jsonClaimTypeProperty = Namespace + "/json_type";
        private int _maximumTokenSizeInBytes = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;
        private int _defaultTokenLifetimeInMinutes = DefaultTokenLifetimeInMinutes;
        private IDictionary<string, string> _inboundClaimTypeMap;
        private IDictionary<string, string> _outboundClaimTypeMap;
        private ISet<string> _inboundClaimFilter;

        /// <summary>
        /// Default lifetime of tokens created. When creating tokens, if 'expires' and 'notbefore' are both null, then a default will be set to: expires = DateTime.UtcNow, notbefore = DateTime.UtcNow + TimeSpan.FromMinutes(TokenLifetimeInMinutes).
        /// </summary>
        public static readonly int DefaultTokenLifetimeInMinutes = 60;

        /// <summary>
        /// Default claim type mapping for inbound claims.
        /// </summary>
        public static IDictionary<string, string> DefaultInboundClaimTypeMap = ClaimTypeMapping.InboundClaimTypeMap;

        /// <summary>
        /// Default claim type maping for outbound claims.
        /// </summary>
        public static IDictionary<string, string> DefaultOutboundClaimTypeMap = ClaimTypeMapping.OutboundClaimTypeMap;

        /// <summary>
        /// Default claim type filter list.
        /// </summary>
        public static ISet<string> DefaultInboundClaimFilter = ClaimTypeMapping.InboundClaimFilter;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtSecurityTokenHandler"/> class.
        /// </summary>
        public JwtSecurityTokenHandler()
        {
            _inboundClaimTypeMap = new Dictionary<string, string>(DefaultInboundClaimTypeMap);
            _outboundClaimTypeMap = new Dictionary<string, string>(DefaultOutboundClaimTypeMap);
            _inboundClaimFilter = new HashSet<string>(DefaultInboundClaimFilter);
            SetDefaultTimesOnTokenCreation = true;
        }

        /// <summary>
        /// Gets or sets the <see cref="InboundClaimTypeMap"/> which is used when setting the <see cref="Claim.Type"/> for claims in the <see cref="ClaimsPrincipal"/> extracted when validating a <see cref="JwtSecurityToken"/>. 
        /// <para>The <see cref="Claim.Type"/> is set to the JSON claim 'name' after translating using this mapping.</para>
        /// <para>The default value is ClaimTypeMapping.InboundClaimTypeMap</para>
        /// </summary>
        /// <exception cref="ArgumentNullException">'value is null.</exception>
        public IDictionary<string, string> InboundClaimTypeMap
        {
            get
            {
                return _inboundClaimTypeMap;
            }

            set
            {
                if (value == null)
                    throw LogHelper.LogException<ArgumentNullException>(LogMessages.IDX10001, "InboundClaimTypeMap");

                _inboundClaimTypeMap = value;
            }
        }

        /// <summary>
        /// <para>Gets or sets the <see cref="OutboundClaimTypeMap"/> which is used when creating a <see cref="JwtSecurityToken"/> from <see cref="Claim"/>(s).</para>
        /// <para>The JSON claim 'name' value is set to <see cref="Claim.Type"/> after translating using this mapping.</para>
        /// <para>The default value is ClaimTypeMapping.OutboundClaimTypeMap</para>
        /// </summary>
        /// <remarks>This mapping is applied only when using <see cref="JwtPayload.AddClaim"/> or <see cref="JwtPayload.AddClaims"/>. Adding values directly will not result in translation.</remarks>
        /// <exception cref="ArgumentNullException">'value is null.</exception>
        public IDictionary<string, string> OutboundClaimTypeMap
        {
            get
            {
                return _outboundClaimTypeMap;
            }

            set
            {
                if (value == null)
                    throw LogHelper.LogException<ArgumentNullException>(LogMessages.IDX10001, "OutboundClaimTypeMap");

                _outboundClaimTypeMap = value;
            }
        }

        /// <summary>Gets or sets the <see cref="ISet{String}"/> used to filter claims when populating a <see cref="ClaimsIdentity"/> claims form a <see cref="JwtSecurityToken"/>.
        /// When a <see cref="JwtSecurityToken"/> is validated, claims with types found in this <see cref="ISet{String}"/> will not be added to the <see cref="ClaimsIdentity"/>.
        /// <para>The default value is ClaimTypeMapping.InboundClaimFliter</para>
        /// </summary>
        /// <exception cref="ArgumentNullException">'value' is null.</exception>
        public ISet<string> InboundClaimFilter
        {
            get
            {
                return _inboundClaimFilter;
            }

            set
            {
                if (value == null)
                    throw LogHelper.LogException<ArgumentNullException>(LogMessages.IDX10001, "InboundClaimFilter");

                _inboundClaimFilter = value;
            }
        }

        /// <summary>
        /// Gets or sets the property name of <see cref="Claim.Properties"/> the will contain the original JSON claim 'name' if a mapping occurred when the <see cref="Claim"/>(s) were created.
        /// <para>See <seealso cref="InboundClaimTypeMap"/> for more information.</para>
        /// </summary>
        /// <exception cref="ArgumentException">if <see cref="string"/>.IsIsNullOrWhiteSpace('value') is true.</exception>
        public static string ShortClaimTypeProperty
        {
            get
            {
                return shortClaimTypeProperty;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw LogHelper.LogException<ArgumentNullException>(LogMessages.IDX10001, "ShortClaimTypeProperty");

                shortClaimTypeProperty = value;
            }
        }

        /// <summary>
        /// Gets or sets the property name of <see cref="Claim.Properties"/> the will contain .Net type that was recogninzed when JwtPayload.Claims serialized the value to JSON.
        /// <para>See <seealso cref="InboundClaimTypeMap"/> for more information.</para>
        /// </summary>
        /// <exception cref="ArgumentException">if <see cref="string"/>.IsIsNullOrWhiteSpace('value') is true.</exception>
        public static string JsonClaimTypeProperty
        {
            get
            {
                return jsonClaimTypeProperty;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw LogHelper.LogException<ArgumentNullException>(LogMessages.IDX10001, "JsonClaimTypeProperty");

                jsonClaimTypeProperty = value;
            }
        }

        /// <summary>
        /// Returns 'true' which indicates this instance can validate a <see cref="JwtSecurityToken"/>.
        /// </summary>
        public override bool CanValidateToken
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether the class provides serialization functionality to serialize token handled 
        /// by this instance.
        /// </summary>
        /// <returns>true if the WriteToken method can serialize this token.</returns>
        public override bool CanWriteToken
        {
            get { return true; }
        }

        /// <summary>
        /// Gets or sets the token lifetime in minutes.
        /// </summary>
        /// <remarks>Used by <see cref="CreateToken(string, string, ClaimsIdentity, DateTime?, DateTime?, SigningCredentials)"/> to set the default expiration ('exp'). <see cref="DefaultTokenLifetimeInMinutes"/> for the default.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">'value' less than 1.</exception>
        public int TokenLifetimeInMinutes
        {
            get
            {
                return _defaultTokenLifetimeInMinutes;
            }

            set
            {
                if (value < 1)
                    throw LogHelper.LogException<ArgumentOutOfRangeException>(LogMessages.IDX10104, value);

                _defaultTokenLifetimeInMinutes = value;
            }
        }

        public override Type TokenType
        {
            get { return typeof(JwtSecurityToken); }
        }

        /// <summary>
        /// Gets and sets the maximum size in bytes, that a will be processed.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">'value' less than 1.</exception>
        public int MaximumTokenSizeInBytes
        {
            get
            {
                return _maximumTokenSizeInBytes;
            }

            set
            {
                if (value < 1)
                    throw LogHelper.LogException<ArgumentOutOfRangeException>(LogMessages.IDX10101, value);

                _maximumTokenSizeInBytes = value;
            }
        }

        /// <summary>
        /// Determines if the string is a well formed Json Web token (see: http://tools.ietf.org/html/rfc7519 )
        /// </summary>
        /// <param name="tokenString">string that should represent a valid JSON Web Token.</param>
        /// <remarks>Uses <see cref="Regex.IsMatch(string, string)"/>( token, @"^[A-Za-z0-9-_]+\.[A-Za-z0-9-_]+\.[A-Za-z0-9-_]*$" ).
        /// </remarks>
        /// <returns>
        /// <para>'true' if the token is in JSON compact serialization format.</para>
        /// <para>'false' if token.Length * 2 >  <see cref="MaximumTokenSizeInBytes"/>.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">'tokenString' is null.</exception>
        public override bool CanReadToken(string tokenString)
        {
            if (tokenString == null)
                throw LogHelper.LogArgumentNullException("tokenString");

            if (tokenString.Length * 2 > this.MaximumTokenSizeInBytes)
            {
                IdentityModelEventSource.Logger.WriteInformation(LogMessages.IDX10719, tokenString.Length);
                return false;
            }

            // match jws
            var regex = new Regex(JwtConstants.JsonCompactSerializationRegex, RegexOptions.None, TimeSpan.FromMilliseconds(100));

            // match jwe
            if( !regex.IsMatch(tokenString))
                regex = new Regex(JwtConstants.JweCompactSerializationRegex, RegexOptions.None, TimeSpan.FromMilliseconds(100));

            if (!regex.IsMatch(tokenString))
            {
                IdentityModelEventSource.Logger.WriteInformation(LogMessages.IDX10720);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a Json Web Token (JWT).
        /// </summary>
        /// <param name="tokenDescriptor"> a <see cref="SecurityTokenDescriptor"/> that contains details of contents of the token.</param>
        /// <remarks><see cref="SecurityTokenDescriptor.SigningCredentials"/> is used to sign the JSON.</remarks>
        public virtual string CreateEncodedJwt(SecurityTokenDescriptor tokenDescriptor)
        {
            if (tokenDescriptor == null)
                throw LogHelper.LogArgumentNullException(nameof(tokenDescriptor));

            return CreateJwtSecurityTokenPrivate(
                tokenDescriptor.Issuer,
                tokenDescriptor.Audience,
                tokenDescriptor.Subject,
                tokenDescriptor.NotBefore,
                tokenDescriptor.Expires,
                tokenDescriptor.IssuedAt,
                tokenDescriptor.SigningCredentials).RawData;
        }

        /// <summary>
        /// Creates a <see cref="JwtSecurityToken"/>
        /// </summary>
        /// <param name="issuer">the issuer of the token.</param>
        /// <param name="audience">the audience for this token.</param>
        /// <param name="subject">the source of the <see cref="Claim"/>(s) for this token.</param>
        /// <param name="notBefore">the notbefore time for this token.</param>
        /// <param name="expires">the expiration time for this token.</param>
        /// <param name="issuedAt">the issue time for this token.</param>
        /// <param name="signingCredentials">contains cryptographic material for generating a signature.</param>
        /// <remarks>If <see cref="ClaimsIdentity.Actor"/> is not null, then a claim { actort, 'value' } will be added to the payload. <see cref="CreateActorValue"/> for details on how the value is created.
        /// <para>See <seealso cref="JwtHeader"/> for details on how the HeaderParameters are added to the header.</para>
        /// <para>See <seealso cref="JwtPayload"/> for details on how the values are added to the payload.</para>
        /// <para>Each <see cref="Claim"/> on the <paramref name="subject"/> added will have <see cref="Claim.Type"/> translated according to the mapping found in
        /// <see cref="OutboundClaimTypeMap"/>. Adding and removing to <see cref="OutboundClaimTypeMap"/> will affect the name component of the Json claim.</para>
        /// <para><see cref="SigningCredentials.SigningCredentials(SecurityKey, string)"/> is used to sign the JSON.</para>
        /// </remarks>
        /// <returns>A <see cref="JwtSecurityToken"/>.</returns>
        /// <exception cref="ArgumentException">if 'expires' &lt;= 'notBefore'.</exception>
        public virtual string CreateEncodedJwt(string issuer, string audience, ClaimsIdentity subject, DateTime? notBefore, DateTime? expires, DateTime? issuedAt, SigningCredentials signingCredentials)
        {
            if (tokenDescriptor.EncryptingCredentials == null)
                return CreateJwtSecurityTokenPrivate(issuer, audience, subject, notBefore, expires, issuedAt, signingCredentials).RawData;
            else
                return (CreateJwe(
                    tokenDescriptor.Issuer,
                    tokenDescriptor.Audience,
                    tokenDescriptor.Claims,
                    tokenDescriptor.NotBefore,
                    tokenDescriptor.Expires,
                    tokenDescriptor.IssuedAt,
                    tokenDescriptor.EncryptingCredentials)).RawData;
        }

        /// <summary>
        /// Creates a Json Web Token (JWT).
        /// </summary>
        /// <param name="tokenDescriptor"> a <see cref="SecurityTokenDescriptor"/> that contains details of contents of the token.</param>
        /// <remarks><see cref="SecurityTokenDescriptor.SigningCredentials"/> is used to sign <see cref="JwtSecurityToken.RawData"/>.</remarks>
        public virtual JwtSecurityToken CreateJwtSecurityToken(SecurityTokenDescriptor tokenDescriptor)
        {
            if (tokenDescriptor == null)
                throw LogHelper.LogArgumentNullException(nameof(tokenDescriptor));
            return CreateJwtSecurityTokenPrivate(
                tokenDescriptor.Issuer,
                tokenDescriptor.Audience,
                tokenDescriptor.Subject,
                tokenDescriptor.NotBefore,
                tokenDescriptor.Expires,
                tokenDescriptor.IssuedAt,
                tokenDescriptor.SigningCredentials);
        }

        public JwtSecurityToken CreateJwtSecurityToken(SecurityTokenDescriptor tokenDescriptor, JwtTypes jwtType)
        {
            if (tokenDescriptor == null)
                throw LogHelper.LogArgumentNullException(nameof(tokenDescriptor));

            if (jwtType == JwtTypes.JWS)
                return CreateJwtSecurityTokenPrivate(
                    tokenDescriptor.Issuer,
                    tokenDescriptor.Audience,
                    tokenDescriptor.Subject,
                    tokenDescriptor.NotBefore,
                    tokenDescriptor.Expires,
                    tokenDescriptor.IssuedAt,
                    tokenDescriptor.SigningCredentials);
            else
                return (CreateJwe(
                    tokenDescriptor.Issuer,
                    tokenDescriptor.Audience,
                    tokenDescriptor.Claims,
                    tokenDescriptor.NotBefore,
                    tokenDescriptor.Expires,
                    tokenDescriptor.IssuedAt,
                    tokenDescriptor.EncryptingCredentials));
        }

        /// <summary>
        /// Creates a <see cref="JwtSecurityToken"/>
        /// </summary>
        /// <param name="issuer">the issuer of the token.</param>
        /// <param name="audience">the audience for this token.</param>
        /// <param name="subject">the source of the <see cref="Claim"/>(s) for this token.</param>
        /// <param name="notBefore">the notbefore time for this token.</param>
        /// <param name="expires">the expiration time for this token.</param>
        /// <param name="issuedAt">the issue time for this token.</param>
        /// <param name="signingCredentials">contains cryptographic material for generating a signature.</param>
        /// <remarks>If <see cref="ClaimsIdentity.Actor"/> is not null, then a claim { actort, 'value' } will be added to the payload. <see cref="CreateActorValue"/> for details on how the value is created.
        /// <para>See <seealso cref="JwtHeader"/> for details on how the HeaderParameters are added to the header.</para>
        /// <para>See <seealso cref="JwtPayload"/> for details on how the values are added to the payload.</para>
        /// <para>Each <see cref="Claim"/> on the <paramref name="subject"/> added will have <see cref="Claim.Type"/> translated according to the mapping found in
        /// <see cref="OutboundClaimTypeMap"/>. Adding and removing to <see cref="OutboundClaimTypeMap"/> will affect the name component of the Json claim.</para>
        /// <para><see cref="SigningCredentials.SigningCredentials(SecurityKey, string)"/> is used to sign <see cref="JwtSecurityToken.RawData"/>.</para>
        /// </remarks>
        /// <returns>A <see cref="JwtSecurityToken"/>.</returns>
        /// <exception cref="ArgumentException">if 'expires' &lt;= 'notBefore'.</exception>
        public virtual JwtSecurityToken CreateJwtSecurityToken(string issuer = null, string audience = null, ClaimsIdentity subject = null, DateTime? notBefore = null, DateTime? expires = null, DateTime? issuedAt = null, SigningCredentials signingCredentials = null)
        {
            return CreateJwtSecurityTokenPrivate(issuer, audience, subject, notBefore, expires, issuedAt, signingCredentials);
        }

        /// <summary>
        /// Creates a Json Web Token (JWT).
        /// </summary>
        /// <param name="tokenDescriptor"> a <see cref="SecurityTokenDescriptor"/> that contains details of contents of the token.</param>
        /// <remarks><see cref="SecurityTokenDescriptor.SigningCredentials"/> is used to sign <see cref="JwtSecurityToken.RawData"/>.</remarks>
        public override SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor)
        {
            if (tokenDescriptor == null)
                throw LogHelper.LogArgumentNullException(nameof(tokenDescriptor));

            return CreateJwtSecurityTokenPrivate(
                tokenDescriptor.Issuer,
                tokenDescriptor.Audience,
                tokenDescriptor.Subject,
                tokenDescriptor.NotBefore,
                tokenDescriptor.Expires,
                tokenDescriptor.IssuedAt,
                tokenDescriptor.SigningCredentials);
        }

        public virtual JwtSecurityToken CreateJweToken(string issuer = null, string audience = null, ClaimsIdentity subject = null, DateTime? notBefore = null, DateTime? expires = null, DateTime? issuedAt = null, EncryptingCredentials encryptingCredentials = null)
        {
            var jwe = CreateJwe(issuer, audience, (subject != null ? subject.Claims : null), notBefore, expires, issuedAt, encryptingCredentials);
            if (subject != null && subject.Actor != null)
            {
                jwe.Payload.AddClaim(new Claim(JwtRegisteredClaimNames.Actort, this.CreateActorValue(subject.Actor)));
            }

            return jwe;
        }

        private JwtSecurityToken CreateJwtSecurityTokenPrivate(string issuer, string audience, ClaimsIdentity subject, DateTime? notBefore, DateTime? expires, DateTime? issuedAt, SigningCredentials signingCredentials)
        {
            if (SetDefaultTimesOnTokenCreation)
            {
                DateTime now = DateTime.UtcNow;
                if (!expires.HasValue)
                    expires = now + TimeSpan.FromMinutes(TokenLifetimeInMinutes);

                if (!issuedAt.HasValue)
                    issuedAt = now;

                if (!notBefore.HasValue)
                    notBefore = now;
            }

            IdentityModelEventSource.Logger.WriteVerbose(LogMessages.IDX10721, (audience ?? "null"), (issuer ?? "null"));
            JwtPayload payload = new JwtPayload(issuer, audience, (subject == null ? null : OutboundClaimTypeTransform(subject.Claims)), notBefore, expires, issuedAt);
            JwtHeader header = new JwtHeader(signingCredentials);

            if (subject?.Actor != null)
            {
                payload.AddClaim(new Claim(JwtRegisteredClaimNames.Actort, this.CreateActorValue(subject.Actor)));
            }

            string rawHeader = header.Base64UrlEncode();
            string rawPayload = payload.Base64UrlEncode();
            string rawSignature = string.Empty;
            string signingInput = string.Concat(rawHeader, ".", rawPayload);

            if (signingCredentials != null)
            {
                IdentityModelEventSource.Logger.WriteVerbose(LogMessages.IDX10645);
                rawSignature = CreateEncodedSignature(signingInput, signingCredentials.Key.CryptoProviderFactory.CreateForSigning(signingCredentials.Key, signingCredentials.Algorithm));
            }

            IdentityModelEventSource.Logger.WriteInformation(LogMessages.IDX10722, rawHeader, rawPayload, rawSignature);
            return new JwtSecurityToken(header, payload, rawHeader, rawPayload, rawSignature);
        }

        private JwtSecurityToken CreateJwe(string issuer, string audience, IEnumerable<Claim> claims, DateTime? notBefore, DateTime? expires, DateTime? issuedAt, EncryptingCredentials encryptingCredentials)
        {
            if (encryptingCredentials == null)
                return null;

            if (SetDefaultTimesOnTokenCreation)
            {
                DateTime now = DateTime.UtcNow;
                if (!expires.HasValue)
                    expires = now + TimeSpan.FromMinutes(TokenLifetimeInMinutes);

                if (!issuedAt.HasValue)
                    issuedAt = now;

                if (!notBefore.HasValue)
                    notBefore = now;
            }

            IdentityModelEventSource.Logger.WriteVerbose(LogMessages.IDX10721, (audience ?? "null"), (issuer ?? "null"));
            JwtPayload payload = new JwtPayload(issuer, audience, (claims == null ? null : OutboundClaimTypeTransform(claims)), notBefore, expires, issuedAt);
            JweHeader header = new JweHeader(encryptingCredentials);

            if (encryptingCredentials.AdditionalAuthenticationData == null)
                encryptingCredentials.AdditionalAuthenticationData = Encoding.ASCII.GetBytes(Base64UrlEncoder.Encode(header.SerializeToJson()));

            using (Aes aes = Aes.Create())
            {
                if (encryptingCredentials.ContentEncryptionKey == null)
                    encryptingCredentials.ContentEncryptionKey = new SymmetricSecurityKey(aes.Key);

                if (encryptingCredentials.InitializationVector == null)
                    encryptingCredentials.InitializationVector = aes.IV;
            }

            IdentityModelEventSource.Logger.WriteVerbose(LogMessages.IDX10645);
            string rawHeader = header.Base64UrlEncode();
            string authenticationTag = null;
            string encryptedPayload = EncryptPayload(payload, header.EncryptingCredentials, out authenticationTag);
            string jweEncryptedKey = EncryptContentEncryptionKey(header.EncryptingCredentials.ContentEncryptionKey, header.EncryptingCredentials);

            return new JwtSecurityToken(header, jweEncryptedKey, header.EncryptingCredentials.InitializationVector, encryptedPayload, authenticationTag);
        }

        private string EncryptContentEncryptionKey(SecurityKey contentEncryptionKey, EncryptingCredentials encryptingCredentials)
        {
            string alg = encryptingCredentials.KeyEncryptionAlgorithm;
            KeyManagementModes mode = ResolveKeyManagementMode(alg);
            byte[] encryptedKey = null;
            if (mode == KeyManagementModes.DirectEncryption)
                return string.Empty;
            else if (mode == KeyManagementModes.KeyEncryption)
            {
                // need recipient public key - rsa params
                // use rsa.create() -> rsa.import() -> rsa.encrypt(cek)
                // return base64url encoding
            }
            else if (mode == KeyManagementModes.KeyWrapping)
            {
                int keySize = 128;
                if (alg == SecurityAlgorithms.Aes128KW)
                    keySize = 128;
                if (alg == SecurityAlgorithms.Aes192KW)
                    keySize = 192;
                if (alg == SecurityAlgorithms.Aes256KW)
                    keySize = 256;

                using (Aes aes = Aes.Create())
                {
                    aes.KeySize = keySize;
                    aes.GenerateKey();

                    ICryptoTransform encryptor = aes.CreateEncryptor();
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(contentEncryptionKey);
                            }
                            encryptedKey = msEncrypt.ToArray();
                        }
                    }
                }
            }
            if (encryptedKey != null)
                return Base64UrlEncoder.Encode(encryptedKey);

            return null;
        }

        private string EncryptPayload(JwtPayload payload, EncryptingCredentials encryptingCredentials, out string authenticationTag)
        {
            byte[] cipherText = null;
            authenticationTag = null;

            if (encryptingCredentials.ContentEncryptionKey != null)
            {
                SymmetricEncryptionProvider encProvider = encryptingCredentials.ContentEncryptionKey.CryptoProviderFactory.CreateForEncrypting(encryptingCredentials.ContentEncryptionKey, encryptingCredentials.ContentEncryptionAlgorithm) as SymmetricEncryptionProvider;
                cipherText = encProvider.Encrypt(Encoding.UTF8.GetBytes(payload.SerializeToJson()), encryptingCredentials.InitializationVector, out authenticationTag);
            }
            else if (encryptingCredentials.KeyEncryptionAlgorithm == SecurityAlgorithms.DirectEncryption)
            {
                LogHelper.LogArgumentNullException("ContentEncryptionKey");
            }
            else
            {
                // generate CEK

            }
            return Base64UrlEncoder.Encode(cipherText);
            /*
            using (Aes aes = Aes.Create())
            {
                SymmetricSecurityKey key = encryptingCredentials.ContentEncryptionKey as SymmetricSecurityKey;
                if (key == null)
                    return null;

                aes.Key = key.Key;
                aes.IV = encryptingCredentials.InitializationVector;
                byte[] cek = aes.Key;
                byte[] macKey = new byte[aes.KeySize / 2];
                byte[] encKey = new byte[aes.KeySize / 2];
                int i = 0;
                for (; i < cek.Length / 2; i++)
                    macKey[i] = cek[i];
                for (; i < cek.Length; i++)
                    encKey[i] = cek[i];

                ICryptoTransform encryptor = aes.CreateEncryptor();

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(payload.SerializeToJson());
                        }
                        cipherText = msEncrypt.ToArray();
                    }
                }
            }
            */
            //authenticationTag = ComputeHmac(encryptingCredentials.AdditionalAuthenticationData, encryptingCredentials.InitializationVector, cipherText);
            //return Base64UrlEncoder.Encode(cipherText);
        }

        private string ComputeHmac(byte[] aad, byte[] iv, byte[] cipher)
        {
            HMACSHA256 hmac = new HMACSHA256();
            byte[] al = BitConverter.GetBytes(aad.Length);

            int totalLength = aad.Length + iv.Length + al.Length + cipher.Length;
            byte[] input = new byte[totalLength];

            int j = 0;
            for (int i = 0; i < aad.Length; i++) input[j++] = aad[i];
            for (int i = 0; i < iv.Length; i++) input[j++] = iv[i];
            for (int i = 0; i < cipher.Length; i++) input[j++] = cipher[i];
            for (int i = 0; i < al.Length; i++) input[j++] = al[i];
            byte[] hash = hmac.ComputeHash(input);
            byte[] authTag = new byte[hash.Length / 2];
            for (int i = 0; i < hash.Length / 2; i++)
                authTag[i] = hash[i];

            return Base64UrlEncoder.Encode(authTag);
        }

        private KeyManagementModes ResolveKeyManagementMode(string alg)
        {
            if (string.IsNullOrEmpty(alg))
                throw new ArgumentException();

            switch (alg)
            {
                case SecurityAlgorithms.RsaOaep:
                case SecurityAlgorithms.RsaOaep256: return KeyManagementModes.KeyEncryption;
                case SecurityAlgorithms.Aes128KW:
                case SecurityAlgorithms.Aes192KW:
                case SecurityAlgorithms.Aes256KW:
                case SecurityAlgorithms.Aes128GcmKW:
                case SecurityAlgorithms.Aes192GcmKW:
                case SecurityAlgorithms.Aes256GcmKW: return KeyManagementModes.KeyWrapping;
                case SecurityAlgorithms.DirectEncryption: return KeyManagementModes.DirectEncryption;
                case "default":
                    throw new ArgumentException();
            }
            throw new ArgumentException();
        }

        private IEnumerable<Claim> OutboundClaimTypeTransform(IEnumerable<Claim> claims)
        {
            foreach (Claim claim in claims)
            {
                string type = null;
                if (_outboundClaimTypeMap.TryGetValue(claim.Type, out type))
                {
                    yield return new Claim(type, claim.Value, claim.ValueType, claim.Issuer, claim.OriginalIssuer, claim.Subject);
                }
                else
                {
                    yield return claim;
                }
            }
        }

        public JwtSecurityToken ReadJwtToken(string token)
        {
            if (token == null)
                throw LogHelper.LogArgumentNullException("token");

            if (token.Length * 2 > MaximumTokenSizeInBytes)
                throw LogHelper.LogException<ArgumentException>(LogMessages.IDX10209, token.Length, MaximumTokenSizeInBytes);

            if (!CanReadToken(token))
                throw LogHelper.LogException<ArgumentException>(LogMessages.IDX10708, GetType(), token);

            var jwt = new JwtSecurityToken();
            jwt.Decode(token);
            return jwt;
        }

        /// <summary>
        /// Reads a token encoded in JSON Compact serialized format.
        /// </summary>
        /// <param name="token">A 'JSON Web Token' (JWT). May be signed as per 'JSON Web Signature' (JWS).</param>
        /// <remarks>
        /// The JWT must be encoded using Base64UrlEncoding of the UTF-8 representation of the JWT: Header, Payload and Signature.
        /// The contents of the JWT returned are not validated in any way, the token is simply decoded. Use ValidateToken to validate the JWT.
        /// </remarks>
        /// <returns>A <see cref="JwtSecurityToken"/></returns>
        public override SecurityToken ReadToken(string token)
        {
            return ReadJwtToken(token);
        }

        /// <summary>
        /// Reads and validates a token encoded in JSON Compact serialized format.
        /// </summary>
        /// <param name="token">A 'JSON Web Token' (JWT) that has been encoded as a JSON object. May be signed using 'JSON Web Signature' (JWS).</param>
        /// <param name="validationParameters">Contains validation parameters for the <see cref="JwtSecurityToken"/>.</param>
        /// <param name="validatedToken">The <see cref="JwtSecurityToken"/> that was validated.</param>
        /// <exception cref="ArgumentNullException">'securityToken' is null or whitespace.</exception>
        /// <exception cref="ArgumentNullException">'validationParameters' is null.</exception>
        /// <exception cref="ArgumentException">'securityToken.Length' > <see cref="MaximumTokenSizeInBytes"/>.</exception>
        /// <returns>A <see cref="ClaimsPrincipal"/> from the jwt. Does not include the header claims.</returns>
        public virtual ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw LogHelper.LogArgumentNullException("token");

            if (validationParameters == null)
                throw LogHelper.LogArgumentNullException("validationParameters");

            if (token.Length > MaximumTokenSizeInBytes)
                throw LogHelper.LogException<ArgumentException>(LogMessages.IDX10209, token.Length, MaximumTokenSizeInBytes);

            if (validationParameters.CryptoProviderFactory != null)
                CryptoProviderFactory.Default = validationParameters.CryptoProviderFactory;

            JwtSecurityToken jwt = ReadJwtToken(token);
            if (jwt.IsJwe)
                jwt = ValidateJweToken(jwt, validationParameters);
            else
                jwt = ValidateJwsToken(jwt, validationParameters);

            ClaimsIdentity identity = CreateClaimsIdentity(jwt, jwt.Issuer, validationParameters);
            if (validationParameters.SaveSigninToken)
            {
                identity.BootstrapContext = token;
            }

            IdentityModelEventSource.Logger.WriteInformation(LogMessages.IDX10241, token);
            validatedToken = jwt;
            return new ClaimsPrincipal(identity);
        }

        public JwtSecurityToken ValidateJweToken(JwtSecurityToken jwt, TokenValidationParameters validationParameters)
        {
            SecurityKey keyEncryptionKey = null;
            SymmetricSecurityKey contentEncryptionKey = GetContentEncryptionKey(jwt, validationParameters, out keyEncryptionKey) as SymmetricSecurityKey;
            string plainText = null;

            if (contentEncryptionKey != null)
            {
                if (validationParameters.DecryptCipherText != null)
                {
                    plainText = validationParameters.DecryptCipherText(jwt, contentEncryptionKey, validationParameters);
                }
                else
                {
                    SymmetricEncryptionProvider encryptionProvider = new SymmetricEncryptionProvider(contentEncryptionKey, jwt.JweHeader.Enc);
                    byte[] plainTextBytes = encryptionProvider.Decrypt(Encoding.UTF8.GetBytes(jwt.CipherText), jwt.InitializationVector, jwt.AuthenticationTag);
                    if (plainTextBytes != null)
                        plainText = Encoding.UTF8.GetString(plainTextBytes);
                }
            }

            var innerJwtToken = new JwtSecurityToken(plainText);
            jwt = new JwtSecurityToken(innerJwtToken.Header, innerJwtToken.Payload, innerJwtToken.RawSignature, jwt.JweHeader, jwt.EncryptedContentEncryptionKey, jwt.InitializationVector, jwt.AuthenticationTag);
            jwt.ContentEncryptionKey = contentEncryptionKey;
            jwt.KeyEncryptionKey = keyEncryptionKey;
            return ValidateJwsToken(jwt, validationParameters);
        }

        public JwtSecurityToken ValidateJwsToken(JwtSecurityToken jwt, TokenValidationParameters validationParameters)
        {
            if (validationParameters.RequireSignedTokens)
            {
                SecurityKey signingKey = null;
                if (validationParameters.SignatureValidator != null)
                {
                    signingKey = validationParameters.SignatureValidator(jwt, validationParameters);
                    if (signingKey == null)
                        throw LogHelper.LogException<SecurityTokenInvalidSignatureException>(LogMessages.IDX10505, jwt.RawData);
                }
                else
                {
                    signingKey = ValidateSignature(jwt, validationParameters);
                    if (signingKey == null)
                        throw LogHelper.LogException<SecurityTokenInvalidSignatureException>(LogMessages.IDX10507, jwt.RawData);
                }
                jwt.SigningKey = signingKey;
            }

            if (jwt.SigningKey != null && validationParameters.ValidateIssuerSigningKey)
            {
                if (validationParameters.IssuerSigningKeyValidator != null)
                {
                    if (!validationParameters.IssuerSigningKeyValidator(jwt.SigningKey, validationParameters))
                        throw LogHelper.LogException<SecurityTokenInvalidSigningKeyException>(LogMessages.IDX10232, jwt.SigningKey);
                }
                else
                {
                    ValidateIssuerSecurityKey(jwt.SigningKey, jwt, validationParameters);
                }
            }

            DateTime? notBefore = null;
            if (jwt.Payload.Nbf != null)
            {
                notBefore = new DateTime?(jwt.ValidFrom);
            }

            DateTime? expires = null;
            if (jwt.Payload.Exp != null)
            {
                expires = new DateTime?(jwt.ValidTo);
            }

            Validators.ValidateTokenReplay(jwt.RawData, expires, validationParameters);
            if (validationParameters.ValidateLifetime)
            {
                if (validationParameters.LifetimeValidator != null)
                {
                    if (!validationParameters.LifetimeValidator(notBefore: notBefore, expires: expires, securityToken: jwt, validationParameters: validationParameters))
                        throw LogHelper.LogException<SecurityTokenInvalidLifetimeException>(LogMessages.IDX10230, jwt);
                }
                else
                {
                    ValidateLifetime(notBefore: notBefore, expires: expires, securityToken: jwt, validationParameters: validationParameters);
                }
            }

            if (validationParameters.ValidateAudience)
            {
                if (validationParameters.AudienceValidator != null)
                {
                    if (!validationParameters.AudienceValidator(jwt.Audiences, jwt, validationParameters))
                        throw LogHelper.LogException<SecurityTokenInvalidAudienceException>(LogMessages.IDX10231, jwt.ToString());
                }
                else
                {
                    this.ValidateAudience(jwt.Audiences, jwt, validationParameters);
                }
            }

            string issuer = jwt.Issuer;
            if (validationParameters.ValidateIssuer)
            {
                if (validationParameters.IssuerValidator != null)
                {
                    issuer = validationParameters.IssuerValidator(issuer, jwt, validationParameters);
                }
                else
                {
                    issuer = ValidateIssuer(issuer, jwt, validationParameters);
                }
            }

            if (validationParameters.ValidateActor && !string.IsNullOrWhiteSpace(jwt.Actor))
            {
                SecurityToken actor = null;
                ValidateToken(jwt.Actor, validationParameters.ActorValidationParameters ?? validationParameters, out actor);
            }

            return jwt;
        }

        //public string DecryptPayload(JwtSecurityToken jwt, TokenValidationParameters validationParameters)
        //{
        //    SymmetricSecurityKey contentEncryptionKey = GetContentEncryptionKey(jwt, validationParameters) as SymmetricSecurityKey;
        //    string plainText = null;

        //    if (contentEncryptionKey != null)
        //    {
        //        if (validationParameters.DecryptCipherText != null)
        //        {
        //            plainText = validationParameters.DecryptCipherText(jwt, contentEncryptionKey, validationParameters);
        //        }
        //        else
        //        {
        //            SymmetricEncryptionProvider encryptionProvider = new SymmetricEncryptionProvider(contentEncryptionKey, jwt.JweHeader.Enc);
        //            byte[] plainTextBytes = encryptionProvider.Decrypt(Encoding.UTF8.GetBytes(jwt.CipherText), jwt.InitializationVector, jwt.AuthenticationTag);
        //            if (plainTextBytes != null)
        //                plainText = Encoding.UTF8.GetString(plainTextBytes);
        //        }
        //    }

        //    return plainText;
        //}

        public SecurityKey GetContentEncryptionKey(JwtSecurityToken jwt, TokenValidationParameters validationParameters, out SecurityKey keyEncryptionKey)
        {
            string keyEncAlg = jwt.JweHeader.Alg;
            IEnumerable<SecurityKey> jweEncryptionKeys = null;
            byte[] contentEncryptionKey = null;
            if (validationParameters.EncryptionKeyResolver != null)
            {
                jweEncryptionKeys = validationParameters.EncryptionKeyResolver(jwt.RawData, jwt, jwt.JweHeader.Kid, validationParameters);
            }
            else
            {
                SecurityKey jweEncryptionKey = ResolveJweEncryptionKey(keyEncAlg, jwt.JweHeader.Kid, jwt, validationParameters);
                jweEncryptionKeys = new List<SecurityKey> { jweEncryptionKey };
            }

            if (jweEncryptionKeys == null)
                jweEncryptionKeys = GetAllJweEncryptionKeys(jwt, validationParameters);

            foreach (SecurityKey key in jweEncryptionKeys)
            {
                if (key == null)
                    LogHelper.LogArgumentNullException("key");
                try
                {
                    var encryptionProvider = key.CryptoProviderFactory.CreateForDecrypting(key, keyEncAlg, null);
                    contentEncryptionKey = encryptionProvider.Decrypt(Encoding.UTF8.GetBytes(jwt.EncryptedContentEncryptionKey));
                    if (contentEncryptionKey != null)
                    {
                        keyEncryptionKey = key;
                        return new SymmetricSecurityKey(contentEncryptionKey);
                    }
                }
                catch (Exception)
                {

                }
            }
            keyEncryptionKey = null;
            return null;
        }

        private IEnumerable<SecurityKey> GetAllJweEncryptionKeys(JwtSecurityToken jwt, TokenValidationParameters validationParameters)
        {
            if (validationParameters == null)
                LogHelper.LogArgumentNullException("validationParameters");

            foreach (SecurityKey key in validationParameters.TokenDecryptionKeys)
                if (key != null)
                    yield return key;
        }

        private SecurityKey ResolveJweEncryptionKey(string keyEncAlg, string kid, JwtSecurityToken jwt, TokenValidationParameters validationParameters)
        {
            KeyManagementModes mode = GetKeyManagementMode(keyEncAlg);
            foreach (SecurityKey key in validationParameters.TokenDecryptionKeys)
            {
                if (key != null && string.Equals(kid, key.KeyId, StringComparison.Ordinal))
                {
                    return key;
                }
            }
            return null;
        }

        private KeyManagementModes GetKeyManagementMode(string keyEncAlg)
        {
            return KeyManagementModes.DirectEncryption;
        }

        /// <summary>
        /// Writes the <see cref="JwtSecurityToken"/> as a JSON Compact serialized format string.
        /// </summary>
        /// <param name="token"><see cref="JwtSecurityToken"/> to serialize.</param>
        /// <remarks>
        /// <para>If the <see cref="JwtSecurityToken.SigningCredentials"/> are not null, the encoding will contain a signature.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">'token' is null.</exception>
        /// <exception cref="ArgumentException">'token' is not a not <see cref="JwtSecurityToken"/>.</exception>
        /// <returns>The <see cref="JwtSecurityToken"/> as a signed (if <see cref="SigningCredentials"/> exist) encoded string.</returns>
        public override string WriteToken(SecurityToken token)
        {
            if (token == null)
                throw LogHelper.LogArgumentNullException("token");

            JwtSecurityToken jwt = token as JwtSecurityToken;
            if (jwt == null)
                throw LogHelper.LogException<ArgumentNullException>(LogMessages.IDX10706, new object[] { GetType(), typeof(JwtSecurityToken), token.GetType() });

            string signingInput = string.Concat(jwt.EncodedHeader, ".", jwt.EncodedPayload);
            if (jwt.SigningCredentials == null)
                return signingInput + ".";
            else
                return signingInput + "." + CreateEncodedSignature(signingInput, jwt.SigningCredentials.Key.CryptoProviderFactory.CreateForSigning(jwt.SigningCredentials.Key, jwt.SigningCredentials.Algorithm));
        }

        /// <summary>
        /// Produces a signature over the 'input' using the <see cref="SecurityKey"/> and algorithm specified.
        /// </summary>
        /// <param name="input">string to be signed</param>
        /// <param name="signatureProvider">the <see cref="SignatureProvider"/> used to sign the token</param>
        /// <returns>The bse64urlendcoded signature over the bytes obtained from UTF8Encoding.GetBytes( 'input' ).</returns>
        /// <exception cref="ArgumentNullException">'input' or 'signatureProvider' is null.</exception>
        internal static string CreateEncodedSignature(string input, SignatureProvider signatureProvider)
        {
            if (input == null)
                throw LogHelper.LogArgumentNullException("input");

            if (signatureProvider == null)
                throw LogHelper.LogArgumentNullException("signatureProvider");

            return Base64UrlEncoder.Encode(signatureProvider.Sign(Encoding.UTF8.GetBytes(input)));
        }

        private bool ValidateSignature(byte[] encodedBytes, byte[] signature, SecurityKey key, string algorithm)
        {
            SignatureProvider signatureProvider = key.GetSignatureProviderForVerifying(algorithm);
            if (signatureProvider == null)
                throw LogHelper.LogException<InvalidOperationException>(LogMessages.IDX10636, (key == null ? "Null" : key.ToString()), (algorithm == null ? "Null" : algorithm));

            return signatureProvider.Verify(encodedBytes, signature);
        }

        /// <summary>
        /// Validates that the signature, if found and / or required is valid.
        /// </summary>
        /// <param name="token">A 'JSON Web Token' (JWT) that has been encoded as a JSON object. May be signed 
        /// using 'JSON Web Signature' (JWS).</param>
        /// <param name="validationParameters"><see cref="TokenValidationParameters"/> that contains signing keys.</param>
        /// <exception cref="ArgumentNullException"> thrown if 'token is null or whitespace.</exception>
        /// <exception cref="ArgumentNullException"> thrown if 'validationParameters is null.</exception>
        /// <exception cref="SecurityTokenValidationException"> thrown if a signature is not found and <see cref="TokenValidationParameters.RequireSignedTokens"/> is true.</exception>
        /// <exception cref="SecurityTokenSignatureKeyNotFoundException"> thrown if the 'token' has a key identifier and none of the <see cref="SecurityKey"/>(s) provided result in a validated signature. 
        /// This can indicate that a key refresh is required.</exception>
        /// <exception cref="SecurityTokenInvalidSignatureException"> thrown if after trying all the <see cref="SecurityKey"/>(s), none result in a validated signture AND the 'token' does not have a key identifier.</exception>
        /// <returns><see cref="JwtSecurityToken"/> that has the signature validated if token was signed and <see cref="TokenValidationParameters.RequireSignedTokens"/> is true.</returns>
        /// <remarks><para>If the 'token' is signed, the signature is validated even if <see cref="TokenValidationParameters.RequireSignedTokens"/> is false.</para>
        /// <para>If the 'token' signature is validated, then the <see cref="JwtSecurityToken.SigningKey"/> will be set to the key that signed the 'token'.</para></remarks>
        protected virtual SecurityKey ValidateSignature(JwtSecurityToken jwt, TokenValidationParameters validationParameters)
        {
            //if (string.IsNullOrWhiteSpace(token))
            //    throw LogHelper.LogArgumentNullException("token");

            if (validationParameters == null)
                throw LogHelper.LogArgumentNullException("validationParameters");

            //JwtSecurityToken jwt = ReadJwtToken(token);
            if (string.IsNullOrEmpty(jwt.RawSignature))
            {
                if (validationParameters.RequireSignedTokens)
                    throw LogHelper.LogException<SecurityTokenInvalidSignatureException>(LogMessages.IDX10504, jwt.RawData);
                else
                    return null;
            }

            byte[] encodedBytes = Encoding.UTF8.GetBytes(jwt.RawHeader + "." + jwt.RawPayload);
            byte[] signatureBytes = Base64UrlEncoder.DecodeBytes(jwt.RawSignature);

            // if the kid != null and the signature fails, throw SecurityTokenSignatureKeyNotFoundException
            string kid = jwt.Header.Kid;
            IEnumerable<SecurityKey> securityKeys = null;
            if (validationParameters.IssuerSigningKeyResolver != null)
            {
                securityKeys = validationParameters.IssuerSigningKeyResolver(jwt, kid, validationParameters);
            }
            else
            {
                var securityKey = ResolveIssuerSigningKey(jwt, validationParameters);
                if (securityKey != null)
                {
                    securityKeys = new List<SecurityKey> { securityKey };
                }
            }

            if (securityKeys == null)
            {
                // Try all keys since there is no keyidentifier
                securityKeys = GetAllKeys(jwt, kid, validationParameters);
            }

            // try the keys
            if (securityKeys != null)
            {
                StringBuilder exceptionStrings = new StringBuilder();
                StringBuilder keysAttempted = new StringBuilder();

                foreach (SecurityKey securityKey in securityKeys)
                {
                    try
                    {
                        if (ValidateSignature(encodedBytes, signatureBytes, securityKey, jwt.Header.Alg))
                        {
                            IdentityModelEventSource.Logger.WriteInformation(LogMessages.IDX10242, jwt.RawData);
                            jwt.SigningKey = securityKey;
                            return securityKey;
                        }
                    }
                    catch (Exception ex)
                    {
                        exceptionStrings.AppendLine(ex.ToString());
                    }

                    if (securityKey != null)
                        keysAttempted.AppendLine(securityKey.ToString() + " , KeyId: " + securityKey.KeyId);
                }

                if (keysAttempted.Length > 0)
                    throw LogHelper.LogException<SecurityTokenInvalidSignatureException>(LogMessages.IDX10503, keysAttempted.ToString(), exceptionStrings.ToString(), jwt.ToString());
            }

            throw LogHelper.LogException<SecurityTokenInvalidSignatureException>(LogMessages.IDX10500);
        }

        private IEnumerable<SecurityKey> GetAllKeys(JwtSecurityToken securityToken, string kid, TokenValidationParameters validationParameters)
        {
            IdentityModelEventSource.Logger.WriteInformation(LogMessages.IDX10243);
            if (validationParameters.IssuerSigningKey != null)
                yield return validationParameters.IssuerSigningKey;

            if (validationParameters.IssuerSigningKeys != null)
                foreach (SecurityKey securityKey in validationParameters.IssuerSigningKeys)
                    yield return securityKey;
        }

        /// <summary>
        /// Creates a <see cref="ClaimsIdentity"/> from a <see cref="JwtSecurityToken"/>.
        /// </summary>
        /// <param name="jwt">The <see cref="JwtSecurityToken"/> to use as a <see cref="Claim"/> source.</param>
        /// <param name="issuer">The value to set <see cref="Claim.Issuer"/></param>
        /// <param name="validationParameters"> contains parameters for validating the token.</param>
        /// <returns>A <see cref="ClaimsIdentity"/> containing the <see cref="JwtSecurityToken.Claims"/>.</returns>
        protected virtual ClaimsIdentity CreateClaimsIdentity(JwtSecurityToken jwt, string issuer, TokenValidationParameters validationParameters)
        {
            if (jwt == null)
                throw LogHelper.LogArgumentNullException("jwt");

            if (string.IsNullOrWhiteSpace(issuer))
                IdentityModelEventSource.Logger.WriteVerbose(LogMessages.IDX10244, ClaimsIdentity.DefaultIssuer);

            ClaimsIdentity identity = validationParameters.CreateClaimsIdentity(jwt, issuer);
            foreach (Claim jwtClaim in jwt.Claims)
            {
                if (_inboundClaimFilter.Contains(jwtClaim.Type))
                {
                    continue;
                }

                string claimType;
                bool wasMapped = true;
                if (!_inboundClaimTypeMap.TryGetValue(jwtClaim.Type, out claimType))
                {
                    claimType = jwtClaim.Type;
                    wasMapped = false;
                }

                if (claimType == ClaimTypes.Actor)
                {
                    if (identity.Actor != null)
                        throw LogHelper.LogException<InvalidOperationException>(LogMessages.IDX10710, JwtRegisteredClaimNames.Actort, jwtClaim.Value);

                    if (CanReadToken(jwtClaim.Value))
                    {
                        JwtSecurityToken actor = ReadToken(jwtClaim.Value) as JwtSecurityToken;
                        identity.Actor = CreateClaimsIdentity(actor, issuer, validationParameters);
                    }
                }

                Claim c = new Claim(claimType, jwtClaim.Value, jwtClaim.ValueType, issuer, issuer, identity);
                if (jwtClaim.Properties.Count > 0)
                {
                    foreach(var kv in jwtClaim.Properties)
                    {
                        c.Properties[kv.Key] = kv.Value;
                    }
                }

                if (wasMapped)
                {
                    c.Properties[ShortClaimTypeProperty] = jwtClaim.Type;
                }

                identity.AddClaim(c);
            }

            return identity;
        }

        /// <summary>
        /// Creates the 'value' for the actor claim: { actort, 'value' }
        /// </summary>
        /// <param name="actor"><see cref="ClaimsIdentity"/> as actor.</param>
        /// <returns><see cref="string"/> representing the actor.</returns>
        /// <remarks>If <see cref="ClaimsIdentity.BootstrapContext"/> is not null:
        /// <para>&#160;&#160;if 'type' is 'string', return as string.</para>
        /// <para>&#160;&#160;if 'type' is 'BootstrapContext' and 'BootstrapContext.SecurityToken' is 'JwtSecurityToken'</para>
        /// <para>&#160;&#160;&#160;&#160;if 'JwtSecurityToken.RawData' != null, return RawData.</para>        
        /// <para>&#160;&#160;&#160;&#160;else return <see cref="JwtSecurityTokenHandler.WriteToken( SecurityToken )"/>.</para>        
        /// <para>&#160;&#160;if 'BootstrapContext.Token' != null, return 'Token'.</para>
        /// <para>default: <see cref="JwtSecurityTokenHandler.WriteToken(SecurityToken)"/> new ( <see cref="JwtSecurityToken"/>( actor.Claims ).</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">'actor' is null.</exception>
        protected virtual string CreateActorValue(ClaimsIdentity actor)
        {
            if (actor == null)
                throw LogHelper.LogException<ArgumentNullException>(LogMessages.IDX10003, "actor");

            if (actor.BootstrapContext != null)
            {
                string encodedJwt = actor.BootstrapContext as string;
                if (encodedJwt != null)
                {
                    IdentityModelEventSource.Logger.WriteVerbose(LogMessages.IDX10713);
                    return encodedJwt;
                }

                JwtSecurityToken jwt = actor.BootstrapContext as JwtSecurityToken;
                if (jwt != null)
                {
                    if (jwt.RawData != null)
                    {
                        IdentityModelEventSource.Logger.WriteVerbose(LogMessages.IDX10714);
                        return jwt.RawData;
                    }
                    else
                    {
                        IdentityModelEventSource.Logger.WriteVerbose(LogMessages.IDX10715);
                        return this.WriteToken(jwt);
                    }
                }

                IdentityModelEventSource.Logger.WriteVerbose(LogMessages.IDX10711);
            }

            IdentityModelEventSource.Logger.WriteVerbose(LogMessages.IDX10712);
            return WriteToken(new JwtSecurityToken(claims: actor.Claims));
        }

        /// <summary>
        /// Determines if the audiences found in a <see cref="JwtSecurityToken"/> are valid.
        /// </summary>
        /// <param name="audiences">The audiences found in the <see cref="JwtSecurityToken"/>.</param>
        /// <param name="securityToken">The <see cref="JwtSecurityToken"/> being validated.</param>
        /// <param name="validationParameters"><see cref="TokenValidationParameters"/> required for validation.</param>
        /// <remarks>see <see cref="Validators.ValidateAudience"/> for additional details.</remarks>
        protected virtual void ValidateAudience(IEnumerable<string> audiences, JwtSecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            Validators.ValidateAudience(audiences, securityToken, validationParameters);
        }

        /// <summary>
        /// Validates the lifetime of a <see cref="JwtSecurityToken"/>.
        /// </summary>
        /// <param name="notBefore">The <see cref="DateTime"/> value of the 'nbf' claim if it exists in the 'jwt'.</param>
        /// <param name="expires">The <see cref="DateTime"/> value of the 'exp' claim if it exists in the 'jwt'.</param>
        /// <param name="securityToken">The <see cref="JwtSecurityToken"/> being validated.</param>
        /// <param name="validationParameters"><see cref="TokenValidationParameters"/> required for validation.</param>
        /// <remarks><see cref="Validators.ValidateLifetime"/> for additional details.</remarks>
        protected virtual void ValidateLifetime(DateTime? notBefore, DateTime? expires, JwtSecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            Validators.ValidateLifetime(notBefore: notBefore, expires: expires, securityToken: securityToken, validationParameters: validationParameters);
        }

        /// <summary>
        /// Determines if an issuer found in a <see cref="JwtSecurityToken"/> is valid.
        /// </summary>
        /// <param name="issuer">The issuer to validate</param>
        /// <param name="securityToken">The <see cref="JwtSecurityToken"/> that is being validated.</param>
        /// <param name="validationParameters"><see cref="TokenValidationParameters"/> required for validation.</param>
        /// <returns>The issuer to use when creating the <see cref="Claim"/>(s) in the <see cref="ClaimsIdentity"/>.</returns>
        /// <remarks><see cref="Validators.ValidateIssuer"/> for additional details.</remarks>
        protected virtual string ValidateIssuer(string issuer, JwtSecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            return Validators.ValidateIssuer(issuer, securityToken, validationParameters);
        }

        /// <summary>
        /// Returns a <see cref="SecurityKey"/> to use when validating the signature of a token.
        /// </summary>
        /// <param name="token">the <see cref="string"/> representation of the token that is being validated.</param>
        /// <param name="securityToken">the <SecurityToken> that is being validated.</SecurityToken></param>
        /// <param name="kid">the key identifier found in the token.</param>
        /// <param name="validationParameters">A <see cref="TokenValidationParameters"/>  required for validation.</param>
        /// <returns>Returns a <see cref="SecurityKey"/> to use for signature validation.</returns>
        /// <remarks>If key fails to resolve, then null is returned</remarks>
        protected virtual SecurityKey ResolveIssuerSigningKey(JwtSecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (validationParameters == null)
                throw LogHelper.LogArgumentNullException("validationParameters");

            if (securityToken == null)
                throw LogHelper.LogArgumentNullException("securityToken");

            if (!string.IsNullOrEmpty(securityToken.Header.Kid))
            {
                string kid = securityToken.Header.Kid;
                if (validationParameters.IssuerSigningKey != null && string.Equals(validationParameters.IssuerSigningKey.KeyId, kid, StringComparison.Ordinal))
                {
                    return validationParameters.IssuerSigningKey;
                }

                if (validationParameters.IssuerSigningKeys != null)
                {
                    foreach (SecurityKey signingKey in validationParameters.IssuerSigningKeys)
                    {
                        if (signingKey != null && string.Equals(signingKey.KeyId, kid, StringComparison.Ordinal))
                        {
                            return signingKey;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(securityToken.Header.X5t))
            {
                string x5t = securityToken.Header.X5t;
                if (validationParameters.IssuerSigningKey != null && string.Equals(validationParameters.IssuerSigningKey.KeyId, x5t, StringComparison.Ordinal))
                {
                    return validationParameters.IssuerSigningKey;
                }

                if (validationParameters.IssuerSigningKeys != null)
                {
                    foreach (SecurityKey signingKey in validationParameters.IssuerSigningKeys)
                    {
                        if (signingKey != null && string.Equals(signingKey.KeyId, x5t, StringComparison.Ordinal))
                        {
                            return signingKey;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets or sets a bool that controls if token creation will set default 'exp', 'nbf' and 'iat' if not specified.
        /// </summary>
        /// <remarks>see: <see cref="DefaultTokenLifetimeInMinutes"/>, <see cref="TokenLifetimeInMinutes"/> for defaults and configuration.</remarks>
        [DefaultValue(true)]
        public bool SetDefaultTimesOnTokenCreation { get; set; }

        /// <summary>
        /// Validates the <see cref="JwtSecurityToken.SigningKey"/> is an expected value.
        /// </summary>
        /// <param name="securityKey">The <see cref="SecurityKey"/> that signed the <see cref="SecurityToken"/>.</param>
        /// <param name="securityToken">The <see cref="JwtSecurityToken"/> to validate.</param>
        /// <param name="validationParameters">the current <see cref="TokenValidationParameters"/>.</param>
        /// <remarks>If the <see cref="JwtSecurityToken.SigningKey"/> is a <see cref="X509SecurityKey"/> then the X509Certificate2 will be validated using <see cref="TokenValidationParameters.CertificateValidator"/>.</remarks>
        protected virtual void ValidateIssuerSecurityKey(SecurityKey securityKey, JwtSecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            Validators.ValidateIssuerSecurityKey(securityKey, securityToken, validationParameters);
        }

        /// <summary>
        /// Serializes to XML a token of the type handled by this instance.
        /// </summary>
        /// <param name="writer">The XML writer.</param>
        /// <param name="token">A token of type <see cref="TokenType"/>.</param>
        public override void WriteToken(XmlWriter writer, SecurityToken token)
        {
            throw new NotImplementedException();
        }

        public override SecurityToken ReadToken(XmlReader reader, TokenValidationParameters validationParameters)
        {
            throw new NotImplementedException();
        }
    }
}
