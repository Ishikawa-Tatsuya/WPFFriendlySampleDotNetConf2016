using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using RM.Friendly.WPFStandardControls;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using FontAwesome.WPF;

namespace Driver.Window
{
    public class 一覧_Driver
    {
        AppVar _core;

        public class MyGrid : WPFDataGrid
        {
            public MyGrid(AppVar core) : base(core) { }
            public int RowCount => this.Dynamic().Items.Count;
        }

        public WPFTextBox TextBox_名前 => new WPFTextBox(_core.LogicalTree().ByBinding("NameSearch.Value").Single());
        public WPFComboBox CombBox_得意言語 => new WPFComboBox(_core.LogicalTree().ByBinding("LanguageSearch.Value").Single());
        public WPFButtonBase Button_検索 => new WPFButtonBase(_core.App.Type(GetType()).FindButton(_core, FontAwesomeIcon.Search));
        public WPFButtonBase Button_削除 => new WPFButtonBase(_core.App.Type(GetType()).FindButton(_core, FontAwesomeIcon.Eraser));
        public MyGrid DataGrid => new MyGrid(_core.LogicalTree().ByType<DataGrid>().Single());

        public 一覧_Driver(AppVar core)
        {
            _core = core;
        }

        static Button FindButton(UIElement element, FontAwesomeIcon icon)
            => element.LogicalTree().ByType<Button>().
                Where(e => e.Content is ImageAwesome).Where(e => ((ImageAwesome)e.Content).Icon == icon).Single();
    }
}
