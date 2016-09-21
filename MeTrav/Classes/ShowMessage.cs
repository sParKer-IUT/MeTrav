using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace MeTrav
{
    public class ShowMessage
    {
        public static async Task<bool> PopupMessage(string message)
        {
            MessageDialog msg = new MessageDialog(message);
            await msg.ShowAsync();
            return true;
        }
    }
}
