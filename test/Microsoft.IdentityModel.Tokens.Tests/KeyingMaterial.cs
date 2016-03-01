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
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.IdentityModel.Tokens.Tests
{
    static public class KeyingMaterial
    {
        static KeyingMaterial()
        {
            byte[] cspbytes_1024 = Base64UrlEncoder.DecodeBytes("BwIAAACkAABSU0EyAAQAAAEAAQAlur35vBYFooH0yfB3G919joyz-7xD8LcoQLRIqV7DdEicgTkJWD8sfDvxHRf18w2bA2kx_Bg89855uR3yDvIqtcZ-vq6Gv6yvx7iSjuXW_SNV4gVSjShBuCMelyU-dsHK-IuLcyMcms93fQ3Gh13_AFeyGuT2P0g7LUEEdz8K099x6CCApMROhY261NbN-d5uDE33bypd8tfLCWj2jlSZUNX__O7OUCVqegyI3rAPCpbtB5V7jw7uKD2lR6OeZfN7fPlPPtQEXyaIzYQHo8td6ASYcIFAUjxkBhN8lMUG2FXe-jH3tYYf3FMywf6GA6bo1LfVMW1Sb935YrGAt_fd-8YFoKDUoZgMTjcEiM0Koq80DM8Hy0rb1F4KnzKpXPy2XeGAxIEM-6MMINuh2aY-gZ6oMUomEcw9uSW1hBNk5mtPAdUqHfud8RBYvTH5yx-Cipu6wDLoQl4UTZcop-tVLpJGWJpDYkeLsda2pKJpJcITs3_gRq-QjUIG7-M2OMEvKwBd3tMfIHNkVA-RBk6v_dEHH8cRHvPZC2wna7FQztJqN5ybOSWcpqCX5RvbMkfK38hdGo6oPUkQ3YmtFLFOialpsJo-c_HDOlI32fCnjTLRsR9B-JbSDRLEHg0bVmgVyL1oZaLPYAMyUH6grtel2enOiUODgX9YZbynGtHjvGMn0-3nbz2TRUlchl5b-mQqPbM673WnddAUeaaqNc7gopo5Zofsd6-YV1Z0nL-XzLad2Ax9aAHpVoejevXGz1w");
            byte[] cspbytes_1024_Public = Base64UrlEncoder.DecodeBytes("BgIAAACkAABSU0ExAAQAAAEAAQAlur35vBYFooH0yfB3G919joyz-7xD8LcoQLRIqV7DdEicgTkJWD8sfDvxHRf18w2bA2kx_Bg89855uR3yDvIqtcZ-vq6Gv6yvx7iSjuXW_SNV4gVSjShBuCMelyU-dsHK-IuLcyMcms93fQ3Gh13_AFeyGuT2P0g7LUEEdz8K0w");

            RsaParameters_1024 =
                new RSAParameters
                {
                    D = Base64UrlEncoder.DecodeBytes("XM_G9Xqjh1bpAWh9DNidtsyXv5x0VleYr3fsh2Y5mqLgzjWqpnkU0HWnde86sz0qZPpbXoZcSUWTPW_n7dMnY7zj0RqnvGVYf4GDQ4nO6dml166gflAyA2DPomVovcgVaFYbDR7EEg3SlvhBH7HRMo2n8Nk3UjrD8XM-mrBpqYk"),
                    DP = Base64UrlEncoder.DecodeBytes("5mQThLUluT3MESZKMaiegT6m2aHbIAyj-wyBxIDhXbb8XKkynwpe1NtKywfPDDSvogrNiAQ3TgyYodSgoAXG-w"),
                    DQ = Base64UrlEncoder.DecodeBytes("wTg24-8GQo2Qr0bgf7MTwiVpoqS21rGLR2JDmlhGki5V66col00UXkLoMsC6m4qCH8v5Mb1YEPGd-x0q1QFPaw"),
                    Exponent = Base64UrlEncoder.DecodeBytes("AQAB"),
                    InverseQ = Base64UrlEncoder.DecodeBytes("TrEUrYndEEk9qI4aXcjfykcy2xvll6CmnCU5m5w3atLOULFrJ2wL2fMeEccfB9H9r04GkQ9UZHMgH9PeXQArLw"),
                    Modulus = Base64UrlEncoder.DecodeBytes("0wo_dwRBLTtIP_bkGrJXAP9dh8YNfXfPmhwjc4uL-MrBdj4llx4juEEojVIF4lUj_dbljpK4x6-sv4auvn7GtSryDvIduXnO9zwY_DFpA5sN8_UXHfE7fCw_WAk5gZxIdMNeqUi0QCi38EO8-7OMjn3dG3fwyfSBogUWvPm9uiU"),
                    P = Base64UrlEncoder.DecodeBytes("82Weo0elPSjuDo97lQftlgoPsN6IDHpqJVDO7vz_1VCZVI72aAnL1_JdKm_3TQxu3vnN1tS6jYVOxKSAIOhx3w"),
                    Q = Base64UrlEncoder.DecodeBytes("3fe3gLFi-d1vUm0x1bfU6KYDhv7BMlPcH4a19zH63lXYBsWUfBMGZDxSQIFwmAToXcujB4TNiCZfBNQ-T_l8ew"),
                };

            RsaParameters_1024_Public =
                new RSAParameters
                {
                    Exponent = Base64UrlEncoder.DecodeBytes("AQAB"),
                    Modulus = Base64UrlEncoder.DecodeBytes("0wo_dwRBLTtIP_bkGrJXAP9dh8YNfXfPmhwjc4uL-MrBdj4llx4juEEojVIF4lUj_dbljpK4x6-sv4auvn7GtSryDvIduXnO9zwY_DFpA5sN8_UXHfE7fCw_WAk5gZxIdMNeqUi0QCi38EO8-7OMjn3dG3fwyfSBogUWvPm9uiU"),
                };

            byte[] cspbytes_2048 = Base64UrlEncoder.DecodeBytes("BwIAAACkAABSU0EyAAgAAAEAAQCP7GdRPSJ5_SDCM4mQsuRP72_P19sw7w7qStb9CXj4aun9iY81bhXI8YjGHZPDrhaSi4bwhGIx5JTNsxBejBTU0QOsLqb8IvlabEHD7T2J4GfIsNwh5u_u8chKMRWeUkxqLzaXdVwickmtvG7t7BjghMzl6Ubwv1DL9prH-pkH56fTqbsu1EwEF4DxWS6RBg1DKNzlmtt5SYYmNksH89yXUV5118YY0UMxJZd3a4Ir_r8wNq4ZtotCAyTfLnCKryTLPrJUcduPlTnjXeegPTFH57fK_UQjq3RGOeTkiDLvKrjQ2FRgXtLdHhXDbAqjQPX0_oruXb78kBxMf0sWa-HrmZTz2hg-VrxNsnJPS-2f-4_MIa8q2dOfGgbxQxLZM19_3VHG_qcn3sXPiVzEwjTvR8edj6wCQ96SYAOkSWByuMplbrzFMfq37u0x6JhOrbkBKWgnU0FO8JvMnAO6jpxNifsWjLkk-ca-JDbzX1L_5u54pHNE76rlEe6oKwvCq_1nG3MI3d_X623S9aQwFw94UGseJfma-6tRAj95LniiXBD7ifjLpCeZvO2W_iLHOOp2WB7DSrNQ_rLfRoStq9DxQY8To4O2SMYPQT7To6tRhRV7lsVuzoGrJIlurXjGXeGBD2gvBTy2_7qhE8ZKiYun67JpIsrN84LkM2pKRtgL7gG1kwCRFEPH_7Pl4JUMCV6kv1DeWCdKlz6fFMatcvN_MUQDBdzjpkgabhc7O_-gBCJp71gnIKsQeMNG_LBxKP0wVZBOtCW3bBmNUSHNZF5CMr7TD7TxXNP6K5sDafOc9_vHz2lkxDDaurY659PrXifpdYFKheHeGPB4nZs697MS-3AARGqCDdoekJft6mF613pzUTOBEc00hm0ehVWVent58JU3ThmcIkImGKxgWaCpaEunlcGVgmpJG562zYPWPTUzsmDnTjciCSZMw_F8PdKzN5kU73JX1yY8k0Oh3ETOxl-uFRGPOiucAeCmvw-9_Mkmyjcrv4Xsk9ftE7ZssGH-F3_O4CMGbQVnsBSpAnO8Yi0f3a9vhipydlTzqpUK7LCoRn4gWVDdsbSmlv-2zjypk_BpgoB4L6ASJOyRySk4KoYSSs38yLEg8SQyNSR95GWZJPEmZhxi5R_03TZcaicn_YCMyfNf9hLmNy8zHetIz8An2qLoZv8w-FglW2O5lXFdKgRb_W-Af2GcjyWGWsMywGEsE6p7A8Ytm8RrwZueTcib_YPFG6_Q4rHHGxcbA_fbz6GKLXrx9oY3xZfNi_3ebPE2aci3CXtoWkH3FRW6kZvGnQsHxxfBww3dogWdV9a0nVdBkfVOD8gfTdCLO8RfARUt1UYwdO22aUqSbxlnxdSgrzFKePsGp_iHCNDZlWeQOIXCIjg2oQLPPT8LYKJrxeWMsyhUw3KlK7cuU_STNLO8CmFD7p025xCrzpK1cC4VeODjur7nB-zuLdmjn-JbLSv0ky9eTNyTkkguhcBbpejM8wEuE3R-HKmV6SrgL8OlhAYBneVIpFP9h2UGoQs");
            byte[] cspbytes_2048_Public = Base64UrlEncoder.DecodeBytes("BgIAAACkAABSU0ExAAgAAAEAAQCP7GdRPSJ5_SDCM4mQsuRP72_P19sw7w7qStb9CXj4aun9iY81bhXI8YjGHZPDrhaSi4bwhGIx5JTNsxBejBTU0QOsLqb8IvlabEHD7T2J4GfIsNwh5u_u8chKMRWeUkxqLzaXdVwickmtvG7t7BjghMzl6Ubwv1DL9prH-pkH56fTqbsu1EwEF4DxWS6RBg1DKNzlmtt5SYYmNksH89yXUV5118YY0UMxJZd3a4Ir_r8wNq4ZtotCAyTfLnCKryTLPrJUcduPlTnjXeegPTFH57fK_UQjq3RGOeTkiDLvKrjQ2FRgXtLdHhXDbAqjQPX0_oruXb78kBxMf0sWa-Hr");

            RsaParameters_2048 =
                new RSAParameters
                {
                    D = Base64UrlEncoder.DecodeBytes("C6EGZYf9U6RI5Z0BBoSlwy_gKumVqRx-dBMuAfPM6KVbwIUuSJKT3ExeL5P0Ky1b4p-j2S3u7Afnvrrj4HgVLnC1ks6rEOc2ne5DYQq8szST9FMutyulcsNUKLOM5cVromALPz3PAqE2OCLChTiQZ5XZ0AiH-KcG-3hKMa-g1MVnGW-SSmm27XQwRtUtFQFfxDuL0E0fyA9O9ZFBV5201ledBaLdDcPBF8cHC53Gm5G6FRX3QVpoewm3yGk28Wze_YvNl8U3hvbxei2Koc_b9wMbFxvHseLQrxvFg_2byE2em8FrxJstxgN7qhMsYcAyw1qGJY-cYX-Ab_1bBCpdcQ"),
                    DP = Base64UrlEncoder.DecodeBytes("ErP3OpudePAY3uGFSoF16Sde69PnOra62jDEZGnPx_v3nPNpA5sr-tNc8bQP074yQl5kzSFRjRlstyW0TpBVMP0ocbD8RsN4EKsgJ1jvaSIEoP87OxduGkim49wFA0Qxf_NyrcYUnz6XSidY3lC_pF4JDJXg5bP_x0MUkQCTtQE"),
                    DQ = Base64UrlEncoder.DecodeBytes("YbBsthPt15Pshb8rN8omyfy9D7-m4AGcKzqPERWuX8bORNyhQ5M8JtdXcu8UmTez0j188cNMJgkiN07nYLIzNT3Wg822nhtJaoKVwZWnS2ipoFlgrBgmQiKcGU43lfB5e3qVVYUebYY0zRGBM1Fzetd6Yertl5Ae2g2CakQAcPs"),
                    Exponent = Base64UrlEncoder.DecodeBytes("AQAB"),
                    InverseQ = Base64UrlEncoder.DecodeBytes("lbljWyVY-DD_Zuii2ifAz0jrHTMvN-YS9l_zyYyA_Scnalw23fQf5WIcZibxJJll5H0kNTIk8SCxyPzNShKGKjgpyZHsJBKgL3iAgmnwk6k8zrb_lqa0sd1QWSB-Rqiw7AqVqvNUdnIqhm-v3R8tYrxzAqkUsGcFbQYj4M5_F_4"),
                    Modulus = Base64UrlEncoder.DecodeBytes("6-FrFkt_TByQ_L5d7or-9PVAowpswxUe3dJeYFTY0Lgq7zKI5OQ5RnSrI0T9yrfnRzE9oOdd4zmVj9txVLI-yySvinAu3yQDQou2Ga42ML_-K4Jrd5clMUPRGMbXdV5Rl9zzB0s2JoZJedua5dwoQw0GkS5Z8YAXBEzULrup06fnB5n6x5r2y1C_8Ebp5cyE4Bjs7W68rUlyIlx1lzYvakxSnhUxSsjx7u_mIdywyGfgiT3tw0FsWvki_KYurAPR1BSMXhCzzZTkMWKE8IaLkhauw5MdxojxyBVuNY-J_elq-HgJ_dZK6g7vMNvXz2_vT-SykIkzwiD9eSI9UWfsjw"),
                    P = Base64UrlEncoder.DecodeBytes("_avCCyuo7hHlqu9Ec6R47ub_Ul_zNiS-xvkkuYwW-4lNnI66A5zMm_BOQVMnaCkBua1OmOgx7e63-jHFvG5lyrhyYEmkA2CS3kMCrI-dx0fvNMLEXInPxd4np_7GUd1_XzPZEkPxBhqf09kqryHMj_uf7UtPcrJNvFY-GNrzlJk"),
                    Q = Base64UrlEncoder.DecodeBytes("7gvYRkpqM-SC883KImmy66eLiUrGE6G6_7Y8BS9oD4HhXcZ4rW6JJKuBzm7FlnsVhVGro9M-QQ_GSLaDoxOPQfHQq62ERt-y_lCzSsMeWHbqOMci_pbtvJknpMv4ifsQXKJ4Lnk_AlGr-5r5JR5rUHgPFzCk9dJt69ff3QhzG2c"),
                };

            RsaParameters_2048_Public =
                new RSAParameters
                {
                    Exponent = Base64UrlEncoder.DecodeBytes("AQAB"),
                    Modulus = Base64UrlEncoder.DecodeBytes("6-FrFkt_TByQ_L5d7or-9PVAowpswxUe3dJeYFTY0Lgq7zKI5OQ5RnSrI0T9yrfnRzE9oOdd4zmVj9txVLI-yySvinAu3yQDQou2Ga42ML_-K4Jrd5clMUPRGMbXdV5Rl9zzB0s2JoZJedua5dwoQw0GkS5Z8YAXBEzULrup06fnB5n6x5r2y1C_8Ebp5cyE4Bjs7W68rUlyIlx1lzYvakxSnhUxSsjx7u_mIdywyGfgiT3tw0FsWvki_KYurAPR1BSMXhCzzZTkMWKE8IaLkhauw5MdxojxyBVuNY-J_elq-HgJ_dZK6g7vMNvXz2_vT-SykIkzwiD9eSI9UWfsjw"),
                };

            RsaParameters_2048_MissingModulus =
                new RSAParameters
                {
                    D = Base64UrlEncoder.DecodeBytes("C6EGZYf9U6RI5Z0BBoSlwy_gKumVqRx-dBMuAfPM6KVbwIUuSJKT3ExeL5P0Ky1b4p-j2S3u7Afnvrrj4HgVLnC1ks6rEOc2ne5DYQq8szST9FMutyulcsNUKLOM5cVromALPz3PAqE2OCLChTiQZ5XZ0AiH-KcG-3hKMa-g1MVnGW-SSmm27XQwRtUtFQFfxDuL0E0fyA9O9ZFBV5201ledBaLdDcPBF8cHC53Gm5G6FRX3QVpoewm3yGk28Wze_YvNl8U3hvbxei2Koc_b9wMbFxvHseLQrxvFg_2byE2em8FrxJstxgN7qhMsYcAyw1qGJY-cYX-Ab_1bBCpdcQ"),
                    DP = Base64UrlEncoder.DecodeBytes("ErP3OpudePAY3uGFSoF16Sde69PnOra62jDEZGnPx_v3nPNpA5sr-tNc8bQP074yQl5kzSFRjRlstyW0TpBVMP0ocbD8RsN4EKsgJ1jvaSIEoP87OxduGkim49wFA0Qxf_NyrcYUnz6XSidY3lC_pF4JDJXg5bP_x0MUkQCTtQE"),
                    DQ = Base64UrlEncoder.DecodeBytes("YbBsthPt15Pshb8rN8omyfy9D7-m4AGcKzqPERWuX8bORNyhQ5M8JtdXcu8UmTez0j188cNMJgkiN07nYLIzNT3Wg822nhtJaoKVwZWnS2ipoFlgrBgmQiKcGU43lfB5e3qVVYUebYY0zRGBM1Fzetd6Yertl5Ae2g2CakQAcPs"),
                    Exponent = Base64UrlEncoder.DecodeBytes("AQAB"),
                    InverseQ = Base64UrlEncoder.DecodeBytes("lbljWyVY-DD_Zuii2ifAz0jrHTMvN-YS9l_zyYyA_Scnalw23fQf5WIcZibxJJll5H0kNTIk8SCxyPzNShKGKjgpyZHsJBKgL3iAgmnwk6k8zrb_lqa0sd1QWSB-Rqiw7AqVqvNUdnIqhm-v3R8tYrxzAqkUsGcFbQYj4M5_F_4"),
                    P = Base64UrlEncoder.DecodeBytes("_avCCyuo7hHlqu9Ec6R47ub_Ul_zNiS-xvkkuYwW-4lNnI66A5zMm_BOQVMnaCkBua1OmOgx7e63-jHFvG5lyrhyYEmkA2CS3kMCrI-dx0fvNMLEXInPxd4np_7GUd1_XzPZEkPxBhqf09kqryHMj_uf7UtPcrJNvFY-GNrzlJk"),
                    Q = Base64UrlEncoder.DecodeBytes("7gvYRkpqM-SC883KImmy66eLiUrGE6G6_7Y8BS9oD4HhXcZ4rW6JJKuBzm7FlnsVhVGro9M-QQ_GSLaDoxOPQfHQq62ERt-y_lCzSsMeWHbqOMci_pbtvJknpMv4ifsQXKJ4Lnk_AlGr-5r5JR5rUHgPFzCk9dJt69ff3QhzG2c"),
                };

            RsaParameters_2048_MissingExponent =
                new RSAParameters
                {
                    D = Base64UrlEncoder.DecodeBytes("C6EGZYf9U6RI5Z0BBoSlwy_gKumVqRx-dBMuAfPM6KVbwIUuSJKT3ExeL5P0Ky1b4p-j2S3u7Afnvrrj4HgVLnC1ks6rEOc2ne5DYQq8szST9FMutyulcsNUKLOM5cVromALPz3PAqE2OCLChTiQZ5XZ0AiH-KcG-3hKMa-g1MVnGW-SSmm27XQwRtUtFQFfxDuL0E0fyA9O9ZFBV5201ledBaLdDcPBF8cHC53Gm5G6FRX3QVpoewm3yGk28Wze_YvNl8U3hvbxei2Koc_b9wMbFxvHseLQrxvFg_2byE2em8FrxJstxgN7qhMsYcAyw1qGJY-cYX-Ab_1bBCpdcQ"),
                    DP = Base64UrlEncoder.DecodeBytes("ErP3OpudePAY3uGFSoF16Sde69PnOra62jDEZGnPx_v3nPNpA5sr-tNc8bQP074yQl5kzSFRjRlstyW0TpBVMP0ocbD8RsN4EKsgJ1jvaSIEoP87OxduGkim49wFA0Qxf_NyrcYUnz6XSidY3lC_pF4JDJXg5bP_x0MUkQCTtQE"),
                    DQ = Base64UrlEncoder.DecodeBytes("YbBsthPt15Pshb8rN8omyfy9D7-m4AGcKzqPERWuX8bORNyhQ5M8JtdXcu8UmTez0j188cNMJgkiN07nYLIzNT3Wg822nhtJaoKVwZWnS2ipoFlgrBgmQiKcGU43lfB5e3qVVYUebYY0zRGBM1Fzetd6Yertl5Ae2g2CakQAcPs"),
                    InverseQ = Base64UrlEncoder.DecodeBytes("lbljWyVY-DD_Zuii2ifAz0jrHTMvN-YS9l_zyYyA_Scnalw23fQf5WIcZibxJJll5H0kNTIk8SCxyPzNShKGKjgpyZHsJBKgL3iAgmnwk6k8zrb_lqa0sd1QWSB-Rqiw7AqVqvNUdnIqhm-v3R8tYrxzAqkUsGcFbQYj4M5_F_4"),
                    Modulus = Base64UrlEncoder.DecodeBytes("6-FrFkt_TByQ_L5d7or-9PVAowpswxUe3dJeYFTY0Lgq7zKI5OQ5RnSrI0T9yrfnRzE9oOdd4zmVj9txVLI-yySvinAu3yQDQou2Ga42ML_-K4Jrd5clMUPRGMbXdV5Rl9zzB0s2JoZJedua5dwoQw0GkS5Z8YAXBEzULrup06fnB5n6x5r2y1C_8Ebp5cyE4Bjs7W68rUlyIlx1lzYvakxSnhUxSsjx7u_mIdywyGfgiT3tw0FsWvki_KYurAPR1BSMXhCzzZTkMWKE8IaLkhauw5MdxojxyBVuNY-J_elq-HgJ_dZK6g7vMNvXz2_vT-SykIkzwiD9eSI9UWfsjw"),
                    P = Base64UrlEncoder.DecodeBytes("_avCCyuo7hHlqu9Ec6R47ub_Ul_zNiS-xvkkuYwW-4lNnI66A5zMm_BOQVMnaCkBua1OmOgx7e63-jHFvG5lyrhyYEmkA2CS3kMCrI-dx0fvNMLEXInPxd4np_7GUd1_XzPZEkPxBhqf09kqryHMj_uf7UtPcrJNvFY-GNrzlJk"),
                    Q = Base64UrlEncoder.DecodeBytes("7gvYRkpqM-SC883KImmy66eLiUrGE6G6_7Y8BS9oD4HhXcZ4rW6JJKuBzm7FlnsVhVGro9M-QQ_GSLaDoxOPQfHQq62ERt-y_lCzSsMeWHbqOMci_pbtvJknpMv4ifsQXKJ4Lnk_AlGr-5r5JR5rUHgPFzCk9dJt69ff3QhzG2c"),
                };

            byte[] cspbytes_4096 = Base64UrlEncoder.DecodeBytes("BwIAAACkAABSU0EyABAAAAEAAQCZrcZ3JOjYvkjBZwkC_ukZook4u8se9AjHSP6bySfv69VLwlQLAREu-qz13lsoylKxVT4Abz2TxI4oLKKogKAdinRoNRYrIidsbOe6wHbsxreV6Qt4aiU_YdR0PnisyIx9jnheWtcO-_PCW5dQv9-OnjbnKxgy1A5XhHaj-MhL-uVOBKHmk1cXTMcAOzt1XL_oYjxiftg_InHBBT7hG6HrPyritd_np1TckPnuJq5GeajEcwmSIxGNtR4WlTyGD-5MyIVxxWvQgvWjXHDCXp01eA_R-3KImaE62cAULabdKHbNzQFAfJOb-nmAiZU9bhKw7TdZb9Omjr7DadZ8WrqlY7kYmk73IczxX3Vnvm67_Haj7hBrKewJfvQ3SPyqF85CpyNLCrLXzLxVNqEg-YxNkg5Zlf62-t3BA-QHUW7mOIi87iuwvp6WmoZ_o_JEcAl7DY1XlsMVF5v5KrhsAm9FeUQTjFpZkkrFWkgVzBYZ0RpJbDtREgTJXy8764Gq0qcgk7CvC7RkYAtUXj_0rME6nbooLDpKhiQNl9-OLGas-kh9MtR76jTbteToQz2XsIZI3gnJjzpb_BNSvABRqbSgWJ8mVTkGmIc-YImXUqxriE5P4EdulcX5orRkD1LTWiS5tnLqLCUlK-v9SEC0raKx99H5CXl4xv_EDsM2qubzvZXWPbz3kSQY1zhwdKcemx4iIUg69JK7v9PAApf136EVRX3wjVhLUHJfBVAZ0H_VGSCosxmbUuyyh1akiLwzaH1crZz0M9uJ1iS1HZrxpVGn4DuI0A-2ZTP7GAA_q2PjVcNR65LJxAsQmQLHESnWxqA4RZxcxtCukB158isSW6cpxgnsVG2hSHh9spBUaRjzqzejAgh2lrknSJEDSxrfCusdlLmTwbQvOUANAWEpyyVUAnHToFFY6lg6jkJqlAKUdAXw-zPuE3gDaC4fwZL9pCyY-0V2kVW_xusGKg-peiAPGAdaqnL9O0l_tcHrXLNxjySMHPY925ywNf21gTHVfM_1zfc66A_8qv7bAPFMZ-9X0c2Fxq8sGbKDwwj4mnuwB5zRR1wg7YqsvR6XT29h2j7O1VtzJWAfkoMRO_g0cqQP6c9GHPIO9kmLua0KzxRD-KTaQthvgt3x8cn7Ru_NJ3UNDh5cdzt2ieuQFvTGsyoctrRWta9rzb_C0BisBWw-PejJ5yUbFaTi1pJvLOoZTZZ7lCMw5DyRMS3r59Fv7hCY2MI9uIrr4HGOgCdDtYq5Xq4TkKolf887HkuFnUrPsaqk8N-eib1XOlvdyN4YUTlJx6uLTU-kruwEJBZxZ-IRwKDRTG73dVGgf61FSFmD5jpuWNoVkzx2f1UMCYmQhF3qGRIBar0gA7EQCl7IB2TXKuC746nismR2GhlRzktUloCMrzn9OqTgLu39okB4aE_ik1PElT7NDSZNVR41rQtXhHKKOO--Ch4fFv4zlpxErAIB1hu8w4stBx0ZXOyZegHsG0b6JAq0dVjqE357GzypgrrnwvBBDwAW3hPHyHbbKFcjH2N3RRCAHZlsafa_dBHkeevcktWiEarVAhRf0dGjAapoacrL8izb5mr7NGCmg2cwF_F6WvZJhjG5SPSj2HbfLoEkLGUMtDewYK7717_JRFOUEW2OHBfdL0GB0w6hTgLMoKfYXt_rYZw87Bzim5xXLNCZ5BTiWQO4LA8-O3-8oWkHNpNAAPKLXQaGpqopBHa3ujIUQCsINKZuaMMOnl_PFALxGiVY5f9JpHMGtjAVplAZZ4vPZsW7X7O478pwejNRR3_Y4SWNWZglyBamKRxeSz007lwtnbQoywtX72QNvNgpd6jY6zf4h5hNxhZqVg0FVrQUZUBnrA2TkFP3xUDoUOA26IlSf-qKYUus36wxNet-nyoMrAunQ0RubAwE7WWlGmBX-AQ5hXew7Cb-vjhjaZtGecJxQvVIcSBsvBK9Zfp7T85TaRFsHYH_Em8Eysb3grTTNnw6tkx2n43AOML3zyQiSgO4TxwzIINKrDwx94hH4tgBlph636eQLySS3nH0bSUw0pbBZurtkeX3IKYHdsCZfMtmhJUZxLMSLlZILQLm_6GD3I6kHINvGTdhHnZSoS4BecS3vBL3tXa-p_UAvhra8u7YLuGBkM8ZUV3a7OifB0R5TKhf_nyPsJg9_W5HnFvdv1wRsL1u8vdQOFHu4O9iCbLtDs2yTmRo0UTuWBItAqOhDqcU2wA6blptT-t7KhZvfOApK10kvEeDov6ch49JXX0CIQqUe2uzWh7Hc1xVI48ahK9aautS0EpaplbPuipNp1qIGSOOeVkDYPGd0KtjtlI2ZZdvxHu1UDsfZBBc-ngZWPp8IbGFMLIvv_ZnaCwn9hzCBT87WHg9dAEbudMxNw7mBBkjDJWqNFZVWDdeZk-dx2rAC5cM09CCkjjHjX2_eCpztb65lvM0ir1l8tq3SPaz2N9OLwipLRdm8vjWwSRGYaApXqxC4ZgxzSgTm-9Yd4o60zE5JdHyyCZ0DIuHieJ437ut-ozQ5-cV8d2yUrOQWjx97ehixruKZlf8PnOn-0Q48mGtqOJ2gQNiULIpksv7AGxORmRHKoBGQtzjiGfViOxk6hf62-ilAbz7fLeSLVZhG1ixaQWXkWeSf9G27mgEvt3IfrSJLmb4tMsspeYFYdjlTtkz1XBbpuv-F6-vhbgRnQQeR9OAa5BAtsFHvDU2KSOfkJfqWVQ9ffxriCTY3R1og4U52-Jdt64IlA-7-ZBbourBEbuCti8wC0UsjgtH4uAhhu4I7Td8ja-l2MJAGu5kvOoGRvg09ClyW3Top7Bmr7YkKchSjwyrPwhJtn5edl_N9p7i_1uHTrzTmNW7z4_g8NZl1oy2F14KQXCrjo8Kw_gt7D43nu9A2MXEYvIxEoaRu6SJJh8dL-Wfjo4dNsNS6iq-RxGtlJigMRV_lAG6n9U2YPws3xj6cTRiAnRcJcpRm8QyHoE-BlxWV739ENQXcno-qpYOT6UMO-wwCTU0x92rrDAL9ow4TkjX3VrMV9B1BLUFakDfj_pRp5P4bBMtMah20CZEXgc");
            byte[] cspbytes_4096_Public = Base64UrlEncoder.DecodeBytes("BgIAAACkAABSU0ExABAAAAEAAQCZrcZ3JOjYvkjBZwkC_ukZook4u8se9AjHSP6bySfv69VLwlQLAREu-qz13lsoylKxVT4Abz2TxI4oLKKogKAdinRoNRYrIidsbOe6wHbsxreV6Qt4aiU_YdR0PnisyIx9jnheWtcO-_PCW5dQv9-OnjbnKxgy1A5XhHaj-MhL-uVOBKHmk1cXTMcAOzt1XL_oYjxiftg_InHBBT7hG6HrPyritd_np1TckPnuJq5GeajEcwmSIxGNtR4WlTyGD-5MyIVxxWvQgvWjXHDCXp01eA_R-3KImaE62cAULabdKHbNzQFAfJOb-nmAiZU9bhKw7TdZb9Omjr7DadZ8WrqlY7kYmk73IczxX3Vnvm67_Haj7hBrKewJfvQ3SPyqF85CpyNLCrLXzLxVNqEg-YxNkg5Zlf62-t3BA-QHUW7mOIi87iuwvp6WmoZ_o_JEcAl7DY1XlsMVF5v5KrhsAm9FeUQTjFpZkkrFWkgVzBYZ0RpJbDtREgTJXy8764Gq0qcgk7CvC7RkYAtUXj_0rME6nbooLDpKhiQNl9-OLGas-kh9MtR76jTbteToQz2XsIZI3gnJjzpb_BNSvABRqbSgWJ8mVTkGmIc-YImXUqxriE5P4EdulcX5orRkD1LTWiS5tnLqLCUlK-v9SEC0raKx99H5CXl4xv_EDsM2qubzvQ");

            RsaParameters_4096 =
                new RSAParameters
                {
                    D = Base64UrlEncoder.DecodeBytes("B15EJtB2qDEtE2z4k6dR-o_fQGoFtQR10FfMWt3XSE44jPYLMKyr3cc0NQkw7DsMpU8Olqo-enIX1BD9vVdWXAY-gR4yxJtRyiVcdAJiNHH6GN8s_GA21Z-6AZR_FTGgmJStEUe-KupSwzYdjo6f5S8dHyaJpLuRhhIx8mLExdhA7543Puwt-MMKj46rcEEKXhe2jNZl1vDgj8-71ZjTvE6HW__invbNX3ZefrZJCD-rDI9SyCkktq9msKfodFtyKfQ0-EYG6rxk7hpAwtilr418N-0I7oYh4OJHC44sRQswL7aCuxHB6qJbkPm7D5QIrrdd4ts5hYNoHd3YJIhr_H09VFnql5CfIyk2NbxHwbZAkGuA00ceBJ0RuIWvrxf-66ZbcNUz2U7l2GEF5qUsy7T4Zi6JtH7I3b4EaO620X-SZ5GXBWmxWBthVi2St3z7vAGl6Nv6F-pk7IjVZ4jj3EJGgCpHZEZObAD7y5IpslBiA4F24qitYfI4RPuncz78V2aKu8Zi6O19PFqQs1Ky3fEV5-fQjPqtu9944omHiwx0Jsjy0SU5MdM6indY75sTKM0xmOFCrF4poGFGJMHW-PJmFy2pCC9O39iz9ki32vJlvYo085a5vrVzKni_fY3HOJKC0NMMlwvAasedT2ZeN1hVVjSqlQwjGQTmDjcx07k"),
                    DP = Base64UrlEncoder.DecodeBytes("obx_Oz4PLLgDWeIU5JnQLFecm-Ic7DycYevfXtinoMwCTqEO04FBL90XHI5tEZRTRMm_1_uuYLA3tAxlLCSBLt922KP0SLkxhkn2WnrxFzBng6ZgNPtq5tss8svKaWiqAaPR0V8UAtWqEaLVktzreeQRdL_2aWyZHYAQRXdjHyNXKNt2yMcT3hYAD0Hwwue6gqk8G3t-E-pYdbQKJPpGG-wBepnsXBkdBy2Lw7wb1gECrEScljP-Fh8eCr7vOIpyhFcLrTUeVU0mDc0-lcRTk-JPaHhAov3tLuCkOv05r4yAllRLzlEZGnZksuKp47vgKtdkB8heChCxAyC9agESGQ"),
                    DQ = Base64UrlEncoder.DecodeBytes("cd6SJC-Qp996mJYB2OJHiPcxPKxKgyAzHE-4A0oiJM_3wjjAjZ92TLY6fDbTtIL3xsoEbxL_gR1sEWlTzk97-mW9ErxsIHFI9UJxwnlGm2ljOL7-Juywd4U5BPhXYBqlZe0EDGxuREOnC6wMKp9-6zUxrN-sS2GK6n9Sieg24FDoQMX3U5CTDaxnQGUUtFYFDVZqFsZNmIf4N-vYqHcp2LwNZO9XC8sotJ0tXO40PUteHCmmFsglmFmNJeHYf0dRM3pwyu-4s1-7xWbPi2cZUKYVMLYGc6RJ_-VYJRrxAhTPX54Ow2hupjQIK0AUMrq3dgQpqqaGBl2L8gBAkzYHaQ"),
                    Exponent = Base64UrlEncoder.DecodeBytes("AQAB"),
                    InverseQ = Base64UrlEncoder.DecodeBytes("GwF0PXhYOz8Fwhz2JyxoZ_a_L7IwhbEhfPpYGXj6XBBkHztQtXvEb5dlNlK2Y6vQnfFgA1l5jiMZiFqnTSq6z1amWkrQUutqWq-EGo8jVVxzxx5as2t7lAohAn1dSY-HnP6ig0e8JF0rKeB8bxYqe-tPbVpuOgDbFKcOoaMCLRJY7kTRaGROss0O7bIJYu_g7lE4UPfybr2wEVy_3VucR279PZiwj3z-X6hMeUQHn-js2l1RGc-QgeEu2O7y2hq-APWnvna19xK8t8R5AS6hUnYeYTcZb4McpI7cg6H_5gItSFYuErPEGZWEZst8mcB2B6Yg9-WR7epmwZbSMCVt9A"),
                    Modulus = Base64UrlEncoder.DecodeBytes("vfPmqjbDDsT_xnh5CfnR97GirbRASP3rKyUlLOpytrkkWtNSD2S0ovnFlW5H4E9OiGusUpeJYD6HmAY5VSafWKC0qVEAvFIT_Fs6j8kJ3kiGsJc9Q-jktds06nvUMn1I-qxmLI7flw0khko6LCi6nTrBrPQ_XlQLYGS0C6-wkyCn0qqB6zsvX8kEElE7bEka0RkWzBVIWsVKkllajBNEeUVvAmy4KvmbFxXDlleNDXsJcETyo3-GmpaevrAr7ryIOOZuUQfkA8Hd-rb-lVkOkk2M-SChNlW8zNeyCksjp0LOF6r8SDf0fgnsKWsQ7qN2_Ltuvmd1X_HMIfdOmhi5Y6W6WnzWacO-jqbTb1k37bASbj2ViYB5-puTfEABzc12KN2mLRTA2TqhmYhy-9EPeDWdXsJwXKP1gtBrxXGFyEzuD4Y8lRYetY0RI5IJc8SoeUauJu75kNxUp-ffteIqP-uhG-E-BcFxIj_YfmI8Yui_XHU7OwDHTBdXk-ahBE7l-kvI-KN2hFcO1DIYK-c2no7fv1CXW8Lz-w7XWl54jn2MyKx4PnTUYT8langL6ZW3xux2wLrnbGwnIisWNWh0ih2ggKiiLCiOxJM9bwA-VbFSyihb3vWs-i4RAQtUwkvV6-8nyZv-SMcI9B7LuziJohnp_gIJZ8FIvtjoJHfGrZk"),
                    P = Base64UrlEncoder.DecodeBytes("z3zVMYG1_TWwnNs99hyMJI9xs1zrwbV_STv9cqpaBxgPIHqpDyoG68a_VZF2RfuYLKT9ksEfLmgDeBPuM_vwBXSUApRqQo46WOpYUaDTcQJUJcspYQENQDkvtMGTuZQd6wrfGksDkUgnuZZ2CAKjN6vzGGlUkLJ9eEihbVTsCcYpp1sSK_J5HZCu0MZcnEU4oMbWKRHHApkQC8TJkutRw1XjY6s_ABj7M2W2D9CIO-CnUaXxmh21JNaJ2zP0nK1cfWgzvIikVoey7FKbGbOoIBnVf9AZUAVfclBLWI3wfUUVod_1lwLA07-7kvQ6SCEiHpsep3RwONcYJJH3vD3WlQ"),
                    Q = Base64UrlEncoder.DecodeBytes("6l2EkIkJDFV_djyTFdpYbjrmg1lIRa1_oFF1925M0aDAEeJncRYkBOyupE9Ni6vHSTlRGN7I3Vs6V72Jnt_wpKqxz0qdhUseO89_JaqQE65euYq1QyeAjnHg64q4PcLYmBDub9Hn6y0xkTzkMCOUe5ZNGeosb5LW4qQVGyXnyeg9PmwFrBjQwr_Na6-1VrS2HCqzxvQWkOuJdjt3XB4ODXUnze9G-8nx8d2Cb9hC2qT4QxTPCq25i0n2DvIcRs_pD6RyNPg7EYOSH2Alc1vVzj7aYW9Plx69rIrtIFxH0ZwHsHua-AjDg7IZLK_Ghc3RV-9nTPEA2_6q_A_oOvfN9Q"),
                };

            RsaParameters_4096_Public =
                new RSAParameters
                {
                    Exponent = Base64UrlEncoder.DecodeBytes("AQAB"),
                    Modulus = Base64UrlEncoder.DecodeBytes("vfPmqjbDDsT_xnh5CfnR97GirbRASP3rKyUlLOpytrkkWtNSD2S0ovnFlW5H4E9OiGusUpeJYD6HmAY5VSafWKC0qVEAvFIT_Fs6j8kJ3kiGsJc9Q-jktds06nvUMn1I-qxmLI7flw0khko6LCi6nTrBrPQ_XlQLYGS0C6-wkyCn0qqB6zsvX8kEElE7bEka0RkWzBVIWsVKkllajBNEeUVvAmy4KvmbFxXDlleNDXsJcETyo3-GmpaevrAr7ryIOOZuUQfkA8Hd-rb-lVkOkk2M-SChNlW8zNeyCksjp0LOF6r8SDf0fgnsKWsQ7qN2_Ltuvmd1X_HMIfdOmhi5Y6W6WnzWacO-jqbTb1k37bASbj2ViYB5-puTfEABzc12KN2mLRTA2TqhmYhy-9EPeDWdXsJwXKP1gtBrxXGFyEzuD4Y8lRYetY0RI5IJc8SoeUauJu75kNxUp-ffteIqP-uhG-E-BcFxIj_YfmI8Yui_XHU7OwDHTBdXk-ahBE7l-kvI-KN2hFcO1DIYK-c2no7fv1CXW8Lz-w7XWl54jn2MyKx4PnTUYT8langL6ZW3xux2wLrnbGwnIisWNWh0ih2ggKiiLCiOxJM9bwA-VbFSyihb3vWs-i4RAQtUwkvV6-8nyZv-SMcI9B7LuziJohnp_gIJZ8FIvtjoJHfGrZk"),
                };

            RsaSecurityKey_1024 = new RsaSecurityKey(RsaParameters_1024);
            RsaSecurityKey_1024_Public = new RsaSecurityKey(RsaParameters_1024_Public);
            RsaSecurityKey_2048 = new RsaSecurityKey(RsaParameters_2048) { KeyId = Guid.NewGuid().ToString() };
            RsaSecurityKey_2048_Public = new RsaSecurityKey(RsaParameters_2048_Public);
            RsaSecurityKey_4096 = new RsaSecurityKey(RsaParameters_4096);
            RsaSecurityKey_4096_Public = new RsaSecurityKey(RsaParameters_4096_Public);
            RSASigningCreds_1024 = new SigningCredentials(RsaSecurityKey_1024, SecurityAlgorithms.RsaSha256Signature);
            RSASigningCreds_1024_Public = new SigningCredentials(RsaSecurityKey_1024_Public, SecurityAlgorithms.RsaSha256Signature);
            RSASigningCreds_2048 = new SigningCredentials(RsaSecurityKey_2048, SecurityAlgorithms.RsaSha256);
            RSASigningCreds_2048_Public = new SigningCredentials(RsaSecurityKey_2048_Public, SecurityAlgorithms.RsaSha256Signature);
            RSASigningCreds_4096 = new SigningCredentials(RsaSecurityKey_2048, SecurityAlgorithms.RsaSha256Signature);
            RSASigningCreds_4096_Public = new SigningCredentials(RsaSecurityKey_2048_Public, SecurityAlgorithms.RsaSha256Signature);
            var rsaCsp = new RSACryptoServiceProvider();
            rsaCsp.ImportParameters(RsaParameters_2048);
            RsaSecurityKeyWithCspProvider_2048 = new RsaSecurityKey(rsaCsp);
            var rsaCspPublic = new RSACryptoServiceProvider();
            rsaCspPublic.ImportParameters(RsaParameters_2048_Public);
            RsaSecurityKeyWithCspProvider_2048_Public = new RsaSecurityKey(rsaCspPublic);
#if NETSTANDARDAPP1_5
            var rsaCng = new RSACng();
            rsaCng.ImportParameters(RsaParameters_2048);
            RsaSecurityKeyWithCngProvider_2048 = new RsaSecurityKey(rsaCng);
            var rsaCngPublic = new RSACng();
            rsaCngPublic.ImportParameters(RsaParameters_2048_Public);
            RsaSecurityKeyWithCngProvider_2048_Public = new RsaSecurityKey(rsaCngPublic);
#endif

            //ecdsa
            byte[] ecdsa256KeyBlob = TestUtilities.HexToByteArray("454353322000000096e476f7473cb17c5b38684daae437277ae1efadceb380fad3d7072be2ffe5f0b54a94c2d6951f073bfc25e7b81ac2a4c41317904929d167c3dfc99122175a9438e5fb3e7625493138d4149c9438f91a2fecc7f48f804a92b6363776892ee134");
            byte[] ecdsa384KeyBlob = TestUtilities.HexToByteArray("45435334300000009dc6bb9cdc8dac31e3db6e6b5f58f8e3a304e5c08e632705ca9a236f1134646dca526b89f7ea98653962f4a781f2fc9bf479a2d627561b1269548050e6d2c388018b837f4ceba8ee7fe2eefea67c8418ad1e84f60c1309385e573ea5183e9ae8b6d5308a78da207c6e556af2053983321a5f8ac057b787089ee783c99093b9f2afb2f9a1e9a560ad3095b9667aa699fa");
            byte[] ecdsa521KeyBlob = TestUtilities.HexToByteArray("454353364200000001f9f06ea4e00fd3fecc1753af7983b43cb9b692941ee6364616c9c4168845fce804beca7aa23d0a5049910db45dfb61112f4cb02e93ff62af1be203ad248dd70952015ddc31d1ad7411ca5996b8b76a40ea65f286c665225114bec8557365aa4bc79358f8c68b873cb76a1c86a5a394185d8eeb9602b8b968db1e4ac49b7cc51f83c7170055ad9b0b2d0d5d2306a66bf87a256a3739696121eb131e64ae61991ea23db99b397c32df95efb0cb284147a929c65e9f671073ca3c7a084cb9211dceb06c987277");

            CngKey ecdsa256Key = CngKey.Import(ecdsa256KeyBlob, CngKeyBlobFormat.EccPrivateBlob);
            CngKey ecdsa256Public = CngKey.Import(ecdsa256Key.Export(CngKeyBlobFormat.EccPublicBlob), CngKeyBlobFormat.EccPublicBlob);
            CngKey ecdsa384Key = CngKey.Import(ecdsa384KeyBlob, CngKeyBlobFormat.EccPrivateBlob);
            CngKey ecdsa384Public = CngKey.Import(ecdsa384Key.Export(CngKeyBlobFormat.EccPublicBlob), CngKeyBlobFormat.EccPublicBlob);
            CngKey ecdsa512Key = CngKey.Import(ecdsa521KeyBlob, CngKeyBlobFormat.EccPrivateBlob);
            CngKey ecdsa512Public = CngKey.Import(ecdsa512Key.Export(CngKeyBlobFormat.EccPublicBlob), CngKeyBlobFormat.EccPublicBlob);

            ECDsa256Key = new ECDsaSecurityKey(new ECDsaCng(ecdsa256Key));
            ECDsa384Key = new ECDsaSecurityKey(new ECDsaCng(ecdsa384Key));
            ECDsa521Key = new ECDsaSecurityKey(new ECDsaCng(ecdsa512Key));
            ECDsa256Key_Public = new ECDsaSecurityKey(new ECDsaCng(ecdsa256Public));
            ECDsa384Key_Public = new ECDsaSecurityKey(new ECDsaCng(ecdsa384Public));
            ECDsa521Key_Public = new ECDsaSecurityKey(new ECDsaCng(ecdsa512Public));

            //json web key
            JsonWebKeyRsa256 =
                new JsonWebKey
                {
                    D = "C6EGZYf9U6RI5Z0BBoSlwy_gKumVqRx-dBMuAfPM6KVbwIUuSJKT3ExeL5P0Ky1b4p-j2S3u7Afnvrrj4HgVLnC1ks6rEOc2ne5DYQq8szST9FMutyulcsNUKLOM5cVromALPz3PAqE2OCLChTiQZ5XZ0AiH-KcG-3hKMa-g1MVnGW-SSmm27XQwRtUtFQFfxDuL0E0fyA9O9ZFBV5201ledBaLdDcPBF8cHC53Gm5G6FRX3QVpoewm3yGk28Wze_YvNl8U3hvbxei2Koc_b9wMbFxvHseLQrxvFg_2byE2em8FrxJstxgN7qhMsYcAyw1qGJY-cYX-Ab_1bBCpdcQ",
                    DP = "ErP3OpudePAY3uGFSoF16Sde69PnOra62jDEZGnPx_v3nPNpA5sr-tNc8bQP074yQl5kzSFRjRlstyW0TpBVMP0ocbD8RsN4EKsgJ1jvaSIEoP87OxduGkim49wFA0Qxf_NyrcYUnz6XSidY3lC_pF4JDJXg5bP_x0MUkQCTtQE",
                    DQ = "YbBsthPt15Pshb8rN8omyfy9D7-m4AGcKzqPERWuX8bORNyhQ5M8JtdXcu8UmTez0j188cNMJgkiN07nYLIzNT3Wg822nhtJaoKVwZWnS2ipoFlgrBgmQiKcGU43lfB5e3qVVYUebYY0zRGBM1Fzetd6Yertl5Ae2g2CakQAcPs",
                    E = "AQAB",
                    QI = "lbljWyVY-DD_Zuii2ifAz0jrHTMvN-YS9l_zyYyA_Scnalw23fQf5WIcZibxJJll5H0kNTIk8SCxyPzNShKGKjgpyZHsJBKgL3iAgmnwk6k8zrb_lqa0sd1QWSB-Rqiw7AqVqvNUdnIqhm-v3R8tYrxzAqkUsGcFbQYj4M5_F_4",
                    N = "6-FrFkt_TByQ_L5d7or-9PVAowpswxUe3dJeYFTY0Lgq7zKI5OQ5RnSrI0T9yrfnRzE9oOdd4zmVj9txVLI-yySvinAu3yQDQou2Ga42ML_-K4Jrd5clMUPRGMbXdV5Rl9zzB0s2JoZJedua5dwoQw0GkS5Z8YAXBEzULrup06fnB5n6x5r2y1C_8Ebp5cyE4Bjs7W68rUlyIlx1lzYvakxSnhUxSsjx7u_mIdywyGfgiT3tw0FsWvki_KYurAPR1BSMXhCzzZTkMWKE8IaLkhauw5MdxojxyBVuNY-J_elq-HgJ_dZK6g7vMNvXz2_vT-SykIkzwiD9eSI9UWfsjw",
                    P = "_avCCyuo7hHlqu9Ec6R47ub_Ul_zNiS-xvkkuYwW-4lNnI66A5zMm_BOQVMnaCkBua1OmOgx7e63-jHFvG5lyrhyYEmkA2CS3kMCrI-dx0fvNMLEXInPxd4np_7GUd1_XzPZEkPxBhqf09kqryHMj_uf7UtPcrJNvFY-GNrzlJk",
                    Q = "7gvYRkpqM-SC883KImmy66eLiUrGE6G6_7Y8BS9oD4HhXcZ4rW6JJKuBzm7FlnsVhVGro9M-QQ_GSLaDoxOPQfHQq62ERt-y_lCzSsMeWHbqOMci_pbtvJknpMv4ifsQXKJ4Lnk_AlGr-5r5JR5rUHgPFzCk9dJt69ff3QhzG2c",
                    Kty = "RSA"
                };
            JsonWebKeyRsa256Public =
                new JsonWebKey
                {
                    E = "AQAB",
                    N = "6-FrFkt_TByQ_L5d7or-9PVAowpswxUe3dJeYFTY0Lgq7zKI5OQ5RnSrI0T9yrfnRzE9oOdd4zmVj9txVLI-yySvinAu3yQDQou2Ga42ML_-K4Jrd5clMUPRGMbXdV5Rl9zzB0s2JoZJedua5dwoQw0GkS5Z8YAXBEzULrup06fnB5n6x5r2y1C_8Ebp5cyE4Bjs7W68rUlyIlx1lzYvakxSnhUxSsjx7u_mIdywyGfgiT3tw0FsWvki_KYurAPR1BSMXhCzzZTkMWKE8IaLkhauw5MdxojxyBVuNY-J_elq-HgJ_dZK6g7vMNvXz2_vT-SykIkzwiD9eSI9UWfsjw",
                    Kty = "RSA"
                };

            JsonWebKeyEcdsa256 =
                new JsonWebKey
                {
                    Crv = "P-256",
                    X = "luR290c8sXxbOGhNquQ3J3rh763Os4D609cHK-L_5fA",
                    Y = "tUqUwtaVHwc7_CXnuBrCpMQTF5BJKdFnw9_JkSIXWpQ",
                    D = "OOX7PnYlSTE41BSclDj5Gi_sx_SPgEqStjY3doku4TQ",
                    Kty = "EC"
                };
            JsonWebKeyEcdsa256Public =
                new JsonWebKey
                {
                    Crv = "P-256",
                    X = "luR290c8sXxbOGhNquQ3J3rh763Os4D609cHK-L_5fA",
                    Y = "tUqUwtaVHwc7_CXnuBrCpMQTF5BJKdFnw9_JkSIXWpQ",
                    Kty = "EC"
                };
            JsonWebKeySymmetric256 =
                new JsonWebKey
                {
                    Kty = JsonWebAlgorithmsKeyTypes.Octet,
                    K = "Vbxq2mlbGJw8XH+ZoYBnUHmHga8/o/IduvU/Tht70iE="
                };
        }

        // .../Certs/SelfSigned1024_SHA256.pfx
        // password: SelfSigned1024_SHA256
        public static string SelfSigned1024_SHA256 = @"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";
        public static string SelfSigned1024_SHA256_Public = "MIICWTCCAcKgAwIBAgIQPq8RWqoOL51G/qUxq7tBizANBgkqhkiG9w0BAQsFADA1MTMwMQYDVQQDHioAUwBlAGwAZgBTAGkAZwBuAGUAZAAxADAAMgA0AF8AUwBIAEEAMgA1ADYwHhcNMTQxMjI2MTUyNzAwWhcNMzkxMjMxMjM1OTU5WjA1MTMwMQYDVQQDHioAUwBlAGwAZgBTAGkAZwBuAGUAZAAxADAAMgA0AF8AUwBIAEEAMgA1ADYwgZ8wDQYJKoZIhvcNAQEBBQADgY0AMIGJAoGBAMl0ubbO3e3FpeMN2/gjz0YElurF0UCT25777CmNfzpvxtuvziUxa/ALMxmz+sVLzKb6kMuLNagU12cYZS3ksgntqxz8R/VyZO33hysNAMeMcdeOMhMm6ubTXEqqdHAb4TUPhme6o7Bvpcx3yPlxECoy6XE5A0VlYBgWzqMJN6F5AgMBAAGjajBoMGYGA1UdAQRfMF2AEKiIeaB2ShCC/Mrhyxg2Lf+hNzA1MTMwMQYDVQQDHioAUwBlAGwAZgBTAGkAZwBuAGUAZAAxADAAMgA0AF8AUwBIAEEAMgA1ADaCED6vEVqqDi+dRv6lMau7QYswDQYJKoZIhvcNAQELBQADgYEAaqChtfN/l6xTcMItwFG9jhDPuWeLDXAplM0vSwbia1fIaAXdcFRSaH+5QwqoQSDROcfiWRbPNWhFXfzOj7FEBmtbGifiqDvHislRHYrqnz9FRKiay0KYn0tJ2RUsTlKxZNz0WVu9M05wJjYH4TB04ad5FhgxJZ2h/y1X+An4a/o=";
        public static X509Certificate2 CertSelfSigned1024_SHA256 = new X509Certificate2(Convert.FromBase64String(SelfSigned1024_SHA256), "SelfSigned1024_SHA256", X509KeyStorageFlags.PersistKeySet);
        public static X509SecurityKey X509SecurityKeySelfSigned1024_SHA256 = new X509SecurityKey(CertSelfSigned1024_SHA256);
        public static X509Certificate2 CertSelfSigned1024_SHA256_Public = new X509Certificate2(Convert.FromBase64String(SelfSigned1024_SHA256_Public), "SelfSigned1024_SHA256");
        public static X509SecurityKey X509SecurityKeySelfSigned1024_SHA256_Public = new X509SecurityKey(CertSelfSigned1024_SHA256);

        // .../Certs/SelfSigned2048_SHA256.pfx
        // password: SelfSigned2048_SHA256
        public static string SelfSigned2048_SHA256 = @"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";
        public static string SelfSigned2048_SHA256_Public = @"MIIDXjCCAkagAwIBAgIQY9+BvEWchJpAX/tDKzHwFDANBgkqhkiG9w0BAQsFADA1MTMwMQYDVQQDHioAUwBlAGwAZgBTAGkAZwBuAGUAZAAyADAANAA4AF8AUwBIAEEAMgA1ADYwHhcNMTQxMjI2MTUyNzM2WhcNMzkxMjMxMjM1OTU5WjA1MTMwMQYDVQQDHioAUwBlAGwAZgBTAGkAZwBuAGUAZAAyADAANAA4AF8AUwBIAEEAMgA1ADYwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDWE7VB3zRniE3CsYLy9sCLAdFB7AsGKMkZsJxiwKD1uv+OKshPN9Epm7ZJQWjm6YGeQYKGQUaNs5Z1NaapKLaT52jqcTLRbOC5g331GkXTPICkjDHsR+NPyd7J4O4Hl2ls8q1+mcYhHSJOoamWOGZqtpCfpqqOhHhG75Rn282kA90Ybc6xY+rTgBIYgSt+/l3/muI3XTU6wghifYwZfID1IngBEb+MD346QgpiJcWObL+WIXPGpLNmDjwJZ8IlXvgO5JPSz1wxCyb8EJHUp4hQUc778RtKB82UXbckhL3eW49v1jpuJoqeNm924vlMX3IYAwYDBF93K6F8yu2otpwvAgMBAAGjajBoMGYGA1UdAQRfMF2AEM2V/dQqCNOhP9VPwFcAubuhNzA1MTMwMQYDVQQDHioAUwBlAGwAZgBTAGkAZwBuAGUAZAAyADAANAA4AF8AUwBIAEEAMgA1ADaCEGPfgbxFnISaQF/7Qysx8BQwDQYJKoZIhvcNAQELBQADggEBAKSksE7/5TOc5ngnD54poNnaPWrw4kolFzqYdw1/s/evScT4tgFYR1FrmPB50KYoZ0c8FzDY7PK4SkB7x7xFbjPYZwcEzeHqZ+WsHO3UxI2nU94CUsBmNR09CMMIwt/1A1yfzSNTJE452YtycdLVJUC6NBR30Di5YOFWPwIEO5XE0J7Os1xuhZc6AEKy2STp0I3FL27gHu3R+3Xhqru6fQIOw52Pcp1axXsuE9cQ/HKyeNTvM02FZmjlx/Vy3lC5I/2xiUJrhqzczOMRRB8clpsAp2uNWfrzJ0aLCprQO62pN9L/51PWbSMsNfnfuUo4eZHv2noQ3mGzJPXyK43Omn0=";
        public static X509Certificate2 CertSelfSigned2048_SHA256 = new X509Certificate2(Convert.FromBase64String(SelfSigned2048_SHA256), "SelfSigned2048_SHA256", X509KeyStorageFlags.PersistKeySet);
        public static X509SecurityKey X509SecurityKeySelfSigned2048_SHA256 = new X509SecurityKey(CertSelfSigned2048_SHA256);
        public static X509Certificate2 CertSelfSigned2048_SHA256_Public = new X509Certificate2(Convert.FromBase64String(SelfSigned2048_SHA256_Public), "SelfSigned2048_SHA256");
        public static X509SecurityKey X509SecurityKeySelfSigned2048_SHA256_Public = new X509SecurityKey(CertSelfSigned2048_SHA256_Public);

        // .../Certs/SelfSigned2048_SHA384.pfx
        // password: SelfSigned2048_SHA384
        public static string SelfSigned2048_SHA384 = @"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";
        public static string SelfSigned2048_SHA384_Public = "MIIDXjCCAkagAwIBAgIQmT68AQ3vPa9NmlShoEZVVjANBgkqhkiG9w0BAQwFADA1MTMwMQYDVQQDHioAUwBlAGwAZgBTAGkAZwBuAGUAZAAyADAANAA4AF8AUwBIAEEAMwA4ADQwHhcNMTQxMjI2MTUyODExWhcNMzkxMjMxMjM1OTU5WjA1MTMwMQYDVQQDHioAUwBlAGwAZgBTAGkAZwBuAGUAZAAyADAANAA4AF8AUwBIAEEAMwA4ADQwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCdtHnoaSiqQy+hBEmLnxgOy2euwvtpC0CHnzIJILsoyBdGELQ8cuPCTKT1JNBluphBtFF3oB3xoxRJWrAzNgvDqm09yS57rFXbV9jK9j8zP/QdEr9j4i1CckwNVY4PNCerF3bDpRDD00IHD+NObLLcaadFBjlzMxRjqd4D8ko8Jxl0g+gO15QwlXNQW83H058Qcd20chiifr1nILcJKEdV25fhfPIqvQny39zaydEDMnfAGo7Puj6ODjYbjZ50U8mUhtYdVtye8tP7g2C8lhwUT4N2B+sJCbInApT3ONTa9clPT23Fif/2JsNMRISt5bnDJpBCtP29l0i2Z7+5PUnfAgMBAAGjajBoMGYGA1UdAQRfMF2AEGd1geu132c0YEEInmdrMdehNzA1MTMwMQYDVQQDHioAUwBlAGwAZgBTAGkAZwBuAGUAZAAyADAANAA4AF8AUwBIAEEAMwA4ADSCEJk+vAEN7z2vTZpUoaBGVVYwDQYJKoZIhvcNAQEMBQADggEBAGrxk6od8vgHb/tRoFgGcGfubMlZMRvH2Ly+4cdLpgIYKqvEWTnze0KVrd/y7qyoJc3pbAqs3XRrhAFiB6NACP5PeOFV+WvA2PF45qSO7kXwACXlYqvHQcnEo33BWfLiBpKY2zXUZzUOyZAOpNYM8SDLNH4wj0PH8F4pU18i6Q/E2t5cVUR11PpHCpKoUlMNClx8sCW4/Pamvn3BQw+NyrMpCz9NmeY5k46h8smPB2Z8hflLUc9zcJ8fv+dvun87bLa0S8Y3pM/gMrbVswsfkQ4uZY6iAKN4hGQw+SLb8KG8vYRGI726S8h99el0kRJBLZv7JD6oBbUI+92s2KN7I7A=";
        public static X509Certificate2 CertSelfSigned2048_SHA384 = new X509Certificate2(Convert.FromBase64String(SelfSigned2048_SHA384), "SelfSigned2048_SHA384", X509KeyStorageFlags.PersistKeySet);
        public static X509SecurityKey X509SecurityKeySelfSigned2048_SHA384 = new X509SecurityKey(CertSelfSigned2048_SHA384);
        public static X509Certificate2 CertSelfSigned2048_SHA384_Public = new X509Certificate2(Convert.FromBase64String(SelfSigned2048_SHA384_Public), "SelfSigned2048_SHA384");
        public static X509SecurityKey X509SecurityKeySelfSigned2048_SHA384_Public = new X509SecurityKey(CertSelfSigned2048_SHA384);

        // .../Certs/SelfSigned2048_SHA512.pfx
        // password: SelfSigned2048_SHA512
        public static string SelfSigned2048_SHA512 = @"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";
        public static string SelfSigned2048_SHA512_Public = "MIIDXjCCAkagAwIBAgIQzTfvf7ABYLxKOErOiooMIjANBgkqhkiG9w0BAQ0FADA1MTMwMQYDVQQDHioAUwBlAGwAZgBTAGkAZwBuAGUAZAAyADAANAA4AF8AUwBIAEEANQAxADIwHhcNMTQxMjI2MTUyODMxWhcNMzkxMjMxMjM1OTU5WjA1MTMwMQYDVQQDHioAUwBlAGwAZgBTAGkAZwBuAGUAZAAyADAANAA4AF8AUwBIAEEANQAxADIwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCzk2XIZKwGbblamSyJ3l+Co+SzCqSV4X7COoJM0R7UwgYT12hD30xhvDUpQrhuHc2ecF5SPggpG/Z63BXsIaqSMCcmzOoV9igFDqWzecbj5Mz/xSm/ZjJ7HXxiklSekWFG1q/8crd1HfdHjaCm+hMxpfXFSH+h3bSxv+2XBkT+Y35wUXnnw6Q/rE+ieW5+xn0MUvh1UkFCl7+ZDOhIzbXkzM/BKUAPxbJBjXeAr0Cum/IVFStavxi21Sgj1XoMuW6tlFX+eG6wUvmh4LjVTlIVdrV8bqxDIm/w/vCwlyvcQxERlP3TVlusUkc157pXKvhqNlPdEGG9kUumMVaziNxNAgMBAAGjajBoMGYGA1UdAQRfMF2AEMQYtKMfx+sbsi5bQ7l/LTShNzA1MTMwMQYDVQQDHioAUwBlAGwAZgBTAGkAZwBuAGUAZAAyADAANAA4AF8AUwBIAEEANQAxADKCEM0373+wAWC8SjhKzoqKDCIwDQYJKoZIhvcNAQENBQADggEBAAvM9kCf8fpwiLYtEQK6ryHmg6MC5L2BNGsuRUOZNDkp+/LZF48tkfL6dTYslj3kPeEBGAA9ggIjDinTbPEveEpPUoZTllkMj9A9T963yhaLlsVAJDPbJw8XlHO+c01VjUtIjaVtIvJaCNoXF0S+TfbRDKceiEXYcDS+ySyssCi4x23nd2fylRvpiaOrFAMDXHF440vR/I18VEqKuPa/NWj/JbVdaXAmyjjtHi7GIcL6rQQFpbDDfrzKmNCc/SXLkNQ97ArngEkOzZhOmP2aZ7ZqdX/h3mRQ3wo91+WfVfFfbhKc4qTUQHr9rop2JeEhu8WaRwVoqz6PtXB6WtIX8hk=";
        public static X509Certificate2 CertSelfSigned2048_SHA512 = new X509Certificate2(Convert.FromBase64String(SelfSigned2048_SHA512), "SelfSigned2048_SHA512", X509KeyStorageFlags.PersistKeySet);
        public static X509SecurityKey X509SecurityKeySelfSigned2048_SHA512 = new X509SecurityKey(CertSelfSigned2048_SHA512);
        public static X509Certificate2 CertSelfSigned2048_SHA512_Public = new X509Certificate2(Convert.FromBase64String(SelfSigned2048_SHA512_Public), "SelfSigned2048_SHA512");
        public static X509SecurityKey X509SecurityKeySelfSigned2048_SHA512_Public = new X509SecurityKey(CertSelfSigned2048_SHA512_Public);

        // all asymmetric material has private key unless public is included in variable name
        public const string CertPassword = "abcd";
        public const string X509Data_AAD_Public = @"MIIDPjCCAiqgAwIBAgIQVWmXY/+9RqFA/OG9kFulHDAJBgUrDgMCHQUAMC0xKzApBgNVBAMTImFjY291bnRzLmFjY2Vzc2NvbnRyb2wud2luZG93cy5uZXQwHhcNMTIwNjA3MDcwMDAwWhcNMTQwNjA3MDcwMDAwWjAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArCz8Sn3GGXmikH2MdTeGY1D711EORX/lVXpr+ecGgqfUWF8MPB07XkYuJ54DAuYT318+2XrzMjOtqkT94VkXmxv6dFGhG8YZ8vNMPd4tdj9c0lpvWQdqXtL1TlFRpD/P6UMEigfN0c9oWDg9U7Ilymgei0UXtf1gtcQbc5sSQU0S4vr9YJp2gLFIGK11Iqg4XSGdcI0QWLLkkC6cBukhVnd6BCYbLjTYy3fNs4DzNdemJlxGl8sLexFytBF6YApvSdus3nFXaMCtBGx16HzkK9ne3lobAwL2o79bP4imEGqg+ibvyNmbrwFGnQrBc1jTF9LyQX9q+louxVfHs6ZiVwIDAQABo2IwYDBeBgNVHQEEVzBVgBCxDDsLd8xkfOLKm4Q/SzjtoS8wLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldIIQVWmXY/+9RqFA/OG9kFulHDAJBgUrDgMCHQUAA4IBAQAkJtxxm/ErgySlNk69+1odTMP8Oy6L0H17z7XGG3w4TqvTUSWaxD4hSFJ0e7mHLQLQD7oV/erACXwSZn2pMoZ89MBDjOMQA+e6QzGB7jmSzPTNmQgMLA8fWCfqPrz6zgH+1F1gNp8hJY57kfeVPBiyjuBmlTEBsBlzolY9dd/55qqfQk6cgSeCbHCy/RU/iep0+UsRMlSgPNNmqhj5gmN2AFVCN96zF694LwuPae5CeR2ZcVknexOWHYjFM0MgUSw0ubnGl0h9AJgGyhvNGcjQqu9vd1xkupFgaN+f7P3p3EVN5csBg5H94jEcQZT7EKeTiZ6bTrpDAnrr8tDCy8ng";
        public static X509Certificate2 Cert_AAD_Public = new X509Certificate2(Convert.FromBase64String(X509Data_AAD_Public));
        public static X509SecurityKey X509SecurityKey_AAD_Public = new X509SecurityKey(Cert_AAD_Public);
        public static SigningCredentials X509SigningCreds_AAD_Public = new SigningCredentials(X509SecurityKey_AAD_Public, SecurityAlgorithms.RsaSha256Signature);

        public const string X509Data_LocalSts = @"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";
        public static X509Certificate2 Cert_LocalSts = new X509Certificate2(Convert.FromBase64String(X509Data_LocalSts), CertPassword, X509KeyStorageFlags.MachineKeySet);
        public static X509SecurityKey X509SecurityKey_LocalSts = new X509SecurityKey(Cert_LocalSts);
        public static SigningCredentials X509SigningCreds_LocalSts = new SigningCredentials(X509SecurityKey_LocalSts, SecurityAlgorithms.RsaSha256Signature);

        // 1024 bit RSA
        public const string X509Data_1024 = @"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";
        public static X509Certificate2 Cert_1024 = new X509Certificate2(Convert.FromBase64String(X509Data_1024), CertPassword, X509KeyStorageFlags.MachineKeySet);
        public static X509SecurityKey X509SecurityKey_1024 = new X509SecurityKey(Cert_1024);
        public static SigningCredentials X509SigningCreds_1024_RsaSha2_Sha2 = new SigningCredentials(X509SecurityKey_1024, SecurityAlgorithms.RsaSha256Signature);

        // 2048 bit RSA
        public const string DefaultX509Data_2048 = @"MIIKHAIBAzCCCdwGCSqGSIb3DQEHAaCCCc0EggnJMIIJxTCCBf4GCSqGSIb3DQEHAaCCBe8EggXrMIIF5zCCBeMGCyqGSIb3DQEMCgECoIIE/jCCBPowHAYKKoZIhvcNAQwBAzAOBAivxX2ENGkqRwICB9AEggTYFn22METHTeg0HaAP8Abvg7vjXbyVReMNsR0dBiY0waqF69lXGECwKMZL75biisgxx2y16ek2n/jIidyB/3pQwJLXr8DeIcRguiUi3edMaBel8fOOUbg4CpeHiLmEriKf/g1p1d7uLz5cGGpNKgAI5Xe9eYoisF06UnS+Sg9l4Z6FFs14YvkTRpn/QhllN0Oshy0TxaQtA6EylZPZ3QetvcS3Cl4BjXey0Q1vn6Cm7S1xP8HwBQ2uJRMzYeACFnDCwrASMxaNhIKhGJIfHwxzk/peuJix91wjCOPx6R1JTZRpSMKcyg8I/MO4CfII0wzutBpxGRjq5zQTkeg5nfJgye66RLxbDsyA2YMsEDGkQWbRVGRVq/R0d3MTt2b/mY6nmhbGSY24suHjY05A67BBTURjCO1u9fXFrvdaq+WrcEtjcdo75DWfOzCqtxQ2nRaA6qF48CX8LF6GX05meTug5Zl7Cixa8jOw+M88OxM2R0TayAV6AxO/hBTFq5WcLmHl/gGcjLY8ypWj3i8HB3akQYUoqV/mCwILhdQwfG/E8UcjRA5yplWRnz346RA7NJ/Ae84VY4hR7Fxrgam765uLl063GAQhW+M3lctJL3Xooo7rduXeVL6RDQhYdz6cOkEIyyH+4ftArhesgGUECQxQTibWiXeLTQbJfc/g4+BG4iQTBgl599LjfR044THpH20y3gNm2bYe8VAcJgVrwlQOgFQAGAVLSQKFNvznHPfWGPFMuK1xfsNVdaTugOE9YGQv16CDcCJMTgeYqVPXm9Hq1TKL7nqRR3FqkNCaE1aMn2v3TOmv4TuCfepe+CxR5WFJin06PMjaBibUTQ520H1eIudjUJSN5VQ+Rfh05HQVagBPT4dkcjLNZtKPZJSNC72HhEdxLO1+s12mLN2ZdBtbPVBfXHtrfHrXYVhk1vcvztoA6Wq92Z9x+ZMlJZuhXk45xWsH/dL4H0S+f/keakpLSCRB9zBdXMhyixSN3gyE/YHcbc6XHuIdhMDOEDwINmZJBG29FTrG7F2QUrPfRLlRYLD3xgDJHH/p2BgpGyAN3FlwSQjhNW6UCLTsKl24qaDVwJ60+9SpypJbJra0o3l+6gc4CxGuLY9TBR6jrSToaE476uyyoYWUn2hCzlOOtedd6hGZQECh8fh5rf93nfhCQghKtUakdjSJUW8XWSnouv+Z5dNoYlqflkGQ/AfCkWj3jgO+MYLriJVf5tDxcYyj+trfV7HWI3GgL4fPXsrc740/AesDUrf+JqK36Hm2s3GQe9eqeUg9+ohxVY5QO3QBkUMbvaMHqlXYo0EbW91SyLZQBlcx97q9YFkAtwp3311hoP2bl7+N/T5XMw1EoA89GutLkzIVuE+AQk94eEcIJwmjb9pYKl58tZLlqfDBS6sV+j95Dh83dwMG+8gRCwS8qFYRyXO0UVcjWPv/qeHVEEorgyhJveLrjimdEzheFlQrBit8YAS+akXOBVQC4QQ42biCWsz2qO1sQIMZndN6dVke88Br7Ilh9UJ0qojXR0mc6BPUKl3Zh9d32WyFbKm3Qj6AS2vnmkZCdjBO9PTT5oY/j17ClLTshps2B5ruysXotcrGNjuOhPE05fkYUFzknfSv7HhrjMvQYQNulHxGijTvksey1NedbDGB0TATBgkqhkiG9w0BCRUxBgQEAQAAADBbBgkqhkiG9w0BCRQxTh5MAHsAMwA1AEEAQgBFADcAOQBBAC0AOQA3ADIAQwAtADQAOAAxAEYALQA4AEIAMQA3AC0AQQBDADAAOQAxAEEANwAwADgAQwA2AEYAfTBdBgkrBgEEAYI3EQExUB5OAE0AaQBjAHIAbwBzAG8AZgB0ACAAUwB0AHIAbwBuAGcAIABDAHIAeQBwAHQAbwBnAHIAYQBwAGgAaQBjACAAUAByAG8AdgBpAGQAZQByMIIDvwYJKoZIhvcNAQcGoIIDsDCCA6wCAQAwggOlBgkqhkiG9w0BBwEwHAYKKoZIhvcNAQwBBjAOBAg6DCoLVPw6qAICB9CAggN4Xykvk2tPB1lZ00HE84x0B0384ZMGb8UgjGbjr7fMnSMUXDgHijcevFNmdeP/II/Ltd+F73MbEsaA1d1CEH72cPk4wdoDsgrTt/Fg9xL+jja9HB35HuitgmLsfF1NJ6NdPZZK+0yfvlIKbz/MmKRrGfAwNuWtOVU3bnOv0myfXmfLg5O4mp/JdHJ5kjG4O81nUq6+OCyFbARuDVkrlIZbLO1ck3TPA7Dd2a8ujayY8mtFzMBrGV7U5LJH1V/LprpEA1dZmqt3kmXdLvIwSzNUub23wJDFWc0wQZ2/CJp33RiulZIe9na5bj7S0adOj/Jloot0V9Rxf46sevvsMM/M9rQXVAz0rquwW2o4yRUJxQgajntn75/Dridu+hj++j+Nq5Fs3pII5yjv517YzTZihoWB1xhO9yZhmMUq6OtJQFgQlB9YQTvCvleeC0AoU2lRZ5dvyrzxEMFEbHN72vG7Sps5vyyz/joF1RVNZw/hP4/hoFGuGcIFkI3Dsz+JSi0iZEqgmAaq2LUihT2rx40r49aSCU1VXs7DDnBLhh3w20Z1hx2IQmc2wp0YGKSbQDjA4hItRG6xXapMrlizaIp0LzWtmgV+qRbZN39xvXOkc0kITFdbyWILA04WgNwGeAlwtiSeO+C2c/EVXFOLOH+ibJ/OCUexw6yDTtIBqsk8oUCTMvJNNKguJCC2pSEKPhH606HAnuYTbWqUxY9GWK6wNIFAJaQnHD2pprq9j4va69qq0xy9rn7pfiEB7GeGlRb7QtOd5myfG1SZ5S/oP0Pnx+G6tA1Xkx8vMVeZhzH128+zApqVLd/xtMGJ24RlTgViyJsN1Z5k77Ces5YdwTZjAnJ6kyMbiVhZIpwzlKiJ23Aq00RpinF7ZvzPK0L3RDWbahU1eM98zhokRW3c5dKKBEAGePzyCVUnyoCCBpLUWkSXhL58Qm6Us1IfsoiYGnE/YtWwpArHXqndDnArrqTECUxf4VAqZ3Sj3CDRQN54aLBPgllNB2VjzmS4qKbyT7VP1HkxAbE5B1PRLqKCSzgeIJTMpGbHkaacz9Kme+O99d6OOdLr2OyogX5g6FkEc32n+lwnDww5VgbfdLV8JBjgS1WeEQk2UgJqXzwlNEjLvtX6RReLljUi7QLDeEaC2WKJFGnRlbX0JYL+ugggUY5UhXnJ6BvYv6P2MDcwHzAHBgUrDgMCGgQUPN6zb4ZCGV3dy3+JzgHFLCrHlGIEFNnQRFK67cH21VJ27RcK5qgEvQfh";
        public static X509Certificate2 DefaultCert_2048 = new X509Certificate2(Convert.FromBase64String(DefaultX509Data_2048), CertPassword, X509KeyStorageFlags.MachineKeySet);
        public static X509SecurityKey DefaultX509Key_2048 = new X509SecurityKey(DefaultCert_2048);
        public static SigningCredentials DefaultX509SigningCreds_2048_RsaSha2_Sha2 = new SigningCredentials(DefaultX509Key_2048, SecurityAlgorithms.RsaSha256Signature);
        public static X509Certificate2 DefaultAsymmetricCert_2048 = new X509Certificate2(Convert.FromBase64String(DefaultX509Data_2048), CertPassword, X509KeyStorageFlags.MachineKeySet);

        public static string DefaultX509Data_Public_2048 = @"MIICyjCCAbKgAwIBAgIQJPMYqnyiTY1GQYAwZxadMjANBgkqhkiG9w0BAQsFADAhMR8wHQYDVQQDExZBREZTIFNpZ25pbmcgLSBTVFMuY29tMB4XDTEyMTAwOTIyMTA0OVoXDTEzMTAwOTIyMTA0OVowITEfMB0GA1UEAxMWQURGUyBTaWduaW5nIC0gU1RTLmNvbTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMmeVPJz8o7ayB3AS2dJtsIo/eXqeNhZ+ZqEJgHVHc0JAAgNNwR++moMt8+iIlOKZiAL8dvQBKOuPms+FfqrG1HshnMiLcuadtWUqOntxUdyQLcEKvdaFOqOppqmasqGFtRLPwYKIkZOkj8ikndNzI6PZV46mw18nLaN6rTByMnjVA5n9Lf7Cdu7lmxlKGJOI5F0IfeaW68/kY1bdw3KAEb1aOKHj0r7RJ2joRuHJ+96kw1bA2T6bGC/1LYND3DFsnQQtMBl7LlDrSG1gGoiZxCoQmPCxfrTCrYKGK6y9j6IQ4MCmJpnt0l/INL5i88TjctF4IkJwbJGn9iY2fIIBxMCAwEAATANBgkqhkiG9w0BAQsFAAOCAQEAq/SyHGCLpBm+Gmh5I7BAWJXvtPaIelt30WgKVXRHccxRVIYpKOfAA2iPuD/CVruFz6pnP4K7o2KLAs+XJptigYzLEjKw6rY4836ZJC8m5kfBVanu45OW39nxzxp1udbxQ5gAdmvnY/2agpFhCFR8M1BtWON6G3SzHwo2dXHh+ettOO2LtK38e1+Uy+KGowRw/m4gprSIvgN3AAo7e0PnFblZn6vRgMsK60QB5D8f+Kxdg2I3ZGQcPBQI2fpjEDQCZVc2LV4ywPX4QDPfmYjn+1IaU9w7unbh+oUGQsrdKw3gsdzWEsX/IMXTDf46FEOjV+JqE7VilzcNuDcQ0x9K8gAA";
        public static X509Certificate2 DefaultCert_Public_2048 = new X509Certificate2(Convert.FromBase64String(DefaultX509Data_Public_2048));
        public static X509SecurityKey DefaultX509Key_Public_2048 = new X509SecurityKey(DefaultCert_Public_2048);
        public static SigningCredentials DefaultX509SigningCreds_Public_2048_RsaSha2_Sha2 = new SigningCredentials(DefaultX509Key_Public_2048, SecurityAlgorithms.RsaSha256Signature);

        // RSA securityKey
        public static RSAParameters RsaParameters_1024;
        public static RSAParameters RsaParameters_1024_Public;
        public static RSAParameters RsaParameters_2048;
        public static RSAParameters RsaParameters_2048_Public;
        public static RSAParameters RsaParameters_4096;
        public static RSAParameters RsaParameters_4096_Public;
        public static RSAParameters RsaParameters_2048_MissingModulus;
        public static RSAParameters RsaParameters_2048_MissingExponent;

        public static RsaSecurityKey RsaSecurityKey_1024;
        public static RsaSecurityKey RsaSecurityKey_1024_Public;
        public static RsaSecurityKey RsaSecurityKey_2048;
        public static RsaSecurityKey RsaSecurityKey_2048_Public;
        public static RsaSecurityKey RsaSecurityKey_4096;
        public static RsaSecurityKey RsaSecurityKey_4096_Public;
        public static RsaSecurityKey RsaSecurityKeyWithCspProvider_2048;
        public static RsaSecurityKey RsaSecurityKeyWithCspProvider_2048_Public;
        public static RsaSecurityKey RsaSecurityKeyWithCngProvider_2048;
        public static RsaSecurityKey RsaSecurityKeyWithCngProvider_2048_Public;

        public static SigningCredentials RSASigningCreds_1024;
        public static SigningCredentials RSASigningCreds_1024_Public;
        public static SigningCredentials RSASigningCreds_2048;
        public static SigningCredentials RSASigningCreds_2048_Public;
        public static SigningCredentials RSASigningCreds_4096;
        public static SigningCredentials RSASigningCreds_4096_Public;

        // ECDSA Cng security keys
        public static readonly ECDsaSecurityKey ECDsa256Key;
        public static readonly ECDsaSecurityKey ECDsa384Key;
        public static readonly ECDsaSecurityKey ECDsa521Key;
        public static readonly ECDsaSecurityKey ECDsa256Key_Public;
        public static readonly ECDsaSecurityKey ECDsa384Key_Public;
        public static readonly ECDsaSecurityKey ECDsa521Key_Public;

        // JsonWebKey security keys
        public static readonly JsonWebKey JsonWebKeyRsa256;
        public static readonly JsonWebKey JsonWebKeyRsa256Public;
        public static readonly JsonWebKey JsonWebKeyEcdsa256;
        public static readonly JsonWebKey JsonWebKeyEcdsa256Public;
        public static readonly JsonWebKey JsonWebKeySymmetric256;

        public static string DefaultSymmetricKeyEncoded_256 = "Vbxq2mlbGJw8XH+ZoYBnUHmHga8/o/IduvU/Tht70iE=";
        public static byte[] DefaultSymmetricKeyBytes_256 = Convert.FromBase64String(DefaultSymmetricKeyEncoded_256);
        public static SymmetricSecurityKey DefaultSymmetricSecurityKey_256 = new SymmetricSecurityKey(DefaultSymmetricKeyBytes_256);
        public static SigningCredentials DefaultSymmetricSigningCreds_256_Sha2 = new SigningCredentials(DefaultSymmetricSecurityKey_256, SecurityAlgorithms.HmacSha256Signature);

        // used in negative cases
        public static string SymmetricKeyEncoded2_256 = "VbbbbmlbGJw8XH+ZoYBnUHmHga8/o/IduvU/Tht70iE=";
        public static byte[] SymmetricKeyBytes2_256 = Convert.FromBase64String(SymmetricKeyEncoded2_256);
        public static SymmetricSecurityKey SymmetricSecurityKey2_256 = new SymmetricSecurityKey(SymmetricKeyBytes2_256);

        public static SymmetricSecurityKey SymmetricSecurityKey_56 = new SymmetricSecurityKey(new byte[7]);

        // These signingCreds have algorithms and hashs that are not supported
        public static SigningCredentials SymmetricSigningCreds_256_Rsa256_Sha2 = new SigningCredentials(DefaultSymmetricSecurityKey_256, SecurityAlgorithms.RsaSha256Signature);
        //public static SigningCredentials SymmetricSigningCreds_2048RSA_H256_Sha2 = new SigningCredentials(RsaSecurityKey_2048, SecurityAlgorithms.HmacSha256Signature);
    }
}
