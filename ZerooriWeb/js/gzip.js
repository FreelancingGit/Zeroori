***** LZ - string.js
// Copyright (c) 2013 Pieroxy <pieroxy@pieroxy.net>
// This work is free. You can redistribute it and/or modify it
// under the terms of the WTFPL, Version 2
// For more information see LICENSE.txt or http://www.wtfpl.net/
//
// For more information, the home page:
// http://pieroxy.net/blog/pages/lz-string/testing.html
//
// LZ-based compression algorithm, version 1.4.3
var LZString = (function () {

    // private property
    var f = String.fromCharCode;
    var keyStrBase64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    var keyStrUriSafe = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+-$";
    var baseReverseDic = {};

    function getBaseValue(alphabet, character) {
        if (!baseReverseDic[alphabet]) {
            baseReverseDic[alphabet] = {};
            for (var i = 0; i < alphabet.length; i++) {
                baseReverseDic[alphabet][alphabet[i]] = i;
            }
        }
        return baseReverseDic[alphabet][character];
    }

    var LZString = {
        compressToBase64: function (input) {
            if (input == null) return "";
            var res = LZString._compress(input, 6, function (a) { return keyStrBase64.charAt(a); });
            switch (res.length % 4) { // To produce valid Base64
                default: // When could this happen ?
                case 0: return res;
                case 1: return res + "===";
                case 2: return res + "==";
                case 3: return res + "=";
            }
        },

        decompressFromBase64: function (input) {
            if (input == null) return "";
            if (input == "") return null;
            return LZString._decompress(input.length, 32, function (index) { return getBaseValue(keyStrBase64, input.charAt(index)); });
        },

        compressToUTF16: function (input) {
            if (input == null) return "";
            return LZString._compress(input, 15, function (a) { return f(a + 32); }) + " ";
        },

        decompressFromUTF16: function (compressed) {
            if (compressed == null) return "";
            if (compressed == "") return null;
            return LZString._decompress(compressed.length, 16384, function (index) { return compressed.charCodeAt(index) - 32; });
        },

        //compress into uint8array (UCS-2 big endian format)
        compressToUint8Array: function (uncompressed) {
            var compressed = LZString.compress(uncompressed);
            var buf = new Uint8Array(compressed.length * 2); // 2 bytes per character

            for (var i = 0, TotalLen = compressed.length; i < TotalLen; i++) {
                var current_value = compressed.charCodeAt(i);
                buf[i * 2] = current_value >>> 8;
                buf[i * 2 + 1] = current_value % 256;
            }
            return buf;
        },

        //decompress from uint8array (UCS-2 big endian format)
        decompressFromUint8Array: function (compressed) {
            if (compressed === null || compressed === undefined) {
                return LZString.decompress(compressed);
            } else {
                var buf = new Array(compressed.length / 2); // 2 bytes per character
                for (var i = 0, TotalLen = buf.length; i < TotalLen; i++) {
                    buf[i] = compressed[i * 2] * 256 + compressed[i * 2 + 1];
                }

                var result = [];
                buf.forEach(function (c) {
                    result.push(f(c));
                });
                return LZString.decompress(result.join(''));

            }

        },


        //compress into a string that is already URI encoded
        compressToEncodedURIComponent: function (input) {
            if (input == null) return "";
            return LZString._compress(input, 6, function (a) { return keyStrUriSafe.charAt(a); });
        },

        //decompress from an output of compressToEncodedURIComponent
        decompressFromEncodedURIComponent: function (input) {
            if (input == null) return "";
            if (input == "") return null;
            input = input.replace(/ /g, "+");
            return LZString._decompress(input.length, 32, function (index) { return getBaseValue(keyStrUriSafe, input.charAt(index)); });
        },

        compress: function (uncompressed) {
            return LZString._compress(uncompressed, 16, function (a) { return f(a); });
        },
        _compress: function (uncompressed, bitsPerChar, getCharFromInt) {
            if (uncompressed == null) return "";
            var i, value,
                context_dictionary = {},
                context_dictionaryToCreate = {},
                context_c = "",
                context_wc = "",
                context_w = "",
                context_enlargeIn = 2, // Compensate for the first entry which should not count
                context_dictSize = 3,
                context_numBits = 2,
                context_data = [],
                context_data_val = 0,
                context_data_position = 0,
                ii;

            for (ii = 0; ii < uncompressed.length; ii += 1) {
                context_c = uncompressed[ii];
                if (!Object.prototype.hasOwnProperty.call(context_dictionary, context_c)) {
                    context_dictionary[context_c] = context_dictSize++;
                    context_dictionaryToCreate[context_c] = true;
                }

                context_wc = context_w + context_c;
                if (Object.prototype.hasOwnProperty.call(context_dictionary, context_wc)) {
                    context_w = context_wc;
                } else {
                    if (Object.prototype.hasOwnProperty.call(context_dictionaryToCreate, context_w)) {
                        if (context_w.charCodeAt(0) < 256) {
                            for (i = 0; i < context_numBits; i++) {
                                context_data_val = (context_data_val << 1);
                                if (context_data_position == bitsPerChar - 1) {
                                    context_data_position = 0;
                                    context_data.push(getCharFromInt(context_data_val));
                                    context_data_val = 0;
                                } else {
                                    context_data_position++;
                                }
                            }
                            value = context_w.charCodeAt(0);
                            for (i = 0; i < 8; i++) {
                                context_data_val = (context_data_val << 1) | (value & 1);
                                if (context_data_position == bitsPerChar - 1) {
                                    context_data_position = 0;
                                    context_data.push(getCharFromInt(context_data_val));
                                    context_data_val = 0;
                                } else {
                                    context_data_position++;
                                }
                                value = value >> 1;
                            }
                        } else {
                            value = 1;
                            for (i = 0; i < context_numBits; i++) {
                                context_data_val = (context_data_val << 1) | value;
                                if (context_data_position == bitsPerChar - 1) {
                                    context_data_position = 0;
                                    context_data.push(getCharFromInt(context_data_val));
                                    context_data_val = 0;
                                } else {
                                    context_data_position++;
                                }
                                value = 0;
                            }
                            value = context_w.charCodeAt(0);
                            for (i = 0; i < 16; i++) {
                                context_data_val = (context_data_val << 1) | (value & 1);
                                if (context_data_position == bitsPerChar - 1) {
                                    context_data_position = 0;
                                    context_data.push(getCharFromInt(context_data_val));
                                    context_data_val = 0;
                                } else {
                                    context_data_position++;
                                }
                                value = value >> 1;
                            }
                        }
                        context_enlargeIn--;
                        if (context_enlargeIn == 0) {
                            context_enlargeIn = Math.pow(2, context_numBits);
                            context_numBits++;
                        }
                        delete context_dictionaryToCreate[context_w];
                    } else {
                        value = context_dictionary[context_w];
                        for (i = 0; i < context_numBits; i++) {
                            context_data_val = (context_data_val << 1) | (value & 1);
                            if (context_data_position == bitsPerChar - 1) {
                                context_data_position = 0;
                                context_data.push(getCharFromInt(context_data_val));
                                context_data_val = 0;
                            } else {
                                context_data_position++;
                            }
                            value = value >> 1;
                        }


                    }
                    context_enlargeIn--;
                    if (context_enlargeIn == 0) {
                        context_enlargeIn = Math.pow(2, context_numBits);
                        context_numBits++;
                    }
                    // Add wc to the dictionary.
                    context_dictionary[context_wc] = context_dictSize++;
                    context_w = String(context_c);
                }
            }

            // Output the code for w.
            if (context_w !== "") {
                if (Object.prototype.hasOwnProperty.call(context_dictionaryToCreate, context_w)) {
                    if (context_w.charCodeAt(0) < 256) {
                        for (i = 0; i < context_numBits; i++) {
                            context_data_val = (context_data_val << 1);
                            if (context_data_position == bitsPerChar - 1) {
                                context_data_position = 0;
                                context_data.push(getCharFromInt(context_data_val));
                                context_data_val = 0;
                            } else {
                                context_data_position++;
                            }
                        }
                        value = context_w.charCodeAt(0);
                        for (i = 0; i < 8; i++) {
                            context_data_val = (context_data_val << 1) | (value & 1);
                            if (context_data_position == bitsPerChar - 1) {
                                context_data_position = 0;
                                context_data.push(getCharFromInt(context_data_val));
                                context_data_val = 0;
                            } else {
                                context_data_position++;
                            }
                            value = value >> 1;
                        }
                    } else {
                        value = 1;
                        for (i = 0; i < context_numBits; i++) {
                            context_data_val = (context_data_val << 1) | value;
                            if (context_data_position == bitsPerChar - 1) {
                                context_data_position = 0;
                                context_data.push(getCharFromInt(context_data_val));
                                context_data_val = 0;
                            } else {
                                context_data_position++;
                            }
                            value = 0;
                        }
                        value = context_w.charCodeAt(0);
                        for (i = 0; i < 16; i++) {
                            context_data_val = (context_data_val << 1) | (value & 1);
                            if (context_data_position == bitsPerChar - 1) {
                                context_data_position = 0;
                                context_data.push(getCharFromInt(context_data_val));
                                context_data_val = 0;
                            } else {
                                context_data_position++;
                            }
                            value = value >> 1;
                        }
                    }
                    context_enlargeIn--;
                    if (context_enlargeIn == 0) {
                        context_enlargeIn = Math.pow(2, context_numBits);
                        context_numBits++;
                    }
                    delete context_dictionaryToCreate[context_w];
                } else {
                    value = context_dictionary[context_w];
                    for (i = 0; i < context_numBits; i++) {
                        context_data_val = (context_data_val << 1) | (value & 1);
                        if (context_data_position == bitsPerChar - 1) {
                            context_data_position = 0;
                            context_data.push(getCharFromInt(context_data_val));
                            context_data_val = 0;
                        } else {
                            context_data_position++;
                        }
                        value = value >> 1;
                    }


                }
                context_enlargeIn--;
                if (context_enlargeIn == 0) {
                    context_enlargeIn = Math.pow(2, context_numBits);
                    context_numBits++;
                }
            }

            // Mark the end of the stream
            value = 2;
            for (i = 0; i < context_numBits; i++) {
                context_data_val = (context_data_val << 1) | (value & 1);
                if (context_data_position == bitsPerChar - 1) {
                    context_data_position = 0;
                    context_data.push(getCharFromInt(context_data_val));
                    context_data_val = 0;
                } else {
                    context_data_position++;
                }
                value = value >> 1;
            }

            // Flush the last char
            while (true) {
                context_data_val = (context_data_val << 1);
                if (context_data_position == bitsPerChar - 1) {
                    context_data.push(getCharFromInt(context_data_val));
                    break;
                }
                else context_data_position++;
            }
            return context_data.join('');
        },

        decompress: function (compressed) {
            if (compressed == null) return "";
            if (compressed == "") return null;
            return LZString._decompress(compressed.length, 32768, function (index) { return compressed.charCodeAt(index); });
        },

        _decompress: function (length, resetValue, getNextValue) {
            var dictionary = [],
                next,
                enlargeIn = 4,
                dictSize = 4,
                numBits = 3,
                entry = "",
                result = [],
                i,
                w,
                bits, resb, maxpower, power,
                c,
                data = { val: getNextValue(0), position: resetValue, index: 1 };

            for (i = 0; i < 3; i += 1) {
                dictionary[i] = i;
            }

            bits = 0;
            maxpower = Math.pow(2, 2);
            power = 1;
            while (power != maxpower) {
                resb = data.val & data.position;
                data.position >>= 1;
                if (data.position == 0) {
                    data.position = resetValue;
                    data.val = getNextValue(data.index++);
                }
                bits |= (resb > 0 ? 1 : 0) * power;
                power <<= 1;
            }

            switch (next = bits) {
                case 0:
                    bits = 0;
                    maxpower = Math.pow(2, 8);
                    power = 1;
                    while (power != maxpower) {
                        resb = data.val & data.position;
                        data.position >>= 1;
                        if (data.position == 0) {
                            data.position = resetValue;
                            data.val = getNextValue(data.index++);
                        }
                        bits |= (resb > 0 ? 1 : 0) * power;
                        power <<= 1;
                    }
                    c = f(bits);
                    break;
                case 1:
                    bits = 0;
                    maxpower = Math.pow(2, 16);
                    power = 1;
                    while (power != maxpower) {
                        resb = data.val & data.position;
                        data.position >>= 1;
                        if (data.position == 0) {
                            data.position = resetValue;
                            data.val = getNextValue(data.index++);
                        }
                        bits |= (resb > 0 ? 1 : 0) * power;
                        power <<= 1;
                    }
                    c = f(bits);
                    break;
                case 2:
                    return "";
            }
            dictionary[3] = c;
            w = c;
            result.push(c);
            while (true) {
                if (data.index > length) {
                    return "";
                }

                bits = 0;
                maxpower = Math.pow(2, numBits);
                power = 1;
                while (power != maxpower) {
                    resb = data.val & data.position;
                    data.position >>= 1;
                    if (data.position == 0) {
                        data.position = resetValue;
                        data.val = getNextValue(data.index++);
                    }
                    bits |= (resb > 0 ? 1 : 0) * power;
                    power <<= 1;
                }

                switch (c = bits) {
                    case 0:
                        bits = 0;
                        maxpower = Math.pow(2, 8);
                        power = 1;
                        while (power != maxpower) {
                            resb = data.val & data.position;
                            data.position >>= 1;
                            if (data.position == 0) {
                                data.position = resetValue;
                                data.val = getNextValue(data.index++);
                            }
                            bits |= (resb > 0 ? 1 : 0) * power;
                            power <<= 1;
                        }

                        dictionary[dictSize++] = f(bits);
                        c = dictSize - 1;
                        enlargeIn--;
                        break;
                    case 1:
                        bits = 0;
                        maxpower = Math.pow(2, 16);
                        power = 1;
                        while (power != maxpower) {
                            resb = data.val & data.position;
                            data.position >>= 1;
                            if (data.position == 0) {
                                data.position = resetValue;
                                data.val = getNextValue(data.index++);
                            }
                            bits |= (resb > 0 ? 1 : 0) * power;
                            power <<= 1;
                        }
                        dictionary[dictSize++] = f(bits);
                        c = dictSize - 1;
                        enlargeIn--;
                        break;
                    case 2:
                        return result.join('');
                }

                if (enlargeIn == 0) {
                    enlargeIn = Math.pow(2, numBits);
                    numBits++;
                }

                if (dictionary[c]) {
                    entry = dictionary[c];
                } else {
                    if (c === dictSize) {
                        entry = w + w[0];
                    } else {
                        return null;
                    }
                }
                result.push(entry);

                // Add w+entry[0] to the dictionary.
                dictionary[dictSize++] = w + entry[0];
                enlargeIn--;

                w = entry;

                if (enlargeIn == 0) {
                    enlargeIn = Math.pow(2, numBits);
                    numBits++;
                }

            }
        }
    };
    return LZString;
})();

