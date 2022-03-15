# -*- coding: utf-8 -*-
from random import randint, choice
from Crypto.PublicKey import RSA
from Crypto.Cipher import AES, PKCS1_OAEP
from Crypto.Hash import SHA256


class CryptoUtils:    # Tested OK

    @staticmethod
    def encrypt_rsa(byte_data, rsa_public_key):
        rsa_key = RSA.importKey(rsa_public_key)
        cipher_rsa = PKCS1_OAEP.new(rsa_key, hashAlgo=SHA256)
        return cipher_rsa.encrypt(byte_data)

    @staticmethod
    def encrypt_aes(data, key, iv):
        while len(data) % 16 != 0:
            data += '\u0000'.encode()
        return AES.new(key, AES.MODE_CBC, iv=iv).encrypt(data)

    @staticmethod
    def decrypt_aes(data, key, iv):
        res = AES.new(key, AES.MODE_CBC, iv=iv).decrypt(data)
        while len(res) and bytes([res[-1]]) == '\u0000'.encode():
            res = res[:-1]
        return res

    @staticmethod
    def get_random_bytes(count):
        return bytes([randint(0, 255) for _ in range(count)])

    @staticmethod
    def generate_token(length=64):
        return ''.join([choice('0123456789abcdefghijklmnopqrstuvwxyz') for _ in range(length)])

    @staticmethod
    def get_password_hash(password):
        return SHA256.new(password.encode()).hexdigest()
