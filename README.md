# ElGamalExtension

### What is the ElGamal Algorithm

ElGamal requires mathematical computations that are not available in the .NET framework without the development of extra support functions.

The ElGamal algorithm was created by Taher ElGamal and published in 1985. It has since gained popularity as a replacement for RSA, which, at the time, used to have problems with licensing and patents.

The ElGamal algorithm is divided into three steps:
1. Generating keys
2. Encryption
3. Decryption

#### Generating Keys

We use a specific procedure to generate all keys required to move on to the following step:

1. Pick a prime number at random, *p*. The ElGamal "*modulus*" is represented by this number. The size of the public key is determined by the number of bits needed to represent *p*, therefore if *p* is represented using 1024 bits, then the key created by following the protocol is also 1024 bits in size. 

2. Choose two more integers at random, *g* and *x*, that are both smaller than *p*; these numbers do not need to be prime.

3. Calculate y such that:

$$y = g^xmodp$$

Our public key consists of (p, g, y), whereas our private key is (x).

#### Encryption

Only numerical numbers that are smaller than the modulus, *p*, are encrypted using the ElGamal technique. We must divide the plaintext into smaller values in order to encrypt messages larger than the modulus.

1. Block the plaintext according to the public key modulus's length.
2. Process each block of plaintext, *m*:
	1. Pick a random number, *k*, that is relatively prime to *p-1* (have no common factors).

	2. Calculate *a* and *b*: $$a = g^kmodp$$ $$b = (y^km)modp$$ 
	3. Concantenate a and b to form an encrypted data block.
3. To create the ciphertext, join the encrypted data blocks together.

Because we join a and b to get the ciphertext, the size of the ciphertext is twice the size of the plaintext.

#### Decryption

1. Blocks of the ciphertext that are twice as long as the key modulus should be created.

2. Process each block of ciphertext:

	1. To create letters *a* and *b*, divide the ciphertext block in half.
	
	2. Calculate the decrypted block *m* using the private key where: $$m = (b/a^x)modp$$
	3. To create the plaintext, combine the decrypted blocks.

**NOTE**: In this algorithm we use and modify the class BigInteger. The variable *maxLength* has been set to 140. The necessity to conduct arithmetic operations on extremely large integer numbers, which can comprise several hundreds of digits, is a challenge presented by asymmetric algorithms. Our requirements, where 1024- and 2048-bit integers are frequently required, fall short of the capabilities of the .NET Framework, which is unable to process numbers that require more than 64 bits to represent them.

### Results

Here we take some examples to see how the Algorithm works. Lets see how the sentence '*According to an article on Medium Elgamal's first love was mathematics.*' will do with different versions of key sizes.

KeySize: 384 (min), Time: 214ms.
```
What is your message?
According to an article on Medium Elgamal's first love was mathematics.

What key size do you want?
384


Key P: 34YCkFa73/Cbts+VW69UY4Q7BPdUgMwKAYzTzWd6MNaWdzsgzm+b3jZMhqqnzNzl
Key G: dQs2NqMEC+xSkphhUnY7Qpf1bpUAFppI3MwVJ056ZR0FgtSM+4sbWjr9cUR/82ey
Key Y: RFI0n8bl/zX9rRU+P/sfNUlKQF4grrvF9InzmoLuGptqJXPNbXMMUR6/z8qqpnth
Key X: ckbRYW44f8xKpDK+xj6N0FAWLTz0Vh4oc6CenNgtVxPb3WkNkYnJK9TqzQa/4Wya

Plaintext: According to an article on Medium Elgamal's first love was mathematics.
Result: According to an article on Medium Elgamal's first love was mathematics.

The Execution time of the program is 214ms
```

KeySize: 768, Time: 2770ms.
```
What is your message?
According to an article on Medium Elgamal's first love was mathematics.

What key size do you want?
768


Key P: n0CUXs+KWv+EjKar3BPcibJCFMSb1w+0wjdgTFZk2gChkLNDsjuCxzbTWVswILSDx8GntTCuYTm/Hmlr7Ofpp6J91xXb8rQneOcEa2uBGbTY/1Z1u+vuLn4PwBm7VeeZ
Key G: fIEJ44zXQvtaylzCnm3X/9ijvmOXCIfbwhwFojr/Te4ndJLV5FX7snXt2y660XBGvMo5GD8BFyjXICO29KwxxGMDRnRAjpg/vhqzSGFCMUAJc6XdZsaGYnEFQSFZR7DF
Key Y: OTo8+DPQW+NOU7M3gX4qwsdE7SJyzxylT34abBJnvnzh/DKEyjVWmZVh86398/s7rVmT1yUVcKYACSdsI3CbzMkJSRIc/m9k5+8J6R+XQo8g+uNcliuof8SymV6+qaLk
Key X: UptasA987zk0YOobnou0QJWFPT6nKO1pevMShV8E4kBylzF10fv/2adSfc7kI+qqvukKyU7HX8vpvrZBoTvRJKxnH/59kDbztWeJCy2n5iZb3aoKi7KK9ORkcdG0GQZ4

Plaintext: According to an article on Medium Elgamal's first love was mathematics.
Result: According to an article on Medium Elgamal's first love was mathematics.

The Execution time of the program is 2770ms
```

