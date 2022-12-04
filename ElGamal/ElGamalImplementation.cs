using ElGamal.Helper;
using System.Security.Cryptography;

namespace ElGamal;

public struct ElGamalKeyStruct {
    public BigInteger P;
    public BigInteger G;
    public BigInteger Y;
    public BigInteger X;
}
public class ElGamalImplementation : ElGamal
{
    private ElGamalKeyStruct _oKeyStruct;
    
    public ElGamalImplementation(  ) {
        //  Create the key Structure
        _oKeyStruct = new ElGamalKeyStruct(  );
        //  This is where we set all the keys to 0
        _oKeyStruct.P = new BigInteger(0);
        _oKeyStruct.G = new BigInteger(0);
        _oKeyStruct.Y = new BigInteger(0);
        _oKeyStruct.X = new BigInteger(0);
        
        //  If we don't give any key size then we'll use the default
        KeySizeValue = 1024;
        //  The range of keys that can be used
        LegalKeySizesValue = new KeySizes[] {new KeySizes(384, 2048, 8)};
    }
    
    public override void ImportParameters(ElGamalParameters pParameters) {
        //  Obtain the parameters when imported
        _oKeyStruct.P = new BigInteger(pParameters.P);
        _oKeyStruct.G = new BigInteger(pParameters.G);
        _oKeyStruct.Y = new BigInteger(pParameters.Y);
        if (pParameters.X != null && pParameters.X.Length > 0)
        {
            _oKeyStruct.X = new BigInteger(pParameters.X);
        }
        //  Since we need the length of the modulus P we save it as the key size value
        KeySizeValue = _oKeyStruct.P.bitCount(  );
    }

    public override ElGamalParameters ExportParameters(bool pIncludePrivateParams) {

        if (NeedToGenerateKey(  )) {
            //  If we haven't generated the keys already we create a new pair 
            CreateKeyPair(KeySizeValue);
        }

        // create the parameter set
        ElGamalParameters xParams = new ElGamalParameters(  );
        // set the public values of the parameters
        xParams.P = _oKeyStruct.P.getBytes(  );
        xParams.G = _oKeyStruct.G.getBytes(  );
        xParams.Y = _oKeyStruct.Y.getBytes(  );

        // if required, include the private value, X
        if (pIncludePrivateParams) {
            xParams.X = _oKeyStruct.X.getBytes(  );
        } else {
            // ensure that we zero the value
            xParams.X = new byte[1];
        }
        // return the parameter set
        return xParams;
    }

    public override byte[] EncryptData(byte[] pData) {
        if (NeedToGenerateKey(  )) {
            // we need to create a new key before we can export 
            CreateKeyPair(KeySizeValue);
        }
        // encrypt the data
        ElGamalEncryptor xEnc = new ElGamalEncryptor(_oKeyStruct);
        return xEnc.ProcessData(pData);
    }

    public override byte[] DecryptData(byte[] pData) {
        if (NeedToGenerateKey(  )) {
            // we need to create a new key before we can export 
            CreateKeyPair(KeySizeValue);
        }
        // encrypt the data
        ElGamalDecryptor xEnc = new ElGamalDecryptor(_oKeyStruct);
        return xEnc.ProcessData(pData);
    }

    public override byte[] Sign(byte[] pHashcode)
    {
        throw new NotImplementedException();
    }

    protected override void Dispose(bool pBool) {
        throw new NotImplementedException();
    }
    
    public override bool VerifySignature(byte[] pHashcode, byte[] pSignature)
    {
        throw new NotImplementedException();
    }
    
    
    public override string SignatureAlgorithm {
        get {
            return "ElGamal";
        }
    }

    public override string KeyExchangeAlgorithm {
        get {
            return "ElGamal";
        }
    }
    
    private void CreateKeyPair(int pKeyStrength) {
        Random randomGenerator = new Random(  );

        //  Set the modulus P - prime number
        _oKeyStruct.P = BigInteger.genPseudoPrime(pKeyStrength, 16, randomGenerator);

        //  We set g and x which are integers smaller than the modulus P
        _oKeyStruct.X = new BigInteger(  );
        _oKeyStruct.X.genRandomBits(pKeyStrength - 1, randomGenerator);
        _oKeyStruct.G = new BigInteger(  );
        _oKeyStruct.G.genRandomBits(pKeyStrength - 1, randomGenerator);

        //  Compute Y = (G^X)*modP
        _oKeyStruct.Y = _oKeyStruct.G.modPow(_oKeyStruct.X, _oKeyStruct.P);        
    }
    
    private bool NeedToGenerateKey(  ) {
        return _oKeyStruct.P == 0 && _oKeyStruct.G == 0 && _oKeyStruct.Y == 0;
    }

    public ElGamalKeyStruct KeyStruct {
        get {
            if (NeedToGenerateKey(  )) {
                CreateKeyPair(KeySizeValue);
            }
            return _oKeyStruct;
        }
        set {
            _oKeyStruct = value;
        }
    }
}