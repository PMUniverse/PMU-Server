﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Security;
using PMU.Sockets;
using System.IO.Compression;
using System.IO;

namespace Server.Network
{
    public class PacketModifiers
    {
        Security.Encryption crypt;
        bool obtainedKey;

        bool encryptionEnabled = true;

        const string DEFAULT_KEY = "abcdefgh!6876b)(gjhgfy8u7y";//"abcdefgh76876bfgjhgfy8u7iy";

        public bool ObtainedKey {
            get { return obtainedKey; }
        }

        public PacketModifiers() {
            crypt = new Security.Encryption();
            crypt.SetKey(DEFAULT_KEY);
        }

        public void SetKey(string key) {
            if (string.IsNullOrEmpty(key)) {
                encryptionEnabled = false;
            } else {
                if (!obtainedKey) {
                    crypt = new Encryption(key);
                    obtainedKey = true;
                }
            }
        }

        public byte[] DecryptPacket(byte[] packet) {
            return crypt.DecryptBytes(packet);
        }

        public byte[] EncryptPacket(byte[] packet) {
            return crypt.EncryptBytes(packet);
        }

        public byte[] CompressPacket(byte[] packet) {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) {
                using (GZipStream gzip = new GZipStream(ms, CompressionMode.Compress, true)) {
                    gzip.Write(packet, 0, packet.Length);
                }
                return ms.ToArray();
            }
        }

        public byte[] DecompressPacket(byte[] packet) {
            // Create a GZIP stream with decompression mode.
            // ... Then create a buffer and write into while reading from the GZIP stream.
            using (GZipStream stream = new GZipStream(new MemoryStream(packet), CompressionMode.Decompress)) {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream()) {
                    int count = 0;
                    do {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0) {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }
    }
}
