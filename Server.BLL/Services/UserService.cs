using Server.BLL.Services.Inrerfaces;
using Server.DAL.Interfaces;
using Server.DAL.Models.DTO;
using Server.DAL.Models.Entities.Education;
using Server.DAL.Models.Entities.Educators;
using Server.DAL.Models.Entities.Users;
using Server.DAL.Models.Enums;
using Server.DAL.Repositories;

namespace Server.BLL.Services {

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEducatorRepository _educatorRepository;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;

    public UserService(IUserRepository userRepository, IFileService fileService, IEducatorRepository educatorRepository, IFileRepository fileRepository)
    {
        _userRepository = userRepository;
        _fileService = fileService;
        _educatorRepository = educatorRepository;
        _fileRepository = fileRepository;
    }
    
    public async Task<List<User>> GetUsersAsync()
    {
        var users = await _userRepository.GetUsersAsync();
        if (users is null || users.Count <= 0)
            return new List<User>();
        return users;
    }

    public async Task<List<StudentSolution>> GetSolutionsStudentByTaskIdSimple(int taskId)
    {
        var users = await _userRepository.GetSolutionStudentByTaskIdSimpleAsync(taskId);
        if (users is null || users.Count <= 0)
            return new List<StudentSolution>();
        return users;
    }

    public async Task<User> GetUser(int userId)
    {
        var user = await _userRepository.GetUserAsync(userId);
        if (user is null)
            return new User();
        return user;
    }

    public async Task<bool> UpdateSolutionStatus(int solutionId, SolutionStatus status)
    {
        var solution = await _userRepository.GetSolutionByIdAsync(solutionId);
        if (solution is null)
            return false;

        solution.Status = status;
        solution.UpdatedAt = DateTime.UtcNow.AddHours(3);

        return await _userRepository.UpdateSolutionStatus(solution);
    }

        public async Task<List<User>> GetUserStudentByGroupId(int userId)
    {
        var user = await _userRepository.GetUsersStudentByGroupAsync(userId);
        if (user is null)
            return new List<User>();
        return user;
    }

    public async Task<User> GetUserByUserId(int userId)
    {
        var user = await _userRepository.GetUserByUserIdAsync(userId);
        return user;
    }
    public async Task<Student> GetStudentByStudentId(int userId)
    {
        var user = await _userRepository.GetStudentByStudentIdAsync(userId);
        if (user is null)
            return new Student();
        return user;
    }

    public async Task<Student> GetStudentByUserId(int userId)
    {
        var user = await _userRepository.GetStudentByUserIdAsync(userId);
        if (user is null)
            return new Student();
        return user;
    }
    
    public async Task<bool> AddUserByDTOAsync(UserDTO userDTO)
    {
        var user = await ParseUserByDTOAsync(userDTO);
        if (user is null)
            return false;
        return await _userRepository.AddUserAsync(user);
    }

        public async Task<bool> EditUserByDTOAsync(int userId, UserDTO userDTO)
        {
            var existingUser = await _userRepository.GetUserFullInfoAsync(userId);
            if (existingUser is null)
                return false;

            existingUser.Email = userDTO.Email;
            existingUser.FirstName = userDTO.FirstName;
            existingUser.LastName = userDTO.LastName;
            existingUser.MiddleName = userDTO.MiddleName;

            if (!string.IsNullOrEmpty(userDTO.PasswordHash))
            {
                existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.PasswordHash);
            }

            switch (existingUser.Role)
            {
                case Role.admin:
                    if (existingUser.Administrator != null && userDTO.Administrator != null)
                    {
                        existingUser.Administrator.Position = userDTO.Administrator.Position;
                    }
                    break;

                case Role.educator:
                    if (existingUser.Educator != null && userDTO.Educator != null)
                    {
                        existingUser.Educator.Profession = userDTO.Educator.Profession;
                        existingUser.Educator.AcademicDegree = userDTO.Educator.AcademicDegree;

                        if (existingUser.Educator.EducatorAdditionalInfo != null && userDTO.Educator.EducatorAdditionalInfo != null)
                        {
                            var info = existingUser.Educator.EducatorAdditionalInfo;
                            var dtoInfo = userDTO.Educator.EducatorAdditionalInfo;

                            info.EducationLevel = dtoInfo.EducationLevel;
                            info.SpecialtyOrFieldOfStudy = dtoInfo.SpecialtyOrFieldOfStudy;
                            info.Qualification = dtoInfo.Qualification;
                            info.AdditionalInfo = dtoInfo.AdditionalInfo;
                            info.AcademicTitle = dtoInfo.AcademicTitle;

                            if (!string.IsNullOrEmpty(dtoInfo.Image))
                            {
                                info.Image = Convert.FromBase64String(dtoInfo.Image);
                            }

                            if (dtoInfo.EducatorDisciplines != null)
                            {
                                info.EducatorDisciplines.Clear();
                                foreach (var discipline in dtoInfo.EducatorDisciplines)
                                {
                                    info.EducatorDisciplines.Add(new EducatorDiscipline
                                    {
                                        DisciplineId = discipline.DisciplineId
                                    });
                                }
                            }
                        }
                    }
                    break;

                case Role.student:
                    if (existingUser.Student != null && userDTO.Student != null)
                    {
                        existingUser.Student.GroupId = userDTO.Student.GroupId ?? existingUser.Student.GroupId;
                        existingUser.Student.StudentCard = userDTO.Student.StudentCardId;
                    }
                    break;
            }

