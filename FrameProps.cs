using System;
using System.Collections.Generic;
using System.Text;

namespace MP3Reader
{
    public static class FrameProps
    {
        public enum FrameType
        { 

            NONE,
            AENC,            //[[#sec4.20|Audio encryption]]
            APIC,            //[#sec4.15 Attached picture]
            COMM,            //[#sec4.11 Comments]
            COMR,            //[#sec4.25 Commercial frame]
            ENCR,            //[#sec4.26 Encryption method registration]
            EQUA,            //[#sec4.13 Equalization]
            ETCO,            //[#sec4.6 Event timing codes]
            GEOB,            //[#sec4.16 General encapsulated object]
            GRID,            //[#sec4.27 Group identification registration]
            IPLS,            //[#sec4.4 Involved people list]
            LINK,            //[#sec4.21 Linked information]
            MCDI,            //[#sec4.5 Music CD identifier]
            MLLT,            //[#sec4.7 MPEG location lookup table]
            OWNE,            //[#sec4.24 Ownership frame]
            PRIV,            //[#sec4.28 Private frame]
            PCNT,            //[#sec4.17 Play counter]
            POPM,            //[#sec4.18 Popularimeter]
            POSS,            //[#sec4.22 Position synchronisation frame]
            RBUF,            //[#sec4.19 Recommended buffer size]
            RVAD,            //[#sec4.12 Relative volume adjustment]
            RVRB,            //[#sec4.14 Reverb]
            SYLT,            //[#sec4.10 Synchronized lyric/text]
            SYTC,            //[#sec4.8 Synchronized tempo codes]
            TALB,            //[#TALB Album/Movie/Show title]
            TBPM,            //[#TBPM BPM (beats per minute)]
            TCOM,            //[#TCOM Composer]
            TCON,            //[#TCON Content type]
            TCOP,            //[#TCOP Copyright message]
            TDAT,            //[#TDAT Date]
            TDLY,            //[#TDLY Playlist delay]
            TENC,            //[#TENC Encoded by]
            TEXT,            //[#TEXT Lyricist/Text writer]
            TFLT,            //[#TFLT File type]
            TIME,            //[#TIME Time]
            TIT1,            //[#TIT1 Content group description]
            TIT2,            //[#TIT2 Title/songname/content description]
            TIT3,            //[#TIT3 Subtitle/Description refinement]
            TKEY,            //[#TKEY Initial key]
            TLAN,            //[#TLAN Language(s)]
            TLEN,            //[#TLEN Length]
            TMED,            //[#TMED Media type]
            TOAL,            //[#TOAL Original album/movie/show title]
            TOFN,            //[#TOFN Original filename]
            TOLY,            //[#TOLY Original lyricist(s)/text writer(s)]
            TOPE,            //[#TOPE Original artist(s)/performer(s)]
            TORY,            //[#TORY Original release year]
            TOWN,            //[#TOWN File owner/licensee]
            TPE1,            //[#TPE1 Lead performer(s)/Soloist(s)]
            TPE2,            //[#TPE2 Band/orchestra/accompaniment]
            TPE3,            //[#TPE3 Conductor/performer refinement]
            TPE4,            //[#TPE4 Interpreted, remixed, or otherwise modified by]
            TPOS,            //[#TPOS Part of a set]
            TPUB,            //[#TPUB Publisher]
            TRCK,            //[#TRCK Track number/Position in set]
            TRDA,            //[#TRDA Recording dates]
            TRSN,            //[#TRSN Internet radio station name]
            TRSO,            //[#TRSO Internet radio station owner]
            TSIZ,            //[#TSIZ Size]
            TSRC,            //[#TSRC ISRC (international standard recording code)]
            TSSE,            //[#TSEE Software/Hardware and settings used for encoding]
            TYER,            //[#TYER Year]
            TXXX,            //[#TXXX User defined text information frame]
            UFID,            //[#sec4.1 Unique file identifier]
            USER,            //[#sec4.23 Terms of use]
            USLT,            //[#sec4.9 Unsychronized lyric/text transcription]
            WCOM,            //[#WCOM Commercial information]
            WCOP,            //[#WCOP Copyright/Legal information]
            WOAF,            //[#WOAF Official audio file webpage]
            WOAR,            //[#WOAR Official artist/performer webpage]
            WOAS,            //[#WOAS Official audio source webpage]
            WORS,            //[#WORS Official internet radio station homepage]
            WPAY,            //[#WPAY Payment]
            WPUB,            //[#WPUB Publishers official webpage]
            WXXX,            //[#WXXX User defined URL link frame]
        }

