using Reactive.Bindings;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;

namespace WpfApplication
{
    public class ViewControlVM
    {
        public class EntryInfoDataGridRowVM : INotifyPropertyChanged
        {
            internal EntryInfo Core { get; }
            public string Name { get { return GetCore(); } set { SetCore(value); } }
            public string Mail { get { return GetCore(); } set { SetCore(value); } }
            public string Language { get { return GetCore(); } set { SetCore(value); } }
            public string Sex
            {
                get
                {
                    return Core.IsMan ? "男性" : "女性";
                }
                set
                {
                    var isMan = (value == "男性");
                    if (isMan != Core.IsMan)
                    {
                        Core.IsMan = isMan;
                        PropertyChanged(this, new PropertyChangedEventArgs(nameof(Sex)));
                    }
                }
            }
            public DateTime BirthDay { get { return GetCore(); } set { SetCore(value); } }

            public event PropertyChangedEventHandler PropertyChanged = (_, __) => { };

            public EntryInfoDataGridRowVM(EntryInfo core)
            {
                Core = core;
            }

            void SetCore(object objNew, [CallerMemberName] string name = "")
            {
                var objSrc = Core.GetType().GetProperty(name).GetValue(Core);
                if (!Equals(objSrc, objNew))
                {
                    Core.GetType().GetProperty(name).SetValue(Core, objNew);
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
            }
            NormalValue GetCore([CallerMemberName] string name = "")
                => new NormalValue(Core.GetType().GetProperty(name).GetValue(Core));
        }

        List<EntryInfo> _infos;

        public ReactiveProperty<string> NameSearch { get; set; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> LanguageSearch { get; set; } = new ReactiveProperty<string>();
        public ReactiveProperty<EntryInfoDataGridRowVM> SelectedItem { get; set; } = new ReactiveProperty<EntryInfoDataGridRowVM>();
        public ObservableCollection<EntryInfoDataGridRowVM> EntryInfos { get; set; } = new ObservableCollection<EntryInfoDataGridRowVM>();

        public Func<bool?> AskDelete { get; set; }

        public ViewControlVM(List<EntryInfo> infos)
        {
            _infos = infos;
            infos.ForEach(e=>EntryInfos.Add(new EntryInfoDataGridRowVM(e)));
        }

        public void Search()
        {
            IEnumerable<EntryInfo> result = _infos.ToArray();
            if (!NameSearch.IsNullOrEmpty()) result = result.Where(e => e.Name.ToLower().Contains(NameSearch.Value.ToLower()));
            if (!LanguageSearch.IsNullOrEmpty()) result = result.Where(e => e.Language == LanguageSearch.Value);
            EntryInfos.Clear();
            result.ToList().ForEach(e => EntryInfos.Add(new EntryInfoDataGridRowVM(e)));
        }

        public void Erase()
        {
            if (SelectedItem.Value == null) return;
            if (AskDelete() != true) return;
            _infos.Remove(SelectedItem.Value.Core);
            Search();
        }
    }
}
