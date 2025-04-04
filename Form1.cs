using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fishing
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _httpClient;

        public Form1()
        {
            InitializeComponent();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.solunar.org/solunar/"),
                Timeout = TimeSpan.FromSeconds(10)
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }

        public class FishingData
        {
            [JsonProperty("sunRise")]
            public string SunRise { get; set; }

            [JsonProperty("sunSet")]
            public string SunSet { get; set; }

            [JsonProperty("moonPhase")]
            public string MoonPhase { get; set; }

            [JsonProperty("major1Start")]
            public string Major1Start { get; set; }

            [JsonProperty("major1Stop")]
            public string Major1Stop { get; set; }

            [JsonProperty("major2Start")]
            public string Major2Start { get; set; }

            [JsonProperty("major2Stop")]
            public string Major2Stop { get; set; }

            [JsonProperty("minor1Start")]
            public string Minor1Start { get; set; }

            [JsonProperty("minor1Stop")]
            public string Minor1Stop { get; set; }

            [JsonProperty("minor2Start")]
            public string Minor2Start { get; set; }

            [JsonProperty("minor2Stop")]
            public string Minor2Stop { get; set; }

            [JsonProperty("dayRating")]
            public int DayRating { get; set; }
        }

        private async void fetchBtn_Click(object sender, EventArgs e)
        {
            await FetchFishingDataAsync();
        }

        private async Task FetchFishingDataAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(latitudeBox.Text) || 
                    string.IsNullOrWhiteSpace(longitudeBox.Text) || 
                    string.IsNullOrWhiteSpace(dateBox.Text))
                {
                    MessageBox.Show("Please fill in all required fields.", "Input Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                fetchBtn.Enabled = false;

                string url = $"{latitudeBox.Text},{longitudeBox.Text},{dateBox.Text},-5";
                string response = await _httpClient.GetStringAsync(url);

                var data = JsonConvert.DeserializeObject<FishingData>(response) 
                    ?? throw new JsonException("Failed to deserialize response");

                UpdateLabels(data);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Network error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Data parsing error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fetchBtn.Enabled = true;
            }
        }

        private void UpdateLabels(FishingData data)
        {
            sunRiseLabel.Text = $"Sunrise: {data.SunRise ?? "N/A"}";
            sunSetLabel.Text = $"Sunset: {data.SunSet ?? "N/A"}";
            moonPhaseLabel.Text = $"Moon phase: {data.MoonPhase ?? "N/A"}";
            major1StartLabel.Text = $"Major 1 Time Start: {data.Major1Start ?? "N/A"}";
            major1StopLabel.Text = $"Major 1 Time End: {data.Major1Stop ?? "N/A"}";
            major2StartLabel.Text = $"Major 2 Time Start: {data.Major2Start ?? "N/A"}";
            major2StopLabel.Text = $"Major 2 Time End: {data.Major2Stop ?? "N/A"}";
            minor1StartLabel.Text = $"Minor 1 Time Start: {data.Minor1Start ?? "N/A"}";
            minor1StopLabel.Text = $"Minor 1 Time End: {data.Minor1Stop ?? "N/A"}";
            minor2StartLabel.Text = $"Minor 2 Time Start: {data.Minor2Start ?? "N/A"}";
            minor2StopLabel.Text = $"Minor 2 Time End: {data.Minor2Stop ?? "N/A"}";
            dayRatingLabel.Text = $"Day Rating: {data.DayRating}";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
