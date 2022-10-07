using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using VietnamBackpackerHostels.Core.Models;

namespace VietnamBackpackerHostels.Core.Interfaces.Repositories
{
    public interface IJobVacanciesRepository
    {
        Task<IEnumerable<JobVacancy>> GetJobVacancies();
    }
}

