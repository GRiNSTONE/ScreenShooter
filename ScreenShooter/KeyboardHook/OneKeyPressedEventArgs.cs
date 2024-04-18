using System;
using System.Windows.Forms;

namespace ScreenShooter
{
    public class OneKeyPressedEventArgs : EventArgs
    {
        private Keys _key;

        internal OneKeyPressedEventArgs(Keys key)
        {
            _key = key;
        }

        public Keys Key
        {
            get { return _key; }
        }
    }
}