if (typeof define === 'function' && define.amd) {
    define(function () { return LZString; });
} else if (typeof module !== 'undefined' && module != null) {
    module.exports = LZString
}

***** LZString.cs
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace XYZ.Helpers {
    public class LZString {
        private class Context_Compress
        {
            public Dictionary < string, int > dictionary { get; set; }
            public Dictionary < string, bool > dictionaryToCreate { get; set; }
            public string c { get; set; }
            public string wc { get; set; }
            public string w { get; set; }
            public int enlargeIn { get; set; }
            public int dictSize { get; set; }
            public int numBits { get; set; }
            public Context_Compress_Data data { get; set; }
    }

    private class Context_Compress_Data {
        public string str { get; set; }
            public int val { get; set; }
            public int position { get; set; }
}

private class Decompress_Data {
    public string str { get; set; }
            public int val { get; set; }
            public int position { get; set; }
            public int index { get; set; }
        }

        private static Context_Compress_Data writeBit(int value, Context_Compress_Data data)
{
    data.val = (data.val << 1) | value;

    if (data.position == 15) {
        data.position = 0;
        data.str += (char)data.val;
        data.val = 0;
    }
    else
        data.position++;

    return data;
}

        private static Context_Compress_Data writeBits(int numbits, int value, Context_Compress_Data data)
{

    for (var i = 0; i < numbits; i++) {
        data = writeBit(value & 1, data);
        value = value >> 1;
    }

    return data;
}

        private static Context_Compress produceW(Context_Compress context)
{

    if (context.dictionaryToCreate.ContainsKey(context.w)) {
        if (context.w[0] < 256) {
            context.data = writeBits(context.numBits, 0, context.data);
            context.data = writeBits(8, context.w[0], context.data);
        }
        else {
            context.data = writeBits(context.numBits, 1, context.data);
            context.data = writeBits(16, context.w[0], context.data);
        }

        context = decrementEnlargeIn(context);
        context.dictionaryToCreate.Remove(context.w);
    }
    else {
        context.data = writeBits(context.numBits, context.dictionary[context.w], context.data);
    }

    return context;
}

        private static Context_Compress decrementEnlargeIn(Context_Compress context)
{

    context.enlargeIn--;
    if (context.enlargeIn == 0) {
        context.enlargeIn = (int)Math.Pow(2, context.numBits);
        context.numBits++;
    }
    return context;
}

        public static string compress(string uncompressed)
{

    Context_Compress context = new Context_Compress();
    Context_Compress_Data data = new Context_Compress_Data();

    context.dictionary = new Dictionary<string, int>();
    context.dictionaryToCreate = new Dictionary<string, bool>();
    context.c = "";
    context.wc = "";
    context.w = "";
    context.enlargeIn = 2;
    context.dictSize = 3;
    context.numBits = 2;

    data.str = "";
    data.val = 0;
    data.position = 0;

    context.data = data;

    try {
        for (int i = 0; i < uncompressed.Length; i++)
        {
            context.c = uncompressed[i].ToString();

            if (!context.dictionary.ContainsKey(context.c)) {
                context.dictionary[context.c] = context.dictSize++;
                context.dictionaryToCreate[context.c] = true;
            };

            context.wc = context.w + context.c;

            if (context.dictionary.ContainsKey(context.wc)) {
                context.w = context.wc;
            }
            else {
                context = produceW(context);
                context = decrementEnlargeIn(context);
                context.dictionary[context.wc] = context.dictSize++;
                context.w = context.c;
            }
        }

        if (context.w != "") {
            context = produceW(context);
        }

        // Mark the end of the stream
        context.data = writeBits(context.numBits, 2, context.data);

        // Flush the last char
        while (true) {
            context.data.val = (context.data.val << 1);
            if (context.data.position == 15) {
                context.data.str += (char)context.data.val;
                break;
            }
            else
                context.data.position++;
        }

    }
    catch (Exception ex)
    {
        throw ex;
    }

    return context.data.str;
}

        private static int readBit(Decompress_Data data)
{

    var res = data.val & data.position;

    data.position >>= 1;

    if (data.position == 0) {
        data.position = 32768;

        // This 'if' check doesn't appear in the orginal lz-string javascript code.
        // Added as a check to make sure we don't exceed the length of data.str
        // The javascript charCodeAt will return a NaN if it exceeds the index but will not error out
        if (data.index < data.str.Length) {
            data.val = data.str[data.index++]; // data.val = data.string.charCodeAt(data.index++); <---javascript equivilant
        }
    }

    return res > 0 ? 1 : 0;
}

        private static int readBits(int numBits, Decompress_Data data)
{

    int res = 0;
    int maxpower = (int)Math.Pow(2, numBits);
    int power = 1;

    while (power != maxpower) {
        res |= readBit(data) * power;
        power <<= 1;
    }

    return res;
}

        public static string decompress(string compressed)
{

    Decompress_Data data = new Decompress_Data();

    List < string > dictionary = new List<string>();
    int next = 0;
    int enlargeIn = 4;
    int numBits = 3;
    string entry = "";
    StringBuilder result = new StringBuilder();
    int i = 0;
    string w = "";
    string sc = "";
    int c = 0;
    int errorCount = 0;

    data.str = compressed;
    data.val = (int)compressed[0];
    data.position = 32768;
    data.index = 1;

    try {
        for (i = 0; i < 3; i++) {
            dictionary.Add(i.ToString());
        }

        next = readBits(2, data);

        switch (next) {
            case 0:
                sc = Convert.ToChar(readBits(8, data)).ToString();
                break;
            case 1:
                sc = Convert.ToChar(readBits(16, data)).ToString();
                break;
            case 2:
                return "";
        }

        dictionary.Add(sc);

        result.Append(sc);
        w = result.ToString();

        while (true) {
            c = readBits(numBits, data);
            int cc = c;

            switch (cc) {
                case 0:
                    if (errorCount++ > 10000)
                        throw new Exception("To many errors");

                    sc = Convert.ToChar(readBits(8, data)).ToString();
                    dictionary.Add(sc);
                    c = dictionary.Count - 1;
                    enlargeIn--;

                    break;
                case 1:
                    sc = Convert.ToChar(readBits(16, data)).ToString();
                    dictionary.Add(sc);
                    c = dictionary.Count - 1;
                    enlargeIn--;

                    break;
                case 2:
                    return result.ToString();
            }

            if (enlargeIn == 0) {
                enlargeIn = (int)Math.Pow(2, numBits);
                numBits++;
            }

            if (dictionary.Count - 1 >= c) // if (dictionary[c] ) <------- original Javascript Equivalant
            {
                entry = dictionary[c];
            }
            else {
                if (c == dictionary.Count) {
                    entry = w + w[0];
                }
                else {
                    return null;
                }
            }

            result.Append(entry);
            dictionary.Add(w + entry[0]);
            enlargeIn--;
            w = entry;

            if (enlargeIn == 0) {
                enlargeIn = (int)Math.Pow(2, numBits);
                numBits++;
            }
        }
    }
    catch (Exception ex)
    {
        throw ex;
    }
}

        public static string compressToUTF16(string input)
{

    string output = "";
    int status = 0;
    int current = 0;

    try {
        if (input == null)
            throw new Exception("Input is Null");

        input = compress(input);
        if (input.Length == 0)
            return input;

        for (int i = 0; i < input.Length; i++)
        {
            int c = (int)input[i];
            switch (status++) {
                case 0:
                    output += (char)((c >> 1) + 32);
                    current = (c & 1) << 14;
                    break;
                case 1:
                    output += (char)((current + (c >> 2)) + 32);
                    current = (c & 3) << 13;
                    break;
                case 2:
                    output += (char)((current + (c >> 3)) + 32);
                    current = (c & 7) << 12;
                    break;
                case 3:
                    output += (char)((current + (c >> 4)) + 32);
                    current = (c & 15) << 11;
                    break;
                case 4:
                    output += (char)((current + (c >> 5)) + 32);
                    current = (c & 31) << 10;
                    break;
                case 5:
                    output += (char)((current + (c >> 6)) + 32);
                    current = (c & 63) << 9;
                    break;
                case 6:
                    output += (char)((current + (c >> 7)) + 32);
                    current = (c & 127) << 8;
                    break;
                case 7:
                    output += (char)((current + (c >> 8)) + 32);
                    current = (c & 255) << 7;
                    break;
                case 8:
                    output += (char)((current + (c >> 9)) + 32);
                    current = (c & 511) << 6;
                    break;
                case 9:
                    output += (char)((current + (c >> 10)) + 32);
                    current = (c & 1023) << 5;
                    break;
                case 10:
                    output += (char)((current + (c >> 11)) + 32);
                    current = (c & 2047) << 4;
                    break;
                case 11:
                    output += (char)((current + (c >> 12)) + 32);
                    current = (c & 4095) << 3;
                    break;
                case 12:
                    output += (char)((current + (c >> 13)) + 32);
                    current = (c & 8191) << 2;
                    break;
                case 13:
                    output += (char)((current + (c >> 14)) + 32);
                    current = (c & 16383) << 1;
                    break;
                case 14:
                    output += (char)((current + (c >> 15)) + 32);
                    output += (char)((c & 32767) + 32);
                    status = 0;
                    break;
            }
        }
    }
    catch (Exception ex)
    {
        throw ex;
    }

    return output + (char)(current + 32);
}

        public static string decompressFromUTF16(string input)
{

    string output = "";
    int status = 0;
    int current = 0;
    int i = 0;

    try {
        if (input == null)
            throw new Exception("input is Null");

        while (i < input.Length) {
            int c = ((int)input[i]) - 32;

            switch (status++) {
                case 0:
                    current = c << 1;
                    break;
                case 1:
                    output += (char)(current | (c >> 14));
                    current = (c & 16383) << 2;
                    break;
                case 2:
                    output += (char)(current | (c >> 13));
                    current = (c & 8191) << 3;
                    break;
                case 3:
                    output += (char)(current | (c >> 12));
                    current = (c & 4095) << 4;
                    break;
                case 4:
                    output += (char)(current | (c >> 11));
                    current = (c & 2047) << 5;
                    break;
                case 5:
                    output += (char)(current | (c >> 10));
                    current = (c & 1023) << 6;
                    break;
                case 6:
                    output += (char)(current | (c >> 9));
                    current = (c & 511) << 7;
                    break;
                case 7:
                    output += (char)(current | (c >> 8));
                    current = (c & 255) << 8;
                    break;
                case 8:
                    output += (char)(current | (c >> 7));
                    current = (c & 127) << 9;
                    break;
                case 9:
                    output += (char)(current | (c >> 6));
                    current = (c & 63) << 10;
                    break;
                case 10:
                    output += (char)(current | (c >> 5));
                    current = (c & 31) << 11;
                    break;
                case 11:
                    output += (char)(current | (c >> 4));
                    current = (c & 15) << 12;
                    break;
                case 12:
                    output += (char)(current | (c >> 3));
                    current = (c & 7) << 13;
                    break;
                case 13:
                    output += (char)(current | (c >> 2));
                    current = (c & 3) << 14;
                    break;
                case 14:
                    output += (char)(current | (c >> 1));
                    current = (c & 1) << 15;
                    break;
                case 15:
                    output += (char)(current | c);
                    status = 0;
                    break;
            }

            i++;
        }
    }
    catch (Exception ex)
    {
        throw ex;
    }

    return decompress(output);
}

        public static string compressToBase64(string input)
{

    string _keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    string output = "";

    // Using the data type 'double' for these so that the .Net double.NaN & double.IsNaN functions can be used
    // later in the function. .Net doesn't have a similar function for regular integers.
    double chr1, chr2, chr3 = 0.0;

    int enc1 = 0;
    int enc2 = 0;
    int enc3 = 0;
    int enc4 = 0;
    int i = 0;

    try {
        if (input == null)
            throw new Exception("input is Null");

        input = compress(input);

        while (i < input.Length * 2) {
            if (i % 2 == 0) {
                chr1 = (int)input[i / 2] >> 8;
                chr2 = (int)input[i / 2] & 255;
                if (i / 2 + 1 < input.Length)
                    chr3 = (int)input[i / 2 + 1] >> 8;
                        else
                chr3 = double.NaN;//chr3 = NaN; <------ original Javascript Equivalent
            }
            else {
                chr1 = (int)input[(i - 1) / 2] & 255;
                if ((i + 1) / 2 < input.Length) {
                    chr2 = (int)input[(i + 1) / 2] >> 8;
                    chr3 = (int)input[(i + 1) / 2] & 255;
                }
                else {
                    chr2 = chr3 = double.NaN; // chr2 = chr3 = NaN; <------ original Javascript Equivalent
                }
            }
            i += 3;


            enc1 = (int)(Math.Round(chr1)) >> 2;

            // The next three 'if' statements are there to make sure we are not trying to calculate a value that has been
            // assigned to 'double.NaN' above. The orginal Javascript functions didn't need these checks due to how
            // Javascript functions.
            // Also, due to the fact that some of the variables are of the data type 'double', we have to do some type
            // conversion to get the 'enc' variables to be the correct value.
            if (!double.IsNaN(chr2)) {
                enc2 = (((int)(Math.Round(chr1)) & 3) << 4) | ((int)(Math.Round(chr2)) >> 4);
            }

            if (!double.IsNaN(chr2) && !double.IsNaN(chr3)) {
                enc3 = (((int)(Math.Round(chr2)) & 15) << 2) | ((int)(Math.Round(chr3)) >> 6);
            }
            // added per issue #3 logged by ReuvenT
            else {
                enc3 = 0;
            }

            if (!double.IsNaN(chr3)) {

                enc4 = (int)(Math.Round(chr3)) & 63;
            }

            if (double.IsNaN(chr2)) //if (isNaN(chr2)) <------ original Javascript Equivalent
            {
                enc3 = enc4 = 64;
            }
            else if (double.IsNaN(chr3)) //else if (isNaN(chr3)) <------ original Javascript Equivalent
            {
                enc4 = 64;
            }

            output = output + _keyStr[enc1] + _keyStr[enc2] + _keyStr[enc3] + _keyStr[enc4];
        }
    }
    catch (Exception ex)
    {
        throw ex;
    }

    return output;
}

        public static string decompressFromBase64(string input)
{

    string _keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

    string output = "";
    int output_ = 0;
    int ol = 0;
    int chr1, chr2, chr3 = 0;
    int enc1, enc2, enc3, enc4 = 0;
    int i = 0;

    try {
        if (input == null)
            throw new Exception("input is Null");

        var regex = new Regex(@"[^A-Za-z0-9-\+\/\=]");
        input = regex.Replace(input, "");

        while (i < input.Length) {
            enc1 = _keyStr.IndexOf(input[i++]);
            enc2 = _keyStr.IndexOf(input[i++]);
            enc3 = _keyStr.IndexOf(input[i++]);
            enc4 = _keyStr.IndexOf(input[i++]);

            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;

            if (ol % 2 == 0) {
                output_ = chr1 << 8;

                if (enc3 != 64) {
                    output += (char)(output_ | chr2);
                }

                if (enc4 != 64) {
                    output_ = chr3 << 8;
                }
            }
            else {
                output = output + (char)(output_ | chr1);

                if (enc3 != 64) {
                    output_ = chr2 << 8;
                }
                if (enc4 != 64) {
                    output += (char)(output_ | chr3);
                }
            }
            ol += 3;
        }

        // Send the output out to the main decompress function
        output = decompress(output);
    }
    catch (Exception ex)
    {
        throw ex;
    }

    return output;
}

    }
}
***** GZIP

