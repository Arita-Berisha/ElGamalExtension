using ElGamal.Helper;

namespace ElGamal;

public class ElGamalDecryptor : ElGamalAbstractCipher {
    
    public ElGamalDecryptor(ElGamalKeyStruct pStruct) : base(pStruct) {
        //  Set the default block size to be ciphertext
        OBlockSize = OCiphertextBlocksize;
    }
    
    protected override byte[] ProcessDataBlock(byte[] pBlock) {
        //  Get a and b
        byte[] xABytes = new byte[OCiphertextBlocksize / 2];
        Array.Copy(pBlock, 0, xABytes, 0, xABytes.Length);
        byte[] xBBytes = new Byte[OCiphertextBlocksize / 2];
        Array.Copy(pBlock, xABytes.Length, xBBytes, 0, xBBytes.Length);
        
        BigInteger a = new BigInteger(xABytes);
        BigInteger b = new BigInteger(xBBytes);
        
        //  Calculate the value m = (b/(a^X))modP
        BigInteger m = (b * 
                        a.modPow(OKeyStruct.X, OKeyStruct.P).modInverse(OKeyStruct.P)) 
                       % OKeyStruct.P;
  
        byte[] xMBytes = m.getBytes(  );

        if (xMBytes.Length < OPlaintextBlocksize) {
            byte[] xFullResult = new byte[OPlaintextBlocksize];
            Array.Copy(xMBytes, 0, xFullResult, 
                OPlaintextBlocksize - xMBytes.Length, xMBytes.Length);
            xMBytes = xFullResult;
        }
        return xMBytes;
    }
    
    protected override byte[] ProcessFinalDataBlock(byte[] pFinalBlock) {
        if (pFinalBlock.Length > 0) {
            return ProcessDataBlock(pFinalBlock);
        } else {
            return new byte[0];
        }
    }
}