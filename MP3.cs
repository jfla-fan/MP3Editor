using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Console;


namespace MP3Reader
{
    public class MP3
    {
        // mp3 metadata
        private Tag         _tag;
        // other data
        private byte[]      _data;
        // file_stream associated with this instance
        private FileStream  _stream;
        
        public Tag          tag     { get { return _tag; } }
        public byte[]       Data    { get { return _data; } }
        public FileStream   Stream  { get { return _stream; } }


        public MP3(FileStream filestream)
        {
            _stream = filestream;
            _tag    = new Tag();
        }

        public MP3(string path)
        {
            _stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite);
            _tag    = new Tag();
        }


        public bool ReadTag()
        {
            if (!tag.IsRead)
            {
                BinaryReader reader = new BinaryReader(Stream);

                return tag.ReadHeader(reader)
                    && tag.ReadAllFrames(reader);
            }

            return false;
        }

        public int ReadData()
        {
            if (!tag.IsRead)
            {
                WriteLine("Tag is not read yet");
                return -1;
            }

            long dataLength = _stream.Length;

            _data = new byte[dataLength];

            return Stream.Read(_data);
        }

        public bool WriteTag()
        {
            _stream.Seek(0, SeekOrigin.Begin);
            
            BinaryWriter streamWriter = new BinaryWriter(_stream);

            if (!Tag.WriteTag(streamWriter, tag))
            {
                WriteLine("Couldn't write tag");
                return false;
            }

            streamWriter.Write(_data);

            return true;
        }


    }
}
