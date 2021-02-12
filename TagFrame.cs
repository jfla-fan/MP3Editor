using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using static MP3Reader.FrameProps;
using static System.Console;



namespace MP3Reader
{
    public class TagFrame
    {
        public static int          FRAME_MIN_SIZE      = 10;                     // 1 byte
        public static int          DEFAULT_FRAME_SIZE  = 10;                 // 1 byte
        public static string       DEFAULT_FRAME_NAME  = "NSU_";            // not set up name
        public static string       DEFAULT_FRAME_VALUE = "NSU_";           // not set up value
        public static byte[]       DEFAULT_FRAME_FLAGS = { 0b00000000, 0b00000000 };
        public static FrameType    DEFAULT_FRAME_TYPE  = FrameType.NONE;    // not defined

        public static void PrintByte(byte b)
        {
            WriteLine(Convert.ToString(b, 2).PadLeft(8, '0'));
        }

        public static void PrintBytes(byte[] b)
        {
            foreach(byte _b in b)
            {
                PrintByte(_b);
            }
        }

        private string      _name;
        private int         _size;
        private byte[]      _flags;
        private string      _value;
        private FrameType   _type;

        public string       Name { get { return _name; } set { _name = value; } }
        public int          Size { get { return _size; } set { _size = value; } }
        public byte[]       Flags { get { return _flags; } set { _flags = value; } }
        public string       Value { get { return _value; } set { _value = value; } }
        public FrameType    Type { get { return _type; } set { _type = value; } }

        public TagFrame()
        {
            _name   = DEFAULT_FRAME_NAME;
            _size   = DEFAULT_FRAME_SIZE;
            _value  = DEFAULT_FRAME_VALUE;
            _type   = DEFAULT_FRAME_TYPE;
            _flags  = DEFAULT_FRAME_FLAGS;
        }

        public TagFrame(string name, int size, string value)
        {
            this._name  = name;
            this._size  = size;
            this._value = value;
            this._type  = GetTypeOnString(name);
            this._flags = DEFAULT_FRAME_FLAGS;
        }


        public TagFrame(string name, int size, string value, FrameType type, byte[] flags)
            : this(name, size, value)
        {
            this._type  = type;
            this._flags = flags;
        }

        public TagFrame(string name, int size, string value, FrameType type)
            : this(name, size, value)
        {
            this._type = type;
        }

        public override string ToString()
        {
            return $"- - - - - - - - - - - - - -\n" +
                    $"Name - {Name}\nsize - {Size}\nflags1 - {Flags[0]}" +
                    $"\nflags2-{Flags[1]}\nvalue-{Value}\n" +
                    $"- - - - - - - - - - - - - -";
        }

        public bool Read(BinaryReader reader)
        {
            string name = Encoding.ASCII.GetString(reader.ReadBytes(4));

            if (name == null || GetTypeOnString(name) == FrameType.NONE)
            {
                WriteLine($"Wrong frame name, got  |{name}|");
                return false;
            }


            int size = DecodeSynchSafeInt32(reader.ReadBytes(4));

            byte flags1 = reader.ReadByte();

            byte flags2 = reader.ReadByte();

            string value = Encoding.Default.GetString(reader.ReadBytes(size));



            Name  = name;
            Size  = size;
            Flags = new byte[] { flags1, flags2 };
            Value = value;
            Type  = GetTypeOnString(name);



            return true;
        }


        public static void WriteFrame(BinaryWriter writer, TagFrame frame)
        {
            writer.Write(frame.Name.ToCharArray());
            writer.Write(EncodeToSynchSafeInt32(frame.Size));
            writer.Write(frame.Flags);
            writer.Write(Encoding.ASCII.GetBytes(frame.Value.ToCharArray()));
            

            WriteLine();
        }
    }
}