namespace XYZ.CustomAttributes {
    /// <summary>
    /// Attribute that can be added to controller methods to force content
    /// to be GZip encoded if the client supports it
    /// </summary>
    public class CompressContentAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Override to compress the content that is generated by
        /// an action method.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            GZipEncodePage();
        }

        /// <summary>
        /// Determines if GZip is supported
        /// </summary>
        /// <returns></returns>
        public static bool IsGZipSupported()
        {
            string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
            if (!string.IsNullOrEmpty(AcceptEncoding) &&
                (AcceptEncoding.Contains("gzip") || AcceptEncoding.Contains("deflate")))
                return true;
            return false;
        }

        /// <summary>
        /// Sets up the current page or handler to use GZip through a Response.Filter
        /// IMPORTANT:  
        /// You have to call this method before any output is generated!
        /// </summary>
        public static void GZipEncodePage()
        {
            HttpResponse Response = HttpContext.Current.Response;

            if (IsGZipSupported()) {
                string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];

                if (AcceptEncoding.Contains("gzip")) {
                    Response.Filter = new System.IO.Compression.GZipStream(Response.Filter,
                        System.IO.Compression.CompressionMode.Compress);
                    Response.Headers.Remove("Content-Encoding");
                    Response.AppendHeader("Content-Encoding", "gzip");
                }
                else {
                    Response.Filter = new System.IO.Compression.DeflateStream(Response.Filter,
                        System.IO.Compression.CompressionMode.Compress);
                    Response.Headers.Remove("Content-Encoding");
                    Response.AppendHeader("Content-Encoding", "deflate");
                }

            }

            // Allow proxy servers to cache encoded and unencoded versions separately
            Response.AppendHeader("Vary", "Content-Encoding");
        }
    }
}
