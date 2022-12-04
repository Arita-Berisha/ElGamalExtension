namespace ElGamal;

public abstract class ElGamalAbstractCipher {
    protected int OBlockSize;
    protected int OPlaintextBlocksize;
    protected int OCiphertextBlocksize;
    protected ElGamalKeyStruct OKeyStruct;

    public ElGamalAbstractCipher(ElGamalKeyStruct pKeyStruct) {
        OKeyStruct = pKeyStruct;

        //  In order to encrypt messages greater than the modulus, we must break the plaintext in smaller values
        OPlaintextBlocksize = (pKeyStruct.P.bitCount(  ) - 1) / 8;
        
        //  The size of the ciphertext created by the encryption function is the double size of the plaintext
        OCiphertextBlocksize = ((pKeyStruct.P.bitCount(  ) + 7) / 8) * 2;

        //  Set the default block for plaintext, which is suitable for encryption
        OBlockSize = OPlaintextBlocksize;
    }
    
    public byte[] ProcessData(byte[] pData) {
       
        //  Create a stream backed by a memory array
        MemoryStream xStream = new MemoryStream(  );
        
        //  Calculate how many complete blocks there are
        int xCompleteBlocks = pData.Length / OBlockSize;

        //  Create a block
        byte[] xBlock = new Byte[OBlockSize];

        //  Calculate for each block
        int i = 0;
        for (; i < xCompleteBlocks; i++) {
            //  Copy the block and create the big integer
            Array.Copy(pData, i * OBlockSize, xBlock, 0, OBlockSize);
            //  Process the block
            byte[] xResult = ProcessDataBlock(xBlock);
            //  Write the processed data into the stream
            xStream.Write(xResult, 0, xResult.Length);
        }

        //  Process the final block
        byte[] xFinalBlock = new Byte[pData.Length - 
                                        (xCompleteBlocks * OBlockSize)];
        Array.Copy(pData, i * OBlockSize, xFinalBlock, 0, 
            xFinalBlock.Length);

        byte[] xFinalResult = ProcessFinalDataBlock(xFinalBlock);

        xStream.Write(xFinalResult, 0, xFinalResult.Length);

        return xStream.ToArray(  );
    }
    
    protected abstract byte[] ProcessDataBlock(byte[] pBlock);

    protected abstract byte[] ProcessFinalDataBlock(byte[] pFinalBlock);
}
