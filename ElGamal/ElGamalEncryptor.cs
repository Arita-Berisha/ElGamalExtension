using ElGamal.Helper;

namespace ElGamal;

public class ElGamalEncryptor : ElGamalAbstractCipher {
    Random _oRandom;

    public ElGamalEncryptor(ElGamalKeyStruct pStruct) : base(pStruct) {
        _oRandom = new Random(  );
    }
    
    protected override byte[] ProcessDataBlock(byte[] pBlock) {
        BigInteger k;          
        //  Create k, which is the random number, relatively prime to modulus P-1  
        do {
            k = new BigInteger(  );
            k.genRandomBits(OKeyStruct.P.bitCount(  ) -1, _oRandom);
        } while (k.gcd(OKeyStruct.P  -1) != 1);

        //  Compute the value a = (G^K)*modP
        BigInteger a = OKeyStruct.G.modPow(k, OKeyStruct.P);
        
        //  Compute the value b = ((y^K)*m)modP
        BigInteger b = (OKeyStruct.Y.modPow(k, OKeyStruct.P) 
                        * new BigInteger(pBlock)) % (OKeyStruct.P);

        //  Create an array to contain the ciphertext
        byte[] xResult = new byte[OCiphertextBlocksize];
        //  Concatenate the array a and array b
        byte[] xABytes = a.getBytes(  );
        Array.Copy(xABytes, 0, xResult, OCiphertextBlocksize / 2 
                                           - xABytes.Length, xABytes.Length);
        byte[] xBBytes = b.getBytes(  );
        Array.Copy(xBBytes, 0, xResult, OCiphertextBlocksize 
                                           - xBBytes.Length, xBBytes.Length);
        
        return xResult;
    }
    
    protected override byte[] ProcessFinalDataBlock(byte[] pFinalBlock) {
        if (pFinalBlock.Length > 0) {
            if (pFinalBlock.Length < OBlockSize) {
                //  Create a fullsize block which contains the data to encrypt followed by trailing zeros
                byte[] xPadded = new byte[OBlockSize];
                Array.Copy(pFinalBlock, 0, xPadded, 0, pFinalBlock.Length);
                return ProcessDataBlock(xPadded);
            } else {
                return ProcessDataBlock(pFinalBlock);
            }
        } else {
            return new byte[0];
        }
    }
}