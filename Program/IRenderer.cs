using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Program
{
    internal interface IRenderer
    {
        Color[] NextFrame();
    }
}