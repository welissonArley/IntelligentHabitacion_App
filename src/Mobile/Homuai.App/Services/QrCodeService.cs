using System.Collections.Generic;
using System.Threading.Tasks;
using ZXing.Mobile;

namespace Homuai.App.Services
{
    public class QrCodeService
    {
        public async Task<string> Scan()
        {
            var optionsCustom = new MobileBarcodeScanningOptions()
            {
                PossibleFormats = new List<ZXing.BarcodeFormat>()
                {
                    ZXing.BarcodeFormat.QR_CODE
                }
            };

            var scanner = new MobileBarcodeScanner()
            {
                TopText = ResourceText.TITLE_APPROACH_CAMERA_READ_QRCODE,
                BottomText = ResourceText.TITLE_TOUCH_SCREEN_TO_FOCUS
            };

            var scanResults = await scanner.Scan(optionsCustom);

            return (scanResults != null) ? scanResults.Text : string.Empty;
        }
    }
}
