using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Infrastructure.Repositories;
using ProjectBase.Core.Utils;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;
using VietnamBackpackerHostels.Core.Models;
using Microsoft.CodeAnalysis;
using Rollbar.DTOs;

namespace VietnamBackpackerHostels.Infrastructure.Repositories
{
    public class JobVacanciesRepository : BaseRepository, IJobVacanciesRepository
    {
        public JobVacanciesRepository(IDapperContext dapperContext) : base(dapperContext)
        {
        }

        public async Task<IEnumerable<JobVacancy>> GetJobVacancies()
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<JobVacancy>("GetJobVacancies");
        }
    }
}

