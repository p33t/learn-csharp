using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace fundamental_c_sharp
{
    public static class EncryptionAes
    {
        public static async Task Demo()
        {

            Console.WriteLine("Encryption with AES ===========================");
            // Setup input
            var input = $"My secret string - {Guid.NewGuid()}";
            Console.WriteLine($"Input:  {input}");
            var inputBytes = Encoding.UTF8.GetBytes(input);
            
            // Setup Aes object
            var aes = Aes.Create();
            var keyBytes = Guid.Parse("9961fc4c-4831-41b5-abf7-78426f86c7eb").ToByteArray();
            Trace.Assert(keyBytes.Length == 16); // one of the valid key lengths
            aes.Key = keyBytes;
            
            // Encrypt
            // NOTE: InitializationVector is generated but you can use a known one (of the correct size)
            var encryptedBytes = aes.EncryptCbc(inputBytes, aes.IV);
            Console.WriteLine($"Encrypted data: {Convert.ToBase64String(encryptedBytes)}");

            // Decrypt
            var outputBytes = aes.DecryptCbc(encryptedBytes, aes.IV);
            var output = Encoding.UTF8.GetString(outputBytes);
            Console.WriteLine($"Output: {output}");
            Trace.Assert(input == output, "Output does NOT match Input");
            
            Console.WriteLine("Success");
        }
    }
}