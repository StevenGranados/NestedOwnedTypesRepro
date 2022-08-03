using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedOwnedTypesRepro;

internal class BEntity
{
    public string Name { get; set; }
    public string AName { get; set; }
    public string Data { get; set; }
    public AEntity A { get; set; }
    public ICollection<CEntity> Cs { get; set; }
}
internal class B
{
    public string Name { get; set; }
    public string Data { get; set; }
    public ICollection<C> Cs { get; set; }
}
