﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.Study
{
    internal class Exercise(Question[] questions)
    {
        public Question[] Questions => questions;
    }
}