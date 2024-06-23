using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public class Form
    {
        public required string Word { get; init; }

        public Inflection Inflection { get; init; }

        public List<Reference> References { get; } = [];

    }
}
