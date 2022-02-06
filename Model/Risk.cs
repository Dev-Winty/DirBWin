namespace DirbWin.Model
{
    internal class Risk
    {
        public Risk(string dirName, int resCode)
        {
            _dirName = dirName;
            _resCode = resCode;
        }

        private string _dirName;

        public string DirName
        {
            get { return _dirName; }
            set { _dirName = value; }
        }

        private int _resCode;

        public int HTTPResCode
        {
            get { return _resCode; }
            set { _resCode = value; }
        }
    }
}