using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using log4net;

namespace BMSConverter.Data.BMS
{
    public class BMSFileParser
    {
        public static BMSSong ScanFile(string pFile)
        {
            ILog log = LogManager.GetLogger(typeof(BMSFileParser));
            
            BMSSong song = new BMSSong();

            StreamReader reader = null;
            try
            {
                reader = new StreamReader(File.Open(pFile, FileMode.Open));
                log.Debug(string.Format("Opened File {0} for reading"));

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.Length > 0 && line[0] == '#')
                    {
                        string cmd = GetCommand(line);
                        string param = GetParam(line);
                        switch (cmd)
                        {
                            case "BPM":
                                song.SetBPM(int.Parse(param));
                                log.Debug(string.Format("Set BPM to {0}", int.Parse(param)));
                                break;
                            case "GENRE":
                                song.Genre = param;
                                log.Debug(string.Format("Set Genre to \"{0}\"", param));
                                break;
                            case "TITLE":
                                song.Title = param;
                                log.Debug(string.Format("Set Title to \"{0}\"", param));
                                break;
                            case "ARTIST":
                                song.Artist = param;
                                log.Debug(string.Format("Set Artist to \"{0}\"", param));
                                break;
                            case "WAV":
                                byte id = GetExCmd(line);
                                AudioSample audioSample = new AudioSample(param, id);
                                song.SetAudioSample(audioSample);
                                log.Debug(string.Format("Set WAV{0} to {1}", id.ToString("X2"), param));
                                break;
                            case "BMP":
                                id = GetExCmd(line);
                                BackgroundFile bgFile = new BackgroundFile(param, id);
                                song.SetBackgroundFile(bgFile);
                                log.Debug(string.Format("Set BPM{0} to {1}", id.ToString("X2"), param));
                                break;
                            default:
                                if (Regex.IsMatch(cmd, "^[0-9][0-9][0-9][0-F][0-F]$"))
                                {
                                    int track = int.Parse(cmd.Substring(0, 3));
                                    byte channel = byte.Parse(cmd.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);

                                    switch (channel)
                                    {
                                        case 0x11:
                                        case 0x12:
                                        case 0x13:
                                        case 0x14:
                                        case 0x15:
                                        case 0x16:
                                        case 0x17:
                                        case 0x21:
                                        case 0x22:
                                        case 0x23:
                                        case 0x24:
                                        case 0x25:
                                        case 0x26:
                                        case 0x27:
                                        case 0x01:
                                            byte[] ids = new byte[param.Length / 2];
                                            for (int i = 0; i < ids.Length; i++)
                                            {
                                                ids[i] = byte.Parse(param.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                                            }
                                            song.AddNotes(ids, track);
                                            log.Debug(string.Format("Added {0} Note(s) to Track {1}", ids.Length, track));
                                            break;

                                        case 0x03:
                                            int bpm = int.Parse(param);
                                            song.SetBPM(bpm);
                                            log.Debug(string.Format("Set BPM to {0}", bpm));
                                            break;

                                        default:
                                            log.Warn(string.Format("{0} is an unknown channel", channel.ToString("X2")));
                                            break;
                                    }
                                }
                                else
                                {
                                    log.Warn(string.Format("{0} is an unknown command", cmd));
                                }
                                break;
                        }
                    }
                }

                reader.Close();
            }
            catch (Exception e)
            {
                log.Error("Error while parsing file", e);
                if (reader != null)
                {
                    reader.Close();
                }
                throw;
            }

            return song;
        }

        static string GetCommand(string line)
        {
            int index = line.IndexOf(' ');
            int ind2 = line.IndexOf(':');
            if (ind2 > index)
            {
                index = ind2;
            }

            string cmd = line.Substring(1, index).ToUpper();
            if (cmd.StartsWith("WAV") || cmd.StartsWith("BMP"))
            {
                cmd = cmd.Substring(0, 3);
            }
            return cmd;
        }

        static byte GetExCmd(string line)
        {
            int index = line.IndexOf(' ');
            int ind2 = line.IndexOf(':');
            if (ind2 > index)
            {
                index = ind2;
            }

            string cmd = line.Substring(1, index).ToUpper();
            if (cmd.StartsWith("WAV") || cmd.StartsWith("BMP"))
            {
                cmd = cmd.Substring(3, 2);
            }
            return byte.Parse(cmd, System.Globalization.NumberStyles.HexNumber);
        }

        static string GetParam(string line)
        {
            int index = line.IndexOf(' ');
            int ind2 = line.IndexOf(':');
            if (ind2 > index)
            {
                index = ind2;
            }

            return line.Substring(index + 1);
        }
    }
}
