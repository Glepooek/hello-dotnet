using System;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;

/***
 * 支持:
 * 1、男声、女声
 * 2、调节语速
 * 3、调节音量
 * 4、实时读文本，还可追加暂停时间等
 * 5、保存到文件
 * 
 * 优点
 * 1、没有字数限制
 * 2、不需要网络
 * 
 * 缺点：
 * 1、保存文件较大
 * 
 * ********/

namespace Test.DragControl.Helper
{
    public class SystemSpeaker
    {
        #region Fields

        private static string rootPath = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 语音合成器
        /// </summary>
        private SpeechSynthesizer mSpeechSynthesizer;

        #endregion

        #region Constructor

        public SystemSpeaker()
        {
            Initilize();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 同步播放语音
        /// </summary>
        /// <param name="textToSpeak">文本内容</param>
        public void Speak(string textToSpeak)
        {
            PromptBuilder builder = new PromptBuilder();
            builder.AppendText(textToSpeak);
            mSpeechSynthesizer.Speak(builder);
        }

        /// <summary>
        /// 同步播放语音
        /// </summary>
        /// <param name="textToSpeak">文本内容</param>
        /// <param name="pauseTime">暂停时长，单位：秒</param>
        public void Speak(string textToSpeak, int pauseTime)
        {
            PromptBuilder builder = new PromptBuilder();
            builder.AppendText(textToSpeak);
            builder.AppendBreak(TimeSpan.FromSeconds(pauseTime));
            mSpeechSynthesizer.Speak(builder);
        }

        /// <summary>
        /// 异步播放语音
        /// </summary>
        /// <param name="textToSpeak"></param>
        /// <returns></returns>
        public Prompt SpeakAsync(string textToSpeak)
        {
            return mSpeechSynthesizer.SpeakAsync(textToSpeak);
        }

        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="fileName"></param>
        public void SetOutputToWaveFile(string fileName)
        {
            SpeechAudioFormatInfo formatInfo = new SpeechAudioFormatInfo(16000, AudioBitsPerSample.Sixteen, AudioChannel.Stereo);
            //SpeechAudioFormatInfo formatInfo = new SpeechAudioFormatInfo(8000, AudioBitsPerSample.Sixteen, AudioChannel.Stereo);
            mSpeechSynthesizer.SetOutputToWaveFile($"{rootPath}{fileName}", formatInfo);
        }

        /// <summary>
        /// 设置语速，-10到10
        /// </summary>
        /// <param name="rate"></param>
        public void SetRate(int rate)
        {
            mSpeechSynthesizer.Rate = Math.Max(-10, Math.Min(10, rate));
        }

        /// <summary>
        /// 设置音量，0到100
        /// </summary>
        /// <param name="volume"></param>
        public void SetVolume(int volume)
        {
            mSpeechSynthesizer.Volume = Math.Max(0, Math.Min(100, volume));
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        public void Stop()
        {
            mSpeechSynthesizer.SpeakAsyncCancelAll();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (mSpeechSynthesizer != null)
            {
                mSpeechSynthesizer.Dispose();
                mSpeechSynthesizer = null;
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// 初始化<see cref="SystemSpeaker"/>实例
        /// </summary>
        private void Initilize()
        {
            mSpeechSynthesizer = new SpeechSynthesizer();
            var voices = mSpeechSynthesizer.GetInstalledVoices();
            foreach (var voice in voices)
            {
                if (voice.VoiceInfo.Id.Contains("ZH-CN"))
                {
                    mSpeechSynthesizer.SelectVoice(voice.VoiceInfo.Name);
                    break;
                }
            }

            mSpeechSynthesizer.Rate = 0;
            mSpeechSynthesizer.Volume = 100;
            mSpeechSynthesizer.SelectVoiceByHints(VoiceGender.Female);
        }

        #endregion
    }
}
