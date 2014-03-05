using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Mix;
using Un4seen.Bass.AddOn.Enc;
using BMSConverter.Data.BMS;

namespace BMSConverter.Data.Audio
{
    public class AudioEncoder
    {
        public int CreateBassMixer(BMSSong pSong)
        {
            int mixer = BassMix.BASS_Mixer_StreamCreate(44100, 2, BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_MIXER_END);
            Dictionary<AudioSample, int> samples = PreloadSamples(pSong.AudioSamples);
            List<Track> tracks = pSong.Tracks;

            for (int i = 0; i < tracks.Count; i++)
            {
                MixTrack(mixer, tracks[i], samples);
            }

            return mixer;
        }

        Dictionary<AudioSample, int> PreloadSamples(Dictionary<byte, AudioSample> pAudioSamples)
        {
            Dictionary<AudioSample, int> ret = new Dictionary<AudioSample, int>();
            foreach (AudioSample a in pAudioSamples.Values)
            {
                ret[a] = Bass.BASS_StreamCreateFile(a.File, 0, 0, BASSFlag.BASS_STREAM_DECODE);
            }
            return ret;
        }

        void MixTrack(int pMixer, Track pTrack, Dictionary<AudioSample, int> pSamples)
        {
            foreach (Note n in pTrack.Notes)
            {
                //TODO: Maybe use another length instead of 0
                long start = Bass.BASS_ChannelSeconds2Bytes(pMixer, PositionOfNote(n));
                BassMix.BASS_Mixer_StreamAddChannelEx(pMixer, pSamples[n.Sample], 0, start, 0);
            }
        }

        double LengthOfTrack(Track pTrack)
        {
            int bpm = pTrack.BPM;
            double l = (60d / (double)bpm) * 4d;
            return l;
        }

        double PositionOfTrack(Track pTrack)
        {
            double ret = 0;
            for (int i = 0; i < pTrack.Number; i++)
            {
                ret += LengthOfTrack(pTrack.Song.Tracks[i]);
            }
            return ret;
        }

        double PositionOfNote(Note pNote)
        {
            double l = LengthOfTrack(pNote.Track);
            double pos = PositionOfTrack(pNote.Track);
            return pos + (l * pNote.Position);
        }

        public void EncodeMP3(int pMixer)
        {
            
        }
    }
}
