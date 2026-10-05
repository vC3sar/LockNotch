using System;
using System.Security.Cryptography;

var rsa = RSA.Create(2048);
byte[] pubKey = rsa.ExportSubjectPublicKeyInfo();
string base64 = Convert.ToBase64String(pubKey);

using var sha256 = SHA256.Create();
byte[] hash = sha256.ComputeHash(pubKey);
string hex = BitConverter.ToString(hash).Replace("-", "").ToLower().Substring(0, 32);

string id = "";
foreach (char c in hex)
{
    int val = Convert.ToInt32(c.ToString(), 16);
    id += (char)('a' + val);
}

Console.WriteLine($"KEY:{base64}");
Console.WriteLine($"ID:{id}");
