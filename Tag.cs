#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static MP3Reader.FrameProps;
using static System.Console;


namespace MP3Reader
{
    /*
     *  Tag consists of header, frames and optional padding 
     *  Header:     Field                       Description
     *              ID3v2/file identifier       "ID3"
     *              ID3v2 version               $03 00
     *              ID3v2 flags                 %abc00000
     *              ID3v2 size                  4 * %0xxxxxxx
     */
    public partial class Tag
    {
        static string           ID3_IDENTIFIER              = "ID3";

        static string           DEFAULT_TAG_HEADER          = "[TAG_HEADER]";
        static (int, int)       DEFAULT_TAG_VERSION         = (3, 0);
        static byte             DEFAULT_TAG_FLAGS           = 0b00000000;
        static int              DEFAULT_TAG_SIZE            = 11;


        // _header[3]: "ID3"
        private string              _header;
        // _version[2]: $03 00
        private (int main, int sub) _version;
        // _flags[1]: %abc00000
        private byte                _flags;
        // _size[4]: 4 * %0xxxxxxx , stored as 32 synch safe integer
        private int                 _size;
        // collection of frames
        private List<TagFrame>      _frames;
        // flag to check whether tag header is read or not
        private bool                _read;

        public string           ID                  { get { return _header; } set { _header = value; } }
        public (int, int)       Version             { get { return (_version.main, _version.sub); } set { _version = value; } }
        public byte             Flags               { get { return _flags; } }
        public bool             IsUnsynchronised    { get { return Convert.ToBoolean(_flags & 0b10000000); } set { _flags |= 0b10000000; } }
        public bool             HasExtendedHeader   { get { return Convert.ToBoolean(_flags & 0b01000000); } set { _flags |= 0b01000000; } }
        public bool             IsExperimental      { get { return Convert.ToBoolean(_flags & 0b00100000); } set { _flags |= 0b00100000; } }
        public bool             IsRead              { get { return _read; } set { _read = value; } }

        public int              Size    { get { return _size; } set { _size = value; } }
        public List<TagFrame>   Frames  { get { return _frames; } }

        public Tag()
        {
            ID      = DEFAULT_TAG_HEADER;
            Version = DEFAULT_TAG_VERSION;
            _flags  = DEFAULT_TAG_FLAGS;
            Size    = DEFAULT_TAG_SIZE;
            _frames = new List<TagFrame>();
            _read   = false;
        }

        public Tag(string header, (int m, int s) version, byte flags, int size)
        {
            _header   = header;
            _version  = version;
            _flags    = flags;
            _size     = size;
            _frames   = new List<TagFrame>();
            _read     = false;
        }

        public bool ReadHeader(BinaryReader reader)
        {
            if (_read)
            {
                WriteLine("TagHeader has already been read");
                return false;
            }

            string ID3 = Encoding.Default.GetString(reader.ReadBytes(3));

            if (ID3 == null || ID3 != ID3_IDENTIFIER)
            {
                return false;
            }

            int?    version     = Convert.ToInt32(reader.ReadByte());
            int?    subversion  = Convert.ToInt32(reader.ReadByte());
            byte?   flags       = reader.ReadByte();
            int?    tagsize     = DecodeSynchSafeInt32(reader.ReadBytes(4));

            if (version == null || subversion == null || flags == null || tagsize == null)
            {
                WriteLine($"Couldn't read parameters.");
                return false;
            }

            ID      = ID3;
            Version = ((int)version, (int)subversion);
            _flags  = (byte)flags;
            Size    = (int)tagsize;


            WriteLine("ID3 = {0}\n" +
                "Version - {1}\n" +
                "Subversion - {2}\n" +
                "flags - {3}\n" +
                "TagSize - {4}",
                ID3, version, subversion, Convert.ToString((byte)flags, 2).PadLeft(8, '0'), tagsize);


            IsRead = true;

            return true;
        }

        public TagFrame? ReadFrame(BinaryReader reader)
        {
            if (!IsRead)
            {
                WriteLine("TagHeader has not been read yet");
                return null;
            }

            TagFrame frame = new TagFrame();
            if (!frame.Read(reader))
            {
                return null;
            }
            
            return frame;
        }

        public bool ReadAllFrames(BinaryReader reader)
        {
            TagFrame? newFrame = null;
            int i = 10;

            while (i < Size && (newFrame = ReadFrame(reader)) != null)
            {
                Frames.Add(newFrame);
                i += newFrame.Size + TagFrame.DEFAULT_FRAME_SIZE;
            }


            return true;
        }
     
        public TagFrame? GetFrame(string name)
        {
            int index = Frames.FindIndex(fr => fr.Name == name);
            if (index == -1)
            {
                return null;
            }

            return Frames[index];
        }

        public bool ChangeFrame(TagFrame frame)
        {
            int index = Frames.FindIndex(fr => fr.Name == frame.Name);
            if (index != -1)
            {
                int delta = Frames[index].Size - frame.Size;
                Frames.RemoveAt(index);
                Frames.Add(frame);
                Size += delta;

                return true;
            }

            return false;
        }

        public bool AddFrame(TagFrame frame)
        {
            if (!ChangeFrame(frame))
            {
                Frames.Add(frame);
                return true;
            }

            return false;
        }

        public void PrintAllFrames()
        {
            if (Frames.Count != 0)
            {
                foreach (TagFrame frame in Frames)
                {
                    WriteLine(frame);
                }
            } else
            {
                WriteLine("No frames");
            }
        }


    }

}
