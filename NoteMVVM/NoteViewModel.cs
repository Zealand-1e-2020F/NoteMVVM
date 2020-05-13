using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NoteMVVM.Annotations;

namespace NoteMVVM
{
    class NoteViewModel : INotifyPropertyChanged
    {
        private int _nr;
        public ObservableCollection<NoteModel> Notes { get; set; }

        public int Nr
        {
            get => _nr;
            set { _nr = value; OnPropertyChanged(); }
        }

        public string Tekst { get; set; }
        public string Emne { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public NoteViewModel()
        {
            Notes = new ObservableCollection<NoteModel>()
            {
                //new NoteModel("Husk at komme til LektieCafe, ugens emne er MVVM", "SWC", DateTime.Now),
                //new NoteModel("LektieCafe - det er altså vigtig at komme!!", "Programmering", new DateTime(2019, 11, 14)),
                //new NoteModel("Husk at undersøge hvornår der er spørgetime", "SWD og SWC", DateTime.Now)
            };

            HentNoter();

            AddCommand = new RelayCommand(Add);
            RemoveCommand = new RelayCommand(Remove);
            SaveCommand = new RelayCommand(Save);
            // Nr = NoteModel.Count;
           
        }

        public async void HentNoter()
        {
            var noter = await PersistencyService.LoadNotesFromJsonAsync();
            int max = 0;
            foreach (var note in noter)
            {
                Notes.Add(note);
                if (max < note.Nr) max = note.Nr; //finder største notenummer
            }
            Nr = ++max; //sætter Nr til næste notenummer
        }

        

        public void Add()
        {
            Notes.Add(new NoteModel(Tekst, Emne, DateTime.Now));
            Nr = NoteModel.Count;
        }

        public void Remove()
        {
            foreach (NoteModel note in Notes)
            {
                if (note.Nr == Nr)
                {
                    Notes.Remove(note);
                    break;
                }
            }
        }

        public async Task Save()
        {
           await PersistencyService.SaveNotesAsJsonAsync(Notes);
        }

        #region PropertyChangeSupport

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
