using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSConverter.Data.BMS
{
    public class Note
    {
        double position;
        AudioSample sample;
        Track track;

        public Note(AudioSample pAudioSample, double pPosition, Track pTrack)
        {
            sample = pAudioSample;
            position = pPosition;
            track = pTrack;
        }

        public double Position
        {
            get
            {
                return position;
            }
        }

        public AudioSample Sample
        {
            get
            {
                return sample;
            }
        }

        public Track Track
        {
            get
            {
                return track;
            }
        }
    }
}
