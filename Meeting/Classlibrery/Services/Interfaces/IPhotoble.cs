using DataAccess.Data.Entities;

namespace DataAccess.Services.Interfaces
{
    internal interface IPhotoble<T> where T : class
    {
        ICollection<Photo<T>>? Photos { get; set; }
    }
    
}
