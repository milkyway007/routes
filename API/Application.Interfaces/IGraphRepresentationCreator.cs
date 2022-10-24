namespace Application.Interfaces
{
    public interface IGraphRepresentationCreator
    {
        IGraphRepresentation GraphRepresentation { get; }

        void Create();
    }
}
