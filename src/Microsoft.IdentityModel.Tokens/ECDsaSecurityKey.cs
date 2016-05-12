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

using System;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Represents a ECDsa security key.
    /// </summary>
    public class ECDsaSecurityKey : AsymmetricSecurityKey
    {
        private bool? _hasPrivateKey;

        /// <summary>
        /// Returns a new instance of <see cref="ECDsaSecurityKey"/>.
        /// </summary>
        /// <param name="ecdsa"><see cref="System.Security.Cryptography.ECDsa"/></param>
        public ECDsaSecurityKey(ECDsa ecdsa)
        {
            if (ecdsa == null)
                throw LogHelper.LogArgumentNullException("ecdsa");

            ECDsa = ecdsa;
        }

        /// <summary>
        /// <see cref="System.Security.Cryptography.ECDsa"/> instance used to initialize the key.
        /// </summary>
        public ECDsa ECDsa { get; private set; }

        /// <summary>
        /// true if has private key; otherwise, false.
        /// </summary>
        public override bool HasPrivateKey
        {
            get
            {
                if (_hasPrivateKey == null)
                {
                    try
                    {
                        // imitate signing
                        byte[] hash = new byte[20];
#if NETSTANDARD1_4
                        ECDsa.SignData(hash, HashAlgorithmName.SHA256);
#else
                    ECDsa.SignHash(hash);
#endif
                        _hasPrivateKey = true;
                    }
                    catch (CryptographicException)
                    {
                        _hasPrivateKey = false;
                    }
                }
                return _hasPrivateKey.Value;
            }
        }

        /// <summary>
        /// Gets <see cref="System.Security.Cryptography.ECDsa"/> key size.
        /// </summary>
        public override int KeySize
        {
            get
            {
                return ECDsa.KeySize;
            }
        }

        /// <summary>
        /// Returns a <see cref="SignatureProvider"/> instance that supports the algorithm.
        /// </summary>
        /// <param name="algorithm">the algorithm to use for verifying/signing.</param>
        /// <param name="verifyOnly">This value has to be false if need create Signatures.</param>
        /// <returns></returns>
        public override SignatureProvider GetSignatureProvider(string algorithm, bool verifyOnly)
        {
            if (verifyOnly)
                return CryptoProviderFactory.CreateForVerifying(this, algorithm);
            else
                return CryptoProviderFactory.CreateForSigning(this, algorithm);
        }

        /// <summary>
        /// true if this <see cref="ECDsaSecurityKey"/> supports the algorithm; otherwise, false.
        /// </summary>
        /// <param name="algorithm">the crypto algorithm to use.</param>
        public override bool IsSupportedAlgorithm(string algorithm)
        {
            if (string.IsNullOrEmpty(algorithm))
                return false;

            switch (algorithm)
            {
                case SecurityAlgorithms.EcdsaSha256:
                case SecurityAlgorithms.EcdsaSha384:
                case SecurityAlgorithms.EcdsaSha512:
                case SecurityAlgorithms.EcdsaSha256Signature:
                case SecurityAlgorithms.EcdsaSha384Signature:
                case SecurityAlgorithms.EcdsaSha512Signature:
                    return true;

                default:
                    return false;
            }
        }
    }
}
