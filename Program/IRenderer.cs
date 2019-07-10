using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Program
{
    internal interface IRenderer
    {
        Color[] NextFrame();
    }
}