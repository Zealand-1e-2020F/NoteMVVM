using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteMVVM
{
    class NoteModel
    {
        public static int Count { get; set; } = 1;
        public int Nr { get; set; }
        public string Tekst { get; set; }
        public string Emne { get; set; }
        public DateTime Dato { get; set; }

        public NoteModel(string tekst, string emne, DateTime dato)
        {
            Nr = Count++;
            Tekst = tekst;
            Emne = emne;
            Dato = dato;
        }

        public override string ToString()
        {
            return $"Nr: {Nr}, Tekst: {Tekst}, Emne: {Emne}, Dato: {Dato}";
        }
    }
}
