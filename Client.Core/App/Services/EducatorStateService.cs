using Client.Core.Entities.Models.User.EducatorModel;

namespace Client.Core.App.Services {
    public class EducatorStateService {
        public Educator? CurrentEducator { get; private set; }

        public void SetEducator(Educator educator) {
            CurrentEducator = educator;
        }
    }
}
