using System;
using System.Speech.Synthesis;
using System.Windows;

namespace SoundAndVideo
{
	/// <summary>
	/// Interaction logic for SpeechSynthesis.xaml
	/// </summary>
	public partial class SpeechSynthesis : System.Windows.Window
	{
		public SpeechSynthesis()
		{
			InitializeComponent();
		}

		private void cmdSpeak_Click(object sender, RoutedEventArgs e)
		{
			SpeechSynthesizer synthesizer = new SpeechSynthesizer();
			synthesizer.Speak(txtWords.Text);
		}

		private void cmdPromptTest_Click(object sender, RoutedEventArgs e)
		{
			PromptBuilder prompt = new PromptBuilder();

			prompt.AppendText("How are you");
			prompt.AppendBreak(TimeSpan.FromSeconds(2));
			prompt.AppendText("How ", PromptEmphasis.Reduced);
			PromptStyle style = new PromptStyle();
			style.Rate = PromptRate.ExtraSlow;
			style.Emphasis = PromptEmphasis.Strong;
			prompt.StartStyle(style);
			prompt.AppendText("are ");
			prompt.EndStyle();
			prompt.AppendText("you?");

			SpeechSynthesizer synthesizer = new SpeechSynthesizer();
			synthesizer.Speak(prompt);
		}
	}
}