using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL.Repositories
{
    internal class FileRepository
    {
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
    }
}
