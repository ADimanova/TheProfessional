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
        IRepository<EndorsementOfPost> EndorsementsOfPosts { get; }
        IRepository<EndorsementOfUser> EndorsementsOfUsers { get; }
        IRepository<FieldOfExpertise> FieldsOfExpertise { get; }
        IRepository<Occupation> Occupations { get; }
        int SaveChanges();
    }
}
