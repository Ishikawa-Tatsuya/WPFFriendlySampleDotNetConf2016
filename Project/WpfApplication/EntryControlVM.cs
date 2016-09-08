using Reactive.Bindings;
using System;
using System.Collections.Generic;

namespace WpfApplication
{
    public class EntryControlVM
    {
        List<EntryInfo> _infos;

        public ReactiveProperty<string> Name { get; set; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> Mail { get; set; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> Language { get; set; } = new ReactiveProperty<string>();
        public ReactiveProperty<bool> IsMan { get; set; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsWoman { get; set; } = new ReactiveProperty<bool>();
        public ReactiveProperty<DateTime?> BirthDay { get; set; } = new ReactiveProperty<DateTime?>();

        public event EventHandler Registed = (_, __) => { };

        public Func<bool?> NotifyDataError { get; set; }

        public EntryControlVM(List<EntryInfo> infos)
        {
            _infos = infos;
        }

        public void Regist()
        {
            if (Name.IsNullOrEmpty() || Mail.IsNullOrEmpty() || Language.IsNullOrEmpty() || BirthDay.Value == null)
            {
                NotifyDataError();
                return;
            }
            if (IsMan.Value == IsWoman.Value)
            {
                NotifyDataError();
                return;
            }
            _infos.Add(new EntryInfo()
            {
                Name = Name.Value,
                Mail = Mail.Value,
                Language = Language.Value,
                IsMan = IsMan.Value,
                BirthDay = BirthDay.Value.Value
            });
            Name.Value = Mail.Value = Language.Value = string.Empty;
            IsMan.Value = IsWoman.Value = false;
            BirthDay.Value = null;
            Registed(this, EventArgs.Empty);
        }
    }
}
