﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Dtos
{
    public class DialogDto : BaseDto
    {
        public string MessageResourceKey { get; set; }
        public string AfterAction { get; set; }
    }
}
