using Microsoft.EntityFrameworkCore;
using Server.DAL.Context.ApplicationDbContext;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly UniversityDbContext _context;

        public FileRepository(UniversityDbContext context)
            => _context = context;

        public async Task<bool> AddTaskFile(List<TaskFile> task)
        {
            try
            {
                await _context.TaskFiles.AddRangeAsync(task);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AddSolutionFile(List<SolutionFile> files)
        {
            try
            {
                await _context.SolutionFiles.AddRangeAsync(files);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TaskFile> GetTaskFile(int fileId)
        {
            try
            {
                return await _context.TaskFiles.FirstOrDefaultAsync(x => x.Id == fileId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TaskFile> GetSolutionFile(int fileId)
        {
            try
            {
                return await _context.TaskFiles.FirstOrDefaultAsync(x => x.Id == fileId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
