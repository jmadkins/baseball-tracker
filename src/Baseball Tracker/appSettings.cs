using Baseball_Tracker.Common;

namespace Baseball_Tracker
{
    class appSettings : BindableBase
    {
        private bool customImages;

        public bool CustomImages
        {
            get { return CustomImages; }
            set { this.SetProperty(ref customImages, value); }
        }

        public int customImagesChangeTime;

        public int CustomImagesChangeTime
        {
            get { return CustomImagesChangeTime; }
            set { this.SetProperty(ref customImagesChangeTime, value); }
        }

        public int inningNumbers;

        public int InningNumbers
        {
            get { return InningNumbers; }
            set { this.SetProperty(ref inningNumbers, value); }
        }
    }
}
