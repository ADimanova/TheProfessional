using Professional.Data;
using Professional.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Data
{
    public interface IApplicationData
    {
        IRepository<Post> Posts { get; }
        IRepository<PostEndorsement> PostEndorsements { get; }
        IRepository<UserEndorsement> UserEndorsements { get; }
    }
}
