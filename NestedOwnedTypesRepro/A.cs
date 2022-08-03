namespace NestedOwnedTypesRepro;

internal class AEntity
{
    public string Name { get; set; }
    public ICollection<BEntity> Bs { get; set; }
}
internal class A
{
    public string Name { get; set; }
    public ICollection<B> Bs { get; set; }
}
