using System;
using System.Speech.Recognition;

namespace SoundAndVideo
{
	/// <summary>
	/// Interaction logic for SpeechRecognition.xaml
	/// </summary>

	public partial class SpeechRecognition : System.Windows.Window
	{
		SpeechRecognizer recognizer = new SpeechRecognizer();

		public SpeechRecognition()
		{
			InitializeComponent();

			GrammarBuilder grammar = new GrammarBuilder();
			grammar.Append(new Choices("red", "blue", "green", "black", "white"));
			grammar.Append(new Choices("on", "off"));

			recognizer.LoadGrammar(new Grammar(grammar));
			recognizer.SpeechDetected += recognizer_SpeechDetected;
			recognizer.SpeechRecognized += recognizer_SpeechRecognized;
			recognizer.SpeechRecognitionRejected += recognizer_SpeechRejected;
			recognizer.SpeechHypothesized += recognizer_SpeechHypothesized;
		}

		private void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		{
			lbl.Content = "You said: " + e.Result.Text;
		}

		private void recognizer_SpeechDetected(object sender, SpeechDetectedEventArgs e)
		{
			lbl.Content = "Speech detected.";
		}

		private void recognizer_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
		{
			lbl.Content = "Speech uncertain.";
		}

		private void recognizer_SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
		{
			lbl.Content = "Speech rejected.";
		}

		protected override void OnClosed(EventArgs e)
		{
			recognizer.Dispose();
		}
	}
}