using System.Text;
using System.Xml.Linq;
using System.Diagnostics;

namespace ElGamal;

public static class Program
{

    static void Main(string[] args)
    {
        /*
            The ElGamal Algorithms goes through a series of steps:
                1. Generate Public and Private Keys
                2. Encrypt the Message with the generated Keys
                3. Decrypt the Message and Compare
        */
        // Choose a message to test out
        Console.WriteLine( "What is your message?" );
        string plaintext = Console.ReadLine();
        
        // Choose the key size to use when encrypting the message
        Console.WriteLine("\nWhat key size do you want?");
        int keySize = Convert.ToInt32(Console.ReadLine());
        
        // Starting the Stopwatch
        var watch = Stopwatch.StartNew();
        if (plaintext != null && keySize != 0)
        {
            // Encode the Plaintext
            byte[] xPlaintext 
                = Encoding.Default.GetBytes(plaintext);
        
            //  1. Generate Public and Private Keys
            ElGamal xAlg = new ElGamalImplementation(  );
            //  Set the key size
            xAlg.KeySize = keySize;
            
            //  Extract keys from xml and print them
            string xXmlString = xAlg.ToXmlString(true);
            var elGamalKeyValue = XElement.Parse(xXmlString);
            var keys = elGamalKeyValue.Elements();

            Console.WriteLine("\n");
            foreach(var key in keys)
            {
                Console.WriteLine("Key {0}: {1}", key.Name, key.Value);
            }
            
            //  Create a new instance of the algorithm to encrypt
            ElGamal xEncryptAlg = new ElGamalImplementation(  );
            xEncryptAlg.FromXmlString(xAlg.ToXmlString(false));
            byte[] xCiphertext = xAlg.EncryptData(xPlaintext);
       
            //  Create a new instance of the algorithm to decrypt
            ElGamal xDecryptAlg = new ElGamalImplementation(  );
            xDecryptAlg.FromXmlString(xAlg.ToXmlString(true));
            byte[] xResult = xDecryptAlg.DecryptData(xCiphertext);
            
            //  Result
            Console.WriteLine("\nPlaintext: {0}", Encoding.Default.GetString(xPlaintext));
            Console.WriteLine("Result: {0}", Encoding.Default.GetString(xResult));
            
            watch.Stop();
            // The execution time in milliseconds
            Console.WriteLine(
                $"\nThe Execution time of the program is {watch.ElapsedMilliseconds}ms");
     
        }
    }
}