            return await _userRepository.UpdateUserAsync(existingUser);
        }

        #region FullInfo

        public async Task<User> GetUserWithAutInfoByUserId(int id)
        {
            var user = await _userRepository.GetUserFullInfoAsync(id);
            if (user is null)
                return new User();
            return user;
        }

        public async Task<User> GetUserWithStudentInfoByUserId(int id)
        {
            var user = await _userRepository.GetUserWithStudentInfoByIdAsync(id);
            if (user is null)
                return new User();
            return user;
        }

        public async Task<User> GetUserWithEducatorInfoByUserId(int id)
        {
            var user = await _userRepository.GetUserWithEducatorInfoByIdAsync(id);
            if (user is null)
                return new User();
            return user;
        }

        public async Task<User> GetUserWithAdminInfoByUserId(int id)
        {
            var user = await _userRepository.GetUserWithAdminInfoByIdAsync(id);
            if (user is null)
                return new User();
            return user;
        }

        #endregion

        #region Message
        public async Task<bool> AddMessageByDTOAsync(MessageInChatDTO messageDTO) 
        {
            var message = new MessageInChat()
            {
                ChatId = messageDTO.ChatId,
                SenderId = messageDTO.SenderId,
                SenderRole = messageDTO.SenderRole,
                MessageText = messageDTO.MessageText,
                SentAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
            };
            var user = await _userRepository.AddMessageAsync(message);
            return true;
        }
        #endregion
        public async Task<bool> AddSolutionByDTOAsync(SolutionStudentDTO solutionDTO)
        {
            var compFiles = new List<SolutionFile>();

            var solution = new StudentSolution
            {
                TaskId = solutionDTO.TaskId,
                StudentId = solutionDTO.StudentId,
                SolutionText = solutionDTO.SolutionText,
                Status = solutionDTO.Status,
                CreatedAt = DateTime.SpecifyKind(solutionDTO.CreatedAt, DateTimeKind.Utc),
                UpdatedAt = DateTime.SpecifyKind(solutionDTO.UpdatedAt, DateTimeKind.Utc),
                SolutionChat = new SolutionChat()
                {
                    CreatedAt = DateTime.SpecifyKind(solutionDTO.CreatedAt, DateTimeKind.Utc),
                }
            };

            var solutionId = await _userRepository.AddSolutionAsync(solution);

            var task = await _educatorRepository.GetTasksEducatorByIdWithDicipline(solution.TaskId);

            if (solutionId is not 0 && solutionDTO.SolutionFiles?.Count != 0 && solutionDTO.SolutionFiles != null)
            {
                foreach (var file in solutionDTO.SolutionFiles)
                {
                    var bytes = Convert.FromBase64String(file.ContentBase64);
                    var physicalPath = await _fileService.SaveFileToDisk(bytes, file.FileName, task.Dicipline.NameDiscipline, FileType.Solution);
                    compFiles.Add(new SolutionFile()
                    {
                        SolutionId = solutionId,
                        FileName = file.FileName,
                        OriginalFileName = file.FileName,
                        PhysicalPath = physicalPath,
                        FileSize = file.FileSize,
                        UploadedAt = DateTime.UtcNow,
                        FileType = file.FileType
                    });
                }

                if (compFiles.Any())
                    await _fileRepository.AddSolutionFile(compFiles);
            }

            return true;
    }

        private async Task<User> ParseUserByDTOAsync(UserDTO userDTO)
    {
        var user = new User()
        {
            Email = userDTO.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.PasswordHash),
            FirstName = userDTO.FirstName, 
            LastName = userDTO.LastName, 
            MiddleName = userDTO.MiddleName, 
            Role = userDTO.Role
        };

            switch (userDTO.Role)
            {
                case Role.admin:
                    user.Administrator = new Admin()
                    {
                        Position = userDTO.Administrator.Position
                    };
                    break;
                case Role.educator:
                    user.Educator = new Educator()
                    {
                        Profession = userDTO.Educator.Profession,
                        AcademicDegree = userDTO.Educator.AcademicDegree,
                        EducatorAdditionalInfo = new EducatorAdditionalInfo
                        {
                            EducationLevel = userDTO.Educator.EducatorAdditionalInfo.EducationLevel,
                            SpecialtyOrFieldOfStudy = userDTO.Educator.EducatorAdditionalInfo.SpecialtyOrFieldOfStudy,
                            Qualification = userDTO.Educator.EducatorAdditionalInfo.Qualification,
                            AdditionalInfo = userDTO.Educator.EducatorAdditionalInfo.AdditionalInfo,
                            Image = Convert.FromBase64String(userDTO.Educator.EducatorAdditionalInfo.Image),
                            AcademicTitle = userDTO.Educator.EducatorAdditionalInfo.AcademicTitle,
                            EducatorDisciplines = userDTO.Educator.EducatorAdditionalInfo.EducatorDisciplines
                            .Select(x => new EducatorDiscipline() { DisciplineId = x.DisciplineId })
                            .ToList()
                        }
                    };
                break;
            case Role.student:
                    user.Student = new Student()
                    {
                        GroupId = (int)userDTO.Student.GroupId,
                        StudentCard = userDTO.Student.StudentCardId
                    };

                break;
            default:
                break;
        }
        return user;
        }
    }
}