        public static Dictionary<string, FrameType> NamesToTags =
            new Dictionary<string, FrameType>
            {
                { "NONE", FrameType.NONE },
                { "AENC", FrameType.AENC },
                { "APIC", FrameType.APIC },
                { "COMM", FrameType.COMM },
                { "COMR", FrameType.COMR },
                { "ENCR", FrameType.ENCR },
                { "EQUA", FrameType.EQUA },
                { "ETCO", FrameType.ETCO },
                { "GEOB", FrameType.GEOB },
                { "GRID", FrameType.GRID },
                { "IPLS", FrameType.IPLS },
                { "LINK", FrameType.LINK },
                { "MCDI", FrameType.MCDI },
                { "MLLT", FrameType.MLLT },
                { "OWNE", FrameType.OWNE },
                { "PRIV", FrameType.PRIV },
                { "PCNT", FrameType.PCNT },
                { "POPM", FrameType.POPM },
                { "POSS", FrameType.POSS },
                { "RBUF", FrameType.RBUF },
                { "RVAD", FrameType.RVAD },
                { "RVRB", FrameType.RVRB },
                { "SYLT", FrameType.SYLT },
                { "SYTC", FrameType.SYTC },
                { "TALB", FrameType.TALB },
                { "TBPM", FrameType.TBPM },
                { "TCOM", FrameType.TCOM },
                { "TCON", FrameType.TCON },
                { "TCOP", FrameType.TCOP },
                { "TDAT", FrameType.TDAT },
                { "TDLY", FrameType.TDLY },
                { "TENC", FrameType.TENC },
                { "TEXT", FrameType.TEXT },
                { "TFLT", FrameType.TFLT },
                { "TIME", FrameType.TIME },
                { "TIT1", FrameType.TIT1 },
                { "TIT2", FrameType.TIT2 },
                { "TIT3", FrameType.TIT3 },
                { "TKEY", FrameType.TKEY },
                { "TLAN", FrameType.TLAN },
                { "TLEN", FrameType.TLEN },
                { "TMED", FrameType.TMED },
                { "TOAL", FrameType.TOAL },
                { "TOFN", FrameType.TOFN },
                { "TOLY", FrameType.TOLY },
                { "TOPE", FrameType.TOPE },
                { "TORY", FrameType.TORY },
                { "TOWN", FrameType.TOWN },
                { "TPE1", FrameType.TPE1 },
                { "TPE2", FrameType.TPE2 },
                { "TPE3", FrameType.TPE3 },
                { "TPE4", FrameType.TPE4 },
                { "TPOS", FrameType.TPOS },
                { "TPUB", FrameType.TPUB },
                { "TRCK", FrameType.TRCK },
                { "TRDA", FrameType.TRDA },
                { "TRSN", FrameType.TRSN },
                { "TRSO", FrameType.TRSO },
                { "TSIZ", FrameType.TSIZ },
                { "TSRC", FrameType.TSRC },
                { "TSSE", FrameType.TSSE },
                { "TYER", FrameType.TYER },
                { "TXXX", FrameType.TXXX },
                { "UFID", FrameType.UFID },
                { "USER", FrameType.USER },
                { "USLT", FrameType.USLT },
                { "WCOM", FrameType.WCOM },
                { "WCOP", FrameType.WCOP },
                { "WOAF", FrameType.WOAF },
                { "WOAR", FrameType.WOAR },
                { "WOAS", FrameType.WOAS },
                { "WORS", FrameType.WORS },
                { "WPAY", FrameType.WPAY },
                { "WPUB", FrameType.WPUB },
                { "WXXX", FrameType.WXXX },

        };

        static public FrameType GetTypeOnString(string frametype)
        {
            if (!NamesToTags.ContainsKey(frametype)) return NamesToTags["NONE"];
            return NamesToTags[frametype];
        }

        // size of frame is stored as a big-endian synch-safe int32
        // this means the highest bit (7) is always set to 0.
        // Then, 255 (1111 1111) is stored as (0000 0001 0111 1111)
        // We have to omit the first bit and use the other 28
        static public int DecodeSynchSafeInt32(byte[] bytes)
        {
            return
                    bytes[0] * 0x200000 +   //2^21
                    bytes[1] * 0x4000 +     //2^14
                    bytes[2] * 0x80 +       //2^7
                    bytes[3];
        }

        static public byte[] EncodeToSynchSafeInt32(int value)
        {
            byte[] result = new byte[4];

            result[0] = (byte)((value & 0xFE00000) >> 21);
            result[1] = (byte)((value & 0x01FC000) >> 14);
            result[2] = (byte)((value & 0x0003F80) >> 7);
            result[3] = (byte)((value & 0x000007F));


            return result;
        }
    }

}