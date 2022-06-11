using System;
using System.Text;
using System.Security.Cryptography;
namespace proyecto_ing_software_api.Libraries
{
    public static class TokenGenerador
    {
        internal static char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

        public static string GenerarToken(){
            byte[] data = new byte[32];

            using(var encrip = RandomNumberGenerator.Create()){
                encrip.GetBytes(data);
            }

            StringBuilder result = new StringBuilder(8);
            for(int i=0; i<8; i++){
                var rnd = BitConverter.ToUInt32(data, i*4);
                var inx = rnd % chars.Length;

                result.Append(chars[inx]);
            }
            return result.ToString();
        }
    }
}