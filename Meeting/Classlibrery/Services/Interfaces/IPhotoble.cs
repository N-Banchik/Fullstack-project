using DataAccess.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Interfaces
{
    internal interface IPhotoble<T> where T : class
    {
        ICollection<Photo<T>>? Photos { get; set; }
    }
    
}
