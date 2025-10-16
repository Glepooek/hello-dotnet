using NAudio.MediaFoundation;
using NAudio.Wave;
using System.Collections.Generic;
using System.IO;

// 修剪文件
// https://www.jb51.cc/csharp/92804.html
// https://markheath.net/post/trimming-wav-file-using-naudio

namespace Test.DragControl.Helper
{
    public class NAudioHelper
    {
        /// <summary>
        /// 合并wav格式文件
        /// </summary>
        /// <param name="inputFiles">待合并文件列表</param>
        /// <param name="outputFileName">输出文件名</param>
        public static void MergeWAVFiles(List<string> inputFiles, string outputFileName)
        {
            WaveStream waveStream = WaveFormatConversionStream.CreatePcmStream(new WaveFileReader(inputFiles[0]));
            using (WaveFileWriter waveFileWriter = new WaveFileWriter(outputFileName, waveStream.WaveFormat))
            {
                foreach (string file in inputFiles)
                {
                    waveStream = WaveFormatConversionStream.CreatePcmStream(new WaveFileReader(file));
                    byte[] bytes = new byte[waveStream.Length];
                    waveStream.Position = 0;
                    waveStream.Read(bytes, 0, (int)waveStream.Length);
                    waveFileWriter.Write(bytes, 0, bytes.Length);
                }
            }
        }

        /// <summary>
        /// 合并Mp3格式文件
        /// </summary>
        /// <param name="inputFiles">待合并文件列表</param>
        /// <param name="outputFileName">输出文件名</param>
        public static void MergeMP3Files(List<string> inputFiles, string outputFileName)
        {
            using (FileStream outputStream = new FileStream(outputFileName, FileMode.OpenOrCreate))
            {
                foreach (string file in inputFiles)
                {
                    Mp3FileReader reader = new Mp3FileReader(file);
                    if ((outputStream.Position == 0) && (reader.Id3v2Tag != null))
                    {
                        outputStream.Write(reader.Id3v2Tag.RawData, 0, reader.Id3v2Tag.RawData.Length);
                    }
                    Mp3Frame frame;
                    while ((frame = reader.ReadNextFrame()) != null)
                    {
                        outputStream.Write(frame.RawData, 0, frame.RawData.Length);
                    }
                }
            }
        }

        /// <summary>
        /// 将MP3文件转换为WAV文件
        /// </summary>
        /// <param name="inMP3FileName">待转换的MP3文件</param>
        /// <param name="outWAVFileName">转换后的WAV文件</param>
        public static void ConvertMP3ToWAV(string inMP3FileName, string outWAVFileName)
        {
            using (var reader = new Mp3FileReader(inMP3FileName))
            {
                WaveFileWriter.CreateWaveFile(outWAVFileName, reader);
            }

            //var mediaType = MediaFoundationEncoder.SelectMediaType(AudioSubtypes.MFAudioFormat_WMAudioV8, new WaveFormat(16000, 1), 16000);
            //using (var reader = new MediaFoundationReader(inMP3FileName))
            //{
            //    using (var encoder = new MediaFoundationEncoder(mediaType))
            //    {
            //        encoder.Encode(outWAVFileName, reader);
            //    }
            //}
        }

        /// <summary>
        /// 将WAV文件转换为MP3文件
        /// </summary>
        /// <param name="inWAVFileName">待转换的WAV文件</param>
        /// <param name="outMP3FileName">转换后的MP3文件</param>
        public static void ConvertWAVToMP3(string inWAVFileName, string outMP3FileName)
        {
            //using (var reader = new WaveFileReader(inWAVFileName))
            //{
            //    MediaFoundationEncoder.EncodeToMp3(reader, outMP3FileName);
            //}

            var mediaType = MediaFoundationEncoder.SelectMediaType(AudioSubtypes.MFAudioFormat_MP3, new WaveFormat(44100, 1), 0);
            using (var reader = new MediaFoundationReader(inWAVFileName))
            {
                using (var encoder = new MediaFoundationEncoder(mediaType))
                {
                    encoder.Encode(outMP3FileName, reader);
                }
            }
        }
    }
}
