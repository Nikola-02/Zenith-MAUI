using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith_MAUI.Common;

namespace Zenith_MAUI.ViewModels
{
    public class MProfileViewModel
    {
        public MProp<string> Username { get; set; } = new MProp<string>();

        public MProfileViewModel()
        {
            
        }
    }
}
