using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QRCoder;
using NToastNotify;
namespace QR_Code_generator.Pages
{
    public class IndexModel : PageModel
    {

        public string QRDataGen => (string)TempData[nameof(QRDataGen)];

        public void OnGet()
        {

        }

        public IActionResult OnPost([FromForm] string QRData)

        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    QRCodeGenerator oQrcodeGenerator = new QRCodeGenerator();
                    QRCodeData oQRCodeData = oQrcodeGenerator.CreateQrCode(QRData, QRCodeGenerator.ECCLevel.Q);
                    QRCode oQRCode = new QRCode(oQRCodeData);
                    using (System.Drawing.Bitmap oBitmap = oQRCode.GetGraphic(6))
                    {
                        oBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        TempData["QRDataGen"] = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    }
                }
            } catch (NullReferenceException e) 
            {
                Console.WriteLine("Please enter some text to generate your QR Code", e);
            }
            {
                return RedirectToPage("Index");
            }
        }
    }
}



