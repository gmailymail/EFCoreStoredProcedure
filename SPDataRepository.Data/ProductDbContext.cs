using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SPDataRepository.Data.Entities;
using SPDataRepository.Domain.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SPDataRepository.Data
{
#nullable disable
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductDto> ProductDtos { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Generic Pattern to retreive data from any Stored Procedure
        /// </summary>
        /// <typeparam name="T">a Class Type</typeparam>
        /// <param name="Schema"></param>
        /// <param name="StoredProcedure">Cannot be NULL</param>
        /// <param name="parameters"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetTsFromSpAsync<T>(string StoredProcedure, string Schema = "", SqlParameter[] parameters=null,
            CancellationToken token=default) where T : class
        {
            if (parameters is null)
                parameters = new SqlParameter[0];

            string spName = BuildSpName(StoredProcedure, Schema,parameters);

            var resultSet = await Set<T>()
                .FromSqlRaw($"{spName}", parameters)
                .ToListAsync(token);

            return resultSet;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //This must be set in order to treat ProductDto as a non table
            modelBuilder.Entity<ProductDto>().HasNoKey().ToView(null);

        }

        #region Helper Methods

        /// <summary>
        /// Build corresponding Stored Procedure Command to execute
        /// </summary>
        /// <param name="Schema"></param>
        /// <param name="StoredProcedure">Cannot be NULL</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string BuildSpName(string StoredProcedure, string Schema = "", SqlParameter[] parameters = null)
        {
            //Unicode for Space
            var spaceChar = "\u0020";
            string spName = $"EXECUTE{spaceChar}";

            if (!string.IsNullOrWhiteSpace(Schema))
                spName += $"{spaceChar}{Schema}.";

            spName += $"{StoredProcedure}";

            if (parameters.Length>0)
            {
                foreach (var param in parameters)
                    //Add spcae front and after along with Parameter name
                    //The only purpose of these spaces is to look cleaner
                    spName += $"{spaceChar}@{param.ParameterName},{spaceChar}";

                //Remove the last Comma and tje White space
                int lettersToRemove = 2;
                spName = spName.Remove(spName.Length - lettersToRemove, lettersToRemove);
            }
            return spName;
        }

        #endregion Helper Methods
    }
}
