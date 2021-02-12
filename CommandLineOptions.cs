using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;
using static System.Console;


namespace MP3Reader
{
    public class CommandLineOptions
    {
        [Option('f', "filepath", Required = true, HelpText = "File path is necessary")]
        public string    Filepath { get; set; }
        
        [Option("show", Required = false, HelpText = "Show flag is used to show all the flags")]
        public bool      Show    { get; set; }  
        
        [Option("get", Required = false, HelpText = "Get flag is used to get specific frame")]
        public string?   Get     { get; set; }

        [Option("set", Required = false, HelpText = "set flag")]
        public string?   Set     { get; set; }

        [Option("value", Required = false, HelpText = "Value to be set to set flag")]
        public string?   Value   { get; set; }
    }
}
