using System;
using System.Drawing;
using System.IO;
//using Tesseract;
using RestSharp;

namespace Snipping_OCR
{
    public class ImageSearch
    {
        public static OcrResult Process(string filePath)
        {
            return ProcessProc(filePath);
        }

        public static OcrResult Process(Image image, string language = "eng")
        {
            string tpath = "c:/temp/capture.png";
            image.Save(tpath, System.Drawing.Imaging.ImageFormat.Png);
            return ProcessProc(tpath);
        }

        private static OcrResult ProcessProc(string filePath)
        {
            try
            {

                var searchUrl = "http://www.google.ca/searchbyimage/upload";
                RestRequest request = new RestRequest(Method.POST);
                request.AddFile("encoded_image", File.ReadAllBytes(filePath), "");
                request.AlwaysMultipartFormData = true;
                var client = new RestClient(new Uri(searchUrl));
                var response = client.Execute(request);


                string url = response.ResponseUri.Scheme + "://" + response.ResponseUri.Host + "/search" + response.ResponseUri.Query;


                System.Diagnostics.Process.Start(url);


                var text = "dummy";
                return new OcrResult()
                {
                    Text = text,
                    Confidence = 1.0F,
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new OcrResult()
                {
                    Error = "fail",
                    Success = false
                };
            }
        }
    } // class Ocr
}
