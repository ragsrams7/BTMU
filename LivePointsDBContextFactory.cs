using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using LivePoints.Repository;

namespace LivePoints.DBRepository
{
    public class LivePointsDBContextFactory : IDesignTimeDbContextFactory<LivePointsDBContext>
    {
        public LivePointsDBContextFactory()
        {

        }
        public LivePointsDBContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LivePointsDBContext>();
            //builder.UseSqlServer(
            //    @"Server=(localdb)\mssqllocaldb;Database=LivePoints;Trusted_Connection=True;MultipleActiveResultSets=true;");
            builder.UseSqlServer(
                @"Server=sql5021.site4now.net;Database=DB_9B4C66_LivePoints;Trusted_Connection=False;MultipleActiveResultSets=true; uid=DB_9B4C66_LivePoints_admin; pwd=Origin#130;");
           
            return new LivePointsDBContext(builder.Options);
        }
    }
}
