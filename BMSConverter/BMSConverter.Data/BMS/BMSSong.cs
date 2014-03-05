using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSConverter.Data.BMS
{
    public class BMSSong
    {
        List<Track> tracks;
        Dictionary<byte, AudioSample> audioSamples;
        Dictionary<byte, BackgroundFile> bgFiles;
        int bpm = 130;
        string genre;
        string title;
        string artist;

        public BMSSong()
        {
            tracks = new List<Track>();
            audioSamples = new Dictionary<byte, AudioSample>();
            bgFiles = new Dictionary<byte, BackgroundFile>();
        }

        public void SetAudioSample(AudioSample audioSample)
        {
            audioSamples[audioSample.Id] = audioSample;
        }

        public void SetBackgroundFile(BackgroundFile bgFile)
        {
            bgFiles[bgFile.Id] = bgFile;
        }

        public void AddNotes(byte[] ids, int track)
        {
            while (track >= tracks.Count)
            {
                tracks.Add(new Track(bpm, tracks.Count, this));
            }

            double space = 1d / (double)ids.Length;
            for (int i = 0; i < ids.Length; i++)
            {
                if (ids[i] != 0)
                {
                    Note note = new Note(audioSamples[ids[i]], space * i, tracks[track]);
                    tracks[track].AddNote(note);
                }
            }
        }

        public void SetBPM(int pBPM)
        {
            bpm = pBPM;
        }

        public string Artist
        {
            get
            {
                return artist;
            }
            set
            {
                artist = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public string Genre
        {
            get
            {
                return genre;
            }
            set
            {
                genre = value;
            }
        }

        public Dictionary<byte, AudioSample> AudioSamples
        {
            get
            {
                return audioSamples;
            }
        }

        public List<Track> Tracks
        {
            get
            {
                return tracks;
            }
        }
    }
}
