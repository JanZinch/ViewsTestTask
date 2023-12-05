using Models;

namespace Progress
{
    public class ProgressService
    {
        public ProgressDataModel Model { get; private set; }

        public ProgressService()
        {
            Model = new ProgressDataModel();
        }

    }
}