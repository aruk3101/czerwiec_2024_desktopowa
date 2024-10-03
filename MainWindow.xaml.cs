using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Music> musicList = new List<Music>();
        public int currentMusicIndex = 0;

        public void Refresh(int index)
        {
            if (index >= musicList.Count)
            {
                currentMusicIndex = index = 0;
            }
            else if (index < 0)
            {
                currentMusicIndex = index = musicList.Count - 1;
            }
            Music currentMusic = musicList[index];
            ArtistLabel.Content = currentMusic.Artist;
            AlbumTitleLabel.Content = currentMusic.Album;
            YearLabel.Content = currentMusic.Year;
            SongsNumberLabel.Content = currentMusic.SongsNumber + " utworów";
            DownloadNumberLabel.Content = currentMusic.DownloadNumber;
        }

        public static List<Music> ReadDataFromFile(string path)
        {
            List<Music> musicList = new List<Music>();
            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    musicList.Add(new Music()
                    {
                        Artist = streamReader.ReadLine(),
                        Album = streamReader.ReadLine(),
                        SongsNumber = int.Parse(streamReader.ReadLine()),
                        Year = int.Parse(streamReader.ReadLine()),
                        DownloadNumber = int.Parse(streamReader.ReadLine())
                    });
                    streamReader.ReadLine();

                }
            }
            return musicList;
        }

        public MainWindow()
        {
            InitializeComponent();
            musicList = ReadDataFromFile("Data.txt");
            Refresh(currentMusicIndex);
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            musicList[currentMusicIndex].DownloadNumber++;
            Refresh(currentMusicIndex);
        }

        private void PreviousMusic_Click(object sender, RoutedEventArgs e)
        {
            Refresh(--currentMusicIndex);
        }

        private void NextMusic_Click(object sender, RoutedEventArgs e)
        {
            Refresh(++currentMusicIndex);
        }

    }
}