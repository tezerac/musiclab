using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    public class MusicClass
    {

        private const string MEDIA_FILE_NAME = "MultiMediaFile.mp4";
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Length { get; set; }
        public string Genre { get; set; }

        public static async Task<ICollection<MusicClass>> GetMusic()
        {
            var musicClasses = new List<MusicClass>();
            var fileContent = await MediaHelper.GetMediaFileAsync(MEDIA_FILE_NAME);
            var lines = fileContent.Split(new char[] { '\r', '\n' });
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var linesPart = line.Split(',');
                var musicClass = new MusicClass
                {

                    Title = linesPart[0],
                    Artist = linesPart[1],
                    Album = linesPart[2],
                    Length = linesPart[3],
                    Genre = linesPart[4]
                };
                musicClasses.Add(musicClass);
            }
            return musicClasses;
        }


        public static void AddMusic(MusicClass musicClass)
        {
            var musicData = $"{musicClass.Title },{musicClass.Artist},{musicClass.Album },{musicClass.Length },{musicClass.Genre }";
            MediaHelper.WriteMediaFileAsync(MEDIA_FILE_NAME, musicData);
        }


    }
}

