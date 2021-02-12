#nullable enable
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings;
using CommandLine;
using static MP3Reader.FrameProps;
using static System.Console;


namespace MP3Reader
{



    class Program
    {
        static void Main(string[] args)
        {
            MP3? mp3file = null;

            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(o =>
                {
                    mp3file = new MP3(o.Filepath.Substring(o.Filepath.IndexOf("=") + 1));
                    mp3file.ReadTag();
                    mp3file.ReadData();

                    if (o.Get != null)
                    {
                        string frameName = o.Get.Substring(o.Get.IndexOf("=") + 1);
                        TagFrame? frame = mp3file.tag.GetFrame(frameName);
                        if (frame == null)
                        {
                            WriteLine($"Couldn't find frame {frameName}");
                        }
                        else
                        {
                            WriteLine(frame);
                        }
                    }

                    if (o.Show)
                    {
                        mp3file.tag.PrintAllFrames();
                    }

                    if (o.Set != null && o.Value != null)
                    {
                        string frameName = o.Set.Substring(o.Set.IndexOf("=") + 1);
                        string newValue = o.Value.Substring(o.Value.IndexOf("=") + 1);
                        TagFrame frame = new TagFrame(frameName, newValue.Length, newValue);
                        if (!mp3file.tag.AddFrame(frame))
                        {
                            WriteLine("Frame has been changed");
                        }
                        else
                        {
                            WriteLine("Frame has been added");
                        }
                        mp3file.WriteTag();
                    }


                });

            ReadLine();

        }
    }
}
