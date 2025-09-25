const crypto = require("crypto");
const algorithm = 'aes-256-gcm';
const key = crypto.randomBytes(32);
const iv = crypto.randomBytes(16);

// Encrypt data
const cipher = crypto.createCipheriv(algorithm, key, iv);
let data = 'This is a secret text';
let encrypted = cipher.update(data, 'utf8', 'hex');
encrypted += cipher.final('hex');
console.log(`Cipher Text: ${encrypted}`);

const tag = cipher.getAuthTag();
console.log(`Auth TAG: ${tag.toString('hex')}`);

// Decrypt data
const decipher = crypto.createDecipheriv(algorithm, key, iv);
decipher.setAuthTag(tag);
let decrypted = decipher.update(encrypted, 'hex', 'utf-8');
decrypted += decipher.final('utf-8');
console.log(`Secret Text: ${decrypted}`);