using System.Collections.Generic;

namespace Tinder_DZ
{
    public struct UserPreferences
    {
        
        public (int, int) Age;
        public  (int, int) Height;
        public  (int, int) Weight;
        public  bool? SmokeAt;
        public  bool? AlcAt;
        public  List<MusicGenre> MusicPref;

        public UserPreferences((int, int) age, (int, int) height, (int, int) weight, bool? smokeAt, bool? alcAt, List<MusicGenre> musicPref )
        {
            Age = age;
            Height = height;
            Weight = weight;
            SmokeAt = smokeAt;
            AlcAt = alcAt;
            MusicPref = musicPref;

        }
        
        
    }

    public enum MusicGenre
    {
        HipHop,
        Rap,
        Rock
    }
}