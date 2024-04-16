using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace WebAPI.Utils.OCR
{
    public class OCRService
    {
        private readonly string _subscriptKey = "0662f572663c4b2a92e0f2d25e754af3";

        private readonly string _endpoint = "https://computervisiong11t.cognitiveservices.azure.com/";


        public async Task<string> RecognizeTextAsync(Stream imageStream) 
        {
            try
            {
                var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_subscriptKey))
                {
                    Endpoint = _endpoint
                };

                var ocrResult = await client.RecognizePrintedTextInStreamAsync(true, imageStream);

                return ProcessRecognitionResult(ocrResult);
            }
            catch (Exception e)
            {

                return "Erro ao reconhecer o texto" +  e.Message;
            }
        }


        private static string ProcessRecognitionResult(OcrResult result)
        {
            try
            {
                string recognizedText = "";

                foreach (var region in result.Regions)
                {
                    foreach (var line in region.Lines)
                    {
                        foreach (var word in line.Words)
                        {
                            recognizedText += word.Text + " ";
                        }

                        recognizedText += "/n";
                    }
                }
                return recognizedText;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
