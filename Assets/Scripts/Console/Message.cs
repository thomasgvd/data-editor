using TMPro;

// A message is displayed on the console
public class Message
{
    public string Text { get; set; }
    public TextMeshProUGUI TextObject { get; set; }

    public Message(string text) => Text = text;
}