namespace api.Models
{
    public class Player
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Hand { get; set; } = new List<string>();
        public string HandType { get; set; } = string.Empty;
        public GameResult GameResult { get; set; } = GameResult.Loss;
    }
}
