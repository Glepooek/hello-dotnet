using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

//注意：最多激活到21.10版本，再高就没办法了。
//Aspose.Cells 21.10
//Aspose.Words 21.8
//Aspose.Slides 21.8
//Aspose.PDF 21.10, 在.NET 10上设置License会报错。

namespace HarmonyAsposeSamples
{
    internal class AsposeActivationHelper
    {
        private const string lic_base64_str = "DQo8TGljZW5zZT4NCjxEYXRhPg0KPExpY2Vuc2VkVG8+VGhlIFdvcmxkIEJhbms8L0xpY2Vuc2VkVG8+DQo8RW1haWxUbz5ra3VtYXIzQHdvcmxkYmFua2dyb3VwLm9yZzwvRW1haWxUbz4NCjxMaWNlbnNlVHlwZT5EZXZlbG9wZXIgU21hbGwgQnVzaW5lc3M8L0xpY2Vuc2VUeXBlPg0KPExpY2Vuc2VOb3RlPjEgRGV2ZWxvcGVyIEFuZCAxIERlcGxveW1lbnQgTG9jYXRpb248L0xpY2Vuc2VOb3RlPg0KPE9yZGVySUQ+MjEwMzE2MTg1OTU3PC9PcmRlcklEPg0KPFVzZXJJRD43NDQ5MTY8L1VzZXJJRD4NCjxPRU0+VGhpcyBpcyBub3QgYSByZWRpc3RyaWJ1dGFibGUgbGljZW5zZTwvT0VNPg0KPFByb2R1Y3RzPg0KPFByb2R1Y3Q+QXNwb3NlLlRvdGFsIGZvciAuTkVUPC9Qcm9kdWN0Pg0KPC9Qcm9kdWN0cz4NCjxFZGl0aW9uVHlwZT5Qcm9mZXNzaW9uYWw8L0VkaXRpb25UeXBlPg0KPFNlcmlhbE51bWJlcj4wM2ZiMTk5YS01YzhhLTQ4ZGItOTkyZS1kMDg0ZmYwNjZkMGM8L1NlcmlhbE51bWJlcj4NCjxTdWJzY3JpcHRpb25FeHBpcnk+MjAyMjA1MTY8L1N1YnNjcmlwdGlvbkV4cGlyeT4NCjxMaWNlbnNlVmVyc2lvbj4zLjA8L0xpY2Vuc2VWZXJzaW9uPg0KPExpY2Vuc2VJbnN0cnVjdGlvbnM+aHR0cHM6Ly9wdXJjaGFzZS5hc3Bvc2UuY29tL3BvbGljaWVzL3VzZS1saWNlbnNlPC9MaWNlbnNlSW5zdHJ1Y3Rpb25zPg0KPC9EYXRhPg0KPFNpZ25hdHVyZT5XbkJYNnJOdHpCclNMV3pBdFlqOEtkdDFLSUI5MlFrL2xEbFNmMlM1TFRIWGdkcS9QQ2NqWHVORmp0NEJuRmZwNFZLc3VsSjhWeFExakIwbmM0R1lWcWZLek14SFFkaXFuZU03NTJaMjlPbmdyVW40Yk0rc1l6WWVSTE9UOEpxbE9RN05rRFU0bUk2Z1VyQ3dxcjdnUVYxbDJJWkJxNXMzTEFHMFRjQ1ZncEE9PC9TaWduYXR1cmU+DQo8L0xpY2Vuc2U+DQo=";
        private static readonly string lic_file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Aspose.Total.lic");

        public static void SetWordsLicense()
        {
            var l = new Aspose.Words.License();
            l.SetLicense(lic_file_path);
        }

        /// <summary>
        /// PDF
        /// </summary>
        public static void SetPdfLicense()
        {
            Aspose.Pdf.License l = new Aspose.Pdf.License();
            l.SetLicense(lic_file_path);
        }

        /// <summary>
        /// PPT
        /// </summary>
        public static Aspose.Slides.License SetSlidesLicense()
        {
            Aspose.Slides.License l = new Aspose.Slides.License();
            l.SetLicense(lic_file_path);

            return l;
        }

        /// <summary>
        /// Excel
        /// </summary>
        public static void SetCellsLicense()
        {
            var l = new Aspose.Cells.License();
            l.SetLicense(lic_file_path);
        }

        private static Stream GetLicenseStream()
        {
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(lic_base64_str));
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}
