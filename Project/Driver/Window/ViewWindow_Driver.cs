using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using RM.Friendly.WPFStandardControls;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using FontAwesome.WPF;
using Codeer.Friendly.Windows.Grasp;
using System;

namespace Driver.Window
{
    public class 一覧_Driver
    {
        AppVar _core;

        public dynamic Core => _core.Dynamic();

        public class MyGrid : WPFDataGrid
        {
            public MyGrid(AppVar core) : base(core) { }
            public int RowCount => this.Dynamic().Items.Count;
            public void EmulateChangeDate(int itemIndex, int col, DateTime date)
            {
                EmulateChangeCurrentCell(itemIndex, col);
                App.Type(GetType()).EmulateChangeDate(this, date);
            }
            static void EmulateChangeDate(DataGrid grid, DateTime date)
            {
                EventHandler<DataGridCellEditEndingEventArgs> hanlder = (s, e) =>
                    e.EditingElement.VisualTree().ByType<DatePicker>().Single().SelectedDate = date;
                grid.CellEditEnding += hanlder;
                grid.BeginEdit();
                grid.CommitEdit(DataGridEditingUnit.Row, true);
                grid.CellEditEnding -= hanlder;
            }
        }

        public WPFTextBox TextBox_名前 => new WPFTextBox(_core.LogicalTree().ByBinding("NameSearch.Value").Single());
        public WPFComboBox CombBox_得意言語 => new WPFComboBox(_core.LogicalTree().ByBinding("LanguageSearch.Value").Single());
        public WPFButtonBase Button_検索 => new WPFButtonBase(_core.App.Type(GetType()).FindButton(_core, FontAwesomeIcon.Search));
        public MyGrid DataGrid => new MyGrid(_core.LogicalTree().ByType<DataGrid>().Single());
        WPFButtonBase Button_削除 => new WPFButtonBase(_core.App.Type(GetType()).FindButton(_core, FontAwesomeIcon.Eraser));

        public 一覧_Driver(AppVar core)
        {
            _core = core;
        }

        public MessageBoxDriver Button_削除_Click()
        {
            var async = new Async();
            Button_削除.EmulateClick(async);
            var msg = WindowControl.WaitForIdentifyFromWindowText(Button_削除.App, "質問", async);
            return msg == null ? null : new MessageBoxDriver(msg);
        }

        static Button FindButton(UIElement element, FontAwesomeIcon icon)
            => element.LogicalTree().ByType<Button>().
                Where(e => e.Content is ImageAwesome).Where(e => ((ImageAwesome)e.Content).Icon == icon).Single();
    }
}
