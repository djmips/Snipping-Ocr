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

                var searchUrl = "http://www.google.hr/searchbyimage/upload";

                //multipart = { 'encoded_image': (imagefile, open(imagefile, 'rb')), 'image_content': ''}
                //response = requests.post(searchUrl, files = multipart, allow_redirects = False)
                //fetchUrl = response.headers['Location']
                //print fetchUrl

                RestRequest request = new RestRequest("encoded_image", Method.POST);
                request.AddParameter("filename=", File.ReadAllBytes(@"C:\Temp\Capture.png"), ParameterType.RequestBody);
                request.AddHeader("Content-Type", "image/png");
                //request.AddHeader("image_content", "");

                var client = new RestClient(new Uri(searchUrl));

                var response = client.Execute(request);

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
