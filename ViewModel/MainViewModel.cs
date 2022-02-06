using DirbWin.Model;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace DirbWin.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Risk> risks = new ObservableCollection<Risk>();
        private CollectionViewSource riskCollectionViewSource;
        private FileIO fileIO = null;
        private Exploit exploit = null;
        public ICommand btBrowseCommand { get; set; }
        public ICommand btRunCommand { get; set; }
        public ICommand btStopCommand { get; set; }

        private string exploitUrl = string.Empty;

        public string ExploitUrl
        {
            get
            {
                return exploitUrl;
            }
            set
            {
                Regex regex = new Regex(@"(http|https):\/\/.*\/");

                if (Uri.IsWellFormedUriString(value, UriKind.Absolute) && regex.IsMatch(value))
                {
                    exploitUrl = value;
                    OnPropertyChanged("ExploitUrl");
                }
                else
                {
                    MessageBox.Show("Url 형식 에러" + Environment.NewLine + "예시: http://YourUrl/", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool canExploit = true;

        public bool CanExploit
        {
            get
            {
                return canExploit;
            }
            set
            {
                canExploit = value;
                OnPropertyChanged("CanExploit");
            }
        }

        private bool canStop = false;

        public bool CanStop
        {
            get
            {
                return canStop;
            }
            set
            {
                canStop = value;
                OnPropertyChanged("CanStop");
            }
        }

        private string selectPath;

        public string SelectPath
        {
            get { return selectPath; }
            set
            {
                selectPath = value;
                OnPropertyChanged("SelectPath");
            }
        }

        public ICollectionView riskCollection
        {
            get { return riskCollectionViewSource.View; }
        }

        public MainViewModel()
        {
            btRunCommand = new Command(BtRunExecuteMethod, CanExecuteMethod);
            btStopCommand = new Command(BtStopExcuteMethod, CanExecuteMethod);
            btBrowseCommand = new Command(BtBrowseExecuteMethod, CanExecuteMethod);

            riskCollectionViewSource = new CollectionViewSource();
            riskCollectionViewSource.Source = this.risks;

            OnListChanged();
        }

        public void AddRiskInList(string dirName, int resCode)
        {
            risks.Add(new Risk(dirName, resCode));

            OnListChanged();
        }

        private void BtRunExecuteMethod(object obj)
        {
            risks.Clear();
            CanExploit = false;
            CanStop = true;

            if (!string.IsNullOrWhiteSpace(selectPath))
            {
                fileIO = new FileIO(selectPath);

                exploit = new Exploit(fileIO.GetList(), exploitUrl);

                exploit.StartExploit(AddRiskInList, BtStopExcuteMethod);
            }
        }

        private void BtStopExcuteMethod(object obj)
        {
            CanStop = false;
            CanExploit = true;
            OnListChanged();
        }

        private void BtBrowseExecuteMethod(object obj)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Wordlist file (*.txt)|*.txt";

            if (fileDialog.ShowDialog() == true)
            {
                SelectPath = fileDialog.FileName;
            }
        }

        private bool CanExecuteMethod(object arg)
        {
            return true;
        }

        public void OnListChanged()
        {
            riskCollectionViewSource.View.Refresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}