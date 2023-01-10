### Cryptography
1. HMAC hashing
```
const crypto = require("crypto");
const key = crypto.randomBytes(256).toString('hex');
const hmac = crypto.createHmac('sha256', key);
const data = 'something you want to hash';
hmac.update(data);
console.log(hmac.digest('hex'));
```

1. PBKDF2 hashing
```
const crypto = require("crypto");
const password = "password1";
const salt = crypto.randomBytes(256).toString('hex');
const hashedPassword = crypto.pbkdf2Sync(password, salt, 100000, 512, 'sha512');
console.log(hashedPassword.toString('hex'));
```

1. AES Encryption
```
const crypto = require("crypto");
const algorithm = 'aes-256-gcm';
const password = 'password1';

const salt = crypto.randomBytes(32);
const key = crypto.scryptSync(password, salt, 32);
const iv = crypto.randomBytes(16);

// Encrypt data
const cipher = crypto.createCipheriv(algorithm, key, iv);
let data = 'This is a secret text';
let encrypted = cipher.update(data, 'utf8', 'hex');
encrypted += cipher.final('hex');
console.log(encrypted);

const tag = cipher.getAuthTag();
console.log(tag);

// Decrypt data
const decipher = crypto.createDecipheriv(algorithm, key, iv);
decipher.setAuthTag(tag);
let decrypted = decipher.update(encrypted, 'hex', 'utf-8');
decrypted += decipher.final('utf-8');
console.log(decrypted);
```


1. Diffie Hellman (Key Exchange)
```
const crypto = require("crypto");
const sally  = crypto.createDiffieHellman(2048);
const sallyKey = sally.generateKeys();

const bob = crypto.createDiffieHellman(sally.getPrime(), sally.getGenerator());
const bobKey = bob.generateKeys();

const bobSecret = sally.computeSecret(bobKey);
const sallySecret = bob.computeSecret(sallyKey);

console.log(bobSecret.toString('hex'));
console.log(sallySecret.toString('hex'));
```
