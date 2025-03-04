using System;
using System.IO;
using Tesseract;
namespace WebBanGiay.Areas.Helpers

{
    public class CccdScanner
    {
        public string ExtractText(string imagePath)
        {
            // Đường dẫn tới thư mục tessdata chứa file traineddata (ví dụ: vie.traineddata)
            string tessDataPath = Path.Combine(Environment.CurrentDirectory, "tessdata");

            using (var engine = new TesseractEngine(tessDataPath, "vie", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        return page.GetText();
                    }
                }
            }
        }
    }
}
