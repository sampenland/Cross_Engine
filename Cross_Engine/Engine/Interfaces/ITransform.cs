using CrossEngine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross_Engine.Engine.Interfaces
{
    interface ITransform
    {
        XYf getPos();
        void setPos(XYf pos);

    }
}
