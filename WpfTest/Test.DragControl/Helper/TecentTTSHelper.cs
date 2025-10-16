using System;
using System.Diagnostics;
using System.IO;
using TencentCloud.Common;
using TencentCloud.Common.Profile;
using TencentCloud.Tts.V20190823;
using TencentCloud.Tts.V20190823.Models;

namespace Test.DragControl.Helper
{
    public class TecentTTSHelper
    {
        private static readonly string rootPath
            = $"{AppDomain.CurrentDomain.BaseDirectory}\\Materials";
        private const string SECRET_ID = "AKIDd5HEuJRIsCFTWKsAD1**************";
        private const string SECRET_KEY = "93h5D4h4D9qHSN4VgbGgqk***********";

        public static void TestShortTextTTS(string shortText, string fileName)
        {
            try
            {
                Credential cred = new Credential
                {
                    SecretId = SECRET_ID,
                    SecretKey = SECRET_KEY
                };

                ClientProfile clientProfile = new ClientProfile();
                HttpProfile httpProfile = new HttpProfile();
                httpProfile.Endpoint = ("tts.tencentcloudapi.com");
                clientProfile.HttpProfile = httpProfile;

                TtsClient client = new TtsClient(cred, "ap-beijing", clientProfile);
                TextToVoiceRequest req = new TextToVoiceRequest();
                req.Text = shortText;
                req.SessionId = "12345678900987654321";
                req.ModelType = 1;
                req.Codec = "mp3";

                TextToVoiceResponse resp = client.TextToVoiceSync(req);
                ConvertBase64ToFile(resp.Audio, fileName, rootPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static string TestLongTextTTS(string longText)
        {
            CreateTtsTaskResponse resp = null;

            try
            {
                Credential cred = new Credential
                {
                    SecretId = SECRET_ID,
                    SecretKey = SECRET_KEY
                };

                ClientProfile clientProfile = new ClientProfile();
                HttpProfile httpProfile = new HttpProfile();
                httpProfile.Endpoint = ("tts.tencentcloudapi.com");
                clientProfile.HttpProfile = httpProfile;

                TtsClient client = new TtsClient(cred, "", clientProfile);
                CreateTtsTaskRequest req = new CreateTtsTaskRequest();
                req.Text = longText;
                req.ModelType = 1;

                resp = client.CreateTtsTaskSync(req);
                //Console.WriteLine(AbstractModel.ToJsonString(resp));

                return resp?.Data?.TaskId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static string GetLongTextTTSDownloadUrl(string taskId)
        {
            DescribeTtsTaskStatusResponse resp = null;

            try
            {
                Credential cred = new Credential
                {
                    SecretId = SECRET_ID,
                    SecretKey = SECRET_KEY
                };

                ClientProfile clientProfile = new ClientProfile();
                HttpProfile httpProfile = new HttpProfile();
                httpProfile.Endpoint = ("tts.tencentcloudapi.com");
                clientProfile.HttpProfile = httpProfile;

                TtsClient client = new TtsClient(cred, "", clientProfile);
                DescribeTtsTaskStatusRequest req = new DescribeTtsTaskStatusRequest();
                req.TaskId = taskId;
                resp = client.DescribeTtsTaskStatusSync(req);
                //Console.WriteLine(AbstractModel.ToJsonString(resp));

                return resp?.Data?.ResultUrl;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static void DownloadFile(string downloadUrl, string fileName)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.Encoding = System.Text.Encoding.GetEncoding("UTF-8");
            byte[] bytes = wc.DownloadData(downloadUrl);

            using (var fs = new FileStream($"{rootPath}{fileName}", FileMode.Create, FileAccess.Write))
            {
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
            }
        }

        private static void ConvertBase64ToFile(string source, string filename, string filepath)
        {
            try
            {
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                byte[] bytes = Convert.FromBase64String(source);
                using (var fs = new FileStream(filepath + filename, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

    }
}
