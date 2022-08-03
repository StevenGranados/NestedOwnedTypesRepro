using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedOwnedTypesRepro;

internal class CEntity
{
    public string Name { get; set; }
    public string AName { get; set; }
    public string BName { get; set; }
    public BEntity B { get; set; }
}
internal class C
{
    public string Name { get; set; }
    public string BData { get; set; }
}