KeySize: 1024 (default), Time: 3983ms.
```
What is your message?
According to an article on Medium Elgamal's first love was mathematics.

What key size do you want?
1024


Key P: mriiBCJBw7o4m6EohZUHfyQZsntk4BuuLhSQytkWwSBCnwHuFmZs0XRaj3/N7dJUibh8JUGloEKRZMi8OFHxkYWS+/X8sJPV82yojvwxBlcg2YCLhgL49yL8MTqu/lxJyLvjwzUu5wG/f8eeY/VJqyDDe9KlScv4fHX8Rm7
XxS8=
Key G: VNjOStzyOKAU/2MK/Ef/qHQZA2XuliPTZWkBUjVM/srg5X29TdYoazIWxDWmkNelijByzKraL+HdVlg0M9/KVCOz9eNk5YVw+Kwcc3lKZ8kWgR9KE6AenDyY1sR049nbGp4PzkgLbF2EozM4MdKXp2TaDcG4ySlhjUKFuJS
d+Ec=
Key Y: SZvu/LfdxIIodKp6PtztvDmxsUCdv2GDc0PBHkt5u8RVHvFfARPTTGIaaSN+Ptqu+0yHd9z3uD2POQdDWm4RamjdKfuF5xhbfMQaGyTsosdIGBaVMWn17YzBpFrEnq6buvZm+bmbA/R5CTPEoEImEgK9Tg67SwgEJHk0ysS
ROBA=
Key X: da1oB8Hu92x8QH829kYB1PS1CX8gKZkiSx7T6X3g2JUw9nMtGan4b64uOtddRjnNhbrqqyVmg8/WIGKGHo0PPSsLbC7IxxVS1UHMszIsG0vruq3pBU4tI7yX/XxDM22G29a+Sid5XHTdGm1fCXXYXiatcCPFfDh2nw4eC/7
+zSY=

Plaintext: According to an article on Medium Elgamal's first love was mathematics.
Result: According to an article on Medium Elgamal's first love was mathematics.

The Execution time of the program is 3983ms
```

KeySize: 2048 (max), Time: 33485ms.
```
What is your message?
According to an article on Medium Elgamal's first love was mathematics.

What key size do you want?
2048


Key P: 4bufYJKpyRo78I1+E2vnhrr66AgwGCEegp1b7b7aGpGSBkcpx2eLrmpbEwZzemHnsWmPz80pYcbTspcQl8Wg+woWbnRvXUMYRjTP5dl8aZWq+pG62wHIbBOVjOMMND6za/5Cl73GfGUa8mp+Oi0fLpvQhGU5g3/11nTPViW
cz6IZcAz4JNV3+s7Itlu1HXDONSYu2G/LwkTFPrYQIsqjh7G/zuHFVa+L7OP874DYpwDo517bBoc8KPptuAV34oLEKL8LnkVYQ24Dg42JA8Z4ojmgAsWJdjkQVjNFpCpwy7kmXX7tbuutun654Nxu0DAHFHC498EyWvtTAAza0AD9f
w==
Key G: cc6U98oftNnAFKHsf2lvNOHH+tljl6O2zkBvAR/Drr5A2NE9mGQNp/qSbvS/x2tJHBR2vXKf7h+I3iQgOz717JuUQuTcqoWMXWOABQnuIw+q8tsbXmfHegugjtEcImdrXjMfJX5/pVCZqgdhqVSvHRbHOv3z6Q4ZVAYmdmH
MMAvUdxMzWLlrogiUL30V4oQWjwFR8WHYzlIuL8EHt/A1TvsMzuDOd174Ew7Z15jrYRCXErx0IfjEqQyROdal8p+qs5sbHOk4WFCZ5rSvYlJOG52lzjV43FYEnuAUmgvoib0ybp08nEcvvsQ8c/2LsSQCMxtkMhfvD7XtQIVU6lLMo
A==
Key Y: WYbMMML9r9/4h0euu4qXIeWXYA9ZM8ja3A3KxvcHCr4byK20HdatAIj4aTf1B4h57piXDWFepTAlQDRvgDytKa62GnDtcNiIls3KWcqvdTkjvvCFJOAZxKB/soYp8dWp6lYOpuLgDzHcGAVSR+ZU3rUV/hHJlGpmypq45aP
OlbsVj4HbhXQdSCvSbIYPXRX3xi5MWSwpI0cpMlNvchhAxXZcfI3mUoPV4MPO8+2xOYR9csmr05hl4oo0pUIKr8AKGPRQkLqoo1Jgn3IXAMjOeQWZLxs2YEubBGDHhUnxEcctKIAwmrMyj2U1zf1iVpZquaPIJvu22DUbKo7zXCSp0
Q==
Key X: Xw+G6m9gh/7PIDjXptRyCOiBvwvsRvYhuwMKDJRXPfiUYH9o6L7XxIJHuqj0Y4OOUXFgh8ZRss1bCx18sQDaxddOWQlmDDI4yVlsJ09lMu2wQ+XoThlfQlbLkUMGb6d6CcBn9splZqszNUcQoDpLZUYew6zpM4QWhtuv3xu
JafbuUpjhegBCi72jKmAkmfTAa6PejtWJGlDsjfZieV9pTuZ0ZDMd/itWVKcF/gbY6dGE2jvK9AD81MAFN1qNZxH0Va4sMcJ9/5XyXHtHJsbmi7AzjABAdrJo8dryPu9pjNIyIXykEk+iM1asb3iq+iEgW7WeUP603n6uxF0snnE7W
g==

Plaintext: According to an article on Medium Elgamal's first love was mathematics.
Result: According to an article on Medium Elgamal's first love was mathematics.


The Execution time of the program is 33485ms
```

### References
- [15.3 Extending the .NET Framework](http://etutorials.org/Programming/Programming+.net+security/Part+III+.NET+Cryptography/Chapter+15.+Asymmetric+Encryption/15.3+Extending+the+.NET+Framework/)
- [ElGamal Encryption](https://en.wikipedia.org/wiki/ElGamal_encryption)
