using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSConverter.Data.BMS
{
    public class Track
    {
        List<Note> notes;
        int bpm;
        int number;
        BMSSong song;

        public Track(int pBPM, int pNumber, BMSSong pSong)
        {
            bpm = pBPM;
            notes = new List<Note>();
            song = pSong;
            number = pNumber;
        }

        public int BPM
        {
            get
            {
                return bpm;
            }
        }

        public int Number
        {
            get
            {
                return number;
            }
        }

        public void AddNote(Note note)
        {
            notes.Add(note);
        }

        public List<Note> Notes
        {
            get
            {
                return notes;
            }
        }

        public BMSSong Song
        {
            get
            {
                return song;
            }
        }
    }
}
