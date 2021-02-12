using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static MP3Reader.FrameProps;


namespace MP3Reader
{
    /// <summary>
    /// TAG HEADER STRUCTURE
    ///      Field                          Description
    ///      * ID3v2/file identifier       "ID3"
    ///      * ID3v2 version               $03 00
    ///      * ID3v2 flags                 %abc00000
    ///      * ID3v2 size                  4 * %0xxxxxxx
    /// </summary>

    public partial class Tag
    {
        public static void WriteHeader(BinaryWriter writer, Tag tag)
        {
            writer.Write(tag.ID.ToCharArray());
            writer.Write(Convert.ToByte(tag.Version.Item1));
            writer.Write(Convert.ToByte(tag.Version.Item2));
            writer.Write(tag.Flags);
            writer.Write(EncodeToSynchSafeInt32(tag.Size));

        }

        public static bool WriteTag(BinaryWriter writer, Tag tag)
        {
            WriteHeader(writer, tag);

            foreach (TagFrame frame in tag.Frames)
            {
                TagFrame.WriteFrame(writer, frame);
            }
            
            return true;
        }
    }
}
