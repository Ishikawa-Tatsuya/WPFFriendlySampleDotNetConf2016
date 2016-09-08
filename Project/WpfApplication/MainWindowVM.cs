using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace WpfApplication
{
    public class MainWindowVM
    {
        List<EntryInfo> _infos;
        public EntryControlVM EntryControlVM { get; }
        public ViewControlVM ViewControlVM { get; }

        public Func<string> AskSaveFilePath { get; set; }
        public Func<string> AskOpenFilePath { get; set; }
        public Func<string, bool?> NotifyError { get; set; }

        public MainWindowVM()
        {
            _infos = new List<EntryInfo>();
            EntryControlVM = new EntryControlVM(_infos);
            ViewControlVM = new ViewControlVM(_infos);
            EntryControlVM.Registed += (_, __) => ViewControlVM.Search();
        }

        public void CreateNewData()
        {
            _infos.Clear();
            ViewControlVM.Search();
        }

        public void OpenFile()
        {
            var path = AskOpenFilePath();
            if (path.IsNullOrEmpty()) return;
            _infos.Clear();
            try
            {
                _infos.AddRange(JsonConvert.DeserializeObject<List<EntryInfo>>(File.ReadAllText(path)));
            }
            catch (Exception e)
            {
                NotifyError(e.Message);
            }
            ViewControlVM.Search();
        }

        public void OpenSave()
        {
            var path = AskSaveFilePath();
            if (path.IsNullOrEmpty()) return;
            try
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(_infos, Formatting.Indented));
            }
            catch (Exception e)
            {
                NotifyError(e.Message);
            }
        }
    }
}
