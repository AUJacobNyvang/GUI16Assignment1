using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using DebtBook.Model;
using DebtBook.Pages;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;

namespace DebtBook.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        public Debtors Debtors { get; set; }
        private string filename = "";
        public MainWindowViewModel()
        {
            Debtors = new Debtors();
            Debtor Default = new Debtor("Default",0);
            Debtors.DebtorsList.Add(Default);
            CurrentDebtor = Debtors.DebtorsList[0];
        }

        private Debtor currentDebtor = null;

        public Debtor CurrentDebtor
        {
            get { return currentDebtor; }
            set
            {
                if (currentDebtor != value)
                {
                    SetProperty(ref currentDebtor, value);
                }
            }
        }

        private int currentIndex = -1;
        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {
                SetProperty(ref currentIndex, value);
            }
        }

        private ICommand addDebtorCommand;

        public ICommand AddDebtorCommand
        {
            get
            {
                return addDebtorCommand ??= new DelegateCommand(AddDebtorCommandHandler);
            }
        }

        private void AddDebtorCommandHandler()
        {
            AddPerson windowAddPerson = new AddPerson();
            windowAddPerson.DataContext = new AddPersonViewModel(Debtors,windowAddPerson);
            windowAddPerson.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            windowAddPerson.ShowDialog();
        }

        private ICommand deleteDebtorCommand;

        public ICommand DeleteDebtorCommand
        {
            get
            {
                return deleteDebtorCommand ??= new DelegateCommand(DeleteDebtorCommandExecute, DeleteDebtorCommandCanExecute).ObservesProperty(() =>
                    CurrentIndex);
            }
        }
        private void DeleteDebtorCommandExecute()
        {
            if (CurrentIndex >= 0)
                Debtors.DebtorsList.RemoveAt(CurrentIndex);
        }
        private bool DeleteDebtorCommandCanExecute()
        {
            if (CurrentIndex >= 0 && Debtors.DebtorsList.Count > 0)
                return true;
            else
                return false;
        }

        private ICommand debtorOpenCommand;

        public ICommand DebtorOpenCommand
        {
            get
            {
                return debtorOpenCommand ??= new DelegateCommand(DebtorOpenCommandHandler);
            }
        }

        private void DebtorOpenCommandHandler()
        {
            PersonDebt windowPersonDebt = new PersonDebt();
            windowPersonDebt.DataContext = new PersonDebtViewModel(CurrentDebtor, windowPersonDebt);
            windowPersonDebt.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            windowPersonDebt.Show();
        }
        
        /// <summary>
        /// save and so on logic
        /// </summary>
        
        ICommand closeAppCommand;
        public ICommand CloseAppCommand
        {
            get
            {
                return closeAppCommand ?? (closeAppCommand = new DelegateCommand(() =>
                {
                    App.Current.MainWindow.Close();
                }));
            }
        }

        ICommand saveFileAsCommand;
        public ICommand SaveFileAsCommand
        {
            get
            {
                return saveFileAsCommand ??= new DelegateCommand(() =>
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.ShowDialog();
                    saveFileDialog.FileName += ".txt";
                    if (saveFileDialog.FileName != ".txt")
                    {
                        // Create an instance of the XmlSerializer class and specify the type of object to serialize.
                        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Debtor>));
                        TextWriter writer = new StreamWriter(saveFileDialog.FileName);
                        // Serialize all the agents.
                        serializer.Serialize(writer, Debtors.DebtorsList);
                        writer.Close();
                        filename = saveFileDialog.FileName;
                    }
                });
            }
        }

        ICommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _SaveCommand ??= new DelegateCommand(SaveFileCommand_Execute, SaveFileCommand_CanExecute)
                    .ObservesProperty(() => Debtors.DebtorsList.Count);
            }
        }

        private void SaveFileCommand_Execute()
        {
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Debtor>));
            TextWriter writer = new StreamWriter(filename);
            // Serialize all the agents.
            serializer.Serialize(writer, Debtors.DebtorsList);
            writer.Close();
        }

        private bool SaveFileCommand_CanExecute()
        {
            return (filename != "") && (Debtors.DebtorsList.Count > 0);
        }


        ICommand _NewFileCommand;
        public ICommand NewFileCommand
        {
            get { return _NewFileCommand ??= new DelegateCommand(NewFileCommand_Execute); }
        }

        private void NewFileCommand_Execute()
        {
            MessageBoxResult res = MessageBox.Show("Any unsaved data will be lost. Are you sure you want to initiate a new file?", "Warning",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                Debtors.DebtorsList.Clear();
                filename = "";
            }
        }

        ICommand _OpenFileCommand;
        public ICommand OpenFileCommand
        {
            get { return _OpenFileCommand ??= new DelegateCommand<string>(OpenFileCommandHandler); }
        }

        private void OpenFileCommandHandler(string argFilename)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            argFilename = openFileDialog.FileName;
            if (argFilename == "")
            {

                MessageBox.Show("You must enter a file name in the File Name textbox!", "Unable to save file",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                filename = argFilename;
                var tempDebtors = new ObservableCollection<Debtor>();

                // Create an instance of the XmlSerializer class and specify the type of object to deserialize.
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Debtor>));
                try
                {
                    TextReader reader = new StreamReader(filename);
                    // Deserialize all the agents.
                    tempDebtors = (ObservableCollection<Debtor>) serializer.Deserialize(reader);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Debtors.DebtorsList = tempDebtors;
            }
        }
    }
}