using Newtonsoft.Json;
using System.Net;
using System.Security.Policy;

namespace Fishing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public class FishingData
        {
            public string sunRise;
            public string sunSet;
            public string moonPhase;
            public string major1Start;
            public string major1Stop;
            public string major2Start;
            public string major2Stop;
            public string minor1Start;
            public string minor1Stop;
            public string minor2Start;
            public string minor2Stop;
            public int dayRating;
        }

        private void fetchBtn_Click(object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();

            var response = webClient.DownloadString("https://api.solunar.org/solunar/" + latitudeBox.Text + "," + longitudeBox.Text + "," + dateBox.Text + ",-5");

            var deserializedData = JsonConvert.DeserializeObject<FishingData>(response);

            sunRiseLabel.Text = "Sunrise: " + deserializedData.sunRise;
            sunSetLabel.Text = "Sunset: " + deserializedData.sunSet;
            moonPhaseLabel.Text = "Moon phase: " + deserializedData.moonPhase;
            major1StartLabel.Text = "Major 1 Time Start: " + deserializedData.major1Start;
            major1StopLabel.Text = "Major 1 Time End: " + deserializedData.major1Stop;
            major2StartLabel.Text = "Major 2 Time Start: " + deserializedData.major2Start;
            major2StopLabel.Text = "Major 2 Time End: " + deserializedData.major2Stop;
            minor1StartLabel.Text = "Minor 1 Time Start: " + deserializedData.minor1Start;
            minor1StopLabel.Text = "Minor 1 Time End: " + deserializedData.minor1Stop;
            minor2StartLabel.Text = "Minor 2 Time Start: " + deserializedData.minor2Start;
            minor2StopLabel.Text = "Minor 2 Time End: " + deserializedData.minor2Stop;
            dayRatingLabel.Text = "Day Rating: " + deserializedData.dayRating;
        }
    }
}
